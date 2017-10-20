﻿using System;
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
	public sealed class Sub60LobbySoccerBallMoveEventPayload : BaseSubCommand60, ISerializationEventListener
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
		public short TimeStamp { get; }

		/// <summary>
		/// Y-axis rotation that determines the direction
		/// the ball should move in.
		/// </summary>
		[WireMember(5)]
		private short RawRotation { get; set; }

		/// <summary>
		/// Y-axis rotation that determines the direction
		/// the ball should move in.
		/// </summary>
		public float YAxisRotation { get; private set; }

		/// <summary>
		/// The starting position of the kick.
		/// (Where the ball was on the remote player's screen when he kicked it)
		/// </summary>
		[WireMember(4)]
		public Vector2<float> KickStartPosition { get; }

		//TODO: What is this?
		[KnownSize(4)]
		[WireMember(5)]
		private byte[] UnknownBytes { get; } = new byte[4] {0, 0x59, 0x66, 0};

		/// <inheritdoc />
		public Sub60LobbySoccerBallMoveEventPayload(byte clientId, short timeStamp, short rotation, Vector2<float> kickStartPosition, byte[] unknownBytes2)
			: this()
		{
			YAxisRotation = rotation;
			TimeStamp = timeStamp;
			ClientId = clientId;
			KickStartPosition = kickStartPosition;
		}

		//Serializer ctor
		private Sub60LobbySoccerBallMoveEventPayload()
		{
			CommandSize = 24 / 4;
		}

		public void OnBeforeSerialization()
		{
			RawRotation = (short)(YAxisRotation * 180f);
		}

		/// <inheritdoc />
		public void OnAfterDeserialization()
		{
			YAxisRotation = RawRotation / 180f;
		}
	}
}
