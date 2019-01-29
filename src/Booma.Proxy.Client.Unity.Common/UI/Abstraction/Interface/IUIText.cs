using System;
using System.Collections.Generic;
using System.Text;

namespace Guardians
{
	/// <summary>
	/// Contract for a text rendered in the UI.
	/// </summary>
	public interface IUIText
	{
		/// <summary>
		/// The text field of the UI text.
		/// </summary>
		string Text { get; set; }
	}
}
