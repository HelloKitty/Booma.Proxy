using System.Linq;
using Booma.Proxy;
using FreecraftCore.Serializer;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

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
		[ReadOnly]
		[OdinSerialize]
		[WireMember(1)]
		public MapDatFormatEntityObjectType ObjectType { get; }

		[ReadOnly]
		[OdinSerialize]
		[KnownSize(6)]
		[WireMember(2)]
		public byte[] unk1 { get; }

		//TODO: Is this globally unique? Unique to zone? Unique to area?

		/// <summary>
		/// The identifier for this object.
		/// </summary>
		[ReadOnly]
		[OdinSerialize]
		[WireMember(3)]
		public ushort Identifier { get; }

		//TODO: What is this?

		/// <summary>
		/// TODO: ?
		/// </summary>
		[ReadOnly]
		[OdinSerialize]
		[WireMember(4)]
		public ushort Group { get; }

		//TODO: What is this?

		/// <summary>
		/// Indicates the map section (chunk) this object is apart of.
		/// Sections contain a position offset that must be used. Otherwise
		/// the object will not end up in the correct spot.
		/// </summary>
		[ReadOnly]
		[OdinSerialize]
		[WireMember(5)]
		public ushort Section { get; }

		//TODO: What is this?
		[ReadOnly]
		[OdinSerialize]
		[WireMember(6)]
		public short unk2 { get; }

		/// <summary>
		/// Position of the object.
		/// </summary>
		[ReadOnly]
		[OdinSerialize]
		[WireMember(7)]
		public Vector3<float> Position { get; }

		/// <summary>
		/// Rotation of the object.
		/// (Not in Euler form)
		/// </summary>
		[ReadOnly]
		[OdinSerialize]
		[WireMember(8)]
		public Vector3<int> Rotation { get; }

		//First byte for teleports may be the floor
		//5th byte may be type (red vs blue)
		[ReadOnly]
		[OdinSerialize]
		[KnownSize(6)]
		[WireMember(9)]
		private byte[] unk3 { get; }

		[ReadOnly]
		[OdinSerialize]
		[WireMember(10)]
		public int ObjectActionIdentifier { get; }

		[ReadOnly]
		[OdinSerialize]
		[WireMember(11)]
		public int Action { get; }

		//TODO: What is this?

		[ReadOnly]
		[OdinSerialize]
		[KnownSize(14)]
		[WireMember(12)]
		private byte[] unk4 { get; }

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
