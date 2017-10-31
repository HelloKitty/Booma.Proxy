using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class BlockOtherClientAlertedExistenceEventhandler : Command60Handler<Sub60FinishedWarpAckCommand> //we don't need context
	{
		[Inject]
		private INetworkPlayerFactory PlayerFactory { get; }

		[Inject]
		private INetworkPlayerCollection PlayerCollection { get; }

		[Inject]
		private IUnitScalerStrategy UnitScaler { get; }

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
			PlayerFactory.CreatePlayer(command.Identifier, position, Quaternion.AngleAxis(rotation, Vector3.up));

			return Task.CompletedTask;
		}
	}
}
