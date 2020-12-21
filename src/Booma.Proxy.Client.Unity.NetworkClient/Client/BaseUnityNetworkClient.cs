﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Common.Logging;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	public static class GlobalNetwork
	{
		internal static INetworkClientExportable CurrentExportableClient { get; set; }

		internal static IConnectionService CurrentConnectionService { get; set; }
	}

	/// <summary>
	/// Abstract base network client for Unity3D.
	/// </summary>
	/// <typeparam name="TIncomingPayloadType"></typeparam>
	/// <typeparam name="TOutgoingPayloadType"></typeparam>
	[Injectee]
	public abstract class BaseUnityNetworkClient<TIncomingPayloadType, TOutgoingPayloadType> : MonoBehaviour, INetworkClientExportable, IConnectionService
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
		protected MessageHandlerService<TIncomingPayloadType, TOutgoingPayloadType> Handlers { get; set; }

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
		protected IPeerMessageContextFactory MessageContextFactory { get; }

		[Inject]
		private IGameObjectComponentAttachmentFactory AttachmentFactory { get; }

		//TODO: Move to IoC
		private IPeerRequestSendService<TOutgoingPayloadType> RequestService { get; set; }

		//TODO: Re-enable export showing in inspector
		/// <summary>
		/// Indicates if the managed client has been exported from this container.
		/// </summary>
		//[ReadOnly]
		//[ShowInInspector]
		public bool isClientExported { get; private set; }

		/// <summary>
		/// The token source for canceling the read message await.
		/// </summary>
		protected CancellationTokenSource CancelTokenSource { get; } = new CancellationTokenSource();

		private void Awake()
		{
			Debug.Log($"Setting global network services.");

			//TODO: This is sooooooo a hack but I don't have it in me to change this.
			//Whenever we begin existence, we become the new exportable client
			GlobalNetwork.CurrentExportableClient = this;
			GlobalNetwork.CurrentConnectionService = this;
		}

		/// <summary>
		/// Starts dispatching the messages and won't yield until
		/// the client has stopped or has disconnected.
		/// </summary>
		/// <returns></returns>
		protected async Task StartDispatchingAsync()
		{
			try
			{
				RequestService = new PayloadInterceptMessageSendService<TOutgoingPayloadType>(Client, Client);

				if(!Client.isConnected && Logger.IsWarnEnabled)
					Logger.Warn($"The client {name} was not connected before dispatching started.");

				while(Client.isConnected && !isClientExported) //if we exported we should reading messages
				{
					if(Logger.IsDebugEnabled)
						Logger.Debug("Reading message.");

					NetworkIncomingMessage<TIncomingPayloadType> message = await Client.ReadMessageAsync(CancelTokenSource.Token)
						.ConfigureAwait(true);

					//Supress and continue reading
					try
					{
						//We don't do anything with the result. We should hope someone registered
						//a default handler to deal with this situation
						bool result = await Handlers.TryHandleMessage(MessageContextFactory.Create(Client, Client, RequestService), message)
							.ConfigureAwait(true);
					}
					catch(Exception e)
					{
						if(Logger.IsDebugEnabled)
							Logger.Debug($"Error: {e.Message}\n\n Stack Trace: {e.StackTrace}");
					}
					
				}
			}
			catch(Exception e)
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Error: {e.Message}\n\n Stack Trace: {e.StackTrace}");

				throw;
			}

			if(Logger.IsDebugEnabled)
				Logger.Debug("Network client stopped reading.");
		}

		/// <summary>
		/// Exports a <see cref="NonBehaviourDependency"/> for the network client.
		/// Enabling it for scene persistence.
		/// </summary>
		public void ExportmanagedClient()
		{
			//We should cancel the networking
			//since we're going to export this.
			CancelTokenSource.Cancel();

			GameObject exportClientObject = new GameObject(@"[ExportedNetClient]");

			AttachmentFactory
				.AddTo<ExportedClientDependencyRegisterModule>(exportClientObject);

			//We don't want this client to be destroyed
			DontDestroyOnLoad(exportClientObject);
			isClientExported = true;
		}

		protected virtual void OnApplicationQuit()
		{
			if(!CancelTokenSource.IsCancellationRequested)
				CancelTokenSource.Cancel();

			if(!isClientExported)
				Client?.Disconnect();
		}

		protected virtual void OnDestroy()
		{
			if(!CancelTokenSource.IsCancellationRequested)
				CancelTokenSource.Cancel();
		}

		protected void CreateDispatchTask()
		{
			//Don't await because we want start to end.
			Task.Factory.StartNew(StartDispatchingAsync, CancelTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext())
				.ConfigureAwait(true);
		}

		/// <inheritdoc />
		public async Task<bool> ConnectAsync(string ip, int port)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug("Starting game client");

			//As soon as we start we should attempt to connect to the login server.
			bool result = await Client.ConnectAsync(ip, port)
				.ConfigureAwait(true);

			if(!result)
				throw new InvalidOperationException($"Failed to connect to Server: {ip} Port: {port}");

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Connected client. isConnected: {Client.isConnected}");

			CreateDispatchTask();
			return true;
		}

		/// <inheritdoc />
		public Task DisconnectAsync(int delay)
		{
			CancelTokenSource.Cancel();
			return Client.DisconnectAsync(delay);
		}

		/// <inheritdoc />
		public bool isConnected => Client.isConnected;
	}
}
