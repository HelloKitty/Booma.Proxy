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
	//Lobby handles this simply and specificly compared to the Game
	//In Lobby it should only really mean that they're now fully init
	//and ready for physical world object initialization.
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class LobbyEnteredAreaWarpedEventListener : BaseSingleEventListenerInitializable<IRemotePlayerWarpedToZoneEventSubscribable, PlayerWarpedToZoneEventArgs>
	{
		private IZoneSettings ZoneSettings { get; }

		private ILog Logger { get; }

		private IFactoryCreatable<GameObject, RemotePlayerWorldRepresentationCreationContext> RemotePlayerFactory { get; }

		/// <inheritdoc />
		public LobbyEnteredAreaWarpedEventListener(IRemotePlayerWarpedToZoneEventSubscribable subscriptionService, [NotNull] IZoneSettings zoneSettings, [NotNull] ILog logger, [NotNull] IFactoryCreatable<GameObject, RemotePlayerWorldRepresentationCreationContext> remotePlayerFactory) : base(subscriptionService)
		{
			ZoneSettings = zoneSettings ?? throw new ArgumentNullException(nameof(zoneSettings));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			RemotePlayerFactory = remotePlayerFactory ?? throw new ArgumentNullException(nameof(remotePlayerFactory));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, PlayerWarpedToZoneEventArgs args)
		{
			if(ZoneSettings.ZoneId != args.ZoneId)
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Encountered warped player in Lobby not in the same zone. Current: {ZoneSettings.ZoneId} Remote: {args.ZoneId}. This should only happen if they are cheating.");

			//If the zone id is the same as the current local zone
			//then we should create a world representation for the player.
			RemotePlayerFactory.Create(new RemotePlayerWorldRepresentationCreationContext(args.EntityGuid));
		}
	}
}
