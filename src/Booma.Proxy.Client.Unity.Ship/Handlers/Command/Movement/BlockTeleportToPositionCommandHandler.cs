using System;
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
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	public sealed class BlockTeleportToPositionCommandHandler : Command60Handler<Sub60TeleportToPositionCommand>
	{
		private IUnitScalerStrategy ScalerService { get; }

		private IEntityGuidMappable<WorldTransform> EntityLocationMappable { get; }

		/// <inheritdoc />
		public BlockTeleportToPositionCommandHandler(ILog logger, [NotNull] IUnitScalerStrategy scalerService, [NotNull] IEntityGuidMappable<WorldTransform> entityLocationMappable)
			: base(logger)
		{
			ScalerService = scalerService ?? throw new ArgumentNullException(nameof(scalerService));
			EntityLocationMappable = entityLocationMappable ?? throw new ArgumentNullException(nameof(entityLocationMappable));
		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60TeleportToPositionCommand command)
		{
			//TODO: Don't do anything with this
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved {nameof(Sub60TeleportToPositionCommand)} ClientId: {command.Identifier} X: {command.Position.X} Y: {command.Position.Y} Z: {command.Position.Z}");

			int entityGuid = EntityGuid.ComputeEntityGuid(EntityType.Player, command.Identifier);

			//For this packet, we just directly set the world transform
			//Clients seem to set this when they're loading into a new area
			//or maybe to teleport too from teleports
			//TODO: Is this good enough, what about teleporters??
			EntityLocationMappable[entityGuid] = new WorldTransform(ScalerService.Scale(command.Position), Quaternion.identity);

			return Task.CompletedTask;
		}
	}
}
