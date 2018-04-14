using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class PlayerHitBoxEventHandler : Command60Handler<Sub60ClientBoxHitEventCommand>
	{
		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60ClientBoxHitEventCommand command)
		{
			if(this.Logger.IsDebugEnabled)
				Logger.Debug($"Encountered BoxHit: {command}");

			//TODO: What should we do?

			return Task.CompletedTask;
		}
	}
}
