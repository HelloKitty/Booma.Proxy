using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public interface ILocalPlayerNetworkMovementController
	{
		Task StopMovementAsync(Vector3 position, Quaternion rotation);

		Task UpdatedMovementLocation(Vector3 position, Quaternion rotation);
	}
}
