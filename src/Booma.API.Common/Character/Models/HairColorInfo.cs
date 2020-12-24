using System;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// The hair color.
	/// </summary>
	[WireDataContract]
	public sealed class HairColorInfo
	{
		/// <summary>
		/// RGB - (Red, Green, Blue) vector color channels (2 byte for some reason).
		/// </summary>
		[WireMember(1)]
		public Vector3<ushort> Color { get; internal set; }

		public HairColorInfo(Vector3<ushort> color)
		{
			Color = color ?? throw new ArgumentNullException(nameof(color));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public HairColorInfo()
		{
			
		}
	}
}
