using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;

namespace Booma.Proxy
{
	/// <summary>
	/// Session network client that continues a <see cref="IManagedNetworkClient{TPayloadWriteType,TPayloadReadType}"/>'s session.
	/// Unlike the <see cref="GameNetworkClient"/> this client doesn't require connection details or stetup/configuration for connecting.
	/// </summary>
	[Injectee]
	public sealed class SessionNetworkClient : BaseUnityNetworkClient<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>
	{
		public void Start()
		{
			//If we're already connected then it was probably an exported client
			//and we should start reading messages now
			if(Client.isConnected)
			{
				CreateDispatchTask();
			}
			else
				throw new InvalidOperationException($"Cannot use a {nameof(SessionNetworkClient)} with an unconnected {Client.GetType().Name}.");
		}
	}
}
