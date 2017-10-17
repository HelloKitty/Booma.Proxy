using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class DefaultNetworkTransform : INetworkEntityTransform
	{
		/// <inheritdoc />
		public Vector3 Position { get; set; }

		/// <inheritdoc />
		public Quaternion Rotation { get; set; }

		/// <inheritdoc />
		public DefaultNetworkTransform(Vector3 position, Quaternion rotation)
		{
			Position = position;
			Rotation = rotation;
		}

		public DefaultNetworkTransform()
		{
			
		}
	}
}
