using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Payload broadcasted when a client hits the Lobby soccer ball.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.LobbyBallMove)]
	public sealed partial class Sub60LobbySoccerBallMoveEventPayload : BaseSubCommand60
	{
		//TODO: Is this right?
		[WireMember(1)]
		public byte ClientId { get; internal set; }

		//TODO: Is this right?
		[WireMember(2)]
		public byte LeaderId { get; internal set; }

		/// <summary>
		/// The number of frames (30fps) since the ball spawned.
		/// </summary>
		[WireMember(3)]
		public int TimeStamp { get; internal set; }

		/// <summary>
		/// Y-axis rotation that determines the direction
		/// the ball should move in.
		/// </summary>
		[WireMember(4)]
		internal short RawRotation { get; set; }

		[WireMember(5)]
		internal short unk { get; set; }

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
		public Vector2<float> KickStartPosition { get; internal set; }

		//TODO: What is this?
		[KnownSize(4)]
		[WireMember(7)]
		internal byte[] UnknownBytes { get; set; } = new byte[4] {0, 0x59, 0x66, 0};

		/// <inheritdoc />
		public Sub60LobbySoccerBallMoveEventPayload(byte clientId, short timeStamp, float rotation, Vector2<float> kickStartPosition)
			: this()
		{
			TimeStamp = timeStamp;
			ClientId = clientId;
			KickStartPosition = kickStartPosition;

			RawRotation = rotation.ToNetworkRotation();
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60LobbySoccerBallMoveEventPayload()
			: base(SubCommand60OperationCode.LobbyBallMove)
		{
			CommandSize = 24 / 4;
		}
	}
}
