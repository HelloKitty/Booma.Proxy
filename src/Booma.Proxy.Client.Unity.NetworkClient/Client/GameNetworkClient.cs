using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// The component that manages the game network client.
	/// </summary>
	[Injectee]
	public sealed class GameNetworkClient : BaseUnityNetworkClient<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>
	{
		/// <summary>
		/// Data model for connection details.
		/// </summary>
		[Inject]
		private IGameConnectionEndpointDetails ConnectionEndpoint { get; }

		[Tooltip("Indicates if the client should connect on Start.")]
		[SerializeField]
		private bool ConnectOnStart = false;

		private void Start()
		{
			//If we're already connected then it was probably an exported client
			//and we should start reading messages now
			if(Client.isConnected)
			{
				CreateDispatchTask();
			}
			else if(ConnectOnStart)
				StartConnection();
		}

		//TODO: Is it safe or ok to await in start without ever completing?
		public void StartConnection()
		{
			//Just start the startup task.
			Task.Factory.StartNew(StartNetworkClient, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext())
				.ConfigureAwait(true);
		}

		//Starts the client by connecting
		//If connection seems to succeed it will continue and startup the full client
		private async Task StartNetworkClient()
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug("Starting game client");

			//As soon as we start we should attempt to connect to the login server.
			bool result = await Client.ConnectAsync(ConnectionEndpoint.IpAddress, ConnectionEndpoint.Port)
				.ConfigureAwait(true);

			if(!result)
				throw new InvalidOperationException($"Failed to connect to Server: {ConnectionEndpoint.IpAddress} Port: {ConnectionEndpoint.Port}");

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Connected client. isConnected: {Client.isConnected}");

			CreateDispatchTask();
		}

		private void CreateDispatchTask()
		{
			//Don't await because we want start to end.
			Task.Factory.StartNew(StartDispatchingAsync, CancelTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext())
				.ConfigureAwait(true);
		}
	}
}
