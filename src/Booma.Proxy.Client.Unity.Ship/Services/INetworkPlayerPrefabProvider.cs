using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public interface INetworkPlayerPrefabProvider
	{
		GameObject LocalPlayerPrefab { get; }

		GameObject RemotePlayerPrefab { get; }
	}
}
