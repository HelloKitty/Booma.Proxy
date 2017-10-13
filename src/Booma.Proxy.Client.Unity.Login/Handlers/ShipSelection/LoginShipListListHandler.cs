using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler for recieveing the ship list payload, <see cref="LoginShipListEventPayload"/>.
	/// </summary>
	[Injectee]
	public sealed class LoginShipListListHandler : LoginMessageHandler<LoginShipListEventPayload>
	{
		//TODO: This is a temp handler until we implement the UI.
		[Inject]
		private IShipListingRegisterable ShipRegisterationService { get; }

		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBLoginPacketPayloadClient> context, LoginShipListEventPayload payload)
		{
			//Register every ship
			foreach(ShipListing s in payload.Ships)
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Menu: {s.Selection.MenuId} Item: {s.Selection.ItemId} Content: {s.ShipName.Replace("Desinty", "[redacted]")}");

				ShipRegisterationService.RegisterShip(s);
			}

			return Task.CompletedTask;
		}
	}
}
