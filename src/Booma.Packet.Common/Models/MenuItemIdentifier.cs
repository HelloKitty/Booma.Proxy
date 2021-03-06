using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Model for a menu selection.
	/// </summary>
	[WireDataContract]
	public class MenuItemIdentifier : IMenuItemIdentifiable
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
			: this()
		{
			MenuId = menuId;
			ItemId = itemId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public MenuItemIdentifier()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"MenuID: {MenuId} ItemId: {ItemId}";
		}
	}
}
