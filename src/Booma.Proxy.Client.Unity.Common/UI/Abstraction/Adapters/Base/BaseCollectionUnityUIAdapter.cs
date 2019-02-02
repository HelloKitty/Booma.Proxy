using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Provider the ability for ordered collections of element Type <typeparamref name="TAdaptedToType"/>
	/// to be adapted and registered together.
	/// </summary>
	public sealed class BaseCollectionUnityUIAdapter<TAdaptedToType> : BaseUnityUI<IReadOnlyCollection<TAdaptedToType>>
	{
		
	}
}
