using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using GladNet;
using JetBrains.Annotations;

namespace Booma.Proxy.TestClient
{
	class Program
	{
		public static ICryptoKeyInitializable<byte[]> EncryptionKeyInitializer { get; private set; }

		public static ICryptoKeyInitializable<byte[]> DecryptionKeyInitializer { get; private set; }

		public static IPeerMessageContextFactory MessageContextFactory { get; private set; }

		private static SerializerService Serializer { get; set; }

		public static void Main(string[] args)
		{

			//Create the serializer and register all the needed types
			Serializer = new SerializerService();

			//Registers all the types.
			PacketPatchServerMetadataMarker.SerializableTypes
				.Concat(PacketCommonServerMetadataMarker.SerializableTypes)
				.Concat(PacketLoginServerMetadataMarker.SerializableTypes)
				.Concat(PacketSharedServerMetadataMarker.SerializableTypes)
				.ToList().ForEach(t =>
				{
					Console.WriteLine($"Registering PayloadType: {t.Name}");
					Serializer.RegisterType(t);
				});

			Serializer.Compile();

			RunClient("127.0.0.1", 5055).Wait();
		}

		private static async Task RunClient(string ip, int port)
		{
			EncryptionLazyWithoutKeyDecorator<byte[]> encrypt = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishEncryptionService encryptionService = new BlowfishEncryptionService();
				encryptionService.Initialize(val);
				return encryptionService;
			}, 8);
			EncryptionLazyWithoutKeyDecorator<byte[]> decrypt = new EncryptionLazyWithoutKeyDecorator<byte[]>(val =>
			{
				BlowfishDecryptionService decryptionService = new BlowfishDecryptionService();
				decryptionService.Initialize(val);
				return decryptionService;
			}, 8);

			EncryptionKeyInitializer = encrypt;
			DecryptionKeyInitializer = decrypt;

			//Configurs and builds the clients without all the
			//relevant decorators
			IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client = new DotNetTcpClientNetworkClient()
				.AddCryptHandling(encrypt, decrypt)
				.AddBufferredWrite(4)
				.AddHeaderReading<PSOBBPacketHeader>(new FreecraftCoreGladNetSerializerAdapter(Serializer), 2)
				.AddNetworkMessageReading(new FreecraftCoreGladNetSerializerAdapter(Serializer))
				.For<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, IPacketPayload>(new PSOBBPacketHeaderFactory())
				.AddReadBufferClearing()
				.Build()
				.AsManaged();

			MessageContextFactory = new DefaultMessageContextFactory();

