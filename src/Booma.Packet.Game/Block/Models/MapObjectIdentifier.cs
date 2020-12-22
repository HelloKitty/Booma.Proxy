using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//TODO: Unit test against some known values
	[WireDataContract]
	public sealed class MapObjectIdentifier : IMessageContextIdentifiable, IComparable<MapObjectIdentifier>, IEquatable<MapObjectIdentifier>
	{
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
		public byte Identifier => (byte)(ObjectIdentifier & 0xFF);

		/// <summary>
		/// The index of the object on the serverside/clientside.
		/// </summary>
		public short ObjectIndex => (short)(ObjectIdentifier & 0xFFF);

		/// <summary>
		/// The type of the object hit.
		/// </summary>
		public ObjectHitType ObjectType => (ObjectHitType)((ObjectIdentifier & 0xF000) >> 12);

		/// <inheritdoc />
		public MapObjectIdentifier(short objectIdentifier)
		{
			ObjectIdentifier = objectIdentifier;
		}

		//TODO: This is NOT floorid.
		public MapObjectIdentifier(ObjectHitType objectType, byte floorId, byte objectId)
		{
			//We have to bitshift these together, since the entry is sent packed like this.
			ObjectIdentifier = (short)(((short)objectType << 12) + (floorId << 8) + objectId);
		}

		private MapObjectIdentifier()
		{
			
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return ObjectIdentifier.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is MapObjectIdentifier identifier &&
				   ObjectIdentifier == identifier.ObjectIdentifier;
		}

		/// <inheritdoc />
		public int CompareTo(MapObjectIdentifier other)
		{
			if(other is null) return -1;
			return ObjectIdentifier.CompareTo(other.ObjectIdentifier);
		}

		/// <inheritdoc />
		public bool Equals(MapObjectIdentifier other)
		{
			if(other is null) return false;
			if(ReferenceEquals(this, other)) return true;
			return ObjectIdentifier == other.ObjectIdentifier;
		}
	}
}
