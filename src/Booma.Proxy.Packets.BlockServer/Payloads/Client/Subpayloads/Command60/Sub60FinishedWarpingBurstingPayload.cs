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
	[SubCommand60Client(SubCommand60OperationCode.EnterFreshlyWrappedZoneCommand)]
	public sealed class Sub60FinishedWarpingBurstingPayload : BlockNetworkCommandEventClientPayload
	{
		//Packet is empty. Just tells the server we bursted/warped finished.
		//TODO: Is this client id?
		[WireMember(1)]
		private short unk { get; }

		public Sub60FinishedWarpingBurstingPayload()
		{
			
		}
	}
}
