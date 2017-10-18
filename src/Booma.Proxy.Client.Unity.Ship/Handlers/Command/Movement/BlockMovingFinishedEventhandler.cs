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
	public sealed class BlockMovingFinishedEventhandler : ContextExtendedCommand60Handler<Sub60FinishedMovingCommand, INetworkPlayerNetworkMessageContext>
	{
		/// <summary>
		/// Service that translates the incoming position to the correct unit scale that
		/// Unity3D expects.
		/// </summary>
		[Inject]
		private IUnitScalerStrategy Scaler { get; }

		/// <inheritdoc />
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60FinishedMovingCommand command, INetworkPlayerNetworkMessageContext commandContext)
		{
			//This one sends a Y position, for some reason.
			commandContext.RemotePlayer.Transform.Position = Scaler.Scale(command.Position.ToUnityVector3());

			//Also set the rotation; PSO only appears to use Y axis rotation
			commandContext.RemotePlayer.Transform.Rotation = Quaternion.AngleAxis(Scaler.ScaleYRotation(command.YAxisRotation), Vector3.up);

			return Task.CompletedTask;
		}
	}
}
