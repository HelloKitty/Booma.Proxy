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
	[GameClientPacketPayload(GameNetworkOperationCode.BB_CHECKSUM_TYPE)]
	public sealed class CharacterChecksumRequestPayload : PSOBBGamePacketPayloadClient
	{
		//TODO: What is this a checksum of?
		/// <summary>
		/// The checksum.
		/// </summary>
		[WireMember(1)]
		public uint Checksum { get; internal set; }

		[WireMember(2)]
		internal uint Padding { get; set; }

		public CharacterChecksumRequestPayload(uint checksum)
		{
			Checksum = checksum;
		}

		//Serializer ctor
		private CharacterChecksumRequestPayload()
		{
			
		}
	}
}
