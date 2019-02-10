using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for types that manage the bursting state.
	/// </summary>
	public interface IBurstingService : IReadonlyBurstingService
	{
		/// <summary>
		/// Clears the bursting state
		/// and related bursting data.
		/// </summary>
		void ClearBursting();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityGuid"></param>
		/// <exception cref="InvalidOperationException">Throws if <see cref="isBurstingInProgress"/> is true.</exception>
		/// <returns>True if the bursting state can be set.</returns>
		bool SetBurstingEntity(int entityGuid);
	}

	public interface IReadonlyBurstingService
	{
		/// <summary>
		/// Indicates if a client is currently bursting.
		/// </summary>
		bool isBurstingInProgress { get; }

		/// <summary>
		/// The entity GUID/ID of the bursting player.
		/// </summary>
		int? BurstingEntityGuid { get; }
	}
}
