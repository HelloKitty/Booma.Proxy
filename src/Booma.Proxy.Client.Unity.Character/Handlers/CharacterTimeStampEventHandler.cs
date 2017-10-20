using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class CharacterTimeStampEventHandler : GameMessageHandler<CharacterTimestampEventPayload>
	{
		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, CharacterTimestampEventPayload payload)
		{
			//TODO: How should we account for latency between the server
			//See: https://en.wikipedia.org/wiki/Swatch_Internet_Time#Calculation_from_UTC.2B1
			DateTime serverTime = DateTimeOffset
				.Parse(payload.Timestamp)
				.UtcDateTime
				.ToUniversalTime();

			//Set the start beat time for use during this session
			//beats = (UTC+1seconds + (UTC+1minutes * 60) + (UTC+1hours * 3600)) / 86.4
			TimeService.StartBeatsTime = (serverTime.Second + (serverTime.Minute * 60) + (serverTime.Hour * 3600)) / 86.4d;

			if(Logger.IsInfoEnabled)
				Logger.Info($"CurrentBeat: {TimeService.StartBeatsTime % 1000}");

			return Task.CompletedTask;
		}
	}
}
