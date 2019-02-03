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
	public sealed class BlockListDataRecieveEventListener : NetworkedDynamicMenuPopulationEventListener<IBlockListingEventSubscribable, BlockListingDataRecievedEventArgs>
	{
		/// <inheritdoc />
		public BlockListDataRecieveEventListener(IBlockListingEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.BlockSelectionButton)] IReadOnlyCollection<IUILabeledButton> staticButtons, IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService, bool enableCustomEvents) 
			: base(subscriptionService, staticButtons, sendService, enableCustomEvents)
		{
			//false means we don't listen to events through overridable virtuals

			//We DON'T want it possible to click any more buttons after one is clicked.
			this.DisableInteractionOnMenuButtonsOnClick();
		}
	}
}
