using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class GameClientMessageHandlerRegisterModule : NonBehaviourDependency
	{
		/// <summary>
		/// Indicates the scene type for this handler registeration module.
		/// This will make it so only handlers for the specified scene are registered.
		/// </summary>
		[Tooltip("Specifiy which type of handlers should be registered.")]
		[SerializeField]
		private GameSceneType HandlersType;

		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			if(!Enum.IsDefined(typeof(GameSceneType), HandlersType))
				throw new InvalidOperationException($"Cannot use {nameof(GameSceneType)} with Value: {(int)HandlersType} as handler scene type. Invalid.");

			//This will load and register EACH payload specific handler as itself.
			foreach(Type handlerType in IterateAllAssembliesWithHandlers(HandlersType))
			{
				//This is quite simple, in this project handlers are quite controlling.
				//They can directly define their OWN CanHandle or TypeHandle methods/logic
				//They aren't just purely payload Type handlers
				//so we don't need to make TryHandle semantics around them or anything.
				register.RegisterType(handlerType)
					.As<IPeerMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>()
					.SingleInstance();
			}

			//New IPeerContext generic param now so we register as implemented interface
			register.RegisterType<DefaultPayloadHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>()
				.AsImplementedInterfaces()
				.SingleInstance();

			register.RegisterType<MessageHandlerService<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>()
				.As<MessageHandlerService<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>();
		}

		private static IEnumerable<Type> IterateAllAssembliesWithHandlers(GameSceneType sceneType)
		{
			return new AssemblyHandlerIterator<ClientUnitySharedHandlersMetadataMarker>(sceneType)
				.Concat(new AssemblyHandlerIterator<ClientUnityAuthenticationMetadataMarker>(sceneType))
				.Concat(new AssemblyHandlerIterator<ClientUnityCharacterMetadataMarker>(sceneType))
				.Concat(new AssemblyHandlerIterator<ClientUnityShipMetadataMarker>(sceneType))
				.ToArray();
		}
	}
}
