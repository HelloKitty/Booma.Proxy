using System;
using System.Collections.Generic;
using System.Text;
using GladNet;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public sealed class PsobbHandlerRegisterationModule : PayloadHandlerRegisterationModules<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>
	{
		/// <inheritdoc />
		public PsobbHandlerRegisterationModule()
			: base(new List<PayloadHandlerRegisterationModule<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer, IProxiedMessageContext<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>>(), new List<PayloadHandlerRegisterationModule<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, IProxiedMessageContext<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>>>())
		{

		}

		public void AddServerHandlerModule([NotNull] PayloadHandlerRegisterationModule<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, IProxiedMessageContext<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>> handlerModule)
		{
			if(handlerModule == null) throw new ArgumentNullException(nameof(handlerModule));

			var list = this.ServerMessageHandlerModules as List<PayloadHandlerRegisterationModule<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, IProxiedMessageContext<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>>>;

			list.Add(handlerModule);
		}

		public void AddClientHanderModule([NotNull] PayloadHandlerRegisterationModule<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer, IProxiedMessageContext<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>> handlerModule)
		{
			if(handlerModule == null) throw new ArgumentNullException(nameof(handlerModule));

			var list = this.ClientMessageHandlerModules as List<PayloadHandlerRegisterationModule<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer, IProxiedMessageContext<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>>;

			list.Add(handlerModule);
		}
	}
}
