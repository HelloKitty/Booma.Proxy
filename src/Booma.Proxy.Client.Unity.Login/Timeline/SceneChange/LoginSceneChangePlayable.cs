using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using Unitysync.Async;

namespace Booma.Proxy
{
	[Serializable]
	public sealed class LoginSceneChangeBehaviour : PlayableBehaviour
	{
		/// <summary>
		/// The scene to load.
		/// </summary>
		public int SceneNumberToLoad;

		private AsyncOperation Operation;

		private float timePassed = 0.0f;

		/// <inheritdoc />
		public override void OnGraphStart(Playable playable)
		{
			base.OnGraphStart(playable);

			double duration = playable.GetDuration();

			//We can't have a length of 0. Not enough time to preload
			//and then load the next scene
			if(Mathf.Approximately((float)duration, 0))
				throw new InvalidOperationException("Cannot have a 0 length login scene change playable.");
		}

		/// <inheritdoc />
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			//If we're not actually in playmode we should not try to do loading
			if(!Application.isPlaying)
				return;

			//If we don't have a loading scene we should start one
			if(Operation == null)
			{
				//Start preloading the scene load
				Operation = SceneManager.LoadSceneAsync(SceneNumberToLoad);
				Operation.allowSceneActivation = false;
			}

			timePassed += info.deltaTime;

			//When it's done then we should enable scene load
			if(timePassed >= playable.GetDuration() || Mathf.Approximately((float)playable.GetDuration(), timePassed))
			{
				Operation.allowSceneActivation = true;
			}

			base.ProcessFrame(playable, info, playerData);
		}
	}
}
