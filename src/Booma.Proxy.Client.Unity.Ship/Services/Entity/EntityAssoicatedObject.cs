using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Generic type for data associated with an entity.
	/// </summary>
	/// <typeparam name="TObjectType">The object/data type.</typeparam>
	public sealed class EntityAssoicatedObject<TObjectType> : IEntityIdentifable
		where TObjectType : class
	{
		/// <inheritdoc />
		public int EntityGuid { get; }
		
		/// <summary>
		/// The Object/Data associated with the <see cref="EntityGuid"/>.
		/// </summary>
		public TObjectType AssociatedObject { get; }

		/// <inheritdoc />
		public EntityAssoicatedObject(int entityGuid, [NotNull] TObjectType associatedObject)
		{
			EntityGuid = entityGuid;
			AssociatedObject = associatedObject ?? throw new ArgumentNullException(nameof(associatedObject));
		}
	}
}
