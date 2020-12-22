using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// The hair color.
	/// </summary>
	[WireDataContract]
	public sealed class HairColorInfo
	{
		//TODO: Why is the color 2 bytes?
		/// <summary>
		/// The red amount.
		/// </summary>
		[WireMember(1)]
		public ushort RedChannel { get; internal set; }

		/// <summary>
		/// The green amount.
		/// </summary>
		[WireMember(2)]
		public ushort GreenChannel { get; internal set; }
		
		/// <summary>
		/// The blue amount.
		/// </summary>
		[WireMember(3)]
		public ushort BlueChannel { get; internal set; }

		//Serializer ctor
		private HairColorInfo()
		{
			
		}
	}
}
