using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using GladNet;
using Guardians;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	public sealed class CharacterServerShipListDataRecieveEventListener : NetworkedDynamicMenuPopulationEventListener<IShipListingEventSubscribable, ShipListingDataRecievedEventArgs>
	{
		/// <inheritdoc />
		public CharacterServerShipListDataRecieveEventListener(
			[NotNull] IShipListingEventSubscribable subscriptionService, 
			[NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService, 
			[KeyFilter(UnityUIRegisterationKey.ServerSelectionButton)] [NotNull] IReadOnlyCollection<IUILabeledButton> staticShipButtons) 
			: base(subscriptionService, staticShipButtons, sendService, false)
		{
			//false means we don't listen to events through overridable virtuals
		}

		/// <inheritdoc />
		protected override void Configure()
		{
			//We DON'T want it possible to click any more buttons after one is clicked.
			this.DisableInteractionOnMenuButtonsOnClick();
		}
	}
}
