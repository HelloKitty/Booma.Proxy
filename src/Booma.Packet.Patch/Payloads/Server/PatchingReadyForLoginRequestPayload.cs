using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Packet sent by the patch server when it's ready for the
	/// <see cref="PSOBBGamePacketPayloadClient"/> to be sent.
	/// </summary>
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCode.PATCH_LOGIN_TYPE)]
	public sealed partial class PatchingReadyForLoginRequestPayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// This is empty, just the server telling the client it's ready for the login.
		/// </summary>
		public PatchingReadyForLoginRequestPayload()
			: base(PatchNetworkOperationCode.PATCH_LOGIN_TYPE)
		{
			
		}
	}
}
