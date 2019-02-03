using SceneJect.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using GladNet;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// The component that manages the game network client.
	/// </summary>
	[Injectee]
	public sealed class GameNetworkClient : BaseUnityNetworkClient<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>, IConnectionService
	{
		internal static INetworkClientExportable CurrentExportableClient { get; private set; }

		internal static IConnectionService CurrentConnectionService { get; private set; }

		private void Awake()
		{
			//TODO: This is sooooooo a hack but I don't have it in me to change this.
			//Whenever we begin existence, we become the new exportable client
			CurrentExportableClient = this;
			CurrentConnectionService = this;
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
