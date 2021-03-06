using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
{
	/// <summary>
	/// Sent when a client has stopped moving. Updates the final position
	/// potential the rotation too (?)
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.SetFinalMovingPosition)]
	public sealed partial class Sub60FinishedMovingCommand : BaseSubCommand60, IMessageContextIdentifiable, IWorldPositionable<float>
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		//Unusued in most commands, some commands have this as leaderid though?
		[WireMember(2)]
		internal byte unused { get; set; }

		//TODO: Handle this
		[WireMember(3)]
		internal short AnimationState { get; set; }

		//TODO: The calculation we're doing to init or parse this is wrong. It does not match the binary packet
		//TODO: We must figure out how to exactly compute this value, test cases show off by 1 bit.
		/// <summary>
		/// The raw rotation value
		/// that is sent over the network.
		/// </summary>
		[WireMember(4)]
		internal short RawNetworkRotation { get; set; }

		/// <summary>
		/// The rotation about the Y-axis.
		/// </summary>
		public float YAxisRotation => RawNetworkRotation.FromNetworkRotationToYAxisRotation();

		/// <summary>
		/// ID for the zone the character is in.
		/// </summary>
		[WireMember(5)]
		public short ZoneId { get; internal set; }

		/// <summary>
		/// The ID for the room the character is currently in.
		/// </summary>
		[WireMember(6)]
		public short RoomId { get; internal set; }

		/// <summary>
		/// The position to teleport to.
		/// </summary>
		[WireMember(7)]
		public Vector3<float> Position { get; internal set; }

		Vector2<float> IWorldPositionable<float>.Position => new Vector2<float>(Position.X, Position.Z);

		/// <inheritdoc />
		public Sub60FinishedMovingCommand(byte clientId, float yAxisRotation, [NotNull] Vector3<float> position, short roomId, short zoneId)
			: this()
		{
			Identifier = clientId;
			Position = position ?? throw new ArgumentNullException(nameof(position));
			RoomId = roomId;
			ZoneId = zoneId;

			RawNetworkRotation = yAxisRotation.ToNetworkRotation();
		}

		/// <inheritdoc />
		public Sub60FinishedMovingCommand(int clientId, float yAxisRotation, [NotNull] Vector3<float> position, short roomId, short zoneId)
			: this((byte)clientId, yAxisRotation, position, roomId, zoneId)
		{

		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60FinishedMovingCommand()
			: base(SubCommand60OperationCode.SetFinalMovingPosition)
		{
			CommandSize = 24 / 4;
		}
	}
}
