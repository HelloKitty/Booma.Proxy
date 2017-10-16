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
	public sealed class Sub60FinishedMovingCommand : BaseSubCommand60
	{
		//TODO: Refactor this into an interface or something
		//This is a short to absorb the unused byte
		[WireMember(1)]
		public short ClientId { get; }

		[WireMember(2)]
		private short unk { get; }

		[WireMember(3)]
		public ushort YAxisRotation { get; }

		[WireMember(4)]
		public int W { get; } = 65551;

		/// <summary>
		/// The position to teleport to.
		/// </summary>
		[WireMember(5)]
		public Vector3<float> Position { get; }

		/// <inheritdoc />
		public Sub60FinishedMovingCommand(short clientId, ushort yAxisRotation, [NotNull] Vector3<float> position)
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
	}
}
