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
	/// Handler that deals with the <see cref="Sub60MovingFastPositionChangedEvent"/>
	/// event that is raised by the server when a client is moving fast/running.
	/// </summary>
	public sealed class BlockMovingFastPositionChangedEventHandler : Command60Handler<Sub60MovingFastPositionSetCommand>
	{
		[Required]
		public GameObject TestObject;

		public Vector3 TestScale = new Vector3(1, 1, 1);

		/// <inheritdoc />
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60MovingFastPositionSetCommand payload)
		{
			//This is for visuallizing the result
			TestObject.transform.position = Vector3.Scale(TestScale, new Vector3(payload.Position.X, TestObject.transform.position.y, payload.Position.Y));

			return Task.CompletedTask;
		}
	}
}
