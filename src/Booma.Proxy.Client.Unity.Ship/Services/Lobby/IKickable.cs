using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for an object that is kickable.
	/// </summary>
	public interface IKickable
	{
		/// <summary>
		/// Kicks and objects in the direction specified.
		/// </summary>
		/// <param name="startPosition">The starting position.</param>
		/// <param name="yAxisDirection">The y-axis direction to kick towards.</param>
		void Kick(Vector3 startPosition, float yAxisDirection);
	}
}
