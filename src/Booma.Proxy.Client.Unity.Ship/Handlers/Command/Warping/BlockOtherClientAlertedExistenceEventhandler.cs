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
	[SceneTypeCreate(GameSceneType.RagolDefault)]
	[SceneTypeCreate(GameSceneType.Pioneer2)]
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class BlockOtherClientAlertedExistenceEventHandler : Command60Handler<Sub60FinishedWarpAckCommand> //we don't need context
	{
		private INetworkPlayerFactory PlayerFactory { get; }

		private INetworkPlayerCollection PlayerCollection { get; }

		private IUnitScalerStrategy UnitScaler { get; }

		/// <inheritdoc />
		public BlockOtherClientAlertedExistenceEventHandler([NotNull] INetworkPlayerFactory playerFactory, [NotNull] INetworkPlayerCollection playerCollection, [NotNull] IUnitScalerStrategy unitScaler, [NotNull] ILog logger) 
			: base(logger)
		{
			PlayerFactory = playerFactory ?? throw new ArgumentNullException(nameof(playerFactory));
			PlayerCollection = playerCollection ?? throw new ArgumentNullException(nameof(playerCollection));
			UnitScaler = unitScaler ?? throw new ArgumentNullException(nameof(unitScaler));
		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60FinishedWarpAckCommand command)
		{
			//Clients do a full broadcast and we already know about this client
			//so we should just return
			if(PlayerCollection.ContainsId(command.Identifier))
				return Task.CompletedTask;

			if(Logger.IsInfoEnabled)
				Logger.Info($"Client broadcasted existence Id: {command.Identifier} ZoneId: {command.ZoneId}");

			float rotation = UnitScaler.ScaleYRotation(command.YAxisRotation);
			Vector3 position = UnitScaler.Scale(command.Position);

			//TODO: We should check the ZoneId being sent AND if we already know the player. We shouldn't but we should still verify
			PlayerFactory.CreateEntity(command.Identifier, position, Quaternion.AngleAxis(rotation, Vector3.up));

			return Task.CompletedTask;
		}
	}
}
