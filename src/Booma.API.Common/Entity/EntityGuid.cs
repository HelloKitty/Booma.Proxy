using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Booma;

namespace Booma
{
	//TODO: This is a temporary solution until we have a TRUE NetworkEntityGuid implementation in the distant future.
	/// <summary>
	/// This is a temporary solution before the
	/// far-out future migration to 1st class NetworkEntityGuid support.
	/// </summary>
	public static class EntityGuid
	{
		private const int EntityTypeShiftAmount = 24;

		/// <summary>
		/// Computes the unique entity id for a particular entity <see cref="EntityType"/>
		/// and the <see cref="id"/>. IDs in PSO never go higher than 2 bytes, like boxes or floor items or creatures in a map.
		/// </summary>
		/// <param name="type">The type of entity.</param>
		/// <param name="id">The non-unique index/id of the entity.</param>
		/// <returns>The full unique entity guid.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ComputeEntityGuid(EntityType type, short id)
		{
			//The format is AXBB where A is 1 byte type and BB is the 2 byte id. X is unsued.
			return ((int)type << EntityTypeShiftAmount) + id;
		}

		/// <summary>
		/// Gets the <see cref="EntityType"/> packed into
		/// the 32 unique entity identifier that can be computed from <see cref="ComputeEntityGuid"/>.
		/// </summary>
		/// <param name="entityGuid"></param>
		/// <returns>The type of the entity.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EntityType GetEntityType(int entityGuid)
		{
			return (EntityType)(byte)(entityGuid >> EntityTypeShiftAmount);
		}

		/// <summary>
		/// Gets the non-unique identifier/id for the entity
		/// that is unique within the <see cref="EntityType"/> of the guid.
		/// </summary>
		/// <param name="entityGuid"></param>
		/// <returns>The non-unique id of the entity.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetEntityId(int entityGuid)
		{
			return (0xFFFF & entityGuid);
		}
	}
}
