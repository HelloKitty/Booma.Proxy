using System;
using System.Collections.Generic;
using System.Text;

namespace Booma.Proxy
{
	//Right now things are build the scale of 0.2f, 0.2f, -0.2f
	//PSOBB appears to have an inverted z compared to PSOBB
	//TODO: During refactoring I may have this backwards, it may be Unity3D to PSO scaling. Can't remember.
	public class PsoToUnity3DUnitScalerStrategy : IUnitScalerStrategy
	{
		/// <summary>
		/// The unit scale to scale to and from.
		/// </summary>
		private Vector3<float> Scale { get; set; }

		public PsoToUnity3DUnitScalerStrategy()
		{
			Scale = new Vector3<float>(0.2f, 0.2f, -0.2f);
			if (!VerifyScale(Scale))
				throw new InvalidOperationException($"Scale invalid.");
		}

		/// <summary>
		/// Verifies that the scale isn't 0 for any component.
		/// </summary>
		/// <param name="scale">The scale to check.</param>
		/// <returns>True if the scale is valid.</returns>
		private bool VerifyScale(Vector3<float> scale)
		{
			return scale.X != 0 && scale.Y != 0 && scale.Z != 0;
		}

		/// <inheritdoc />
		public float ScaleX(float x)
		{
			return Scale.X * x;
		}

		/// <inheritdoc />
		public float ScaleY(float y)
		{
			return Scale.Y * y;
		}

		/// <inheritdoc />
		public float ScaleZ(float z)
		{
			return Scale.Z * z;
		}

		/// <inheritdoc />
		public float UnScaleX(float x)
		{
			return x / Scale.X;
		}

		/// <inheritdoc />
		public float UnScaleY(float y)
		{
			return y / Scale.Y;
		}

		/// <inheritdoc />
		public float UnScaleZ(float z)
		{
			return z / Scale.Z;
		}
	}
}
