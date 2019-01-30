using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Metadata attributed used to indicate that
	/// a particular object should be created in a specified <see cref="GameSceneType"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class SceneTypeCreateAttribute : Attribute
	{
		/// <summary>
		/// Indicates the scene type this handler is for.
		/// </summary>
		public GameSceneType SceneType { get; }

		/// <inheritdoc />
		public SceneTypeCreateAttribute(GameSceneType sceneType)
		{
			if(!Enum.IsDefined(typeof(GameSceneType), sceneType)) throw new InvalidEnumArgumentException(nameof(sceneType), (int)sceneType, typeof(GameSceneType));
			SceneType = sceneType;
		}
	}
}
