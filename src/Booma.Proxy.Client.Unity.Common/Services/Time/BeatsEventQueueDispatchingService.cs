using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Services the beat's queue in the scene.
	/// </summary>
	[Injectee]
	public sealed class BeatsEventQueueDispatchingService : MonoBehaviour
	{
		/// <summary>
		/// The dispatching service.
		/// </summary>
		[Inject]
		private IBeatsEventQueueDispatchable Dispatcher { get; }

		//We check every frame for most percise beats handling.
		private void Update()
		{
			//While we have beat events to dispatch we should dispatch them
			while(Dispatcher.isBeatEventReady)
				Dispatcher.DispatchNext();
		}
	}
}
