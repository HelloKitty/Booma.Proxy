using System;
using System.Collections.Generic;
using System.Text;

namespace Guardians
{
	/// <summary>
	/// Contract for types that provide destruction logic for specific
	/// types of <typeparamref name="TDestructionContext"/> which are
	/// contextual objects that contain information required to deconstruct a specific object.
	/// </summary>
	/// <typeparam name="TDestructionContext">The contextual object for destruction.</typeparam>
	public interface IObjectDestructorable<in TDestructionContext>
	{
		/// <summary>
		/// Attempts to destroy the provided <see cref="obj"/>.
		/// </summary>
		/// <param name="obj">The object to destroy.</param>
		/// <returns>True if the object could be successfully deconstructed.</returns>
		bool Destroy(TDestructionContext obj);
	}
}
