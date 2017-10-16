using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// A network version of the <see cref="Transform"/> component.
	/// </summary>
	public interface INetworkEntityTransform
	{
		/// <summary>
		/// The position of the networked entity.
		/// It is also settable.
		/// </summary>
		Vector3 Position { get; set; }

		/// <summary>
		/// The euler angles rotation of the transform.
		/// </summary>
		Vector3 EulerRotation { get; set; }
	}
}
