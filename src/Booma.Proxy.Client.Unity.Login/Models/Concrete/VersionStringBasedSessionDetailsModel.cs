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
	/// Data model for the <see cref="ILoginSessionDetails"/>
	/// </summary>
	public sealed class VersionStringBasedSessionDetailsModel : MonoBehaviour, ILoginSessionDetails
	{
		[Tooltip("The version string of the original PSOBB client that the server expects.")]
		[SerializeField]
		private string VersionString;

		//The original session verification data will be string based
		//on the string of the client
		private byte[] _sessionVerificationData;

		//The version string session details always has a session id of 0.
		private int _sessionId = 0;

		/// <inheritdoc />
		public int SessionId
		{
			get => _sessionId;
			set
			{
				_sessionId = value; 
				SaveSessionId();
			}
		}

		/// <inheritdoc />
		public byte[] SessionVerificationData
		{
			get => _sessionVerificationData;
			set
			{
				_sessionVerificationData = value;
				SaveVerificationData();
			}
		}

		private void Start()
		{
			//Verify the set version string.
			if(String.IsNullOrWhiteSpace(VersionString))
				throw new InvalidOperationException($"{nameof(VersionString)} cannot be null or empty.");
		}

		private void SaveSessionId()
		{
			//Just save the id
			PlayerPrefs.SetInt(nameof(SessionId), SessionId);
		}

		private void SaveVerificationData()
		{
			//Just serialize the bytes to JSON and save it. Loaders will need to deserialize the JSON to bytes.
			PlayerPrefs.SetString(nameof(SessionVerificationData), JsonConvert.SerializeObject(SessionVerificationData));
		}
	}
}
