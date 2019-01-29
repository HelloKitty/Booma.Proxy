using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Metadata attributed used to encode information
	/// about a network message handler.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class NetworkMessageHandlerAttribute : Attribute
	{
		/// <summary>
		/// Indicates the scene type this handler is for.
		/// </summary>
		public HandlerSceneType SceneType { get; }

		/// <inheritdoc />
		public NetworkMessageHandlerAttribute(HandlerSceneType sceneType)
		{
			if(!Enum.IsDefined(typeof(HandlerSceneType), sceneType)) throw new InvalidEnumArgumentException(nameof(sceneType), (int)sceneType, typeof(HandlerSceneType));
			SceneType = sceneType;
		}
	}

	/// <summary>
	/// Enumeration of scene types.
	/// </summary>
	public enum HandlerSceneType
	{
		LobbyDefault = 1,

		LobbySoccer = 2,

		Pioneer2 = 3,

		RagolDefault = 4
	}
}
