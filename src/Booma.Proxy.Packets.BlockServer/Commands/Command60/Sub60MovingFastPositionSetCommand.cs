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
	/// Command request sent by the client when a client changes its position while running/movingfast.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.MovingFastPositionChanged)]
	public sealed class Sub60MovingFastPositionSetCommand : BaseSubCommand60
	{
		/// <summary>
		/// The client that is moving.
		/// </summary>
		[WireMember(1)]
		public short ClientId { get; }

		/// <summary>
		/// The position the client has moved to.
		/// </summary>
		[WireMember(2)]
		public Vector2<float> Position { get; }

		//TODO: This is a 3rd unknown int

		/// <inheritdoc />
		public Sub60MovingFastPositionSetCommand(short clientId, [NotNull] Vector2<float> position)
		{
			if(position == null) throw new ArgumentNullException(nameof(position));
			if(clientId < 0) throw new ArgumentOutOfRangeException(nameof(clientId));

			ClientId = clientId;
			Position = position;
		}

		//Serializer ctor
		private Sub60MovingFastPositionSetCommand()
		{
			
		}
	}
}
