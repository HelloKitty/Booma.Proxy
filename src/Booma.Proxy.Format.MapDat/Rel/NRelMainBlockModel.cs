using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	[WireDataContract]
	public class NRelMainBlockModel
	{
		[WireMember(1)]
		private long unk1 { get; }

		/// <summary>
		/// Number of sections in the NRel file.
		/// </summary>
		[WireMember(2)]
		public uint SectionCount { get; }

		[WireMember(3)]
		private int unk2 { get; }

		/// <summary>
		/// Pointer/offset to the section chunk in the stream/file.
		/// </summary>
		[WireMember(4)]
		public uint SectionPointer { get; }

		/// <summary>
		/// Pointer/offset to the section chunk in the stream/file.
		/// </summary>
		[WireMember(5)]
		public uint TextureNamePointer { get; }
	}
}
