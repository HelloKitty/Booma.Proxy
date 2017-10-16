using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class BlockMovingFinishedEventhandler : ClientAssociatedCommand60Handler<Sub60FinishedMovingCommand>
	{
		/// <summary>
		/// Service that translates the incoming position to the correct unit scale that
		/// Unity3D expects.
		/// </summary>
		[Required]
		[OdinSerialize]
		private IUnitScalerStrategy Scaler { get; set; }

		/// <inheritdoc />
		protected override Task HandleClientMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60FinishedMovingCommand command, INetworkPlayer player)
		{
			//This one sends a Y position, for some reason.
			player.Transform.Position = Scaler.Scale(command.Position.ToUnityVector3());

			//Also set the rotation; PSO only appears to use Y axis rotation
			player.Transform.Rotation = Quaternion.AngleAxis(command.YAxisRotation, Vector3.up);

			return Task.CompletedTask;
		}
	}
}
