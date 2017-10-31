using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using FreecraftCore.Serializer;
using GladNet;
using MahApps.Metro.Controls;

namespace Booma.Proxy.Client.Launcher
{
	public partial class MainWindow : MetroWindow
	{
		private SynchronizationContext Context { get; }

		public MainWindow()
		{
			Context = SynchronizationContext.Current;
			InitializeComponent();
			Task.Factory.StartNew(InitializeNetworkingClient, TaskCreationOptions.LongRunning);
		}

		private async Task InitializeNetworkingClient()
		{
			ContainerBuilder builder = new ContainerBuilder();

			EncryptionLazyWithoutKeyDecorator<uint> encrypt = new EncryptionLazyWithoutKeyDecorator<uint>(val => new PatchServerCryptoProvider(PatchEncryptionKeyFactory.Create(val)), 4);
			EncryptionLazyWithoutKeyDecorator<uint> decrypt = new EncryptionLazyWithoutKeyDecorator<uint>(val => new PatchServerCryptoProvider(PatchEncryptionKeyFactory.Create(val)), 4);
			IFullCryptoInitializationService<uint> intializers = new SeperateAggregateCryptoInitializationService<uint>(encrypt, decrypt);

			builder.RegisterInstance(intializers)
				.As<IFullCryptoInitializationService<uint>>();

			INetworkSerializationService serializerService = new FreecraftCoreGladNetSerializerAdapter(CreateSerializerService());

			//Configurs and builds the clients without all the
			//relevant decorators
			IManagedNetworkClient<PSOBBPatchPacketPayloadClient, PSOBBPatchPacketPayloadServer> client = new DotNetTcpClientNetworkClient()
				.AddCryptHandling(encrypt, decrypt)
				.AddHeaderReading<PSOBBPacketHeader>(serializerService, 2)
				.AddNetworkMessageReading(serializerService)
				.For<PSOBBPatchPacketPayloadServer, PSOBBPatchPacketPayloadClient, IPacketPayload>(null)
				.AddReadBufferClearing()
				.Build()
				.AsManaged();

			builder.RegisterInstance(client)
				.As<IManagedNetworkClient<PSOBBPatchPacketPayloadClient, PSOBBPatchPacketPayloadServer>>();

			//Patch welcome message
			builder.RegisterHandler<PatchWelcomeMessageHandler, PatchingWelcomePayload>();
			builder.RegisterHandler<PatchingLoginReadyMessageHandler, PatchingReadyForLoginRequestPayload>();
			builder.RegisterHandler<PatchingRedirectionMessageHandler, PatchingRedirectPayload>();
			builder.RegisterHandler<PatchingInfoDoneMessageHandler, PatchingInfoRequestDonePayload>(async h =>
			{
				await Dispatcher.InvokeAsync(() => PlayButton.DataContext = h);
			});
			builder.RegisterHandler<PatchMessageMessageHandler, PatchingMessagePayload>(async h =>
			{
				await Dispatcher.InvokeAsync(() => PatchNotesData.DataContext = h);
			});

			IContainer container = builder.Build();

			IEnumerable<IPeerMessageHandler<PSOBBPatchPacketPayloadServer, PSOBBPatchPacketPayloadClient>> Handlers = 
				container.Resolve<IEnumerable<IPeerMessageHandler<PSOBBPatchPacketPayloadServer, PSOBBPatchPacketPayloadClient>>>();

			IPeerMessageContextFactory MessageContextFactory = new DefaultMessageContextFactory();

			await client.ConnectAsync("158.69.215.131", 11000);

			while(client.isConnected)
			{
				NetworkIncomingMessage<PSOBBPatchPacketPayloadServer> message = await client.ReadMessageAsync();

				Console.WriteLine($"Recieved {message.Payload?.GetType().Name}");

				foreach(var h in Handlers)
				{
					//TODO: Fix this and enable request service
					if(await h.TryHandleMessage(MessageContextFactory.Create(client, client, null), message))
						break;
				}
			}
		}

		private static ISerializerService CreateSerializerService()
		{
			//Create the serializer and register all the needed types
			SerializerService serializer = new SerializerService();

			//Registers all the types.
			PacketPatchServerMetadataMarker.SerializableTypes
				.Concat(PacketCommonServerMetadataMarker.SerializableTypes)
				.Concat(PacketLoginServerMetadataMarker.SerializableTypes)
				.ToList().ForEach(t => serializer.RegisterType(t));

			serializer.Compile();

			return serializer;
		}

		private void PlayButton_Click(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
