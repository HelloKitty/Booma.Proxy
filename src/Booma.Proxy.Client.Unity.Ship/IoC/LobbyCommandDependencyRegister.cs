using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SceneJect.Common;

namespace Booma.Proxy
{
	/// <summary>
	/// Dependency registeration component that will handle the registeration of
	/// various context and context factoey related dependencies.
	/// </summary>
	public sealed class LobbyCommandDependencyRegister : NonBehaviourDependency
	{
		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			//We need a player collection in the scene for commands
			register.RegisterType<DefaultNetworkPlayerCollection>()
				.As<INetworkPlayerCollection>()
				.As<INetworkPlayerRegistery>()
				.SingleInstance();

			//Register the player context factory for both types
			register.RegisterType<NetworkPlayerCommandContextFactory>()
				.As<ICommandMessageContextFactory<ICommandClientIdentifiable, INetworkPlayerFullCommandMessageContext>>()
				.As<ICommandMessageContextFactory<ICommandClientIdentifiable, INetworkPlayerCommandMessageContext>>()
				.SingleInstance();
		}
	}
}
