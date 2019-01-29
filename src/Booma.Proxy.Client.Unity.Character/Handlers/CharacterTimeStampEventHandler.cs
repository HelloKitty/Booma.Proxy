using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	public sealed class CharacterTimeStampEventHandler : GameMessageHandler<CharacterTimestampEventPayload>
	{
		/// <inheritdoc />
		public CharacterTimeStampEventHandler(ILog logger) 
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, CharacterTimestampEventPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"TimeStamp: {payload.Timestamp}");

			//TODO: How should we account for latency between the server
			//See: https://en.wikipedia.org/wiki/Swatch_Internet_Time#Calculation_from_UTC.2B1
			//Teht: "%u:%02u:%02u: %02u:%02u:%02u.%03u"
			//rawtime.wYear, rawtime.wMonth, rawtime.wDay, rawtime.wHour, rawtime.wMinute, rawtime.wSecond, rawtime.wMilliseconds

			//Set the start beat time for use during this session
			//beats = (UTC+1seconds + (UTC+1minutes * 60) + (UTC+1hours * 3600)) / 86.4
			//based on: https://stackoverflow.com/questions/10479991/convert-datetime-to-swatch-internet-time-beat-time
			TimeService.StartBeatsTime = DateTime
				.ParseExact(payload.Timestamp, "yyyy:MM:dd: HH:mm:ss.fff", CultureInfo.InvariantCulture)
				.ToUniversalTime()
				.AddHours(-4) //we must remove 4 hours to be in sync with the beats the client reads
				.TimeOfDay
				.TotalSeconds / 86.4d;

			if(Logger.IsInfoEnabled)
				Logger.Info($"CurrentBeat: {TimeService.StartBeatsTime % 1000}");

			return Task.CompletedTask;
		}
	}
}
