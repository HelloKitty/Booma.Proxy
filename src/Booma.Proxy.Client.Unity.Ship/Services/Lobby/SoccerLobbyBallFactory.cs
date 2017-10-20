using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Simple manager that manages the spawning and despawning the the lobby soccer ball.
	/// </summary>
	[Injectee]
	public sealed class SoccerLobbyBallFactory : SerializedMonoBehaviour
	{
		[Inject]
		private IBeatsEventQueueRegisterable BeatEventQueue { get; }

		[Tooltip("The soccer ball prefab.")]
		[SerializeField]
		private GameObject SoccerrBallPrefab;

		[ReadOnly]
		private GameObject CurrentTrackedBall;

		[Required]
		[PropertyTooltip("The spawn point strategy.")]
		[OdinSerialize]
		private ISpawnPointStrategy SpawnStrategy { get; set; }

		private void Start()
		{
			//In the soccer lobby the client expects that
			//the ball will be summoned every beat.
			//it also expects that the ball we be unsummoned
			//15/10 centibeats before a new one spawns

			//On the next beat we want to register a repeating ball spawning
			BeatEventQueue.RegisterOnNextBeat(() =>
			{
				//Spawn the ball
				SpawnBall();

				//Every 1 beat we should spawn the ball
				BeatEventQueue.RegisterRepeating(SpawnBall, Beat.Beats(1));

				//Add a despawn event right before we respawn the ball
				BeatEventQueue.RegisterRepeating(DespawnBall, Beat.CentiBeats(90));
			});
		}

		private void SpawnBall()
		{
			Transform trans = SpawnStrategy.GetSpawnpoint();

			if(trans == null)
				throw new InvalidOperationException($"Cannot spawn a ball from the {nameof(SoccerLobbyBallFactory)} from a null transform. Initialize the {nameof(ISpawnPointStrategy)} field.");

			//Just spawn the ball for now
			CurrentTrackedBall = GameObject.Instantiate(SoccerrBallPrefab, trans.position, trans.rotation);
		}

		private void DespawnBall()
		{
			if(CurrentTrackedBall == null)
				return;

			Destroy(CurrentTrackedBall);
			CurrentTrackedBall = null;
		}
	}
}
