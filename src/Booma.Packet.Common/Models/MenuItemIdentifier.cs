using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Model for a menu selection.
	/// </summary>
	[WireDataContract]
	public sealed class MenuItemIdentifier : IMenuItemIdentifiable
	{
		/// <summary>
		/// The id of the menu selecting from.
		/// </summary>
		[WireMember(1)]
		public uint MenuId { get; internal set; }

		/// <summary>
		/// The item id of the item on the menu
		/// being selected.
		/// </summary>
		[WireMember(2)]
		public uint ItemId { get; internal set; }

		/// <inheritdoc />
		public MenuItemIdentifier(uint menuId, uint itemId)
		{
			MenuId = menuId;
			ItemId = itemId;
		}

		//Serializer ctor
		private MenuItemIdentifier()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"MenuID: {MenuId} ItemId: {ItemId}";
		}
	}
}
