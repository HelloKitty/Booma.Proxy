using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Contract for collections that can have entries
	/// removed via a <see cref="NetworkEntityGuid"/>.
	/// </summary>
	public interface IEntityCollectionRemovable
	{
		/// <summary>
		/// Attempts to remove the collection based
		/// on the provided <see cref="entityGuid"/> key.
		/// </summary>
		/// <param name="entityGuid">The key.</param>
		/// <returns>True if removed.</returns>
		bool RemoveEntityEntry(int entityGuid);
	}
}
