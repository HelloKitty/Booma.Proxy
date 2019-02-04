using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GladNet;
using SceneJect.Common;

namespace Booma.Proxy
{
	/// <summary>
	/// Dependency registeration component that should be used created at runtime
	/// when we need to export the network client and switch scenes.
	/// </summary>
	[Injectee]
	public sealed class ExportedClientDependencyRegisterModule : NonBehaviourDependency
	{
		/// <summary>
		/// The exported managed client that will be used in the next scene.
		/// </summary>
		[Inject]
		private IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> ManagedClient { get; }

		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			if(ManagedClient == null)
				throw new InvalidOperationException($"The {nameof(ManagedClient)} was null in the exported client dependency register.");

			//We just need to register this and then others can check if it has been registered and avoid registering a new client.
			register.RegisterInstance(ManagedClient)
				.As<IManagedNetworkClient<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>>()
				.As<IPeerPayloadSendService<PSOBBGamePacketPayloadClient>>()
				.As<IPayloadInterceptable>();

			register.RegisterType<DefaultMessageContextFactory>()
				.As<IPeerMessageContextFactory>()
				.SingleInstance();

			register.RegisterType<PayloadInterceptMessageSendService<PSOBBGamePacketPayloadClient>>()
				.As<IPeerRequestSendService<PSOBBGamePacketPayloadClient>>()
				.SingleInstance();

			//TODO: This is all so hacky, is this really what we're going to do forever??
			register.RegisterType<GlobalExportableClient>()
				.As<INetworkClientExportable>()
				.SingleInstance();

			register.RegisterType<DefaultConnectionRedirector>()
				.As<IConnectionRedirector>();

			register.RegisterType<GlobalConnectionService>()
				.As<IConnectionService>()
				.SingleInstance();

			//TODO: This is a hack to help prevent mobile network issues
			this.gameObject.AddComponent<DisconnectClientOnNonExportableSceneChange>();

			//Once we're registered we can destroy
			Destroy(this.gameObject);
		}
	}
}
