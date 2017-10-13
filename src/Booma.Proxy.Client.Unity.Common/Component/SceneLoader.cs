using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{
	/// <summary>
	/// Component that allows you to load scenes in a way that
	/// can be hooked up to <see cref="UnityEvent"/>s.
	/// </summary>
	public sealed class SceneLoader : MonoBehaviour
	{
		[Tooltip("The scene to be loaded.")]
		[SerializeField]
		private int SceneId;

		public void LoadScene()
		{
			SceneManager.LoadSceneAsync(SceneId).allowSceneActivation = true;
		}

		public void LoadScene(int sceneId)
		{
			SceneManager.LoadSceneAsync(sceneId).allowSceneActivation = true;
		}
	}
}
