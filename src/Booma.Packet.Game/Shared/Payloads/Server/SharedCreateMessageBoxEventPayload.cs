using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.MESSAGE_BOX_TYPE)]
	public sealed partial class SharedCreateMessageBoxEventPayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// Message to write to the created message box.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[WireMember(1)]
		public string Message { get; internal set; }

		public SharedCreateMessageBoxEventPayload([NotNull] string message) 
			: this()
		{
			Message = message ?? throw new ArgumentNullException(nameof(message));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public SharedCreateMessageBoxEventPayload()
			: base(GameNetworkOperationCode.MESSAGE_BOX_TYPE)
		{
			
		}
	}
}
