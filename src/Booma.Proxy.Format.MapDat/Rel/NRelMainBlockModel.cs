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
		internal long unk1 { get; set; }

		/// <summary>
		/// Number of sections in the NRel file.
		/// </summary>
		[WireMember(2)]
		public uint SectionCount { get; internal set; }

		[WireMember(3)]
		internal int unk2 { get; set; }

		/// <summary>
		/// Pointer/offset to the section chunk in the stream/file.
		/// </summary>
		[WireMember(4)]
		public uint SectionPointer { get; internal set; }

		/// <summary>
		/// Pointer/offset to the section chunk in the stream/file.
		/// </summary>
		[WireMember(5)]
		public uint TextureNamePointer { get; internal set; }
	}
}
