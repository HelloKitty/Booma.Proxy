using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	public sealed class AttackHitResults
	{
		//TODO: What is this?
		[WireMember(1)]
		public short unk1 { get; }

		/// <summary>
		/// Two byte UID for the game itself that identifies
		/// an object with the format of:
		/// ABCC where CC is the ID of the object in the floor/area
		/// and BCC is the index of the object in a flat array serverside and clientside
		/// and A is the object type.
		/// </summary>
		[WireMember(2)]
		private short ObjectIdentifier { get; }

		/// <summary>
		/// The floor ID of the object. Unique per floor.
		/// </summary>
		public byte ObjectFloorId => (byte)(ObjectIdentifier & 0xFF);

		/// <summary>
		/// The index of the object on the serverside/clientside.
		/// </summary>
		public short ObjectIndex => (short)(ObjectIdentifier & 0xFFF);

		/// <summary>
		/// The type of the object hit.
		/// </summary>
		public ObjectHitType ObjectType => (ObjectHitType)((ObjectIdentifier & 0xF000) >> 12);

		/// <inheritdoc />
		public AttackHitResults(short objectIdentifier)
		{
			ObjectIdentifier = objectIdentifier;
		}

		public AttackHitResults(ObjectHitType objectType, byte floorId, byte objectId)
		{
			//We have to bitshift these together, since the entry is sent packed like this.
			ObjectIdentifier = (short)((short)objectType << 12 + floorId + objectId << 8);
		}

		private AttackHitResults()
		{

		}
	}
}