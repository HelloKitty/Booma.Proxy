using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCodes.PATCH_MESSAGE_TYPE)]
	public sealed class PatchingMessagePayload : PSOBBPatchPacketPayloadServer
	{
		// TODO implement WCHAR serialization
		/// <summary>
		/// News, MOTD, whatever
		/// WCHAR string
		/// Requires null terminator
		/// Max length is 2046 including null terminator
		/// </summary>
		[WireMember(1)]
		public string Message { get; }

		public PatchingMessagePayload(string message)
		{
			Message = message;
		}

		//serializer ctor
		private PatchingMessagePayload()
		{

		}
	}
}
