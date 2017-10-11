using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// A data model for a ship listing for a menu
	/// </summary>
	[WireDataContract]
	public sealed class ShipListing
	{
		/// <summary>
		/// The id of the menu selecting from.
		/// </summary>
		[WireMember(1)]
		public MenuItemIdentifier Selection { get; }

		//TODO: What is this for?
		/// <summary>
		/// (?)
		/// </summary>
		[WireMember(2)]
		private ushort Flags { get; }

		/// <summary>
		/// The ship name.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[KnownSize(17)]
		[WireMember(3)]
		public string ShipName { get; }

		//Serializer ctor
		private ShipListing()
		{
			
		}
	}
}
