using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public interface IMovementStatePositionTargetable
	{
		/// <summary>
		/// The target position for the movement state.
		/// </summary>
		Vector2 TargetPosition { get; }
	}
}
