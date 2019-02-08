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
	public sealed class GameNetworkClient : BaseUnityNetworkClient<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>
	{

	}
}
