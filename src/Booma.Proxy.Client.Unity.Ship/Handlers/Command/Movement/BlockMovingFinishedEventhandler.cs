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
	public sealed class BlockMovingFinishedEventhandler : Command60Handler<Sub60FinishedMovingCommand>
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

		public int RotationScale = 180;

		/// <inheritdoc />
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60FinishedMovingCommand command)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Recieved EndPosition With Y: {command.Position.Y} and W: {command.W} RawRot: {command.YAxisRotation}");

			//This is for visuallizing the result
			//Try using the Y for this one
			TestObject.transform.position = Scaler.Scale(new Vector3(command.Position.X, command.Position.Y, command.Position.Z));

			//Also set the rotation
			TestObject.transform.rotation = Quaternion.AngleAxis(command.YAxisRotation / 180f, Vector3.up);

			return Task.CompletedTask;
		}
	}
}
