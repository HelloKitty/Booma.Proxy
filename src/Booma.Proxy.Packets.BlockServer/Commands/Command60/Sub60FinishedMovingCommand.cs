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
	public sealed class Sub60FinishedMovingCommand : BaseSubCommand60, ISerializationEventListener, ICommandClientIdentifiable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte ClientId { get; }

		//Unusued in most commands, some commands have this as leaderid though?
		[WireMember(2)]
		private byte unused { get; }

		[WireMember(3)]
		private short unk { get; }

		/// <summary>
		/// The raw rotation value
		/// that is sent over the network.
		/// </summary>
		[WireMember(4)]
		private ushort RawNetworkRotation { get; set; }

		/// <summary>
		/// The rotation about the Y-axis.
		/// </summary>
		public float YAxisRotation { get; private set; }

		[WireMember(5)]
		public int W { get; } = 65551;

		/// <summary>
		/// The position to teleport to.
		/// </summary>
		[WireMember(6)]
		public Vector3<float> Position { get; }

		/// <inheritdoc />
		public Sub60FinishedMovingCommand(byte clientId, float yAxisRotation, [NotNull] Vector3<float> position)
			: this()
		{
			if(position == null) throw new ArgumentNullException(nameof(position));
			if(clientId < 0) throw new ArgumentOutOfRangeException(nameof(clientId));

			ClientId = clientId;
			YAxisRotation = yAxisRotation;
			Position = position;
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
			RawNetworkRotation = (ushort)((ushort)(YAxisRotation * 180f) + 180);
		}

		/// <inheritdoc />
		public void OnAfterDeserialization()
		{
			YAxisRotation = RawNetworkRotation / 180f + 180;
		}
	}
}
