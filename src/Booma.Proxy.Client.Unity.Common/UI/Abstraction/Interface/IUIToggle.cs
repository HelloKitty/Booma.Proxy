using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Guardians
{
	public interface IUIToggle
	{
		/// <summary>
		/// Registers a callback for the toggle's value change.
		/// </summary>
		/// <param name="action">The callback to register.</param>
		void AddOnToggleChangedListener(Action<bool> action);

		/// <summary>
		/// Registers an async callback for the toggle's value change.
		/// </summary>
		/// <param name="action">The asynccallback to register.</param>
		void AddOnToggleChangedListenerAsync(Func<bool, Task> action);

		//TODO: Extract into interface
		/// <summary>
		/// Indicates if the toggle is interactable.
		/// </summary>
		bool IsInteractable { get; set; }
	}
}
