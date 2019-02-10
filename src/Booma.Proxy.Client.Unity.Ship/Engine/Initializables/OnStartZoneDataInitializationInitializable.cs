using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//Basically, on game start we should put a ZoneData in the
	//entity mapped collection for the player. Since they are here!
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	public sealed class OnStartZoneDataInitializationInitializable : IGameInitializable
	{
		private IEntityGuidMappable<PlayerZoneData> ZoneDataMappable { get; }

		private IZoneSettings ZoneSettings { get; }

		private ICharacterSlotSelectedModel SlotModel { get; }

		/// <inheritdoc />
		public OnStartZoneDataInitializationInitializable([NotNull] IEntityGuidMappable<PlayerZoneData> zoneDataMappable, [NotNull] IZoneSettings zoneSettings, [NotNull] ICharacterSlotSelectedModel slotModel)
		{
			ZoneDataMappable = zoneDataMappable ?? throw new ArgumentNullException(nameof(zoneDataMappable));
			ZoneSettings = zoneSettings ?? throw new ArgumentNullException(nameof(zoneSettings));
			SlotModel = slotModel ?? throw new ArgumentNullException(nameof(slotModel));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			ZoneDataMappable[SlotModel.SlotSelected] = new PlayerZoneData(ZoneSettings.ZoneId);
			return Task.CompletedTask;
		}
	}
}
