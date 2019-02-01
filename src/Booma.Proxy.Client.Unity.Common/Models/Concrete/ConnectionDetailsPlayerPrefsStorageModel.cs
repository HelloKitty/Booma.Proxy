using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	public abstract class ConnectionDetailsPlayerPrefsStorageModel : MonoBehaviour, IGameConnectionEndpointDetails
	{
		[ReadOnly]
		[ShowInInspector]
		private string _IpAddress;

		[ReadOnly]
		[ShowInInspector]
		private int _Port;

		/// <inheritdoc />
		public int Port
		{
			get => _Port;
			set
			{
				_Port = value;
				SavePort();
			}
		}

		/// <inheritdoc />
		public string IpAddress
		{
			get => _IpAddress;
			set
			{
				_IpAddress = value;
				SaveIpAddress();
			}
		}

		public void SaveIpAddress()
		{
			PlayerPrefs.SetString(nameof(IpAddress), IpAddress);
		}

		public void SavePort()
		{
			PlayerPrefs.SetInt(nameof(Port), Port);
		}

		/// <summary>
		/// Loads and initializes the prefs from storage.
		/// </summary>
		protected void InitializeFromPrefs()
		{
			_Port = PlayerPrefs.GetInt(nameof(Port), 0);
			_IpAddress = PlayerPrefs.GetString(nameof(IpAddress), null);
		}
	}
}
