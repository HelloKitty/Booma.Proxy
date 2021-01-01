﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	[WireDataContract]
	[PatchServerPacketPayload(PatchNetworkOperationCode.PATCH_INFO_FINISHED)]
	public sealed partial class PatchingInfoRequestDonePayload : PSOBBPatchPacketPayloadServer
	{
		public PatchingInfoRequestDonePayload()
			: base(PatchNetworkOperationCode.PATCH_INFO_FINISHED)
		{

		}
	}
}
