using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCode.PATCH_MESSAGE_TYPE)]
	public sealed class PatchingMessagePayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// News, MOTD, whatever
		/// WCHAR string
		/// Requires null terminator
		/// Max length is 2046 including null terminator
		/// </summary>
		[Encoding(EncodingType.UTF16)] //wchar 16bit
		[WireMember(1)]
		public string Message { get; }

		public PatchingMessagePayload([NotNull] string message)
		{
			if(message == null) throw new ArgumentNullException(nameof(message));

			Message = message;
		}

		//serializer ctor
		private PatchingMessagePayload()
		{

		}
	}
}
