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
	public sealed class GameNetworkClient : BaseUnityNetworkClient<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>, IConnectable
	{
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
	}
}
