using Booma;
using Common.Logging;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
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

		public override void Register(ContainerBuilder register)
		{
			//Just register the service. Let users define the flags.
			//Single instance is preferable though.
			/*register.RegisterInstance(new UnityLoggingService(LoggingLevel))
				.As<ILog>()
				.SingleInstance();*/

			ILoggylyRemoteLoggingService loggingService = Refit.RestService.For<ILoggylyRemoteLoggingService>(@"http://logs-01.loggly.com/inputs/fcf420d4-3164-42ac-82c9-140e2a68bf91/tag/Unity3D");

			register.RegisterInstance(loggingService)
				.AsImplementedInterfaces()
				.SingleInstance();

			//Loggly
			register.Register(context => { return new LogglyLoggingService(LoggingLevel, context.Resolve<ILoggylyRemoteLoggingService>()); })
				.As<ILog>()
				.SingleInstance();

			//This is important to create yielding to Unity3D sync context.
			//TODO: This is a bad place to put it, but every scene has this so maybe it's not
			UnityExtended.InitializeSyncContext();
		}
	}
}