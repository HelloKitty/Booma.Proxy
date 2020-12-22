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
	public sealed class CharacterDataParametersHeaderResponsePayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// Serialized file headers.
		/// </summary>
		[KnownSize(9)] //Soly said there are only 9 files
		[WireMember(1)]
		internal DataParameterFileHeader[] _Headers { get; set; }

		/// <summary>
		/// The file headers for the parameter files.
		/// </summary>
		public IEnumerable<DataParameterFileHeader> Headers => _Headers; 

		//Serializer ctor
		private CharacterDataParametersHeaderResponsePayload()
		{
			
		}
	}
}
