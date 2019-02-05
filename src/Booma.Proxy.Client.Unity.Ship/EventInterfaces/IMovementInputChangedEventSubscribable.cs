using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IMovementInputChangedEventSubscribable
	{
		event EventHandler<MovementInputChangedEventArgs> OnMovementInputDataChanged;
	}

	public sealed class MovementInputChangedEventArgs : EventArgs
	{
		//The reason we use float is because of controllers
		//They may lightly press forward to WALK.
		//PC players can't really benefit though.
		public float NewVerticalInput { get; }

		public float NewHorizontalInput { get; }

		/// <inheritdoc />
		public MovementInputChangedEventArgs(float newVerticalInput, float newHorizontalInput)
		{
			NewVerticalInput = newVerticalInput;
			NewHorizontalInput = newHorizontalInput;
		}
	}
}
