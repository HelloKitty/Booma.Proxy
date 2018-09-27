using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect;

namespace Booma.Proxy
{
	/// <summary>
	/// This is a really hacky global entry for various services that
	/// are too difficult to manage depdendency injection for.
	/// </summary>
	public static class GlobalGameServices
	{
		/// <summary>
		/// This is a hack, do not reference this unless you're manually doing scene injection.
		/// </summary>
		public static IManualInjectionStrategy InjectionService { get; set; }
	}
}
