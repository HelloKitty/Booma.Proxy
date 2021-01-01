using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Strategy that doesn't scale or unscale the provided components at all.
	/// </summary>
	[Serializable]
	public class NoScalingUnitScalerStrategy : IUnitScalerStrategy
	{
		/// <inheritdoc />
		public float ScaleX(float x)
		{
			return x;
		}

		/// <inheritdoc />
		public float ScaleY(float y)
		{
			return y;
		}

		/// <inheritdoc />
		public float ScaleZ(float z)
		{
			return z;
		}

		/// <inheritdoc />
		public float UnScaleX(float x)
		{
			return x;
		}

		/// <inheritdoc />
		public float UnScaleY(float y)
		{
			return y;
		}

		/// <inheritdoc />
		public float UnScaleZ(float z)
		{
			return z;
		}
	}
}
