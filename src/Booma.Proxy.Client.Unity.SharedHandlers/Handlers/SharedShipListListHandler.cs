using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler for recieveing the ship list payload, <see cref="SharedShipListEventPayload"/>.
	/// </summary>
	[Injectee]
	public sealed class SharedShipListListHandler : GameMessageHandler<SharedShipListEventPayload>
	{
		//TODO: This is a temp handler until we implement the UI.
		private IMenuListingRegisterable ShipRegisterationService => throw new NotSupportedException($"MUST MOVE OVER TO CTOR WORKFLOW");

		[SerializeField]
		private UnityEvent OnRecievedShipList;

		/// <inheritdoc />
		public SharedShipListListHandler(ILog logger)
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, SharedShipListEventPayload payload)
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

			//Once it's completely registered invoke OnRecieved.
			OnRecievedShipList?.Invoke();

			return Task.CompletedTask;
		}
	}
}
