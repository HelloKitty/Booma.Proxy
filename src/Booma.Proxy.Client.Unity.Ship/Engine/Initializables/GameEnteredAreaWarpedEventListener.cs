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
	//We can basically handle GAME and LOBBY warp acks. They work pretty much the same for spawning purposes.
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	public sealed class GameEnteredAreaWarpedEventListener : BaseSingleEventListenerInitializable<IRemotePlayerFinishedWarpedToZoneEventSubscribable, PlayerWarpedToZoneEventArgs>
	{
		private IZoneSettings ZoneSettings { get; }

		private ILog Logger { get; }

		private IFactoryCreatable<GameObject, RemotePlayerWorldRepresentationCreationContext> RemotePlayerFactory { get; }

		/// <inheritdoc />
		public GameEnteredAreaWarpedEventListener(IRemotePlayerFinishedWarpedToZoneEventSubscribable subscriptionService, [NotNull] IZoneSettings zoneSettings, [NotNull] ILog logger, [NotNull] IFactoryCreatable<GameObject, RemotePlayerWorldRepresentationCreationContext> remotePlayerFactory) : base(subscriptionService)
		{
			ZoneSettings = zoneSettings ?? throw new ArgumentNullException(nameof(zoneSettings));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			RemotePlayerFactory = remotePlayerFactory ?? throw new ArgumentNullException(nameof(remotePlayerFactory));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, PlayerWarpedToZoneEventArgs args)
		{
			//This shouldn't happen in lobby
			//But it CAN happen in games, all the time.
			if(ZoneSettings.ZoneId != args.ZoneId)
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Encountered warped player different zone. Current: {ZoneSettings.ZoneId} Remote: {args.ZoneId}.");

				return;
			}

			//If the zone id is the same as the current local zone
			//then we should create a world representation for the player.
			RemotePlayerFactory.Create(new RemotePlayerWorldRepresentationCreationContext(args.EntityGuid));
		}
	}
}
