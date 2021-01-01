using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
{
	/// <summary>
	/// Empty payload the client sends to get character specific options data
	/// that are saved on the server.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.BB_OPTION_REQUEST_TYPE)]
	public sealed partial class CharacterOptionsRequestPayload : PSOBBGamePacketPayloadClient
	{
		//Empty, just a payload that alerts the server to the desire of options
		public CharacterOptionsRequestPayload()
			: base(GameNetworkOperationCode.BB_OPTION_REQUEST_TYPE)
		{
			
		}
	}
}
