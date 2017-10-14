using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// PlayerPrefs based login details model that loads and saves the
	/// details in playerprefs
	/// </summary>
	public sealed class ScenePlayerPrefsAuthDetailsModel : SerializedMonoBehaviour, IAuthenticationDetailsModel
	{
		//TODO: Should we make this an enum for player prefs?
		private const string PasswordPrefKey = "password";

		private const string UsernamePrefKey = "username";

		/// <inheritdoc />
		public string Username { get; set; }

		/// <inheritdoc />
		public string Password { get; set; }

		/// <summary>
		/// Indicates if the password should be loaded from the prefs.
		/// </summary>
		[OdinSerialize]
		public bool shouldLoadPassword { get; private set; } = false;

		//Tries to load existing values in the prefs
		void Awake()
		{
			Username = PlayerPrefs.GetString(UsernamePrefKey, null);

			//We may not want to load the password. It could be dangerous to do so
			if(shouldLoadPassword)
				Password = PlayerPrefs.GetString(PasswordPrefKey, null);
		}

		/// <inheritdoc />
		public void Clear()
		{
			PlayerPrefs.DeleteKey(UsernamePrefKey);
			PlayerPrefs.DeleteKey(PasswordPrefKey);
		}

		//When another scene is about to be loaded we should save this in player prefs
		void OnDestroy()
		{
			//TODO: This isn't secure to save the password in plaintext like this
			PlayerPrefs.SetString(UsernamePrefKey, Username);
			PlayerPrefs.SetString(PasswordPrefKey, Password);
		}
	}
}
