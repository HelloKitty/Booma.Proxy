using System.Linq;
using Booma.Proxy;
using FreecraftCore.Serializer;

namespace Booma
{
	[WireDataContract]
	public sealed class MapDataFormatObjectEntry : IBodySizable
	{
		//DO NOT SERIALIZE
		/// <inheritdoc />
		public int BodySize { get; } = 68;

		/// <summary>
		/// The Entity's object type.
		/// </summary>
		[WireMember(1)]
		public MapDatFormatEntityObjectType ObjectType { get; internal set; }

		[KnownSize(6)]
		[WireMember(2)]
		public byte[] unk1 { get; internal set; }

		//TODO: Is this globally unique? Unique to zone? Unique to area?

		/// <summary>
		/// The identifier for this object.
		/// </summary>
		[WireMember(3)]
		public ushort Identifier { get; internal set; }

		//TODO: What is this?

		/// <summary>
		/// TODO: ?
		/// </summary>
		[WireMember(4)]
		public ushort Group { get; internal set; }

		//TODO: What is this?

		/// <summary>
		/// Indicates the map section (chunk) this object is apart of.
		/// Sections contain a position offset that must be used. Otherwise
		/// the object will not end up in the correct spot.
		/// </summary>
		[WireMember(5)]
		public ushort Section { get; internal set; }

		//TODO: What is this?

		[WireMember(6)]
		public short unk2 { get; internal set; }

		/// <summary>
		/// Position of the object.
		/// </summary>
		[WireMember(7)]
		public Vector3<float> Position { get; internal set; }

		/// <summary>
		/// Rotation of the object.
		/// (Not in Euler form)
		/// </summary>
		[WireMember(8)]
		public Vector3<int> Rotation { get; internal set; }

		//First byte for teleports may be the floor
		//5th byte may be type (red vs blue)
		[KnownSize(6)]
		[WireMember(9)]
		internal byte[] unk3 { get; set; }

		[WireMember(10)]
		public int ObjectActionIdentifier { get; internal set; }

		[WireMember(11)]
		public short Action { get; internal set; }

		[WireMember(12)]
		public short ObjectInteractionId { get; internal set; }

		//TODO: What is this?
		[KnownSize(14)]
		[WireMember(13)]
		internal byte[] unk4 { get; set; }

		protected MapDataFormatObjectEntry()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Id: {Identifier} Group: {Group} Section: {Section} Unk2: {unk2} ObjectType: {ObjectType} Position: {Position}";
		}

		public string ToFullString()
		{
			return $"Type: {ObjectType} \nUnk1: {ConvertByteArrayToLogFormat(unk1)} Hex: {ConvertByteArrayToLogFormatHex(unk1)} \nIdentifier: {Identifier} ({Identifier:X00}) \nGroup: {Group} \nSection: {Section} ({Section:X00}) \n Unk2: {unk2} \nPosition: {Position} \nRotation: {Rotation} \nUnk3: {ConvertByteArrayToLogFormat(unk3)} Hex: {ConvertByteArrayToLogFormatHex(unk3)} \nObjectActionIdentifer: {ObjectActionIdentifier} ({ObjectActionIdentifier:X00}) \nAction: {Action} ({Action:X00}) \nUnk4: {ConvertByteArrayToLogFormat(unk4)} Hex: {ConvertByteArrayToLogFormatHex(unk4)}";
		}

		private string ConvertByteArrayToLogFormat(byte[] bytes)
		{
			return bytes.Aggregate("", (s, b) => $"{s} {b:000}");
		}

		private string ConvertByteArrayToLogFormatHex(byte[] bytes)
		{
			return bytes.Aggregate("", (s, b) => $"{s} {b:X2}");
		}
	}
}
