using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Reinterpret.Net;
using UnityEngine;

namespace Booma.Proxy
{
	public abstract class SessionDetailsPlayerPrefsStorageModel : MonoBehaviour, ILoginSessionDetails
	{
		private byte[] _sessionVerificationData;

		private int _sessionId = 0;
		private uint _guildCardNumber;

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

		/// <inheritdoc />
		public uint GuildCardNumber
		{
			get => _guildCardNumber;
			set
			{
				_guildCardNumber = value;
				SaveGuildCardNumber();
			}
		}

		private void SaveGuildCardNumber()
		{
			//Just save the id
			PlayerPrefs.SetInt(nameof(GuildCardNumber), GuildCardNumber.Reinterpret().Reinterpret<int>());
		}

		/// <summary>
		/// Saves the session id to storage.
		/// </summary>
		protected void SaveSessionId()
		{
			//Just save the id
			PlayerPrefs.SetInt(nameof(SessionId), SessionId);
		}

		/// <summary>
		/// Saved the verification data to storage.
		/// </summary>
		protected void SaveVerificationData()
		{
			//Just serialize the bytes to JSON and save it. Loaders will need to deserialize the JSON to bytes.
			PlayerPrefs.SetString(nameof(SessionVerificationData), JsonConvert.SerializeObject(SessionVerificationData));
		}

		/// <summary>
		/// Loads and initializes the prefs from storage.
		/// </summary>
		protected void InitializeFromPrefs()
		{
			_guildCardNumber = PlayerPrefs.GetInt(nameof(GuildCardNumber), 0)
				.Reinterpret()
				.Reinterpret<uint>();

			_sessionId = PlayerPrefs.GetInt(nameof(SessionId), 0);
			_sessionVerificationData = JsonConvert.DeserializeObject<byte[]>(PlayerPrefs.GetString(nameof(SessionVerificationData), null));
		}
	}
}
