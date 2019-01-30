using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Features.AttributeFilters;
using Booma.Proxy;
using Fasterflect;
using GladNet;
using SceneJect.Common;
using UnityEngine;
using Module = Autofac.Module;

namespace Booma.Proxy
{
	public abstract class AutoCreatedRegisteration<TInterfaceType> : NonBehaviourDependency
	{
		//This is kinda hacky, but we need a cached efficient collection of all IGameInitializable types
		private static IReadOnlyCollection<Type> CachedCreationType { get; } = 
			new ClientUnitySharedHandlersMetadataMarker().AllAssemblyTypes
				.Concat(new ClientUnityAuthenticationMetadataMarker().AllAssemblyTypes)
				.Concat(new ClientUnityCharacterMetadataMarker().AllAssemblyTypes)
				.Concat(new ClientUnityShipMetadataMarker().AllAssemblyTypes)
				.Where(t => t.Implements(typeof(TInterfaceType)))
				.ToArray();

		//TODO: When we have specific floors or special scenes that don't fit type we may want to supply zone id or additional metadata.
		/// <summary>
		/// The scene to load initializables for.
		/// </summary>
		[SerializeField]
		private GameSceneType SceneType;

		void Awake()
		{
			if(!Enum.IsDefined(typeof(GameSceneType), SceneType))
				throw new InvalidOperationException($"Invalid {nameof(GameSceneType)}: {(byte)SceneType}");
		}

		/// <inheritdoc />
		public override void Register(ContainerBuilder builder)
		{
			foreach(var creatable in CachedCreationType
				.Where(t => t.Implements(typeof(TInterfaceType)))
				.Where(t => t.Attributes<SceneTypeCreateAttribute>().Any(a => a.SceneType == SceneType)))
			{
				builder.RegisterType(creatable)
					.As<TInterfaceType>()
					.AsSelf()
					.SingleInstance()
					//TODO: We don't want to have to manually deal with this, we should create Attribute/Metadata to determine if this should be enabled.
					.WithAttributeFiltering();
			}
		}
	}
}
