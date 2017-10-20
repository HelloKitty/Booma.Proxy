using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload broadcasted when a client hits the Lobby soccer ball.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.LobbyBallMove)]
	public sealed class Sub60LobbySoccerBallMoveEventPayload : BaseSubCommand60
	{
		//TODO: Is this right?
		[WireMember(1)]
		public byte ClientId { get; }

		//TODO: Is this right?
		[WireMember(2)]
		public byte LeaderId { get; }

		//TODO Is this offset from the start position? Or rotation?
		[KnownSize(8)]
		[WireMember(3)]
		public byte[] UnknownBytes { get; } = new byte[0];

		/// <summary>
		/// The starting position of the kick.
		/// (Where the ball was on the remote player's screen when he kicked it)
		/// </summary>
		[WireMember(4)]
		public Vector2<float> KickStartPosition { get; }

		[ReadToEnd]
		[WireMember(5)]
		public byte[] UnknownBytes2 { get; } = new byte[0];

		/// <inheritdoc />
		public Sub60LobbySoccerBallMoveEventPayload(byte clientId, Vector2<float> kickStartPosition)
		{
			ClientId = clientId;
			KickStartPosition = kickStartPosition;
		}

		//Serializer ctor
		private Sub60LobbySoccerBallMoveEventPayload()
		{
			
		}
	}
}
