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
	/// <summary>
	/// Handler that deals with the <see cref="Sub60MovingSlowPositionSetCommand"/>
	/// event that is raised by the server when a client is moving slow/walking.
	/// </summary>
	public sealed class BlockMovingSlowPositionChangedEventHandler : ContextExtendedCommand60Handler<Sub60MovingSlowPositionSetCommand, INetworkPlayerCommandMessageContext>
	{
		/// <summary>
		/// Service that translates the incoming position to the correct unit scale that
		/// Unity3D expects.
		/// </summary>
		[Required]
		[OdinSerialize]
		private IUnitScalerStrategy Scaler { get; set; }

		/// <inheritdoc />
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60MovingSlowPositionSetCommand command, INetworkPlayerCommandMessageContext commandContext)
		{
			//This is for visuallizing the result
			commandContext.Remote.Transform.Position = Scaler.Scale(command.Position.ToUnityVector3XZ(commandContext.Remote.Transform.Position.y));

			return Task.CompletedTask;
		}
	}
}
