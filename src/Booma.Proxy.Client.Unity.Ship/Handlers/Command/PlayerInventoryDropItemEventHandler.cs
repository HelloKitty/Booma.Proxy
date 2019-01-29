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
	public sealed class PlayerInventoryDropItemEventHandler : Command60Handler<Sub60DropInventoryItemCommand>
	{
		[Inject]
		private INetworkEntityFactory<INetworkItem> WorldItemFactory { get; }

		[Inject]
		private IUnitScalerStrategy UnitScaler { get; }

		/// <inheritdoc />
		public PlayerInventoryDropItemEventHandler(ILog logger) 
			: base(logger)
		{

		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60DropInventoryItemCommand command)
		{
			if(this.Logger.IsDebugEnabled)
				Logger.Debug($"Encountered ItemDrop ClientId: {command.Identifier} Unknown1: {command.Unknown1} Unknown2: {command.Unknown2} ZoneId: {command.ZoneId} Position: {command.Position} ItemId: {command.ItemId}");

			//Create a network item in the world for others to see
			INetworkItem item = WorldItemFactory.CreateEntity(command.ItemId, UnitScaler.Scale(command.Position), Quaternion.identity);

			return Task.CompletedTask;
		}
	}
}
