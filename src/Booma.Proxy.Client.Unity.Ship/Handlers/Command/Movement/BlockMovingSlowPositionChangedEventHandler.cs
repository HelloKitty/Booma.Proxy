using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		[Required]
		public GameObject TestObject;

		/// <summary>
		/// Service that translates the incoming position to the correct unit scale that
		/// Unity3D expects.
		/// </summary>
		[Required]
		[OdinSerialize]
		private IUnitScalerStrategy Scaler { get; set; }

		[SerializeField]
		public OnPositionChangedEvent OnPositionChanged;

		/// <inheritdoc />
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60MovingSlowPositionSetCommand command)
		{
			//This is for visuallizing the result
			TestObject.transform.position = Scaler.Scale(new Vector3(command.Position.X, TestObject.transform.position.y, command.Position.Y));

			//Broadcast
			OnPositionChanged?.Invoke(Scaler.ScaleYasZ(new Vector2(command.Position.X, command.Position.Y)));

			return Task.CompletedTask;
		}
	}
}
