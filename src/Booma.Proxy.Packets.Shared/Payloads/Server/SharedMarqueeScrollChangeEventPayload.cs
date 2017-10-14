using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//TODO: Do we need to deal with padding manually like in Syl? https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/login_packets.c#L1411
	//See Syl: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/packets.h#L389
	/// <summary>
	/// Scrolling message change payload.
	/// This is the scrolling message seen at the top of the ship list.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_SCROLL_MSG_TYPE)]
	public sealed class SharedMarqueeScrollChangeEventPayload : PSOBBGamePacketPayloadServer
	{
		//TODO: What is this?
		[WireMember(1)]
		private long unusued { get; } //this could be 2 fields, treating it as 1 for effiecency

		//We don't know the size of this. It's dynamic.
		/// <summary>
		/// The scroll message.
		/// UTF16 encoded when sent.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[WireMember(2)]
		public string Message { get; }

		//Serializer ctor
		private SharedMarqueeScrollChangeEventPayload()
		{
			
		}
	}
}
