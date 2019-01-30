using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SceneJect.Common;

namespace Booma.Proxy
{
	public sealed class CharacterSelectionScreenDependencyRegister : NonBehaviourDependency
	{
		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			//This is for anything that needs to touch the character UI.
			register.RegisterType<CharacterTabUIElementsContext>()
				.AsSelf();
		}
	}
}
