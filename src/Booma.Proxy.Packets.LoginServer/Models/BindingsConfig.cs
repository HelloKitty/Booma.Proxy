using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Configuration options for bindings.
	/// </summary>
	[WireDataContract]
	public sealed class BindingsConfig
	{
		//TODO: What is this?
		/// <summary>
		/// Configuration for keybindings.
		/// </summary>
		[KnownSize(364)]
		[WireMember(1)]
		public byte[] KeyConfiguration { get; }

		//TODO: What is this?
		/// <summary>
		/// Configuration for joystick options.
		/// </summary>
		[KnownSize(56)]
		[WireMember(2)]
		public byte[] JoystickConfiguration { get; }

		private BindingsConfig()
		{
			
		}
	}
}
