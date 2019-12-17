using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Unitysync.Async;

namespace Booma.Proxy
{
	public sealed class LoadNewSceneAfterTime : MonoBehaviour
	{
		private CancellationTokenSource tokenSource = new CancellationTokenSource();

		//[MaxValue(500)]
		//[MinValue(0.0f)]
		[SerializeField]
		public float DefaultWaitTime;

		public void LoadScene(float timeToWait, int levelToLoad)
		{
			if(timeToWait < 0) throw new ArgumentOutOfRangeException(nameof(timeToWait));

			if(Mathf.Approximately(0, timeToWait))
				SceneManager.LoadSceneAsync(levelToLoad).allowSceneActivation = true;

			AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(levelToLoad);
			loadSceneAsync.allowSceneActivation = false;

			Task.Run(async () => await WaitForToLoad(timeToWait), tokenSource.Token)
				.UnityAsyncContinueWith(this, () =>
				{
					loadSceneAsync.allowSceneActivation = true;
				});
		}

		public void LoadScene(int levelToLoad)
		{
			LoadScene(DefaultWaitTime, levelToLoad);
		}

		private async Task WaitForToLoad(float timeToWait)
		{
			await Task.Delay((int)(timeToWait * 1000));
		}

		//Cancel to avoid potential crash issues
		private void OnApplicationQuit()
		{
			tokenSource.Cancel(true);
		}

		private void OnDestroy()
		{
			tokenSource.Cancel();
		}
	}
}
