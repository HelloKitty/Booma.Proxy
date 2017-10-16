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
	/// Handler that deals with the <see cref="Sub60MovingSlowPositionChangedEvent"/>
	/// event that is raised by the server when a client is moving slow/walking.
	/// </summary>
	public sealed class BlockMovingSlowPositionChangedEventHandler : Command60Handler<Sub60MovingSlowPositionSetCommand>
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
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60MovingSlowPositionSetCommand command)
		{
			//Not sure if it's possible to encounter this but we should check to be sure
			if(!PlayerCollection.ContainsId(command.ClientId))
			{
				if(Logger.IsInfoEnabled)
					Logger.Warn($"Recieved Code: {command.OpCodeHexString()} {this.MessageName()} for unknown Id: {command.ClientId}");

				return Task.CompletedTask;
			}

			INetworkPlayer player = PlayerCollection[command.ClientId];

			//This is for visuallizing the result
			/*TestObject.transform.position = Scaler.Scale(new Vector3(command.Position.X, TestObject.transform.position.y, command.Position.Y));

			//Broadcast
			OnPositionChanged?.Invoke(Scaler.ScaleYasZ(new Vector2(command.Position.X, command.Position.Y)));*/

			return Task.CompletedTask;
		}
	}
}
