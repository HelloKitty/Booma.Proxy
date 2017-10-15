using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Command event sent by the server when a client changes its position.
	/// This is sent when their movement is slow/walking.
	/// </summary>
	[WireDataContract]
	[SubCommand60Server(SubCommand60OperationCode.MovingFastPositionChanged)]
	public sealed class Sub60MovingSlowPositionChangedEvent : BaseSubCommand60Server
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

		//Serializer ctor
		private Sub60MovingSlowPositionChangedEvent()
		{
			
		}
	}
}
