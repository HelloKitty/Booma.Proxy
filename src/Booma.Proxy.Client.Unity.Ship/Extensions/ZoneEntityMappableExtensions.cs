using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public static class ZoneEntityMappableExtensions
	{
		/// <summary>
		/// Indicates if the two entities <see cref="entityGuidOne"/> and <see cref="entityGuidTwo"/> are in the same zone.
		/// </summary>
		/// <param name="mappable">The mappable.</param>
		/// <param name="entityGuidOne">The entity guid of entity one.</param>
		/// <param name="entityGuidTwo">The entity guid of entity two.</param>
		/// <exception cref="InvalidOperationException">Throws if either <see cref="entityGuidOne"/> or <see cref="entityGuidTwo"/> are not in the <see cref="mappable"/>.</exception>
		/// <returns>True if the provided entities are in the same zone.</returns>
		public static bool AreEntitiesInSameZone([NotNull] this IReadonlyEntityGuidMappable<PlayerZoneData> mappable, int entityGuidOne, int entityGuidTwo)
		{
			if(mappable == null) throw new ArgumentNullException(nameof(mappable));

			//TODO: This is a hack to just cast it
			return AreEntitiesInSameZoneInternal(mappable as IDictionary<int, PlayerZoneData>, entityGuidOne, entityGuidTwo);
		}

		/// <summary>
		/// Indicates if the two entities <see cref="entityGuidOne"/> and <see cref="entityGuidTwo"/> are in the same zone.
		/// </summary>
		/// <param name="mappable">The mappable.</param>
		/// <param name="entityGuidOne">The entity guid of entity one.</param>
		/// <param name="entityGuidTwo">The entity guid of entity two.</param>
		/// <exception cref="InvalidOperationException">Throws if either <see cref="entityGuidOne"/> or <see cref="entityGuidTwo"/> are not in the <see cref="mappable"/>.</exception>
		/// <returns>True if the provided entities are in the same zone.</returns>
		public static bool AreEntitiesInSameZone([NotNull] this IEntityGuidMappable<PlayerZoneData> mappable, int entityGuidOne, int entityGuidTwo)
		{
			if(mappable == null) throw new ArgumentNullException(nameof(mappable));

			return AreEntitiesInSameZoneInternal(mappable, entityGuidOne, entityGuidTwo);
		}

		private static bool AreEntitiesInSameZoneInternal(IDictionary<int, PlayerZoneData> mappable, int entityGuidOne, int entityGuidTwo)
		{
			//TODO: Validate entity guids
			AssertEntityContainedInMappable(mappable, entityGuidOne);
			AssertEntityContainedInMappable(mappable, entityGuidTwo);

			return mappable[entityGuidOne].ZoneId == mappable[entityGuidTwo].ZoneId;
		}

		private static void AssertEntityContainedInMappable(IDictionary<int, PlayerZoneData> mappable, int entityGuid)
		{
			if(!mappable.ContainsKey(entityGuid))
				throw new InvalidOperationException($"Mappable does not contain {nameof(PlayerZoneData)} for EntityType: {EntityGuid.GetEntityType(entityGuid)} Id: {EntityGuid.GetEntityId(entityGuid)}");
		}
	}
}
