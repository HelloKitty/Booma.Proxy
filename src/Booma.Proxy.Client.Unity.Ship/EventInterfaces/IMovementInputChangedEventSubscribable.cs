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

	public sealed class MovementInputChangedEventArgs : EventArgs, IEquatable<MovementInputChangedEventArgs>
	{
		//The reason we use float is because of controllers
		//They may lightly press forward to WALK.
		//PC players can't really benefit though.
		public float NewVerticalInput { get; }

		public float NewHorizontalInput { get; }

		/// <summary>
		/// Indicates if currently moving.
		/// </summary>
		public bool isMoving => (Math.Abs(NewVerticalInput) > 0.005f) || (Math.Abs(NewHorizontalInput) > 0.005f);

		/// <inheritdoc />
		public MovementInputChangedEventArgs(float newVerticalInput, float newHorizontalInput)
		{
			NewVerticalInput = newVerticalInput;
			NewHorizontalInput = newHorizontalInput;
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if(obj == null)
				return false;

			if(obj is MovementInputChangedEventArgs second)
				return (Math.Abs(second.NewHorizontalInput - NewHorizontalInput) < 0.005f) && (Math.Abs(second.NewVerticalInput - NewVerticalInput) < 0.005f);

			return false;
		}

		public bool Equals([NotNull] MovementInputChangedEventArgs other)
		{
			if(other == null)
				return false;

			return NewVerticalInput.Equals(other.NewVerticalInput) && NewHorizontalInput.Equals(other.NewHorizontalInput);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				return (NewVerticalInput.GetHashCode() * 397) ^ NewHorizontalInput.GetHashCode();
			}
		}
	}
}
