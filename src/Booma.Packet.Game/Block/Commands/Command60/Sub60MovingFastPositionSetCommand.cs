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
	/// Command request sent by the client when a client changes its position while running/movingfast.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.MovingFastPositionChanged)]
	public sealed partial class Sub60MovingFastPositionSetCommand : BaseSubCommand60, IMessageContextIdentifiable, IWorldPositionable<float>
	{
		/// <summary>
		/// The client that is moving.
		/// </summary>
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		[WireMember(2)]
		internal byte unused { get; set; }

		/// <summary>
		/// The position the client has moved to.
		/// </summary>
		[WireMember(3)]
		public Vector2<float> Position { get; internal set; }

		//TODO: This is a 3rd unknown int

		/// <inheritdoc />
		public Sub60MovingFastPositionSetCommand(byte clientId, [NotNull] Vector2<float> position)
			: this()
		{
			if(position == null) throw new ArgumentNullException(nameof(position));

			Identifier = clientId;
			Position = position;
		}

		public Sub60MovingFastPositionSetCommand(int clientId, [NotNull] Vector2<float> position)
			: this((byte)clientId, position)
		{
			
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60MovingFastPositionSetCommand()
			: base(SubCommand60OperationCode.MovingFastPositionChanged)
		{
			CommandSize = 12 / 4;
		}
	}
}
