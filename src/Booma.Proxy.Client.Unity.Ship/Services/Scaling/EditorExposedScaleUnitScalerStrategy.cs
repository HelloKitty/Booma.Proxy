using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Exposed the scale vector to the editor.
	/// </summary>
	[Serializable]
	public struct EditorExposedScaleUnitScalerStrategy : IUnitScalerStrategy
	{
		/// <summary>
		/// The unit scale to scale to and from.
		/// </summary>
		[PropertyTooltip("The scale to apply to the ingoing and outgoing Vector based data.")]
		[ValidateInput(nameof(VerifyScale), "The scale must be non-zero for each component.")]
		[OdinSerialize]
		private Vector3 Scale { get; set; }

		public EditorExposedScaleUnitScalerStrategy(Vector3 scale)
		{
			//Can't call methods in struct ctor.
			bool isValid = scale.x != 0 && scale.y != 0 && scale.z != 0;

			if(!isValid)
				throw new ArgumentException($"Provided vector {nameof(scale)} must not have any 0 components.");

			Scale = scale;
		}

		/// <summary>
		/// Verifies that the scale isn't 0 for any component.
		/// </summary>
		/// <param name="scale">The scale to check.</param>
		/// <returns>True if the scale is valid.</returns>
		private bool VerifyScale(Vector3 scale)
		{
			return scale.x != 0 && scale.y != 0 && scale.z != 0;
		}

		/// <inheritdoc />
		public float ScaleX(float x)
		{
			return Scale.x * x;
		}

		/// <inheritdoc />
		public float ScaleY(float y)
		{
			return Scale.y * y;
		}

		/// <inheritdoc />
		public float ScaleZ(float z)
		{
			return Scale.z * z;
		}

		/// <inheritdoc />
		public float UnScaleX(float x)
		{
			return x / Scale.x;
		}

		/// <inheritdoc />
		public float UnScaleY(float y)
		{
			return y / Scale.y;
		}

		/// <inheritdoc />
		public float UnScaleZ(float z)
		{
			return z / Scale.z;
		}
	}
}
