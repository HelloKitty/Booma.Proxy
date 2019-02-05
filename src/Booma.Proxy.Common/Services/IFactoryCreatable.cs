using System;
using System.Collections.Generic;
using System.Text;

namespace Guardians
{
	//TODO: Move this into Glader.Common someday
	/// <summary>
	/// Contract for a generic factory.
	/// </summary>
	/// <typeparam name="TCreateType">The creation type.</typeparam>
	/// <typeparam name="TContextType">The context type containing construction data.</typeparam>
	public interface IFactoryCreatable<out TCreateType, in TContextType>
	{
		/// <summary>
		/// Creates a new instance of the <typeparamref name="TCreateType"/>
		/// based on the provided <see cref="context"/>.
		/// </summary>
		/// <param name="context">The context to construct the instance from.</param>
		/// <returns>A new instance.</returns>
		TCreateType Create(TContextType context);
	}
}
