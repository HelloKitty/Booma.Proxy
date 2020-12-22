using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
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
		[KnownSize(17)]
		[WireMember(3)]
		public string ItemName { get; internal set; }

		//Serializer ctor
		private MenuListing()
		{
			
		}
	}
}
