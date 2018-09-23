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
		/// <summary>
		/// The root path for the packets.
		/// </summary>
		private string RootPath { get; }

		public BinaryPacketWriter([NotNull] string rootPath)
		{
			RootPath = rootPath ?? throw new ArgumentNullException(nameof(rootPath));

			if(!Directory.Exists(rootPath))
				Directory.CreateDirectory(rootPath);
		}

		public async Task WritePacketAsync(GameNetworkOperationCode opCode, [NotNull] IUnknownPayloadType payload)
		{
			if(payload == null) throw new ArgumentNullException(nameof(payload));

			byte[] opCodeBytes = ((short)opCode).Reinterpret();

			using(FileStream fs = File.Open(Path.Combine(RootPath, $"0x{((int)opCode):X}_{Guid.NewGuid()}.packet"), FileMode.CreateNew))
			{
				await fs.WriteAsync(opCodeBytes, 0, opCodeBytes.Length)
					.ConfigureAwait(false);

				await fs.WriteAsync(payload.UnknownBytes, 0, payload.UnknownBytes.Length)
					.ConfigureAwait(false);
			}
		}
	}
}
