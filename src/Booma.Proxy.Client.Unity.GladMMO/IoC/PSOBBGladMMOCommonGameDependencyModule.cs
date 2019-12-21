using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;
using Glader.Essentials;
using GladMMO;

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

			//TransientInteropEntityMappable : IInteropEntityMappable
			builder.RegisterType<TransientInteropEntityMappable>()
				.As<IInteropEntityMappable>()
				.SingleInstance();

			RegisterComponentSystem(builder);
		}

		private void RegisterComponentSystem(ContainerBuilder builder)
		{
			//HelloKitty: We actually have to do this manually, and not use GladerEssentials because we use simplified interfaces.
			//The below is kinda a hack to register the non-generic types in the
			//removabale collection
			List<IEntityCollectionRemovable<int>> removableComponentsList = new List<IEntityCollectionRemovable<int>>(10);

			builder.RegisterGeneric(typeof(EntityGuidDictionary<,>))
				.AsSelf()
				.As(typeof(IReadonlyEntityGuidMappable<,>))
				.As(typeof(IEntityGuidMappable<,>))
				.OnActivated(args =>
				{
					if(args.Instance is IEntityCollectionRemovable<int> removable)
						removableComponentsList.Add(removable);
				})
				.SingleInstance();

			//This will allow everyone to register the removable collection collection.
			builder.RegisterInstance(removableComponentsList)
				.As<IReadOnlyCollection<IEntityCollectionRemovable<int>>>()
				.AsSelf()
				.SingleInstance();
		}
	}
}
