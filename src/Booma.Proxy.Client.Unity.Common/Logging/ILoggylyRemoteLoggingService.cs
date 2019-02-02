using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Refit;
using UnityEngine;

namespace Booma.Proxy
{
	public interface ILoggylyRemoteLoggingService
	{
		[Post(@"/inputs/fcf420d4-3164-42ac-82c9-140e2a68bf91/tag/Unity3D")]
		Task LogAsync([Body(BodySerializationMethod.UrlEncoded)] LogglyLoggingModel logData);
	}

	public sealed class LogglyLoggingModel
	{
		private static string cachedIdentifier = SystemInfo.deviceUniqueIdentifier;

		[AliasAs("LEVEL")]
		public string Level { get; private set; }

		[AliasAs("MESSAGE")]
		public string Message { get; private set; }

		[AliasAs("ID")]
		public string Identifier { get; private set; } = cachedIdentifier;

		/// <inheritdoc />
		public LogglyLoggingModel([NotNull] string level, [NotNull] string message)
		{
			Level = level ?? throw new ArgumentNullException(nameof(level));
			Message = message ?? throw new ArgumentNullException(nameof(message));
		}
	}
}
