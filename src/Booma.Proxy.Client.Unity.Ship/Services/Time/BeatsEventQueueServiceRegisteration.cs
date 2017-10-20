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
	/// Sceneject dependency module for registering the <see cref="IBeatsEventQueueDispatchable"/>
	/// and the <see cref="IBeatsEventQueueRegisterable"/>.
	/// </summary>
	public sealed class BeatsEventQueueServiceRegisteration : NonBehaviourDependency
	{
		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			//Register it for both the dispatcher and the registerable
			register.RegisterInstance(new BeatsBasedEventQueue(() => TimeService.CurrentBeatsTime))
				.As<IBeatsEventQueueRegisterable>()
				.As<IBeatsEventQueueDispatchable>();
		}
	}
}
