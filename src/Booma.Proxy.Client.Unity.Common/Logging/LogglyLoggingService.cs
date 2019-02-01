using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Common.Logging.Simple;

namespace Booma.Proxy
{
	public sealed class LogglyLoggingService : AbstractSimpleLogger
	{
		private ILoggylyRemoteLoggingService LoggingService { get; }

		public LogglyLoggingService([NotNull] ILoggylyRemoteLoggingService loggingService)
			: base("LoggylyLogger", LogLevel.All, false, false, false, "")
		{
			LoggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
		}

		public LogglyLoggingService(LogLevel level, [NotNull] ILoggylyRemoteLoggingService loggingService)
			: base("LoggylyLogger", level, false, false, false, "")
		{
			LoggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
		}

		private LogglyLoggingService(string logName, LogLevel logLevel, bool showlevel, bool showDateTime, bool showLogName, string dateTimeFormat, [NotNull] ILoggylyRemoteLoggingService loggingService)
			: base(logName, logLevel, showlevel, showDateTime, showLogName, dateTimeFormat)
		{
			LoggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
			//None of these parameters matter.
		}

		protected override void WriteInternal(LogLevel level, object message, Exception exception)
		{
			string stringMessage = message is string s ? s : message.ToString();

			if(exception != null)
				stringMessage = $"{message} Exception: {exception.Message}\n\nStack: {exception.StackTrace}";

			LoggingService.LogAsync(new LogglyLoggingModel(level.ToString(), stringMessage));
		}
	}
}
