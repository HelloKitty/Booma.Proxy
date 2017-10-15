using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;
using Reinterpret.Net;

namespace Booma.Proxy
{
	//TODO: We should implement this. Sylverant does something with it. Current Tethella doesn't. Honestly, it's a dumb packet that shouldn't exist
	//This is dumb. But the client sends the character data to the server. Why doesn't the server load it itself? Because Sega...
	//rcv 0061 (2096)
	//Tethella doesn't even do anything with this. It initializes 10 bytes from the middle into an unknown field.
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.BLOCK_SET_CHAR_DATA_TYPE)]
	public sealed class BlockCharacterDataInitializeClientResponsePayload : PSOBBGamePacketPayloadClient
	{
		[WireMember(1)]
		[KnownSize(2096 - 8)]
		public byte[] Bytes { get; } = new byte[2096];

		public BlockCharacterDataInitializeClientResponsePayload()
		{
			//TODO: We should figure out what thisi s all about.
			//Client sends some data, not sure what it is
			0x418851ec.Reinterpret(Bytes, 0x364 - 8);
			0x41200000.Reinterpret(Bytes, 0x368 - 8);
		}
	}
}
