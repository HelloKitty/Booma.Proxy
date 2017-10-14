using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//TODO: We should implement this. Sylverant does something with it. Current Tethella doesn't. Honestly, it's a dumb packet that shouldn't exist
	//This is dumb. But the client sends the character data to the server. Why doesn't the server load it itself? Because Sega...
	//rcv 0061 (2096)
	//Tethella doesn't even do anything with this. It initializes 10 bytes from the middle into an unknown field.
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.BLOCK_SET_CHAR_DATA_TYPE)]
	public sealed class BlockCharacterDataInitializeRequestPayload : PSOBBGamePacketPayloadClient
	{
		[WireMember(1)]
		[KnownSize(2096 - 8)]
		private byte[] Bytes { get; } = new byte[2096];

		public BlockCharacterDataInitializeRequestPayload()
		{
			
		}
	}
}
