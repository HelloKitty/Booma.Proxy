using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Data model for the <see cref="IClientSessionDetails"/>
	/// </summary>
	public sealed class VersionStringBasedSessionDetailsModel : SessionDetailsPlayerPrefsStorageModel
	{
		[Tooltip("The version string of the original PSOBB client that the server expects.")]
		[SerializeField]
		private string VersionString;

		private void Awake()
		{
			//Verify the set version string.
			if(String.IsNullOrWhiteSpace(VersionString))
				throw new InvalidOperationException($"{nameof(VersionString)} cannot be null or empty.");

			SessionVerificationData = ClientVerificationData.FromVersionString(VersionString).SecurityData;
			SessionId = 0;
		}
	}
}
