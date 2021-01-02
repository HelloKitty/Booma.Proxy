using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma
{
	[WireDataContract]
	public sealed class LobbyArrowData
	{
		//Sylverant always sends this constant.
		[WireMember(1)]
		public int Tag { get; internal set; } = 0x00010000;
		
		[WireMember(2)]
		public uint GuildCardNumber { get; internal set; }

		[WireMember(3)]
		public uint Arrow { get; internal set; }

		public LobbyArrowData(uint guildCardNumber, uint arrow)
		{
			GuildCardNumber = guildCardNumber;
			Arrow = arrow;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public LobbyArrowData()
		{

		}
	}

	/// <summary>
	/// Payload sent by the server to indicate the arrows (think soccer lobby) state for
	/// all players.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.LOBBY_ARROW_LIST_TYPE)]
	public sealed partial class SetLobbyArrowsEventPayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// Flags is used to represents the count data sent.
		/// </summary>
		public override bool isFlagsSerialized => false;

		[WireMember(1)]
		[SendSize(PrimitiveSizeType.Int32)]
		public LobbyArrowData[] ArrowData { get; internal set; } = Array.Empty<LobbyArrowData>();

		public SetLobbyArrowsEventPayload(LobbyArrowData[] arrowData) 
			: this()
		{
			ArrowData = arrowData ?? throw new ArgumentNullException(nameof(arrowData));
		}

		public SetLobbyArrowsEventPayload() 
			: base(GameNetworkOperationCode.LOBBY_ARROW_LIST_TYPE)
		{

		}
	}
}
