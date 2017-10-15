using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
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
	public abstract class BaseUnityMessageHandler<TIncomingPayloadBaseType, TOutgoingPayloadType, TPayloadType> : SerializedMonoBehaviour, IClientMessageHandler<TIncomingPayloadBaseType, TOutgoingPayloadType>,
		IClientPayloadSpecificMessageHandler<TPayloadType, TOutgoingPayloadType>
		where TOutgoingPayloadType : class
		where TIncomingPayloadBaseType : class
		where TPayloadType : class, TIncomingPayloadBaseType
	{
		/// <summary>
		/// The actual message handler implementation.
		/// </summary>
		private Lazy<IClientMessageHandler<TIncomingPayloadBaseType, TOutgoingPayloadType>> Handler { get; set; }

		/// <summary>
		/// The message handler logger.
		/// </summary>
		[Inject]
		protected ILog Logger { get; }

		/// <inheritdoc />
		protected override void OnAfterDeserialize()
		{
			//Call to base so the object is fully initialized
			base.OnAfterDeserialize();

			Handler = new Lazy<IClientMessageHandler<TIncomingPayloadBaseType, TOutgoingPayloadType>>(CreateDecoratedHandler, true);
		}

		protected virtual void Start()
		{
			//This just validates that the handler was created
			if(Handler == null)
			{
				ThrowFailedInitialization();
				return;
			}

			IClientMessageHandler<TIncomingPayloadBaseType, TOutgoingPayloadType> handler = null;

			try
			{
				handler = Handler.Value;
			}
			catch(Exception e)
			{
				ThrowFailedInitialization(e);
				throw;
			}

			if(handler == null)
			{
				ThrowFailedInitialization();
				return;
			}
		}

		/// <summary>
		/// Throws an initialization failure exception for this handler.
		/// </summary>
		/// <param name="e">Optional inner-exception.</param>
		private void ThrowFailedInitialization(Exception e = null)
		{
			if(e == null)
				throw new InvalidOperationException($"Failed to create message handler internal dependencies for Type: {GetType().Name}.");
			else
				throw new InvalidOperationException($"Failed to create message handler internal dependencies for Type: {GetType().Name}.", e);
		}

		/// <summary>
		/// Creates the try decorator.
		/// </summary>
		/// <returns>The decorated message handler.</returns>
		private IClientMessageHandler<TIncomingPayloadBaseType, TOutgoingPayloadType> CreateDecoratedHandler()
		{
			return this.AsTryHandler<TPayloadType, TIncomingPayloadBaseType, TOutgoingPayloadType>();
		}

		public abstract Task HandleMessage(IClientMessageContext<TOutgoingPayloadType> context, TPayloadType payload);

		//Just dispatches to the decorated handler.
		/// <inheritdoc />
		public virtual async Task<bool> TryHandleMessage(IClientMessageContext<TOutgoingPayloadType> context, PSOBBNetworkIncomingMessage<TIncomingPayloadBaseType> message)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			if(message == null) throw new ArgumentNullException(nameof(message));

			return await Handler.Value.TryHandleMessage(context, message);
		}
	}
}
