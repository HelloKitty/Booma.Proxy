using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Builder;
using Booma.Proxy;
using GladNet;
using System.Linq;

namespace FreecraftCore
{
	/// <summary>
	/// This module allows for the registeration of all server handlers in the assembly.
	/// </summary>
	public sealed class PsobbProxyTestClientMessageHandlerRegisterationModule : PayloadHandlerRegisterationModule<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer, IProxiedMessageContext<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>
	{
		//We need this to ignore the default handler
		/// <inheritdoc />
		protected override IEnumerable<Type> OnProcessHandlerTypes(IEnumerable<Type> handlerTypes)
		{
			return base.OnProcessHandlerTypes(handlerTypes)
				.Where(t => t != typeof(DefaultClientPayloadHandler));
		}
	}
}
