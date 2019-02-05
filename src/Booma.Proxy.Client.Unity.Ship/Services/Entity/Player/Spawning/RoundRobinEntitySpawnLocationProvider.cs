using Booma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	//From: https://github.com/BoomaNation/Booma.Library/blob/c5af8ed85e0ddd07f86f941588d8962824ae04be/src/Booma.Instance.Server/Entity/RoundRobinEntitySpawnLocationProvider.cs
	[Serializable]
	public class RoundRobinEntitySpawnLocationProvider : MonoBehaviour, ISpawnPointStrategy
	{
		/// <summary>
		/// Spawn points for the entity.
		/// </summary>
		[Tooltip("Collection of spawnpoints for entities.")]
		[SerializeField]
		private List<Transform> SpawnPointTransforms;

		/// <summary>
		/// Used for the simple round-robin strategy.
		/// </summary>
		[ReadOnly]
		[ShowInInspector]
		private int internalCounter = 0;

		void Awake()
		{
			if(!SpawnPointTransforms.Any())
				throw new InvalidOperationException($"{nameof(SpawnPointTransforms)} must not be empty.");
		}

		//Not threadsafe but doesn't have to be
		public Transform GetSpawnpoint()
		{
			if(SpawnPointTransforms.Count() > internalCounter)
				return SpawnPointTransforms[internalCounter++]; //increment after access
			else
				return SpawnPointTransforms[(internalCounter = 0)]; //set counter to 0
		}
	}
}