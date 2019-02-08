using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Sent when a client has stopped moving. Updates the final position
	/// potential the rotation too (?)
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.SetFinalMovingPosition)]
	public sealed class Sub60FinishedMovingCommand : BaseSubCommand60, ISerializationEventListener, IMessageContextIdentifiable, IWorldPositionable<float>
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; }

		//Unusued in most commands, some commands have this as leaderid though?
		[WireMember(2)]
		private byte unused { get; }

		//TODO: Handle this
		[WireMember(3)]
		private short AnimationState { get; }

		//TODO: The calculation we're doing to init or parse this is wrong. It does not match the binary packet
		//TODO: We must figure out how to exactly compute this value, test cases show off by 1 bit.
		/// <summary>
		/// The raw rotation value
		/// that is sent over the network.
		/// </summary>
		[WireMember(4)]
		private short RawNetworkRotation { get; set; }

		/// <summary>
		/// The rotation about the Y-axis.
		/// </summary>
		public float YAxisRotation { get; private set; }

		/// <summary>
		/// ID for the zone the character is in.
		/// </summary>
		[WireMember(5)]
		public short ZoneId { get; }

		/// <summary>
		/// The ID for the room the character is currently in.
		/// </summary>
		[WireMember(6)]
		public short RoomId { get; }

		/// <summary>
		/// The position to teleport to.
		/// </summary>
		[WireMember(7)]
		public Vector3<float> Position { get; }

		Vector2<float> IWorldPositionable<float>.Position => new Vector2<float>(Position.X, Position.Z);

		/// <inheritdoc />
		public Sub60FinishedMovingCommand(byte clientId, float yAxisRotation, [NotNull] Vector3<float> position, short roomId, short zoneId)
			: this()
		{
			if(position == null) throw new ArgumentNullException(nameof(position));
			if(clientId < 0) throw new ArgumentOutOfRangeException(nameof(clientId));

			Identifier = clientId;
			YAxisRotation = yAxisRotation;
			Position = position;
			RoomId = roomId;
			ZoneId = zoneId;
		}

		/// <inheritdoc />
		public Sub60FinishedMovingCommand(int clientId, float yAxisRotation, [NotNull] Vector3<float> position, short roomId, short zoneId)
			: this((byte)clientId, yAxisRotation, position, roomId, zoneId)
		{

		}

		private Sub60FinishedMovingCommand()
		{
			CommandSize = 24 / 4;
		}
		
		//The below serialization event callbacks will properly handle the network rotation
		//conversion
		/// <inheritdoc />
		public void OnBeforeSerialization()
		{
			RawNetworkRotation = (short)(YAxisRotation * 180f);
		}

		/// <inheritdoc />
		public void OnAfterDeserialization()
		{
			YAxisRotation = RawNetworkRotation / 180f;
		}
	}
}
