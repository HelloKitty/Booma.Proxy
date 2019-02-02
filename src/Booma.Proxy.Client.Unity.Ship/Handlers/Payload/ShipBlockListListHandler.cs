using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	/// <summary>
	/// Handles <see cref="ShipBlockListEventPayload"/> and dispatches to the
	/// UI controller service.
	/// </summary>
	[AdditionalRegisterationAs(typeof(IBlockListingEventSubscribable))]
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	public sealed class ShipBlockListListHandler : GameMessageHandler<ShipBlockListEventPayload>, IBlockListingEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<BlockListingDataRecievedEventArgs> OnBlockListingRecieved;

		/// <inheritdoc />
		public event EventHandler OnBlockListFinishedRecieving;

		/// <inheritdoc />
		public ShipBlockListListHandler(ILog logger)
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, ShipBlockListEventPayload payload)
		{
			foreach(MenuListing m in payload.Blocks)
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Block Listing: {m.ItemName}");

				OnBlockListingRecieved?.Invoke(this, new BlockListingDataRecievedEventArgs(m.Selection, m.ItemName));
			}

			//Once it's completely registered invoke OnRecieved.
			OnBlockListFinishedRecieving?.Invoke(this, EventArgs.Empty);

			return Task.CompletedTask;
		}
	}
}
