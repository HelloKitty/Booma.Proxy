using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/*struct {
		uint32_t size;
		uint32_t checksum;
		uint32_t offset;
		char filename[0x40];
	}*/

	//Based on: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/packets.h#L1405
	/// <summary>
	/// Data model for the parameter file header.
	/// </summary>
	[WireDataContract]
	public sealed class DataParameterFileHeader
	{
		/// <summary>
		/// The size of the file.
		/// </summary>
		[WireMember(1)]
		public uint FileSize { get; internal set; }

		/// <summary>
		/// The file checksum
		/// </summary>
		[WireMember(2)]
		public uint Checksum { get; internal set; }

		//TODO: What is this?
		/// <summary>
		/// The offset. (?)
		/// </summary>
		[WireMember(3)]
		public uint Offset { get; internal set; }
		
		/// <summary>
		/// Filename (Maximum of 64 characters.
		/// </summary>
		[KnownSize(64)] //0x40
		[WireMember(4)]
		public string FileName { get; internal set; }

		public DataParameterFileHeader(uint fileSize, uint checksum, uint offset, [NotNull] string fileName)
			: this()
		{
			FileSize = fileSize;
			Checksum = checksum;
			Offset = offset;
			FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DataParameterFileHeader()
		{
			
		}
	}
}
