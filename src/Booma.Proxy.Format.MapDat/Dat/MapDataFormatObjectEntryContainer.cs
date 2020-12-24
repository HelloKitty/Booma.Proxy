using System.Collections.Generic;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// dat_table_object_t: http://sharnoth.com/psodevwiki/format/dat
	/// </summary>
	//[SeperatedCollectionSize(nameof(MapDataFormatObjectEntryContainer._Entries), nameof(MapDatFormatTableEntryContainer.EntryCount))]
	[WireDataContract]
	public sealed partial class MapDataFormatObjectEntryContainer : MapDatFormatTableEntryContainer
	{
		//Do not serialize
		/// <inheritdoc />
		public override MapDatFormatEntityType EntityType { get; } = MapDatFormatEntityType.Object;

#warning This was never fixed for 4.x Serializer. We must update this model.
		/// <inheritdoc />
		protected override int EntrySize { get; } = 68;

		//The size is seperated into the base type property
		//which is linked with the metadata above.
		[WireMember(1)]
		internal MapDataFormatObjectEntry[] _Entries { get; set; }

		/// <summary>
		/// Object entries.
		/// </summary>
		public IEnumerable<MapDataFormatObjectEntry> Entries => _Entries;

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public MapDataFormatObjectEntryContainer()
		{
			
		}
	}
}
