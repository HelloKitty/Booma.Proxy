using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// The patch welcome message acknowledgement sent by the client
	/// after <see cref="PatchingWelcomePayload"/> is recieved.
	/// </summary>
	[WireDataContract]
	[PatchClientPacketPayload(PatchNetworkOperationCode.PATCH_WELCOME_TYPE)]
	public sealed partial class PatchingWelcomeAckPayload : PSOBBPatchPacketPayloadClient
	{
		//Nothing is in the ack payload, it's basically just the header

		/// <summary>
		/// Creates a new patching welcome acknowledgement message.
		/// </summary>
		public PatchingWelcomeAckPayload()
			: base(PatchNetworkOperationCode.PATCH_WELCOME_TYPE)
		{
			
		}
	}
}
