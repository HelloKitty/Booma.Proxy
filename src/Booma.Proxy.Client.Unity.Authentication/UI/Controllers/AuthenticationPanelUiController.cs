using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Booma.Proxy
{
	/// <summary>
	/// Controller for the login panel UI.
	/// </summary>
	public sealed class AuthenticationPanelUiController : SerializedMonoBehaviour
	{
		[Required]
		[OdinSerialize]
		private IAuthenticationDetailsModel Model { get; set; }

		public void OnUsernameChanged(string userName)
		{
			//TODO: Do validation for view controlling. (Ex. enabling the login button when forms are fully filled)
			Model.Username = userName;
		}

		public void OnPasswordChanged(string password)
		{
			//TODO: Do validation for view controlling. (Ex. enabling the login button when forms are fully filled)
			Model.Password = password;
		}

		//TODO: Should login button hit the controller and we fire off the events?
	}
}
