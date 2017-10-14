using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//Syl: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/packets.h#L1350
	/// <summary>
	/// Ack payload sent in response to <see cref="CharacterCharacterSelectionRequestPayload"/> when there is no character
	/// or if it has been selected for play.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_CHARACTER_ACK_TYPE)]
	public sealed class CharacterCharacterSelectionAckPayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// The slot this character ack is for.
		/// </summary>
		[WireMember(1)]
		public int Slot { get; }

		/// <summary>
		/// The type of ack.
		/// </summary>
		[WireMember(2)]
		public CharacterSelectionAckType AckType { get; }

		//Serializer ctor
		private CharacterCharacterSelectionAckPayload()
		{
			
		}
	}
}
