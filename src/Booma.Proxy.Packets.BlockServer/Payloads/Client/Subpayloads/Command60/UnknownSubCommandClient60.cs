using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// An unimplemented or unknown subcommand for the <see cref="BlockNetworkCommandEventClientPayload"/>.
	/// </summary>
	[WireDataContract]
	public sealed class UnknownSubCommandClient60 : BlockNetworkCommandEventClientPayload, IUnknownPayloadType
	{
		/// <inheritdoc />
		public short OperationCode { get; }

		/// <inheritdoc />
		[ReadToEnd]
		[WireMember(1)]
		public byte[] UnknownBytes { get; } = new byte[0]; //readtoend requires at least an empty array init

		private UnknownSubCommandClient60()
		{
			
		}
	}
}
