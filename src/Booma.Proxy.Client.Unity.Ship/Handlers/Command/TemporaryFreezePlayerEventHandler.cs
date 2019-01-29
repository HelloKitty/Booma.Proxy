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
	public sealed class TemporaryFreezePlayerEventHandler : Command60Handler<Sub60PlayerFreezeCommand>
	{
		/// <inheritdoc />
		public TemporaryFreezePlayerEventHandler(ILog logger) 
			: base(logger)
		{

		}

		/// <inheritdoc />
		protected override Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, Sub60PlayerFreezeCommand command)
		{
			if(this.Logger.IsDebugEnabled)
				Logger.Debug($"Freeze ClientId: {command.Identifier} Unknown: {command.Unknown1} Type: {command.Reason} Location1: {command.Position} Unknown2: {command.Unknown2}");

			return Task.CompletedTask;
		}
	}
}
