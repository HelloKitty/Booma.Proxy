using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GladNet;
using Reflect.Extent;
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
				register.RegisterType(handlerType)
					.AsSelf()
					.SingleInstance();

				//This gets the payload type this handles
				Type concretePayloadType = handlerType.GetTypeInfo()
					.ImplementedInterfaces
					.First(i => i.GetTypeInfo().IsGenericType && i.GetTypeInfo().GetGenericTypeDefinition() == typeof(IPeerPayloadSpecificMessageHandler<,>))
					.GetGenericArguments()
					.First();

				Type tryHandlerType = typeof(TrySemanticsBasedOnTypePeerMessageHandler<,,>)
					.MakeGenericType(typeof(PSOBBGamePacketPayloadServer), typeof(PSOBBGamePacketPayloadClient), concretePayloadType);

				register.Register(context =>
					{
						object handler = context.Resolve(handlerType);

						return Activator.CreateInstance(tryHandlerType, handler);
					})
					.AsImplementedInterfaces()
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

		public class AssemblyHandlerIterator<THandlerTypeProvider> : IEnumerable<Type>
			where THandlerTypeProvider : IMessageHandlerTypeContainable, new()
		{
			public GameSceneType GameSceneTypeSearchingFor { get; }

			/// <inheritdoc />
			public AssemblyHandlerIterator(GameSceneType gameSceneTypeSearchingFor)
			{
				if(!Enum.IsDefined(typeof(GameSceneType), gameSceneTypeSearchingFor)) throw new InvalidEnumArgumentException(nameof(gameSceneTypeSearchingFor), (int)gameSceneTypeSearchingFor, typeof(GameSceneType));

				GameSceneTypeSearchingFor = gameSceneTypeSearchingFor;
			}

			/// <inheritdoc />
			public IEnumerator<Type> GetEnumerator()
			{
				THandlerTypeProvider provider = new THandlerTypeProvider();

				//Now, we have to iterate the handler Types from the container
				foreach(Type handlerType in provider.AssemblyDefinedHandlerTyped)
				{
					if(!handlerType.HasAttribute<NetworkMessageHandlerAttribute>())
						throw new InvalidOperationException($"Found Handler: {handlerType.Name} with missing/no {nameof(NetworkMessageHandlerAttribute)}. All handlers must have.");

					bool isForSceneType = DetermineIfHandlerIsForSceneType(handlerType, GameSceneTypeSearchingFor);

					if(isForSceneType)
						yield return handlerType;
					else
						continue;
				}

				yield break;
			}

			/// <inheritdoc />
			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			private static bool DetermineIfHandlerIsForSceneType(Type handlerType, GameSceneType sceneType)
			{
				foreach(NetworkMessageHandlerAttribute attris in handlerType.GetCustomAttributes<NetworkMessageHandlerAttribute>())
				{
					if(attris.SceneType == sceneType)
						return true;
				}

				return false;
			}
		}
	}
}
