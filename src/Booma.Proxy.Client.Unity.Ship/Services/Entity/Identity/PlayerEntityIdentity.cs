using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Creates a new <see cref="IEntityIdentity"/> for a player
	/// entity.
	/// </summary>
	public class PlayerEntityIdentity : EntityIdentity
	{
		/// <inheritdoc />
		public PlayerEntityIdentity(byte entityId) 
			: base(entityId, EntityType.Player)
		{

		}
	}
}
