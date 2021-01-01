using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Payload sent to request the data parameters.
	/// Such as: PMT, battle parameters, etc
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.BB_PARAM_HEADER_REQ_TYPE)]
	public sealed partial class CharacterDataParametersHeaderRequestPayload : PSOBBGamePacketPayloadClient
	{
		//Just a command payload. Nothing to implement
		public CharacterDataParametersHeaderRequestPayload()
			: base(GameNetworkOperationCode.BB_PARAM_HEADER_REQ_TYPE)
		{
			
		}
	}
}
