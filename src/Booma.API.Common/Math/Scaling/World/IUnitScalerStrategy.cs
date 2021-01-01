using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Strategy that can scale vector components
	/// to and from an expected unit scale.
	/// </summary>
	public interface IUnitScalerStrategy
	{
		/// <summary>
		/// Scales the provided <see cref="x"/> component
		/// by the x scale.
		/// </summary>
		/// <param name="x">The x value.</param>
		/// <returns>A scaled <see cref="x"/>.</returns>
		float ScaleX(float x);

		/// <summary>
		/// Scales the provided <see cref="y"/> component
		/// by the y scale.
		/// </summary>
		/// <param name="y">The x value.</param>
		/// <returns>A scaled <see cref="y"/>.</returns>
		float ScaleY(float y);

		/// <summary>
		/// Scales the provided <see cref="z"/> component
		/// by the y scale.
		/// </summary>
		/// <param name="z">The x value.</param>
		/// <returns>A scaled <see cref="z"/>.</returns>
		float ScaleZ(float z);

		/// <summary>
		/// Uncales the provided <see cref="x"/> component
		/// by the x scale. Reverses the <see cref="ScaleX"/>.
		/// </summary>
		/// <param name="x">The x value.</param>
		/// <returns>A scaled <see cref="x"/>.</returns>
		float UnScaleX(float x);

		/// <summary>
		/// Unscales the provided <see cref="y"/> component
		/// by the y scale. Reverses the <see cref="ScaleY"/>
		/// </summary>
		/// <param name="y">The x value.</param>
		/// <returns>A scaled <see cref="y"/>.</returns>
		float UnScaleY(float y);

		/// <summary>
		/// Unscales the provided <see cref="z"/> component
		/// by the y scale. Reverses the <see cref="ScaleZ"/>
		/// </summary>
		/// <param name="z">The x value.</param>
		/// <returns>A scaled <see cref="z"/>.</returns>
		float UnScaleZ(float z);
	}
}
