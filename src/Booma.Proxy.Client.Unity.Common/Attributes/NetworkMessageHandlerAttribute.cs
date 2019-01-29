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
		public GameSceneType SceneType { get; }

		/// <inheritdoc />
		public NetworkMessageHandlerAttribute(GameSceneType sceneType)
		{
			if(!Enum.IsDefined(typeof(GameSceneType), sceneType)) throw new InvalidEnumArgumentException(nameof(sceneType), (int)sceneType, typeof(GameSceneType));
			SceneType = sceneType;
		}
	}
}
