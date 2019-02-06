using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	[AdditionalRegisterationAs(typeof(ILocalPlayerData))]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public class LocalPlayerDataInitializable : ILocalPlayerData, IGameInitializable
	{
		private ICharacterSlotSelectedModel SlotModel { get; }

		private IReadonlyEntityGuidMappable<GameObject> WorldObjectMappable { get; }

		/// <inheritdoc />
		public byte SlotIndex => SlotModel.SlotSelected;

		/// <inheritdoc />
		public int EntityGuid => Booma.EntityGuid.ComputeEntityGuid(EntityType.Player, (short)SlotIndex);

		/// <inheritdoc />
		public bool isWorldObjectSpawned => WorldObjectMappable.ContainsKey(EntityGuid);

		/// <inheritdoc />
		public GameObject WorldObject => WorldObjectMappable[EntityGuid];

		/// <inheritdoc />
		public LocalPlayerDataInitializable(ICharacterSlotSelectedModel slotModel, IReadonlyEntityGuidMappable<GameObject> worldObjectMappable)
		{
			SlotModel = slotModel;
			WorldObjectMappable = worldObjectMappable;
		}

		//TODO: This is kinda just a hack to get it into the scene.
		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
