using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public interface ILocalPlayerData
	{
		byte SlotIndex { get; }

		int EntityGuid { get; }

		bool isWorldObjectSpawned { get; }

		GameObject WorldObject { get; }
	}
}
