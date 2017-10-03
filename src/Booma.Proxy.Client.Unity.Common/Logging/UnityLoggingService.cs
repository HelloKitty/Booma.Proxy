using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Common.Logging.Simple;

namespace Booma.Proxy
{
	//From: https://github.com/BoomaNation/Booma.Library/blob/master/src/Booma.Unity.Common/Logging/UnityLoggingService.cs
	/// <summary>
	/// Simple Unity3D logger that logs through the <see cref="UnityEngine.Debug"/>
	/// logging API. Conforms to the <see cref="ILog"/> <see cref="Common.Logging"/> interface
	/// specification and implementation.
	/// </summary>
	public class UnityLoggingService : AbstractSimpleLogger
	{
		public UnityLoggingService()
			: base("UnityLogger", LogLevel.All, false, false, false, "")
		{

		}

		public UnityLoggingService(LogLevel level)
			: base("UnityLogger", level, false, false, false, "")
		{

		}

		private UnityLoggingService(string logName, LogLevel logLevel, bool showlevel, bool showDateTime, bool showLogName, string dateTimeFormat)
			: base(logName, logLevel, showlevel, showDateTime, showLogName, dateTimeFormat)
		{
			//None of these parameters matter.
		}

		protected override void WriteInternal(LogLevel level, object message, Exception exception)
		{
			switch(level)
			{
				case LogLevel.Trace:
					UnityEngine.Debug.Log(message);
					break;
				case LogLevel.Debug:
					UnityEngine.Debug.Log(message);
					break;
				case LogLevel.Info:
					UnityEngine.Debug.Log(message);
					break;
				case LogLevel.Warn:
					UnityEngine.Debug.LogWarning(message);
					break;
				case LogLevel.Error:
					UnityEngine.Debug.LogError(message);
					break;
				case LogLevel.Fatal:
					UnityEngine.Debug.LogError(message);
					break;
				case LogLevel.Off:
					//Do nothing if the logging is disabled
					break;
				case LogLevel.All:
					//TODO: What should we do here?
					break;
				default:
					UnityEngine.Debug.Log(message);
					break;
			}
		}
	}
}