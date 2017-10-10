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
		/// The menu ID for the ship listing
		/// </summary>
		[WireMember(1)]
		public uint MenuId { get; }

		//TODO: What is this?
		/// <summary>
		/// (?)
		/// </summary>
		[WireMember(2)]
		public uint ItemId { get; }

		//TODO: What is this for?
		/// <summary>
		/// (?)
		/// </summary>
		[WireMember(3)]
		private ushort Flags { get; }

		/// <summary>
		/// The ship name.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[KnownSize(17)]
		[WireMember(4)]
		public string ShipName { get; }

		//Serializer ctor
		private ShipListing()
		{
			
		}
	}
}
