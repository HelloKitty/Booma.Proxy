using System;
using System.Collections.Generic;
using System.Text;
using GladMMO;

namespace Booma.Proxy
{
	public interface IPlayerZoneAreaWarpedEventPublisher : IEventPublisher<IPlayerZoneAreaWarpedEventSubscribable, PlayerZoneAreaWarpedEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(IPlayerZoneAreaWarpedEventPublisher))]
	[AdditionalRegisterationAs(typeof(IPlayerZoneAreaWarpedEventSubscribable))]
	[SceneTypeCreateGladMMO(GladMMO.GameSceneType.InstanceServerScene)]
	public sealed class PlayerZoneAreaWarpedEventPublisher : IPlayerZoneAreaWarpedEventPublisher, IPlayerZoneAreaWarpedEventSubscribable
	{
		public event EventHandler<PlayerZoneAreaWarpedEventArgs> OnPlayerZoneAreWarped;

		public void PublishEvent(object sender, PlayerZoneAreaWarpedEventArgs eventArgs)
		{
			OnPlayerZoneAreWarped?.Invoke(sender, eventArgs);
		}
	}
}
