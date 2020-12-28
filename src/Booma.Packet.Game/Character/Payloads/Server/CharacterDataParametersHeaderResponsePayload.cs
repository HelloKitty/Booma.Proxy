using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/* Blue Burst packet that's a header for the parameter files.
	typedef struct bb_param_hdr
	{
		bb_pkt_hdr_t hdr;
		struct {

			uint32_t size;
			uint32_t checksum;
			uint32_t offset;
			char filename[0x40];
		}
		entries[];
	}
	PACKED bb_param_hdr_pkt;*/

	/// <summary>
	/// Response payload for the <see cref="CharacterDataParametersHeaderRequestPayload"/>
	/// which contains the header and information about the parameters we should
	/// be interested in.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_PARAM_HEADER_TYPE)]
	public sealed partial class CharacterDataParametersHeaderResponsePayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// Indicates the parameter file count expected by PSOBB.
		/// </summary>
		public const int PARAMETER_FILE_COUNT = 9;

		//Sylverant shows that the parameter file count is sent in the flags.
		public override bool isFlagsSerialized { get; } = false;

		/// <summary>
		/// Serialized file headers.
		/// </summary>
		[SendSize(PrimitiveSizeType.Int32)] //Soly said there are only 9 files for PSOBB2
		[WireMember(1)]
		internal DataParameterFileHeader[] _Headers { get; set; }

		/// <summary>
		/// The file headers for the parameter files.
		/// </summary>
		public IEnumerable<DataParameterFileHeader> Headers => _Headers;

		public CharacterDataParametersHeaderResponsePayload([NotNull] DataParameterFileHeader[] headers) 
			: this()
		{
			_Headers = headers ?? throw new ArgumentNullException(nameof(headers));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterDataParametersHeaderResponsePayload()
			: base(GameNetworkOperationCode.BB_PARAM_HEADER_TYPE)
		{
			
		}
	}
}
