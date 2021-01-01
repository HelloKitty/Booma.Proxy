using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
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
		public byte[] KeyConfiguration { get; internal set; }

		//TODO: What is this?
		/// <summary>
		/// Configuration for joystick options.
		/// </summary>
		[KnownSize(56)]
		[WireMember(2)]
		public byte[] JoystickConfiguration { get; internal set; }

		public BindingsConfig([NotNull] byte[] keyConfiguration, [NotNull] byte[] joystickConfiguration)
			: this()
		{
			KeyConfiguration = keyConfiguration ?? throw new ArgumentNullException(nameof(keyConfiguration));
			JoystickConfiguration = joystickConfiguration ?? throw new ArgumentNullException(nameof(joystickConfiguration));
		}

		public static BindingsConfig CreateDefault()
		{
			//TODO: Serialize null array for fixed size!
			return new BindingsConfig(new byte[364], new byte[56]);
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BindingsConfig()
		{
			
		}
	}
}
