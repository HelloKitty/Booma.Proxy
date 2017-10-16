using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Uses the default project-wide scale for Unity->PSOBB
	/// </summary>
	[Serializable]
	public sealed class DefaultPSOScaleUnitScalerStrategy : IUnitScalerStrategy
	{
		//Right now things are build the scale of 0.2f, 0.2f, -0.2f
		//PSOBB appears to have an inverted z compared to PSOBB
		[HideInInspector]
		[SerializeField]
		private EditorExposedScaleUnitScalerStrategy DecoratedScaler = new EditorExposedScaleUnitScalerStrategy(new Vector3(0.2f, 0.2f, -0.2f));

		/// <inheritdoc />
		public float ScaleX(float x)
		{
			return DecoratedScaler.ScaleX(x);
		}

		/// <inheritdoc />
		public float ScaleY(float y)
		{
			return DecoratedScaler.ScaleY(y);
		}

		/// <inheritdoc />
		public float ScaleZ(float z)
		{
			return DecoratedScaler.ScaleZ(z);
		}

		/// <inheritdoc />
		public float UnScaleX(float x)
		{
			return DecoratedScaler.UnScaleX(x);
		}

		/// <inheritdoc />
		public float UnScaleY(float y)
		{
			return DecoratedScaler.UnScaleY(y);
		}

		/// <inheritdoc />
		public float UnScaleZ(float z)
		{
			return DecoratedScaler.UnScaleZ(z);
		}
	}
}
