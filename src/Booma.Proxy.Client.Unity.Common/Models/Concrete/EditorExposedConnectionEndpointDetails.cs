using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Exposes to the editor the ability to set the details.
	/// </summary>
	public sealed class EditorExposedConnectionEndpointDetails : ConnectionDetailsPlayerPrefsStorageModel
	{
		//[Required]
		[SerializeField]
		private string EndpointIpAddress;

		//[MaxValue(short.MaxValue)]
		//[MinValue(1)]
		[SerializeField]
		private int EndpointPort;

		private void Awake()
		{
			//Just init; this will cause a save but it should be ok
			Port = EndpointPort;
			IpAddress = EndpointIpAddress;
		}
	}
}
