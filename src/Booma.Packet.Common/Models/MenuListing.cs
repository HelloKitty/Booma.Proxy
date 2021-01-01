using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
{
	/// <summary>
	/// A data model for a menu listing
	/// </summary>
	[WireDataContract]
	public sealed class MenuListing
	{
		/// <summary>
		/// The id of the menu selecting from.
		/// </summary>
		[WireMember(1)]
		public MenuItemIdentifier Selection { get; internal set; }

		//TODO: What is this for?
		/// <summary>
		/// (?)
		/// </summary>
		[WireMember(2)]
		internal ushort Flags { get; set; }

		/// <summary>
		/// The ship name.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[DontTerminate]
		[KnownSize(17)]
		[WireMember(3)]
		public string ItemName { get; internal set; }

		public MenuListing(MenuItemIdentifier selection, ushort flags, string itemName)
			: this()
		{
			Selection = selection ?? throw new ArgumentNullException(nameof(selection));
			Flags = flags;
			ItemName = itemName ?? throw new ArgumentNullException(nameof(itemName));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public MenuListing()
		{
			
		}
	}
}
