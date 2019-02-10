using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Guardians;
using UnityEngine;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	public sealed class SpawnPlayerOnBurstingFinishedEventListener : BaseSingleEventListenerInitializable<IClientFinishedBurstingEventSubscribable, ClientBurstingEndingEventArgs>
	{
		private ILocalPlayerData PlayerData { get; }

		private IReadonlyEntityGuidMappable<PlayerZoneData> ZoneMappable { get; }

		private IFactoryCreatable<GameObject, RemotePlayerWorldRepresentationCreationContext> RemotePlayerFactory { get; }

		/// <inheritdoc />
		public SpawnPlayerOnBurstingFinishedEventListener([NotNull] IClientFinishedBurstingEventSubscribable subscriptionService, [NotNull] ILocalPlayerData playerData, [NotNull] IReadonlyEntityGuidMappable<PlayerZoneData> zoneMappable, [NotNull] IFactoryCreatable<GameObject, RemotePlayerWorldRepresentationCreationContext> remotePlayerFactory) 
			: base(subscriptionService)
		{
			PlayerData = playerData ?? throw new ArgumentNullException(nameof(playerData));
			ZoneMappable = zoneMappable ?? throw new ArgumentNullException(nameof(zoneMappable));
			RemotePlayerFactory = remotePlayerFactory ?? throw new ArgumentNullException(nameof(remotePlayerFactory));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, ClientBurstingEndingEventArgs args)
		{
			//We only want to spawn for successful bursts.
			if(!args.isSuccessful)
				return;

			//We should spawn IF we're both in the same zone
			//If not, it's not an error. We just aren't interested
			if(ZoneMappable[PlayerData.EntityGuid].ZoneId != ZoneMappable[args.EntityGuid].ZoneId)
				return;

			RemotePlayerFactory.Create(new RemotePlayerWorldRepresentationCreationContext(args.EntityGuid));
		}
	}
}
