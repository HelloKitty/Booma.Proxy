using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Booma.Proxy
{
	[Serializable]
	public sealed class LoginSceneChangeClip : PlayableAsset, ITimelineClipAsset
	{
		/// <inheritdoc />
		public ClipCaps clipCaps => ClipCaps.ClipIn | ClipCaps.SpeedMultiplier;

		public static LoginSceneChangeBehaviour Template = new LoginSceneChangeBehaviour();

		public int SceneNumberToLoad;

		/// <inheritdoc />
		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			var playable = ScriptPlayable<LoginSceneChangeBehaviour>.Create(graph, Template);
			LoginSceneChangeBehaviour clone = playable.GetBehaviour();
			clone.SceneNumberToLoad = SceneNumberToLoad;
			return playable;
		}
	}
}
