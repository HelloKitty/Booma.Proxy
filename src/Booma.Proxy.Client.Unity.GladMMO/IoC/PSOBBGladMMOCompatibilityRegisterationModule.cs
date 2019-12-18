using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Glader.Essentials;
using GladMMO;
using GladNet;
using SceneJect.Common;

namespace Booma.Proxy
{
	//See GladMMO: GameServerNetworkClientAutofacModule for information on what we're Mocking here.
	public sealed class PSOBBGladMMOCompatibilityRegisterationModule : NonBehaviourDependency
	{
		/// <inheritdoc />
		public override void Register(ContainerBuilder builder)
		{
			//This is required to catch outgoing client packets
			//and convert them to PSOBB packets and send them
			builder.RegisterType<GladMMOManagedClientPSOBBInterceptor>()
				.As<IManagedNetworkClient<GameClientPacketPayload, GameServerPacketPayload>>()
				.As<IPayloadInterceptable>()
				.As<IPeerPayloadSendService<GameClientPacketPayload>>()
				.As<IConnectionService>()
				.SingleInstance();

			//We don't need this to change.
			builder.RegisterType<DefaultMessageContextFactory>()
				.As<IPeerMessageContextFactory>()
				.SingleInstance();

			//We don't use or need this, so ignore it for now and leave it
			//may need to mock it eventually.
			builder.RegisterType<PayloadInterceptMessageSendService<GameClientPacketPayload>>()
				.As<IPeerRequestSendService<GameClientPacketPayload>>()
				.SingleInstance();

			//Now, with the new design we also have to register the game client itself
			builder.RegisterType<GladMMO.GameNetworkClient>()
				.AsImplementedInterfaces()
				.SingleInstance();

			RegisterPSOBBHandlers(builder);
			RegisterGladMMOHandlers(builder);
		}

		private void RegisterPSOBBHandlers(ContainerBuilder builder)
		{
			//New IPeerContext generic param now so we register as implemented interface
			builder.RegisterType<DefaultPSOBBInteropPayloadHandler>()
				.As<IPeerPayloadSpecificMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, InteropPSOBBPeerMessageContext>>()
				.InstancePerLifetimeScope();

			builder.RegisterType<MessageHandlerService<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, InteropPSOBBPeerMessageContext>>()
				.As<MessageHandlerService<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, InteropPSOBBPeerMessageContext>>()
				.UsingConstructor(typeof(IEnumerable<IPeerMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, InteropPSOBBPeerMessageContext>>), typeof(IPeerPayloadSpecificMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, InteropPSOBBPeerMessageContext>))
				.InstancePerLifetimeScope();

			//TODO: We need to support scene types for PSOBB handlers probably.
			builder.RegisterModule(new BaseHandlerRegisterationModule<IPeerMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, InteropPSOBBPeerMessageContext>>((int)1, GetType().Assembly));
		}

		private void RegisterGladMMOHandlers(ContainerBuilder builder)
		{
			//New IPeerContext generic param now so we register as implemented interface
			builder.RegisterType<DefaultGladMMOInteropPayloadHandler>()
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();

			builder.RegisterType<MessageHandlerService<GameClientPacketPayload, PSOBBGamePacketPayloadClient>>()
				.As<MessageHandlerService<GameClientPacketPayload, PSOBBGamePacketPayloadClient>>()
				.UsingConstructor(typeof(IEnumerable<IPeerMessageHandler<GameClientPacketPayload, PSOBBGamePacketPayloadClient>>), typeof(IPeerPayloadSpecificMessageHandler<GameClientPacketPayload, PSOBBGamePacketPayloadClient>))
				.InstancePerLifetimeScope();

			//TODO: We need to support scene types for GladMMO to PSOBB packet handlers.
			builder.RegisterModule(new BaseHandlerRegisterationModule<IPeerMessageHandler<GameClientPacketPayload, PSOBBGamePacketPayloadClient>>((int)1, GetType().Assembly));
		}
	}
}
