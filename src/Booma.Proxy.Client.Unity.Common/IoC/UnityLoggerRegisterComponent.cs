﻿using Booma;
using Common.Logging;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Registeration component for <see cref="UnityLoggingService"/>
	/// </summary>
	public class UnityLoggerRegisterComponent : NonBehaviourDependency
	{
		/// <summary>
		/// The log level to use for logging.
		/// </summary>
		[SerializeField]
		private LogLevel LoggingLevel;

		public override void Register(IServiceRegister register)
		{
			//Just register the service. Let users define the flags.
			//Single instance is preferable though.
			register.RegisterInstance<UnityLoggingService, ILog>(new UnityLoggingService(LoggingLevel));
		}
	}
}