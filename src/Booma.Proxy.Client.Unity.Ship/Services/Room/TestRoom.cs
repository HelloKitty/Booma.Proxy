using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	public class TestRoom : MonoBehaviour, IRoom
	{
		/// <inheritdoc />
		[OdinSerialize]
		public short RoomId { get; private set; }

		private IDictionary<int, INetworkPlayer> Players { get; } = new Dictionary<int, INetworkPlayer>();

		/// <inheritdoc />
		public bool TryEnter(INetworkPlayer player)
		{
			//TODO: Do this for real
			Players.Add(player.Identity.EntityId, player);

			return true;
		}

		/// <inheritdoc />
		public bool TryExit(INetworkPlayer player)
		{
			//TODO: Do this for real
			Players.Remove(player.Identity.EntityId);

			return true;
		}
	}
}
