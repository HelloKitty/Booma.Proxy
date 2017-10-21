using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	/// <summary>
	/// Simple manager that manages the spawning and despawning the the lobby soccer ball.
	/// </summary>
	[Injectee]
	public sealed class SoccerLobbyBallFactory : SerializedMonoBehaviour
	{
		[Serializable]
		public class OnNewBallSpawnedEvent : UnityEvent<GameObject> { }

		[Inject]
		private IBeatsEventQueueRegisterable BeatEventQueue { get; }

		[Inject]
		private ILog Logger { get; }

		[Inject]
		private IGameObjectFactory GameObjectFactory { get; }

		[Tooltip("The soccer ball prefab.")]
		[SerializeField]
		private GameObject SoccerrBallPrefab;

		[ReadOnly]
		private GameObject CurrentTrackedBall;

		[Required]
		[PropertyTooltip("The spawn point strategy.")]
		[OdinSerialize]
		private ISpawnPointStrategy SpawnStrategy { get; set; }

		[SerializeField]
		private OnNewBallSpawnedEvent OnBallSpawned;

		private void Start()
		{
			//In the soccer lobby the client expects that
			//the ball will be summoned every beat.
			//it also expects that the ball we be unsummoned
			//15/10 centibeats before a new one spawns

			//On the next beat we want to register a repeating ball spawning
			BeatEventQueue.RegisterOnNextBeat(SpawnBall);
		}

		[Button("Force Spawn")]
		private void SpawnBall()
		{
			if(Logger.IsInfoEnabled)
				Logger.Info("Spawning soccer ball.");

			Transform trans = SpawnStrategy.GetSpawnpoint();

			if(trans == null)
				throw new InvalidOperationException($"Cannot spawn a ball from the {nameof(SoccerLobbyBallFactory)} from a null transform. Initialize the {nameof(ISpawnPointStrategy)} field.");

			//Just spawn the ball for now
			CurrentTrackedBall = GameObjectFactory.Create(SoccerrBallPrefab, trans.position, trans.rotation);

			OnBallSpawned?.Invoke(CurrentTrackedBall);

			//TODO: Handle this cleaner once we have more accurate beat time scheduling
			double beatsTime = TimeService.CurrentBeatsTime;
			//Every 1 beat we should spawn the ball
			BeatEventQueue.RegisterOnNextBeat(SpawnBall);
			BeatEventQueue.RegisterEvent(((long)beatsTime) + Beat.CentiBeats(90), DespawnBall);
		}

		private void DespawnBall()
		{
			if(Logger.IsInfoEnabled)
				Logger.Info("Despawning soccer ball.");

			if(CurrentTrackedBall == null)
				return;

			Destroy(CurrentTrackedBall);
			CurrentTrackedBall = null;
		}
	}
}
