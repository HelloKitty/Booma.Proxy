using Booma.Proxy;
using FreecraftCore.Serializer;

namespace Booma
{
	[WireDataContract]
	public sealed class MapDataFormatObjectEntry
	{
		/// <summary>
		/// The Entity's object type.
		/// </summary>
		[WireMember(1)]
		public MapDatFormatEntityObjectType ObjectType { get; }

		[KnownSize(6)]
		[WireMember(2)]
		public byte[] unk1 { get; }

		//TODO: Is this globally unique? Unique to zone? Unique to area?
		/// <summary>
		/// The identifier for this object.
		/// </summary>
		[WireMember(3)]
		public short Identifier { get; }

		//TODO: What is this?
		/// <summary>
		/// TODO: ?
		/// </summary>
		[WireMember(4)]
		public ushort Group { get; }
		
		//TODO: What is this?
		/// <summary>
		/// TODO: ?
		/// </summary>
		[WireMember(5)]
		public ushort Section { get; }

		//TODO: What is this?
		[WireMember(6)]
		private short unk2 { get; }

		/// <summary>
		/// Position of the object.
		/// </summary>
		[WireMember(7)]
		public Vector3<float> Position { get; }

		/// <summary>
		/// Rotation of the object.
		/// (Not in Euler form)
		/// </summary>
		[WireMember(8)]
		public Vector3<int> Rotation { get; }

		[KnownSize(6)]
		[WireMember(9)]
		private byte[] unk3 { get; }

		[WireMember(10)]
		public int ObjectActionIdentifier { get; }

		[WireMember(11)]
		public int Action { get; }

		//TODO: What is this?
		[KnownSize(14)]
		[WireMember(12)]
		private byte[] unk4 { get; } 

		protected MapDataFormatObjectEntry()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Id: {Identifier} ObjectType: {ObjectType} Position: {Position}";
		}
	}
}
