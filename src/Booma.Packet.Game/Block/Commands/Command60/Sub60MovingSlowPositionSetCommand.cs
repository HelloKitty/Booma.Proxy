using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// The payload that is sent when a client is running to update their position.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.MovingSlowPositionChanged)]
	public sealed partial class Sub60MovingSlowPositionSetCommand : BaseSubCommand60, IMessageContextIdentifiable, IWorldPositionable<float>
	{
		/// <summary>
		/// The ID of the client moving.
		/// </summary>
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		[WireMember(2)]
		internal byte unk1 { get; set; }

		/// <summary>
		/// The new position of the client.
		/// </summary>
		[WireMember(3)]
		public Vector2<float> Position { get; internal set; }

		//Sylverant will pad 4 bytes here, it's possible they aren't 0 and are used for some thing. but for now we leave them off.
		[WireMember(4)]
		internal int unk2 { get; set; }

		//Serializer ctor
		public Sub60MovingSlowPositionSetCommand()
			: base(SubCommand60OperationCode.MovingSlowPositionChanged)
		{
			
		}
	}
}
