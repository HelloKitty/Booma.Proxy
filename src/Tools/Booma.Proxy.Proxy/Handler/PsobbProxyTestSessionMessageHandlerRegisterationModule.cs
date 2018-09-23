using System;
using System.Collections.Generic;
using System.Linq;
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
		//We need this to ignore the default handler
		/// <inheritdoc />
		protected override IEnumerable<Type> OnProcessHandlerTypes(IEnumerable<Type> handlerTypes)
		{
			return base.OnProcessHandlerTypes(handlerTypes)
				.Where(t => t != typeof(DefaultServerPayloadHandler));
		}
	}
}
