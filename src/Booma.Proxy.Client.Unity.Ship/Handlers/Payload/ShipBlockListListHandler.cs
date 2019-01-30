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
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	public sealed class ShipBlockListListHandler : GameMessageHandler<ShipBlockListEventPayload>
	{
		/// <summary>
		/// The menu registeration service.
		/// </summary>
		//[Required]
		//[OdinSerialize]
		[SerializeField] //temp so I don't forget to fix this
		private IMenuListingRegisterable BlockListingRegisterService => throw new NotSupportedException($"TODO: MOVE OVER TO CTOR WORKFLOW");

		[SerializeField]
		private UnityEvent OnRecievedBlockList;

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

				BlockListingRegisterService.RegisterMenuItem(m);
			}

			//Once it's completely registered invoke OnRecieved.
			OnRecievedBlockList?.Invoke();

			return Task.CompletedTask;
		}
	}
}
