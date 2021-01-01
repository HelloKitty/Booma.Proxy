using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
{
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCode.PATCH_MESSAGE_TYPE)]
	public sealed partial class PatchingMessagePayload : PSOBBPatchPacketPayloadServer
	{
		/// <summary>
		/// News, MOTD, whatever
		/// WCHAR string
		/// Requires null terminator
		/// Max length is 2046 including null terminator
		/// </summary>
		[Encoding(EncodingType.UTF16)] //wchar 16bit
		[WireMember(1)]
		public string Message { get; internal set; }

		public PatchingMessagePayload(string message)
			: this()
		{
			if(message == null) throw new ArgumentNullException(nameof(message));

			Message = message;
		}

		/// <summary>
		/// Serializer ctor
		/// </summary>
		public PatchingMessagePayload()
			: base(PatchNetworkOperationCode.PATCH_MESSAGE_TYPE)
		{

		}
	}
}
