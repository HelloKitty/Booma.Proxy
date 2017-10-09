using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Checksum message sent after character information.
	/// </summary>
	[WireDataContract]
	[LoginClientPacketPayload(LoginNetworkOperationCodes.BB_CHECKSUM_TYPE)]
	public sealed class LoginChecksumRequestPayload : PSOBBLoginPacketPayloadClient
	{
		//TODO: What is this a checksum of?
		/// <summary>
		/// The checksum.
		/// </summary>
		[WireMember(1)]
		public uint Checksum { get; }

		[WireMember(2)]
		private uint Padding { get; }

		public LoginChecksumRequestPayload(uint checksum)
		{
			Checksum = checksum;
		}

		//Serializer ctor
		private LoginChecksumRequestPayload()
		{
			
		}
	}
}
