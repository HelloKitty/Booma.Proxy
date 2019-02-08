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
	[SubCommand60(SubCommand60OperationCode.MovingSlowPositionChanged)]
	public sealed class Sub60MovingSlowPositionSetCommand : BaseSubCommand60, IMessageContextIdentifiable, IWorldPositionable<float>
	{
		/// <summary>
		/// The ID of the client moving.
		/// </summary>
		[WireMember(1)]
		public byte Identifier { get; }

		[WireMember(2)]
		private byte unk1 { get; }

		/// <summary>
		/// The new position of the client.
		/// </summary>
		[WireMember(3)]
		public Vector2<float> Position { get; }

		//Sylverant will pad 4 bytes here, it's possible they aren't 0 and are used for some thing. but for now we leave them off.

		//Serializer ctor
		private Sub60MovingSlowPositionSetCommand()
		{
			
		}
	}
}
