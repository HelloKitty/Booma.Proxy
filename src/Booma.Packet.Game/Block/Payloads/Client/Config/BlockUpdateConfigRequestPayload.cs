using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma
{
	//See: https://github.com/Sylverant/ship_server/blob/2695c7c35a2c01d3eefe8d1bdd8a15996e79fff4/src/block.c#L3067
	/// <summary>
	/// Sent by the client when exiting the customization window (such as the action palette window)
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.BB_UPDATE_CONFIG)]
	public sealed partial class BlockUpdateConfigRequestPayload : PSOBBGamePacketPayloadClient
	{
		/// <summary>
		/// The configuration data.
		/// </summary>
		[KnownSize(232)] //0x00E8
		[WireMember(1)]
		public byte[] Data { get; internal set; } = Array.Empty<byte>();

		public BlockUpdateConfigRequestPayload(byte[] data)
			: this()
		{
			Data = data ?? throw new ArgumentNullException(nameof(data));
		}

		public BlockUpdateConfigRequestPayload()
			: base(GameNetworkOperationCode.BB_UPDATE_CONFIG)
		{

		}
	}
}
