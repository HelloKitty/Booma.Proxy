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
	//TODO: This is almost copy-paste of the server selection listener. Maybe we should make a generic version somehow
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	public sealed class BlockListDataRecieveEventListener : BaseSingleEventListenerInitializable<IBlockListingEventSubscribable, BlockListingDataRecievedEventArgs>
	{
		private IReadOnlyCollection<IUILabeledButton> StaticBlockButtons { get; }

		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		/// <inheritdoc />
		public BlockListDataRecieveEventListener(
			[NotNull] IBlockListingEventSubscribable subscriptionService, 
			[KeyFilter(UnityUIRegisterationKey.BlockSelectionButton)] [NotNull] IReadOnlyCollection<IUILabeledButton> staticBlockButtons, 
			[NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService) 
			: base(subscriptionService)
		{
			StaticBlockButtons = staticBlockButtons ?? throw new ArgumentNullException(nameof(staticBlockButtons));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		/// <summary>
		/// This is state persisted to determine which index of block we're on.
		/// </summary>
		private int CurrentBlockNumber = 0;

		private async Task OnBlockButtonClicked(MenuItemIdentifier buttonIdentifier)
		{
			//We should just send that we clicked this menu id.
			//if it's successful it should redirect connection to that ship.
			//We must properly handle redirection to move the scene forward.
			await SendService.SendMessage(new SharedMenuSelectionRequestPayload(buttonIdentifier));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, BlockListingDataRecievedEventArgs args)
		{
			if(StaticBlockButtons.Count <= CurrentBlockNumber)
				throw new InvalidOperationException($"Encountered Block Entry with MenuId: {args.Identifier} Count: {CurrentBlockNumber} BlockUIElementSize: {StaticBlockButtons.Count}");

			//Don't use menu identifier to get the index
			IUILabeledButton button = StaticBlockButtons.ElementAt(CurrentBlockNumber);
			Interlocked.Increment(ref CurrentBlockNumber);

			button.IsInteractable = true;
			button.Text = args.BlockName;

			//On click we disable all buttons.
			button.AddOnClickListener(() =>
			{
				foreach(var b in StaticBlockButtons)
					b.IsInteractable = false;
			});

			button.AddOnClickListenerAsync(async () => await OnBlockButtonClicked(args.Identifier));
		}
	}
}
