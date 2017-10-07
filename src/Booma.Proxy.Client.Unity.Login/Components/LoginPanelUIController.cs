using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Controller for the login panel UI.
	/// </summary>
	public sealed class LoginPanelUIController : MonoBehaviour
	{
		//TODO: Extract into a model
		/// <summary>
		/// The currently tracked value of the username.
		/// </summary>
		private string TrackedUsername { get; set; }

		/// <summary>
		/// The currently tracked value of the password.
		/// </summary>
		private string TrackedPassword { get; set; }

		private void Start()
		{
			//Clear the prefs
			PlayerPrefs.DeleteKey("username");
			PlayerPrefs.DeleteKey("password");
		}

		public void OnUsernameChanged(string userName)
		{
			//TODO: Do validation for view controlling. (Ex. enabling the login button when forms are fully filled)
			TrackedUsername = userName;
		}

		public void OnPasswordChanged(string password)
		{
			//TODO: Do validation for view controlling. (Ex. enabling the login button when forms are fully filled)
			TrackedPassword = password;
		}

		//This will occur when the next scene is about to be loaded
		//We want to init the prefs so that they contain the login details
		private void OnDestroy()
		{
			//TODO: We should hash this. We should also protect this system from replay logins
			//Right now we just set with player prefs
			PlayerPrefs.SetString("username", TrackedUsername);
			PlayerPrefs.SetString("password", TrackedPassword);
		}
	}
}
