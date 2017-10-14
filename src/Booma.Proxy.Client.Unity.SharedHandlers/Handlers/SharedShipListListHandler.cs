using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.Serialization;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler for recieveing the ship list payload, <see cref="SharedShipListEventPayload"/>.
	/// </summary>
	[Injectee]
	public sealed class SharedShipListListHandler : GameMessageHandler<SharedShipListEventPayload>
	{
		//TODO: This is a temp handler until we implement the UI.
		[OdinSerialize]
		private IMenuListingRegisterable ShipRegisterationService { get; set; }

		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, SharedShipListEventPayload payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Recieved ShipCount: {payload.Ships.Count()}");

			//Register every ship
			foreach(MenuListing s in payload.Ships)
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Menu: {s.Selection.MenuId} Item: {s.Selection.ItemId} Content: {s.ItemName.Replace("Desinty", "[redacted]")}");

				ShipRegisterationService.RegisterMenuItem(s);
			}

			return Task.CompletedTask;
		}
	}
}
