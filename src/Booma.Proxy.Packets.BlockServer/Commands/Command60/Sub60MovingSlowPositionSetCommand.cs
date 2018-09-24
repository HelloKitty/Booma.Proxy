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
	public sealed class Sub60MovingSlowPositionSetCommand : BaseSubCommand60, IMessageContextIdentifiable
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

		//Sylverant has this 4 byte value here, but it could be padding from block cipher.
		//TODO: What is this? Is it always 0?
		[WireMember(4)]
		private uint unk2 { get; } = 0;

		//Serializer ctor
		private Sub60MovingSlowPositionSetCommand()
		{
			
		}
	}
}
