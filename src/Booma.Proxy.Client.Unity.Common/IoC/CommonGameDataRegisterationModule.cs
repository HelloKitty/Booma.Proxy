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
		}

		//TODO: Refactor into module.
		private static void RegisterEntityMappableCollections(ContainerBuilder builder)
		{
			//throw new NotImplementedException("Fix the movement collection and other faults with IEntityRemovalable crap.");
			//The below is kinda a hack to register the non-generic types in the
			//removabale collection
			List<IEntityCollectionRemovable> removableComponentsList = new List<IEntityCollectionRemovable>(10);

			builder.RegisterGeneric(typeof(EntityGuidDictionary<>))
				.AsSelf()
				.As(typeof(IReadonlyEntityGuidMappable<>))
				.As(typeof(IEntityGuidMappable<>))
				.OnActivated(args =>
				{
					if(args.Instance is IEntityCollectionRemovable removable)
						removableComponentsList.Add(removable);
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
