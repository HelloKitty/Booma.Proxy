using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class LobbySoccerBallKickTrigger : NetworkPlayerTrigger
	{
		[Inject]
		private IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }
		
		[Inject]
		private ILog Logger { get; }

		[Inject]
		private IUnitScalerStrategy Scaler { get; }

		private float BirthTime;

		private void Start()
		{
			BirthTime = Time.time;
		}

		/// <inheritdoc />
		public override void OnPlayerEnter(INetworkPlayer player)
		{
			if(!player.isLocalPlayer)
				return;

			Logger.Info("About to send kick packet");

			//See: http://answers.unity3d.com/questions/15822/how-to-get-the-positive-or-negative-angle-between.html
			float angle = AngleSigned(Vector3.forward, (player.Transform.Position - transform.position), Vector3.up);

			Logger.Info($"Angle of Kick: {angle}");

			//Frames since birth; PSOBB expects 30fps
			int frames = (int)(30.0f * (Time.time - BirthTime));

			SendService.SendMessage(new Sub60LobbySoccerBallMoveEventPayload((byte)player.Identity.EntityId, (short)frames, Scaler.UnScaleYRotation(angle),
				Scaler.UnScaleYtoZ(transform.position)).ToPayload());

			//Also kick the ball on the client
			GetComponent<IKickable>().Kick(transform.position, angle);
		}

		//See: https://forum.unity.com/threads/need-vector3-angle-to-return-a-negtive-or-relative-value.51092/#post-324018
		/// <summary>
		/// Determine the signed angle between two vectors, with normal 'n'
		/// as the rotation axis.
		/// </summary>
		public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
		{
			return Mathf.Atan2(
				Vector3.Dot(n, Vector3.Cross(v1, v2)),
				Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
		}

		/// <inheritdoc />
		public override void OnPlayerExit(INetworkPlayer player)
		{

		}
	}
}
