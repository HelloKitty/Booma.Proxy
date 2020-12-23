using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload that tells the server we've finsihed loading/bursting/warping.
	/// We should have sent a warp request to the server before this so it should know where we
	/// were going.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.EnterFreshlyWrappedZoneCommand)]
	public sealed partial class Sub60FinishedWarpingBurstingCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		//Packet is empty. Just tells the server we bursted/warped finished.
		//TODO: Is this client id?
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		//Is this really unused?
		[WireMember(2)]
		internal byte unused { get; set; }

		/// <inheritdoc />
		public Sub60FinishedWarpingBurstingCommand(byte identifier)
			: this() //important to call ctor with commandsize too
		{
			Identifier = identifier;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60FinishedWarpingBurstingCommand()
			: base(SubCommand60OperationCode.EnterFreshlyWrappedZoneCommand)
		{
			//Calc static 32bit size
			CommandSize = 4 / 4;
		}
	}
}
