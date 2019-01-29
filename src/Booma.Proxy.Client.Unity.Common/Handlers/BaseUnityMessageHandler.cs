using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// The base type for the message handlers.
	/// </summary>
	/// <typeparam name="TIncomingPayloadBaseType">The incoming payload type.</typeparam>
	/// <typeparam name="TOutgoingPayloadType">The outgoing payload type.</typeparam>
	/// <typeparam name="TPayloadType">The payload type this handler actually handles.</typeparam>
	[Injectee]
	public abstract class BaseUnityMessageHandler<TIncomingPayloadBaseType, TOutgoingPayloadType, TPayloadType> : IPeerMessageHandler<TIncomingPayloadBaseType, TOutgoingPayloadType>,
		IPeerPayloadSpecificMessageHandler<TPayloadType, TOutgoingPayloadType>
		where TOutgoingPayloadType : class
		where TIncomingPayloadBaseType : class
		where TPayloadType : class, TIncomingPayloadBaseType
	{
		/// <summary>
		/// The message handler logger.
		/// </summary>
		[Inject]
		protected ILog Logger { get; }

		public abstract Task HandleMessage(IPeerMessageContext<TOutgoingPayloadType> context, TPayloadType payload);

		/// <inheritdoc />
		public virtual bool CanHandle(NetworkIncomingMessage<TIncomingPayloadBaseType> message)
		{
			//We can't handle it if the payload type
			return message.Payload is TPayloadType;
		}

		//Just dispatches to the decorated handler.
		/// <inheritdoc />
		public virtual async Task<bool> TryHandleMessage(IPeerMessageContext<TOutgoingPayloadType> context, NetworkIncomingMessage<TIncomingPayloadBaseType> message)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			if(message == null) throw new ArgumentNullException(nameof(message));

			try
			{
				if(CanHandle(message))
				{
					Logger.Info($"Recieved: {message.Payload}");

					await HandleMessage(context, message.Payload as TPayloadType);
					return true;
				}
					return false;
			}
			catch(Exception e)
			{
				Logger.Error($"Encounter error in Handle: {GetType().Name} Exception: {e.Message} \n\n StackTrace: {e.StackTrace}");
				throw;
			}
		}
	}
}
