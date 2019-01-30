using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Reflect.Extent;

namespace Booma.Proxy
{
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
				//We just skip now instead. For ease, maybe revert
				if(!handlerType.HasAttribute<NetworkMessageHandlerAttribute>())
					continue;
				//if(!handlerType.HasAttribute<NetworkMessageHandlerAttribute>())
				//	throw new InvalidOperationException($"Found Handler: {handlerType.Name} with missing/no {nameof(NetworkMessageHandlerAttribute)}. All handlers must have.");

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