using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// dat_table_t : http://sharnoth.com/psodevwiki/format/dat
	/// </summary>
	[WireDataContractBaseType(1, typeof(MapDataFormatObjectEntryContainer))]
	[WireDataContract(WireDataContractAttribute.KeyType.Int32)]
	public abstract class MapDatFormatTableEntryContainer
	{
		/// <summary>
		/// Header for the entry.
		/// </summary>
		[WireMember(1)]
		public MapDatFormatTableEntryHeader Header { get; internal set; }

		//We don't prevent int32 consumption on child type polymorphism
		//Which means we'll need to not make this serializable and must have child type
		//indicate the expected type.
		/// <summary>
		/// The type of the entity.
		/// </summary>
		public abstract MapDatFormatEntityType EntityType { get; }

		//This is a unique serialization need, we don't want to read or write this
		//but it is included in the serializer model as a disjoint collection size
		[DontRead]
		[DontWrite]
		[WireMember(2)]
		protected internal int EntryCount => (int)(Header.EntryBodySize / EntrySize);

		//Think of like sizeof(T) where T is  the entry type.
		//We need to allow inheritors to indicate the actual size
		//so we know the actual count of the entries.
		protected abstract int EntrySize { get; internal set; }

		//Serializer ctor
		protected MapDatFormatTableEntryContainer()
		{
			
		}
	}
}
