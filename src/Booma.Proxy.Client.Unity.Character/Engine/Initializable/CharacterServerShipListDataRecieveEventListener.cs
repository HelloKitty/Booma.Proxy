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
	public sealed class CharacterServerShipListDataRecieveEventListener : BaseSingleEventListenerInitializable<IShipListingEventSubscribable, ShipListingDataRecievedEventArgs>
	{
		private IReadOnlyCollection<IUILabeledButton> StaticShipButtons { get; }

		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		/// <summary>
		/// This is state persisted to determine which index of ships we're on.
		/// </summary>
		private int CurrentShipNumber = 0;

		/// <inheritdoc />
		public CharacterServerShipListDataRecieveEventListener(
			[NotNull] IShipListingEventSubscribable subscriptionService, 
			[NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService, 
			[KeyFilter(UnityUIRegisterationKey.ServerSelectionButton)] [NotNull] IReadOnlyCollection<IUILabeledButton> staticShipButtons) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			StaticShipButtons = staticShipButtons ?? throw new ArgumentNullException(nameof(staticShipButtons));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, ShipListingDataRecievedEventArgs args)
		{
			if(StaticShipButtons.Count <= args.Identifier.ItemId)
				throw new InvalidOperationException($"Encountered Ship Entry with MenuId: {args.Identifier}");

			//Don't use menu identifier to get the index
			IUILabeledButton button = StaticShipButtons.ElementAt(CurrentShipNumber);
			Interlocked.Increment(ref CurrentShipNumber);

			button.IsInteractable = true;
			button.Text = args.ShipName;

			//On click we disable all buttons.
			button.AddOnClickListener(() =>
			{
				foreach(var b in StaticShipButtons)
					b.IsInteractable = false;
			});

			button.AddOnClickListenerAsync(async () => await OnShipButtonClicked(args.Identifier));
		}

		private async Task OnShipButtonClicked(MenuItemIdentifier buttonIdentifier)
		{
			//We should just send that we clicked this menu id.
			//if it's successful it should redirect connection to that ship.
			//We must properly handle redirection to move the scene forward.
			await SendService.SendMessage(new SharedMenuSelectionRequestPayload(buttonIdentifier));
		}
	}
}
