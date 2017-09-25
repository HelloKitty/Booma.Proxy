using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy.TestClient
{
	class Program
	{
		public static string PatchServerIp { get; } = "158.69.215.131";

		public static int PatchServerPort { get; } = 11000;

		public static SerializerService Serializer { get; } = new SerializerService();

		static void Main(string[] args)
		{
			Serializer.Link<PatchingInformationPayload, PSOBBPatchPacketPayload>();
			Serializer.Link<PatchingUpOneDirectoryCommandPayload, PSOBBPatchPacketPayload>();
			Serializer.Link<PatchingDoneCommandPayload, PSOBBPatchPacketPayload>();
			Serializer.Link<PatchingWelcomePacket, PSOBBPatchPacketPayload>();
			Serializer.RegisterType<PSOBBPacketHeader>();
			Serializer.Compile();

			Task.Run(AsyncMain).Wait();

			Console.ReadKey();
		}

		private static async Task AsyncMain()
		{
			byte[] buffer = new byte[500];
			int position = 0;

			TcpClient client = new TcpClient();

			await client.ConnectAsync(PatchServerIp, PatchServerPort);

			//Wait until we have 4 bytes for the packet header
			while(2 > (position += await client.GetStream().ReadAsync(buffer, position, 2 - position)));

			PSOBBPacketHeader header = Serializer.Deserialize<PSOBBPacketHeader>(buffer);

			Console.WriteLine($"Recieved packet with Size: {header.PacketSize}");

			//Read the whole packet
			position = 0;
			while(header.PayloadSize > (position += await client.GetStream().ReadAsync(buffer, position, header.PayloadSize - position)));

			PSOBBPatchPacketPayload patchPayload = Serializer.Deserialize<PSOBBPatchPacketPayload>(buffer);

			Console.WriteLine($"Recieved packet with Type: {patchPayload}.");
		}
	}
}
