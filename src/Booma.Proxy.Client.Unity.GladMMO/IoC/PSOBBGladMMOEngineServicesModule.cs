using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Glader.Essentials;

namespace Booma.Proxy
{
	public sealed class PSOBBGladMMOEngineServicesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule(new EngineInterfaceRegisterationModule((int) (GladMMO.GameSceneType.InstanceServerScene), this.GetType().Assembly));
		}
	}
}
