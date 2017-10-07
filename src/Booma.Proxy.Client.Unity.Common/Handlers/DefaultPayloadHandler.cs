using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using SceneJect.Common;

namespace Booma.Proxy
{
	/// <summary>
	/// Default payload handler that can handle all payload types
	/// and logs information about the message.
	/// </summary>
	/// <typeparam name="TPayloadType"></typeparam>
	/// <typeparam name="TOutgoingPayloadType"></typeparam>
	[Injectee]
	public sealed class DefaultPayloadHandler<TPayloadType, TOutgoingPayloadType> : IClientPayloadSpecificMessageHandler<TPayloadType, TOutgoingPayloadType> 
		where TPayloadType : class 
		where TOutgoingPayloadType : class
	{
		[Inject]
		private ILog Logger { get; }

		/// <inheritdoc />
		public Task HandleMessage(IClientMessageContext<TOutgoingPayloadType> context, TPayloadType payload)
		{
			//TODO: We can disconnect if we encounter unknowns or do more indepth logging/decisions
			if(Logger.IsInfoEnabled)
				if(payload is IUnknownPayloadType unk)
					Logger.Info($"Recieved unhandled payload of Type: {payload.GetType().Name} OpCode: {unk}");
				else
					Logger.Info($"Recieved unhandled payload of Type: {payload.GetType().Name}");

			return Task.CompletedTask;
		}
	}
}
