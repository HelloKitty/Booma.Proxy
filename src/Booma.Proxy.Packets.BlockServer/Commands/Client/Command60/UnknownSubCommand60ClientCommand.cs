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
	public sealed class UnknownSubCommand60ClientCommand : BaseSubCommand60Client, IUnknownPayloadType
	{
		/// <inheritdoc />
		public short OperationCode => (short)base.CommandOperationCode;

		/// <inheritdoc />
		[ReadToEnd]
		[WireMember(1)]
		public byte[] UnknownBytes { get; } = new byte[0]; //readtoend requires at least an empty array init

		private UnknownSubCommand60ClientCommand()
		{
			
		}
	}
}
