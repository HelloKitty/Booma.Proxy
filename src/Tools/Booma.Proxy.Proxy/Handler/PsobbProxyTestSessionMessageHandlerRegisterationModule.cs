using System;
using System.Collections.Generic;
using System.Text;
using Booma.Proxy;
using GladNet;

namespace FreecraftCore
{
	/// <summary>
	/// This module allows for the registeration of all server handlers in the assembly.
	/// </summary>
	public sealed class PsobbProxyTestSessionMessageHandlerRegisterationModule : PayloadHandlerRegisterationModule<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, IProxiedMessageContext<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>>
	{

	}
}
