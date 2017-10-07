using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Common.Logging;
using SceneJect.Common;
using Sirenix.OdinInspector;

namespace Booma.Proxy
{
	/// <summary>
	/// Abstract base network client for Unity3D.
	/// </summary>
	/// <typeparam name="TIncomingPayloadType"></typeparam>
	/// <typeparam name="TOutgoingPayloadType"></typeparam>
	public abstract class BaseUnityNetworkClient<TIncomingPayloadType, TOutgoingPayloadType> : SerializedMonoBehaviour
		where TOutgoingPayloadType : class 
		where TIncomingPayloadType : class
	{
		/// <summary>
		/// The managed network client that the Unity3D client is implemented on-top of.
		/// </summary>
		[Inject]
		protected IManagedNetworkClient<TOutgoingPayloadType, TIncomingPayloadType> Client { get; }

		/// <summary>
		/// The message handler service.
		/// </summary>
		[Inject]
		protected MessageHandlerService<TIncomingPayloadType, TOutgoingPayloadType> Handlers { get; }

		/// <summary>
		/// The logger for the client.
		/// </summary>
		[Inject]
		public ILog Logger { get; }

		/// <summary>
		/// The message context factory that builds the contexts
		/// for the handlers.
		/// </summary>
		[Inject]
		protected IClientMessageContextFactory MessageContextFactory { get; }

		/// <summary>
		/// Starts dispatching the messages and won't yield until
		/// the client has stopped or has disconnected.
		/// </summary>
		/// <returns></returns>
		protected async Task StartDispatchingAsync()
		{
			try
			{
				while(Client.isConnected)
				{
					if(Logger.IsDebugEnabled)
						Logger.Debug("Reading message.");

					PSOBBNetworkIncomingMessage<TIncomingPayloadType> message = await Client.ReadMessageAsync()
						.ConfigureAwait(true);

					bool result = await Handlers.TryHandleMessage(MessageContextFactory.Create(Client, Client), message);

					if(!result && Logger.IsInfoEnabled)
						Logger.Info($"Handlers failed to handle message of Type: {message.Payload.GetType().Name}");
				}
			}
			catch(Exception e)
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Error: {e.Message}\n\n Stack Trace: {e.StackTrace}");

				throw;
			}
		}
	}
}
