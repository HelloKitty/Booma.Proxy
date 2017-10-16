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
	/// Handler that deals with the <see cref="Sub60MovingFastPositionSetCommand"/>
	/// event that is raised by the server when a client is moving fast/running.
	/// </summary>
	[Injectee]
	public sealed class BlockMovingFastPositionChangedEventHandler : ClientAssociatedCommand60Handler<Sub60MovingFastPositionSetCommand>
	{
		/// <summary>
		/// Service that translates the incoming position to the correct unit scale that
		/// Unity3D expects.
		/// </summary>
		[Required]
		[OdinSerialize]
		private IUnitScalerStrategy Scaler { get; set; }

		/// <inheritdoc />
		protected override Task HandleClientMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60MovingFastPositionSetCommand command, INetworkPlayer player)
		{
			//Set the position of the network transform
			player.Transform.Position = Scaler.Scale(command.Position.ToUnityVector3XZ(player.Transform.Position.y));

			return Task.CompletedTask;
		}
	}
}
