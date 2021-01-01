using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Just a simple referenceable Type for reflection purposes.
	/// </summary>
	public static class PacketCommonServerMetadataMarker
	{
		public static IEnumerable<Type> SerializableTypes { get; } = typeof(PacketCommonServerMetadataMarker)
			.Assembly
			.GetTypes()
			.Where(t => t.GetCustomAttribute(typeof(WireDataContractAttribute)) != null)
			.ToList();
	}
}
