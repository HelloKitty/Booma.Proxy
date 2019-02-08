using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for commands that have a data component
	/// related to a world positon.
	/// </summary>
	public interface IWorldPositionable<TPositionType>
	{
		/// <summary>
		/// The world position.
		/// </summary>
		Vector2<TPositionType> Position { get; }
	}
}
