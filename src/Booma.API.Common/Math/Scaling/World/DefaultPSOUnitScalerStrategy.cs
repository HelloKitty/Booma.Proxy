using System;
using System.Collections.Generic;
using System.Text;

namespace Booma.Proxy
{
	public class DefaultPSOUnitScalerStrategy : IUnitScalerStrategy
	{
		/// <summary>
		/// The unit scale to scale to and from.
		/// </summary>
		private Vector3<float> Scale { get; set; }

		private DefaultPSOUnitScalerStrategy(Vector3<float> scale)
		{
			//Can't call methods in struct ctor.
			bool isValid = scale.X != 0 && scale.Y != 0 && scale.Z != 0;

			if(!isValid)
				throw new ArgumentException($"Provided vector {nameof(scale)} must not have any 0 components.");

			Scale = scale;
		}

		public DefaultPSOUnitScalerStrategy()
			: this(new Vector3<float>(0.2f, 0.2f, -0.2f))
		{
			
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
