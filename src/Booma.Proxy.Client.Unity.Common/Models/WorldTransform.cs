using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class WorldTransform
	{
		public Vector3 Position { get; }

		public Quaternion Rotation { get; }

		/// <inheritdoc />
		public WorldTransform(Vector3 position, Quaternion rotation)
		{
			Position = position;
			Rotation = rotation;
		}
	}
}