			await Task.Run(() => RunClientAsync(client, ip, port));
		}

		public static async Task RunClientAsync(IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client, string ip, int port)
		{
			await client.ConnectAsync(ip, port);

			while(true)
			{
				NetworkIncomingMessage<PSOBBGamePacketPayloadServer> message = await client.ReadMessageAsync()
					.ConfigureAwait(false);

				LogMessage(message);

				try
				{
					await HandlePayload((dynamic)message.Payload, client);

				}
				catch(Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
		}

		private static bool hasAskedForChars = false;
		private static bool hasSelectedCharacter = false;

		private static async Task HandlePayload(SharedLoginResponsePayload payload, IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client)
		{
			Console.WriteLine($"Login Response: {payload.ResponseCode}");

			if(hasSecurityData && hasAskedForChars && !hasSelectedCharacter)
			{
				hasSelectedCharacter = true;
				await client.SendMessage(new CharacterCharacterSelectionRequestPayload(0, CharacterSelectionType.PlaySelection));

				Task.Factory.StartNew(async () =>
				{
					await Task.Delay(3000);
					DecryptionKeyInitializer.Uninitialize();
					EncryptionKeyInitializer.Uninitialize();
					await client.ConnectAsync("158.69.215.131", 12001);
				});

				return;
			}

			if(hasSecurityData && !hasAskedForChars)
			{
				hasAskedForChars = true;
				for(int i = 0; i < 4; i++)
					await client.SendMessage(new CharacterCharacterSelectionRequestPayload((byte)i, CharacterSelectionType.Preview));

				Task.Factory.StartNew(async () =>
				{
					await Task.Delay(6000);
					await client.ConnectAsync("158.69.215.131", 12001);
					DecryptionKeyInitializer.Uninitialize();
					EncryptionKeyInitializer.Uninitialize();
				});
			}

			//We should recieve a 19 redirect after this
			//We should use the bytes from the response for future sessions
			ClientVerification = new ClientVerificationData(0x41, payload.SecurityData);//.Take(32).Reverse().Concat(payload.SecurityData.Skip(32)).ToArray());
			hasSecurityData = true;
			teamId = payload.TeamId;
			Console.WriteLine($"Set 32bit key: {payload.TeamId}");
		}

		private static async Task HandlePayload(CharacterCharacterUpdateResponsePayload payload, IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client)
		{
			Console.WriteLine($"Character: {payload.CharacterData.CharacterName} Class: {payload.CharacterData.ClassRace} SecId: {payload.CharacterData.SectionId} Level: {payload.CharacterData.Progress.Level} PlayedTime: {payload.CharacterData.PlayedTime}");
			Console.WriteLine($"Character: {Encoding.Unicode.GetBytes(payload.CharacterData.CharacterName).Aggregate("", (s, b) => $"{s} {b}")}");
			//Console.WriteLine($"Leftover Bytes: {payload.CharacterData.LeftoverBytes.Aggregate("", (s, b) => $"{s} {b}")}");
		}


		private static async Task HandlePayload(SharedCreateMessageBoxEventPayload payload, IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client)
		{
			Console.WriteLine($"MessageBox Message: {payload.Message}");

			//We should recieve a 19 redirect after this
		}

		private static async Task HandlePayload(SharedConnectionRedirectPayload payload, IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client)
		{
			Console.WriteLine($"Redirect: {payload.EndpointAddress}:{payload.EndpointerPort}");

			EncryptionKeyInitializer.Uninitialize();
			DecryptionKeyInitializer.Uninitialize();

			//Redirects to character the first time
			await client.ConnectAsync(payload.EndpointAddress, payload.EndpointerPort);
		}

		public static bool hasSecurityData = false;

		public static ClientVerificationData ClientVerification = null;
		public static int teamId;

		private static async Task HandlePayload(SharedWelcomePayload payload, IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client)
		{
			Console.WriteLine($"Server Vector: {payload.ServerVector.Aggregate("", (s, b) => $"{s} {b.ToString()}")}");
			Console.WriteLine($"Client Vector: {payload.ClientVector.Aggregate("", (s, b) => $"{s} {b.ToString()}")}");

			EncryptionKeyInitializer.Initialize(payload.ClientVector);
			DecryptionKeyInitializer.Initialize(payload.ServerVector);

			Console.WriteLine(payload.CopyrightMessage);

			if(hasSecurityData)
			{
				await client.SendMessage(new SharedLoginRequest93Payload(0x41, teamId, "admin", "test", ClientVerification))
					.ConfigureAwait(false);
			}
			else
				await client.SendMessage(new SharedLoginRequest93Payload(0x41, "admin", "test", ClientVerificationData.FromVersionString("TethVer12510")))
					.ConfigureAwait(false);
		}

		private static async Task HandlePayload(object payload, IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> client)
		{

		}

		public static void LogMessage([NotNull] NetworkIncomingMessage<PSOBBGamePacketPayloadServer> message)
		{
			if(message == null) throw new ArgumentNullException(nameof(message));

			if(message.Header == null) throw new InvalidOperationException("Header was null.");
			if(message.Payload == null) throw new InvalidOperationException("Payload was null.");

			if(message.Payload is IUnknownPayloadType unk)
			{
				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.WriteLine($"Size: {message.Header.PacketSize} OpCode: 0x{unk.OperationCode:X} Type: {message.Payload.GetType().Name}");
				Console.BackgroundColor = ConsoleColor.Black;
			}
			else
			{
				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.WriteLine($"Size: {message.Header.PacketSize} OpCode: 0x{message.Payload.GetType().GetTypeInfo().GetCustomAttribute<WireDataContractBaseLinkAttribute>(true).Index:X} Type: {message.Payload.GetType().Name}");
				Console.BackgroundColor = ConsoleColor.Black;
			}
		}
	}

	public sealed class PSOBBPacketHeaderFactory : IPacketHeaderFactory<IPacketPayload>
	{
		/// <inheritdoc />
		public IPacketHeader Create<TPayloadType>(TPayloadType payload, byte[] serializedPayloadData)
			where TPayloadType : IPacketPayload
		{
			//The packet size is simply the length of the payload plus the header which is 2 bytes
			return new PSOBBPacketHeader(serializedPayloadData.Length + 2);
		}
	}
}
