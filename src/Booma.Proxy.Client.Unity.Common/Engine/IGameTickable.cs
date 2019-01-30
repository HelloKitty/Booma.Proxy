using System;
using System.Collections.Generic;
using System.Text;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for services/components that need to be ticked
	/// forward in time.
	/// </summary>
	public interface IGameTickable
	{
		/// <summary>
		/// Called to tick the game forward.
		/// </summary>
		void Tick();
	}
}
