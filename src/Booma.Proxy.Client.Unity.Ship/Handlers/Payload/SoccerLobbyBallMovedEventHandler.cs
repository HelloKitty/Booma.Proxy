using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinterpret.Net;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class SoccerLobbyBallMovedEventHandler : Command60Handler<Sub60LobbySoccerBallMoveEventPayload>
	{
		[Inject]
		private IUnitScalerStrategy UnitScaler { get; }

		/// <summary>
		/// The current ball in play.
		/// </summary>
		public GameObject CurrentBall { get; set; }

		public bool addPotentialOffsets = true;

		/// <inheritdoc />
		protected override Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, Sub60LobbySoccerBallMoveEventPayload command)
		{
			//It's possible we joined the lobby after the ball has spawned. If we havw we won't be able to see it.
			if(CurrentBall == null)
				return Task.CompletedTask;

			//TODO: Move forward in direction of the rotation
			//Set the start position of the ball
			IKickable kickable = CurrentBall.GetComponent<IKickable>();

			if(kickable == null)
				throw new InvalidOperationException("Ball was not kickable.");

			kickable.Kick(new Vector3(UnitScaler.ScaleX(command.KickStartPosition.X), CurrentBall.transform.position.y, UnitScaler.ScaleZ(command.KickStartPosition.Y)), 
				UnitScaler.UnScaleYRotation(command.YAxisRotation));

			return Task.CompletedTask;
		}
	}
}
