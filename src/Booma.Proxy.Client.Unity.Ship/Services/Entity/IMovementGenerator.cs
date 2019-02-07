using System;
using System.Collections.Generic;
using System.Text;

namespace Booma.Proxy
{
	public interface IMovementGenerator<in TEntityType> //we make entity type generic so it will be easy to swap between guid/gameobject if needed.
	{
		/// <summary>
		/// Updates the movement for the <see cref="entity"/>
		/// based on the time <see cref="currentTime"/>.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="currentTime"></param>
		void Update(TEntityType entity, long currentTime);
	}
}