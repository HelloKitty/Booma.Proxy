using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

		/// <summary>
		/// The number of frames (30fps) since the ball spawned.
		/// </summary>
		[WireMember(3)]
		public int TimeStamp { get; }

		/// <summary>
		/// Y-axis rotation that determines the direction
		/// the ball should move in.
		/// </summary>
		[WireMember(4)]
		private short RawRotation { get; set; }

		[WireMember(5)]
		private short unk { get; }

		/// <summary>
		/// Y-axis rotation that determines the direction
		/// the ball should move in.
		/// </summary>
		public float YAxisRotation => RawRotation.FromNetworkRotationToYAxisRotation();

		/// <summary>
		/// The starting position of the kick.
		/// (Where the ball was on the remote player's screen when he kicked it)
		/// </summary>
		[WireMember(6)]
		public Vector2<float> KickStartPosition { get; }

		//TODO: What is this?
		[KnownSize(4)]
		[WireMember(7)]
		private byte[] UnknownBytes { get; } = new byte[4] {0, 0x59, 0x66, 0};

		/// <inheritdoc />
		public Sub60LobbySoccerBallMoveEventPayload(byte clientId, short timeStamp, float rotation, Vector2<float> kickStartPosition)
			: this()
		{
			TimeStamp = timeStamp;
			ClientId = clientId;
			KickStartPosition = kickStartPosition;

			RawRotation = rotation.ToNetworkRotation();
		}

		//Serializer ctor
		private Sub60LobbySoccerBallMoveEventPayload()
		{
			CommandSize = 24 / 4;
		}
	}
}
