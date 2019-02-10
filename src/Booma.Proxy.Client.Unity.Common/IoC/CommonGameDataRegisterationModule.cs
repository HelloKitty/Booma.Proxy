using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SceneJect.Common;

namespace Booma.Proxy
{
	public sealed class CommonGameDataRegisterationModule : NonBehaviourDependency
	{
		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			RegisterEntityMappableCollections(register);

			register.RegisterType<WorldObjectToEntityDictionary>()
				.As<IWorldObjectToEntityMappable>()
				.As<IReadonlyWorldObjectToEntityMappable>()
				.SingleInstance();
		}

		//TODO: Refactor into module.
		private static void RegisterEntityMappableCollections(ContainerBuilder builder)
		{
			//throw new NotImplementedException("Fix the movement collection and other faults with IEntityRemovalable crap.");
			//The below is kinda a hack to register the non-generic types in the
			//removabale collection
			List<IEntityCollectionRemovable> removableComponentsList = new List<IEntityCollectionRemovable>(10);

			DefaultTransientEntityDataCollection transientEntityCollection = new DefaultTransientEntityDataCollection();

			DefaultNonTransientEntityDataCollection nonTransientEntityCollection = new DefaultNonTransientEntityDataCollection();

			builder.RegisterInstance(transientEntityCollection)
				.As<ITransientEntityDataRemovableEnumerable>()
				.SingleInstance();

			builder.RegisterInstance(nonTransientEntityCollection)
				.As<INonTransientEntityDataRemovableEnumerable>()
				.SingleInstance();

			builder.RegisterGeneric(typeof(EntityGuidDictionary<>))
				.AsSelf()
				.As(typeof(IReadonlyEntityGuidMappable<>))
				.As(typeof(IEntityGuidMappable<>))
				.OnActivated(args =>
				{
					if(args.Instance is IEntityCollectionRemovable removable)
					{
						removableComponentsList.Add(removable);

						//TODO: We can make this more efficient, but it only runs on start
						//They should always have a generic type arg
						if(args.Instance.GetType().GetInterfaces().First(t => t.IsGenericType && ((t.GetGenericTypeDefinition() == typeof(IEntityGuidMappable<>))))
							.GetGenericArguments().First().GetCustomAttributes(typeof(NonTransientEntityDataAttribute), false).Any())
						{
							nonTransientEntityCollection.Add(removable);
						}
						else
						{
							transientEntityCollection.Add(removable);
						}
					}
				})
				.SingleInstance();

			//This will allow everyone to register the removable collection collection.
			builder.RegisterInstance(removableComponentsList)
				.AsImplementedInterfaces()
				.AsSelf()
				.SingleInstance();
		}
	}
}
