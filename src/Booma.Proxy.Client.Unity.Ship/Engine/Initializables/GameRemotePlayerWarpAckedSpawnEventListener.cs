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
	//TODO: This is kinda copy-paste of LobbyEnteredAreaWarpedEventListener, can we do anything better here?? 
	//This is basically the same as LobbyEnteredAreaWarpedEventListener
	//Lobby handles this simply and specificly compared to the Game
	//In Lobby it should only really mean that they're now fully init
	//and ready for physical world object initialization.
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class GameRemotePlayerWarpAckedSpawnEventListener : BaseSingleEventListenerInitializable<IRemoteClientAcknowledgedWarpEventSubscribable, RemotePlayerWarpAcknowledgementEventArgs>
	{
		private IZoneSettings ZoneSettings { get; }

		private ILog Logger { get; }

		private IFactoryCreatable<GameObject, RemotePlayerWorldRepresentationCreationContext> RemotePlayerFactory { get; }

		private IReadonlyEntityGuidMappable<PlayerZoneData> ZoneDataMappable { get; }

		/// <inheritdoc />
		public GameRemotePlayerWarpAckedSpawnEventListener(
			IRemoteClientAcknowledgedWarpEventSubscribable subscriptionService, 
			[NotNull] IZoneSettings zoneSettings, 
			[NotNull] ILog logger, 
			[NotNull] IFactoryCreatable<GameObject, RemotePlayerWorldRepresentationCreationContext> remotePlayerFactory, 
			IReadonlyEntityGuidMappable<PlayerZoneData> zoneDataMappable) 
			: base(subscriptionService)
		{
			ZoneSettings = zoneSettings ?? throw new ArgumentNullException(nameof(zoneSettings));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			RemotePlayerFactory = remotePlayerFactory ?? throw new ArgumentNullException(nameof(remotePlayerFactory));
			ZoneDataMappable = zoneDataMappable;
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, RemotePlayerWarpAcknowledgementEventArgs args)
		{
			//We should never call this event unless we have zone data. It would make no sense.
			//Also, this case is very possible in games. They can Ack that they saw our warp. That they are now Forest or
			//something and we're in Caves or Pioneer. We should NOT create a visible/world representation of a
			//remote player that acked on a different zone.
			if(ZoneSettings.ZoneId != ZoneDataMappable[args.EntityGuid].ZoneId)
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Encountered warped player {args.EntityGuid} not in the same zone. Current: {ZoneSettings.ZoneId} Remote: {ZoneDataMappable[args.EntityGuid].ZoneId}.");

				return;
			}

			//If the zone id is the same as the current local zone
			//then we should create a world representation for the player.
			RemotePlayerFactory.Create(new RemotePlayerWorldRepresentationCreationContext(args.EntityGuid));
		}
	}
}
