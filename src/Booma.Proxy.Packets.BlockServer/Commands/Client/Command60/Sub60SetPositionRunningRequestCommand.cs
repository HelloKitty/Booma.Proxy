using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// The payload that is sent when a client is running to update their position.
	/// </summary>
	[WireDataContract]
	[SubCommand60Client(SubCommand60OperationCode.SetPositionRunning)]
	public sealed class Sub60SetPositionRunningRequestCommand : BaseSubCommand60Client
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

		//Serializer ctor
		private Sub60SetPositionRunningRequestCommand()
		{
			
		}
	}
}
