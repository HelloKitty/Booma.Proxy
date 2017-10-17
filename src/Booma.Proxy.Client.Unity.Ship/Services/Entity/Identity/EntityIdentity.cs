using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public class EntityIdentity : IEntityIdentity
	{
		/// <inheritdoc />
		public int EntityId { get; }

		/// <inheritdoc />
		public EntityType EntityType { get; }

		/// <inheritdoc />
		public EntityIdentity(int entityId, EntityType entityType)
		{
			if(entityId < 0) throw new ArgumentOutOfRangeException(nameof(entityId));
			if(!Enum.IsDefined(typeof(EntityType), entityType)) throw new InvalidEnumArgumentException(nameof(entityType), (int)entityType, typeof(EntityType));

			EntityId = entityId;
			EntityType = entityType;
		}
	}
}
