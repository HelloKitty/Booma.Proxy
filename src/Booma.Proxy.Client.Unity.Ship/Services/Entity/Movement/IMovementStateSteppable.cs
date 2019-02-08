using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IMovementStateSteppable
	{
		/// <summary>
		/// The current step of the lerp.
		/// Progress = CurrentStep / LerpDuration.
		/// </summary>
		float CurrentStep { get; set; } //public mutable.
	}
}
