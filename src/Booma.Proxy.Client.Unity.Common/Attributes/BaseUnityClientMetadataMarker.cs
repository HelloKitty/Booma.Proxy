using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace Booma
{
	/// <summary>
	/// Base type for client assembly metadata markers.
	/// </summary>
	public abstract class BaseUnityClientMetadataMarker : IMessageHandlerTypeContainable, IAssemblyTypeContainable
	{
		/// <inheritdoc />
		public virtual IEnumerable<Type> AssemblyDefinedHandlerTyped => GetType()
			.Assembly
			.GetTypes()
			.Where(t => t.GetInterfaces().Any(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IPeerPayloadSpecificMessageHandler<,>)) && !t.IsAbstract)
			.ToArray();

		/// <inheritdoc />
		public IEnumerable<Type> AllAssemblyTypes => GetType()
			.Assembly
			.GetTypes();
	}
}
