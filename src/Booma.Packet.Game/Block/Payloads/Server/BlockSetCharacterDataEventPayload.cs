﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.SERVER_CHARACTER_DATA_TYPE)]
	public sealed partial class BlockSetCharacterDataEventPayload : PSOBBGamePacketPayloadServer
	{
		//TODO: This is a lot of stuff in here. But can't RE it right now. Limited time.
		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BlockSetCharacterDataEventPayload()
			: base(GameNetworkOperationCode.SERVER_CHARACTER_DATA_TYPE)
		{
			
		}
	}
}
