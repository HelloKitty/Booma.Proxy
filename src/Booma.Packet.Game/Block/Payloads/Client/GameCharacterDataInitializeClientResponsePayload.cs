using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;
using Reinterpret.Net;

namespace Booma
{
	//This packet is sent when a player leaves a game, trying to save their character data or something?? But why would we care about
	//their data, they could have cheated it.
	//TODO: Any value to implementing this packet?
	/// <summary>
	/// See documentation of <see cref="BlockCharacterDataInitializeClientResponsePayload"/>.
	/// Same structure and handling in Sylverant.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.LEAVE_GAME_PL_DATA_TYPE)]
	public sealed partial class GameCharacterDataInitializeClientResponsePayload : PSOBBGamePacketPayloadClient
	{
		[WireMember(1)]
		[KnownSize(2096 - 8)]
		public byte[] Bytes { get; internal set; } = Array.Empty<byte>();

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public GameCharacterDataInitializeClientResponsePayload()
			: base(GameNetworkOperationCode.LEAVE_GAME_PL_DATA_TYPE)
		{
			
		}

		[Obsolete]
		public void ApplyClientDataHack()
		{
			//TODO: We should figure out what thisi s all about.
			//Client sends some data, not sure what it is
			0x418851ec.Reinterpret(Bytes, 0x364 - 8);
			0x41200000.Reinterpret(Bytes, 0x368 - 8);
		}
	}
}
