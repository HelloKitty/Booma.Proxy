using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Guardians
{
	/// <summary>
	/// Contract for UI buttons.
	/// </summary>
	public interface IUIButton
	{
		/// <summary>
		/// Registers a callback for the button OnClick.
		/// </summary>
		/// <param name="action">The callback to register.</param>
		void AddOnClickListener(Action action);

		/// <summary>
		/// Registers an async callback for the button OnClick.
		/// </summary>
		/// <param name="action">The asynccallback to register.</param>
		void AddOnClickListenerAsync(Func<Task> action);

		/// <summary>
		/// Indicates if the button is interactable.
		/// </summary>
		bool IsInteractable { get; set; }
	}
}
