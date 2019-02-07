using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public interface IMovementGeneratorState : IMovementGenerator<GameObject>
	{
		/// <summary>
		/// Indicates if the movement generation is finished.
		/// </summary>
		bool isFinished { get; }

		/// <summary>
		/// Indicates if the state is skippable.
		/// AKA think Teleportation (unskippable) vs SlowMovement (skippable).
		/// Some movement generators have CRITICAL importance like that.
		/// </summary>
		bool isSkippable { get; }
	}
}
