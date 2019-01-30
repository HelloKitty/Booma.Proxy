using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class AdditionalRegisterationAsAttribute : Attribute
	{
		/// <summary>
		/// The service type to register the marked type as.
		/// </summary>
		public Type ServiceType { get; }

		/// <inheritdoc />
		public AdditionalRegisterationAsAttribute([NotNull] Type serviceType)
		{
			//TODO: We can do some validated on if it's an invalid type or something that cannot be registered.

			ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
		}
	}
}
