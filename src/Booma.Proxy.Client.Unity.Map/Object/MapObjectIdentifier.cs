using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Data component that should be attached to any PSOBB .dat defined
	/// object.
	/// </summary>
	public sealed class MapObjectIdentifier : SerializedMonoBehaviour
	{
		/// <summary>
		/// The Entity's object type.
		/// </summary>
		[ReadOnly]
		[OdinSerialize]
		public MapDatFormatEntityObjectType ObjectType { get; private set; }

		/// <summary>
		/// The identifier for this object.
		/// </summary>
		[ReadOnly]
		[OdinSerialize]
		public ushort Identifier { get; private set; }

		//TODO: What is this?
		/// <summary>
		/// TODO: ?
		/// </summary>
		[ReadOnly]
		[OdinSerialize]
		public ushort Group { get; private set; }

		//TODO: What is this?
		/// <summary>
		/// Indicates the map section (chunk) this object is apart of.
		/// Sections contain a position offset that must be used. Otherwise
		/// the object will not end up in the correct spot.
		/// </summary>
		[ReadOnly]
		[OdinSerialize] //TODO: Make this recomputed if we ever move it? (Ex. Levle editor)
		public ushort Section { get; private set; }

		internal void SetFromEntry(MapDataFormatObjectEntry objectEntry)
		{
			ObjectType = objectEntry.ObjectType;
			Identifier = objectEntry.Identifier;
			Group = objectEntry.Group;
			Section = objectEntry.Section;
		}
	}
}
