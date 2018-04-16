using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Table entry header data.
	/// </summary>
	[WireDataContract]
	public sealed class MapDatFormatTableEntryHeader
	{
		/// <summary>
		/// The size of the total entry in bytes.
		/// </summary>
		[WireMember(1)]
		public uint EntryTotalSize { get; }

		//TODO: What is this?
		[WireMember(2)]
		public uint Area { get; }

		/// <summary>
		/// The size of the collection of entries.
		/// <see cref="EntryTotalSize"/> - 16.
		/// </summary>
		[WireMember(3)]
		public uint EntryBodySize { get; }

		//Serializer ctor
		protected MapDatFormatTableEntryHeader()
		{
			
		}
	}
}