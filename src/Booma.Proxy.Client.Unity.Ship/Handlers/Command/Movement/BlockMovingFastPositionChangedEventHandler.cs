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
	public sealed class BlockMovingFastPositionChangedEventHandler : Command60Handler<Sub60MovingFastPositionSetCommand>
	{
		/// <summary>
		/// The indextable collection of <see cref="INetworkPlayer"/>s.
		/// </summary>
		[Inject]
		private INetworkPlayerCollection PlayerCollection { get; }

		/// <summary>
		/// Service that translates the incoming position to the correct unit scale that
		/// Unity3D expects.
		/// </summary>
		[Required]
		[OdinSerialize]
		private IUnitScalerStrategy Scaler { get; set; }

		/// <inheritdoc />
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60MovingFastPositionSetCommand command)
		{
			//Try to get the player from the collection
			INetworkPlayer player = PlayerCollection[command.ClientId];

			//Not sure if it's possible to encounter this but we should check to be sure
			if(player == null)
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Recieved {this.MessageName()} 0x60 {command.CommandOperationCode:X} from unregistered ClientId: {command.ClientId}");

				return Task.CompletedTask;
			}

			//Set the position of the network transform
			player.Transform.Position = Scaler.Scale(command.Position.ToUnityVector3XZ(player.Transform.Position.y));

			return Task.CompletedTask;
		}
	}
}
