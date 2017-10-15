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
	/// Payload sent by the server when a position changes
	/// for a particular client.
	/// </summary>
	[WireDataContract]
	[SubCommand60Server(SubCommand60OperationCode.SetPositionRunning)]
	public sealed class Sub60RunningPositionChangedEvent : BaseSubCommand60Server
	{
		/// <summary>
		/// The ID of the client moving.
		/// </summary>
		[WireMember(1)]
		public short ClientId { get; }

		/// <summary>
		/// The new position of the client.
		/// </summary>
		[WireMember(2)]
		public Vector2<float> Position { get; }

		/// <inheritdoc />
		public Sub60RunningPositionChangedEvent(short clientId, [NotNull] Vector2<float> position)
		{
			if(position == null) throw new ArgumentNullException(nameof(position));

			ClientId = clientId;
			Position = position;
		}

		private Sub60RunningPositionChangedEvent()
		{
			
		}
	}
}
