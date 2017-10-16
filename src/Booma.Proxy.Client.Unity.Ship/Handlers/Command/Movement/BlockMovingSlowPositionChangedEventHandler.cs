using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
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

		public Vector3 TestScale = new Vector3(1, 1, 1);

		[SerializeField]
		public OnPositionChangedEvent OnPositionChanged;

		/// <inheritdoc />
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60MovingSlowPositionSetCommand command)
		{
			//This is for visuallizing the result
			TestObject.transform.position = Vector3.Scale(TestScale, new Vector3(command.Position.X, TestObject.transform.position.y, command.Position.Y));

			//Broadcast
			OnPositionChanged?.Invoke(Vector2.Scale(new Vector2(TestScale.x, TestScale.z), new Vector2(command.Position.X, command.Position.Y)));

			return Task.CompletedTask;
		}
	}
}
