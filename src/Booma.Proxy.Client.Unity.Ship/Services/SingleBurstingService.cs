using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//Basically this just manages some bursting state for us.
	[AdditionalRegisterationAs(typeof(IBurstingService))]
	[AdditionalRegisterationAs(typeof(IReadonlyBurstingService))]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	public sealed class SingleBurstingService : IBurstingService, IGameInitializable
	{
		private int _burstingEntityGuid;

		/// <inheritdoc />
		public bool isBurstingInProgress { get; private set; }

		/// <inheritdoc />
		public int? BurstingEntityGuid => isBurstingInProgress ? null : new int?(_burstingEntityGuid);

		/// <inheritdoc />
		public void ClearBursting()
		{
			isBurstingInProgress = false;
		}

		/// <inheritdoc />
		public bool SetBurstingEntity(int entityGuid)
		{
			if(isBurstingInProgress)
				throw new InvalidOperationException($"Failed to set bursting for Entity: {entityGuid}. Bursting already inprogress.");

			//TODO: When would this ever return false?
			_burstingEntityGuid = entityGuid;
			isBurstingInProgress = true;
			return true;
		}

		//TODO: We don't need this, this is just a hack to get it into the scene.
		/// <inheritdoc />
		public Task OnGameInitialized()
		{

		}
	}
}
