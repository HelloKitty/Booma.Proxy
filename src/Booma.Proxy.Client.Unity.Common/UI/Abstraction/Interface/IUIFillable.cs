using System;
using System.Collections.Generic;
using System.Text;

namespace Guardians
{
	/// <summary>
	/// Contract for a fillable UI component.
	/// </summary>
	public interface IUIFillable
	{
		/// <summary>
		/// The fill amount of the fillable.
		/// Should be bounded between 0 and 1.
		/// Implementers should clamp.
		/// </summary>
		float FillAmount { get; set; }
	}
}
