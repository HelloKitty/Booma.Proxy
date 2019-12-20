using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Booma.Proxy
{
	public sealed class PSOBBGladMMOCommonGameDependencyModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<DefaultPSOUnitScalerStrategy>()
				.As<IUnitScalerStrategy>()
				.SingleInstance();

			builder.RegisterType<DefaultCharacterSlotModel>()
				.AsImplementedInterfaces()
				.SingleInstance();
		}
	}
}
