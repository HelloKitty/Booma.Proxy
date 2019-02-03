using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GladNet;

namespace Booma.Proxy
{
	public abstract class NetworkedDynamicMenuPopulationEventListener<TEventSubsribable, TMenuChangeArgs> : BaseSingleEventListenerInitializable<TEventSubsribable, TMenuChangeArgs>
		where TMenuChangeArgs : BaseMenuItemDataChangedEventArgs 
		where TEventSubsribable : class
	{
		//TODO: Should we directly expose the buttons?
		private IReadOnlyCollection<IUILabeledButton> StaticButtons { get; }

		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		/// <summary>
		/// Indicates if the inheritor would like to hook into and listen to events.
		/// </summary>
		protected bool isCustomEventsEnabled { get; }

		/// <inheritdoc />
		protected NetworkedDynamicMenuPopulationEventListener([NotNull] TEventSubsribable subscriptionService, [NotNull] IReadOnlyCollection<IUILabeledButton> staticButtons, [NotNull] IPeerPayloadSendService<PSOBBGamePacketPayloadClient> sendService, bool enableCustomEvents) 
			: base(subscriptionService)
		{
			StaticButtons = staticButtons ?? throw new ArgumentNullException(nameof(staticButtons));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			isCustomEventsEnabled = enableCustomEvents;
		}

		/// <summary>
		/// This is state persisted to determine which index of menu we're on.
		/// </summary>
		private int MenuEntryRunningCount = 0;

		/// <summary>
		/// Indicates the current size of the menu.
		/// </summary>
		protected int CurrentMenuSize => MenuEntryRunningCount;

		/// <inheritdoc />
		protected override void OnEventFired(object source, TMenuChangeArgs args)
		{
			if(StaticButtons.Count <= MenuEntryRunningCount)
				throw new InvalidOperationException($"Encountered Menu Entry Entry with MenuId: {args.Identifier} From: {typeof(TEventSubsribable).Name} With Args: {typeof(TMenuChangeArgs).Name} exceeding static menu size: {StaticButtons.Count}. CurrentCount: {MenuEntryRunningCount}");

			//Don't use menu identifier to get the index
			IUILabeledButton button = StaticButtons.ElementAt(MenuEntryRunningCount);
			Interlocked.Increment(ref MenuEntryRunningCount);

			button.IsInteractable = true;
			button.Text = args.MenuItemName;

			//TODO: Should we send networked menu click first before exposing click events to inheritor?
			button.AddOnClickListenerAsync(async () => await SendNetworkedMenuClick(args.Identifier));

			if(isCustomEventsEnabled)
			{
				button.AddOnClickListenerAsync(async () => await OnButtonClickedAsync(MenuEntryRunningCount, args));
				button.AddOnClickListener(() => OnButtonClicked(MenuEntryRunningCount, args));
			}
		}

		/// <summary>
		/// Called first (before async) when a button is clicked.
		/// ONLY CALLED IF <see cref="isCustomEventsEnabled"/> is enabled via the ctor! 
		/// </summary>
		/// <param name="entryNumber">The index/entry number of the button.</param>
		/// <param name="args">The event args.</param>
		protected virtual void OnButtonClicked(int entryNumber, TMenuChangeArgs args)
		{

		}

		/// <summary>
		/// Called first (before async) when a button is clicked.
		/// ONLY CALLED IF <see cref="isCustomEventsEnabled"/> is enabled via the ctor!
		/// </summary>
		/// <param name="entryNumber">The index/entry number of the button.</param>
		/// <param name="args">The event args.</param>
		protected virtual Task OnButtonClickedAsync(int entryNumber, TMenuChangeArgs args)
		{
			return Task.CompletedTask;
		}

		private async Task SendNetworkedMenuClick(MenuItemIdentifier buttonIdentifier)
		{
			//We should just send that we clicked this menu id.
			//if it's successful it should redirect connection to that ship.
			//We must properly handle redirection to move the scene forward.
			await SendService.SendMessage(new SharedMenuSelectionRequestPayload(buttonIdentifier));
		}

		/// <summary>
		/// Disables the options/buttons interactivity when any of them
		/// are clicked.
		/// DO NOT CALL THIS MORE THAN ONCE.
		/// IT CANNOT BE UNDONE.
		/// </summary>
		protected void DisableInteractionOnMenuButtonsOnClick()
		{
			foreach(var button in StaticButtons)
				//On click we disable all buttons.
				button.AddOnClickListener(() =>
				{
					foreach(var b in StaticButtons)
						b.IsInteractable = false;
				});
		}
	}
}
