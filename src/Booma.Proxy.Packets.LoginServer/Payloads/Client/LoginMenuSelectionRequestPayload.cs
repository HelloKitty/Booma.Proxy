using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload for selecting a menu option.
	/// </summary>
	[WireDataContract]
	[LoginClientPacketPayload(LoginNetworkOperationCodes.MENU_SELECT_TYPE)]
	public sealed class LoginMenuSelectionRequestPayload : PSOBBLoginPacketPayloadClient, IMenuItem
	{
		/// <summary>
		/// The id of the menu selecting from.
		/// </summary>
		[WireMember(1)]
		public uint MenuId { get; }

		/// <summary>
		/// The item id of the item on the menu
		/// being selected.
		/// </summary>
		[WireMember(2)]
		public uint ItemId { get; }

		/// <inheritdoc />
		public LoginMenuSelectionRequestPayload(uint menuId, uint itemId)
		{
			MenuId = menuId;
			ItemId = itemId;
		}

		public LoginMenuSelectionRequestPayload()
		{
			
		}
	}
}
