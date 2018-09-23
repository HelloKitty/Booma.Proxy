using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Reinterpret.Net;

namespace Booma.Proxy
{
	public sealed class BinaryPacketWriter
	{
		public enum PacketType
		{
			Client,
			Server
		}

		/// <summary>
		/// The root path for the packets.
		/// </summary>
		private string RootPath { get; }

		public BinaryPacketWriter([NotNull] string rootPath)
		{
			RootPath = rootPath ?? throw new ArgumentNullException(nameof(rootPath));

			if(!Directory.Exists(rootPath))
				Directory.CreateDirectory(rootPath);

			if(!Directory.Exists(Path.Combine(rootPath, "Client")))
				Directory.CreateDirectory(Path.Combine(rootPath, "Client"));

			if(!Directory.Exists(Path.Combine(rootPath, "Server")))
				Directory.CreateDirectory(Path.Combine(rootPath, "Server"));
		}

		public async Task WritePacketAsync(GameNetworkOperationCode opCode, [NotNull] IUnknownPayloadType payload, PacketType packetType)
		{
			if(payload == null) throw new ArgumentNullException(nameof(payload));

			byte[] opCodeBytes = ((short)opCode).Reinterpret();

			using(FileStream fs = File.Open(Path.Combine(RootPath, packetType == PacketType.Client ? "Client" : "Server", $"0x{((int)opCode):X}_{Guid.NewGuid()}.packet"), FileMode.CreateNew))
			{
				await fs.WriteAsync(opCodeBytes, 0, opCodeBytes.Length)
					.ConfigureAwait(false);

				await fs.WriteAsync(payload.UnknownBytes, 0, payload.UnknownBytes.Length)
					.ConfigureAwait(false);
			}
		}
	}
}
