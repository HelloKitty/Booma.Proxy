using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contracts for types that require initialization when
	/// the game starts.
	/// </summary>
	public interface IGameInitializable
	{
		/// <summary>
		/// Async OnGameInitialized function
		/// called when the game initializes.
		/// </summary>
		/// <returns></returns>
		Task OnGameInitialized();
	}
}
