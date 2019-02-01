using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using UnityEngine;
using Unitysync.Async;

namespace Booma.Proxy
{
	/// <summary>
	/// Redirects the connection to the connection
	/// stored in the connection details model.
	/// </summary>
	public sealed class ConnectionRedirector : MonoBehaviour
	{
		void Awake()
		{
			throw new NotSupportedException($"Used deprecated: {nameof(ConnectionRedirector)} On: {gameObject.name}");
		}

		/// <summary>
		/// Redirects the connection to the details in
		/// the <see cref="ConnectionDetails"/> model.
		/// </summary>
		public void Redirect()
		{
			throw new NotSupportedException($"Used deprecated: {nameof(ConnectionRedirector)} On: {gameObject.name}");
		}
	}
}
