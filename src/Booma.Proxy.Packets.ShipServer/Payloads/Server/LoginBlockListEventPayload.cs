using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Contains the block list for menu rendering.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BLOCK_LIST_TYPE)]
	public sealed class LoginBlockListEventPayload : PSOBBGamePacketPayloadServer, ISerializationEventListener
	{
		//Disable flags serialization so that the ship can get the 4 byte length and
		//handle writing the 4 bytes length
		/// <inheritdoc />
		public override bool isFlagsSerialized { get; } = false;

		//PSOBB sends 4 byte Flags with the entry count. We disable Flags though to steal the 4 bytes
		[SendSize(SendSizeAttribute.SizeType.Int32, 1)] //for some reason they send 1 less than the actual size 
		[WireMember(1)]
		private MenuListing[] _Blocks { get; set; } //settable for removing the garbage entry

		/// <summary>
		/// The ship menu models.
		/// </summary>
		public IEnumerable<MenuListing> Blocks => _Blocks;

		//Serializer ctor
		private LoginBlockListEventPayload()
		{
			
		}

		/// <inheritdoc />
		public void OnBeforeSerialization()
		{
			//TODO: Deal with the bullshit the server adds for some reason
		}

		/// <inheritdoc />
		public void OnAfterDeserialization()
		{
			//Remove the first entry, it's garbage
			_Blocks = _Blocks.Skip(1).ToArray();
		}
	}
}
