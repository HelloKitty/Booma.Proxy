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
	[AdditionalRegisterationAs(typeof(IShipListingEventSubscribable))]
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	public sealed class SharedShipListListHandler : GameMessageHandler<SharedShipListEventPayload>, IShipListingEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<ShipListingDataRecievedEventArgs> OnShipListingRecieved;

		/// <inheritdoc />
		public event EventHandler OnShipListFinishedRecieving;

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

				OnShipListingRecieved?.Invoke(this, new ShipListingDataRecievedEventArgs(s.Selection, s.ItemName));
			}

			//Once it's completely registered invoke OnRecieved.
			OnShipListFinishedRecieving?.Invoke(this, EventArgs.Empty);

			return Task.CompletedTask;
		}
	}
}
