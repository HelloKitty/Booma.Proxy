using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class PlayerPickupItemOnGroundEventHandler : Command60Handler<Sub60PickupItemFromGroundCommand>
	{
		[Inject]
		private INetworkEntityRegistery<INetworkItem> WorldItemRegistery { get; }

		/// <inheritdoc />
		public PlayerPickupItemOnGroundEventHandler(ILog logger) 
			: base(logger)
		{
		
		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60PickupItemFromGroundCommand command)
		{
			if(this.Logger.IsDebugEnabled)
				Logger.Debug($"Encountered ItemPickup ClientId: {command.Identifier} ZoneId: {command.ZoneId} ItemId: {command.ItemId}.");

			//TODO: Convert to using uint or change payload to use int
			//Despawns the world represenation. We should still track it locally though as it still exists
			WorldItemRegistery.RemoveEntity((int)command.ItemId)?.Despawn();

			return Task.CompletedTask;
		}
	}
}
