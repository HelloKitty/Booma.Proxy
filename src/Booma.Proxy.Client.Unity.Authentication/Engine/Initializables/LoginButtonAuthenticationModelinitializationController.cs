using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using GladNet;
using Guardians;

namespace Booma.Proxy
{
	/// <summary>
	/// UI Controller that registers auth model initialization
	/// on login button click.
	/// </summary>
	[SceneTypeCreate(GameSceneType.TitleScreen)]
	public sealed class LoginButtonAuthenticationModelinitializationController : IGameInitializable
	{
		/// <summary>
		/// UI element reference for the username.
		/// </summary>
		private IUIText UsernameTextElement { get; }

		/// <summary>
		/// UI element reference for the password.
		/// </summary>
		private IUIText PasswordTextElement { get; }

		/// <summary>
		/// UI element reference for the login button.
		/// </summary>
		private IUIButton LoginButtonElement { get; }

		private IAuthenticationDetailsModel AuthenticationModel { get; }

		/// <inheritdoc />
		public LoginButtonAuthenticationModelinitializationController(
			[KeyFilter(UnityUIRegisterationKey.TitleLoginUsername)] [NotNull] IUIText usernameTextElement, 
			[KeyFilter(UnityUIRegisterationKey.TitleLoginPassword)] [NotNull] IUIText passwordTextElement, 
			[KeyFilter(UnityUIRegisterationKey.TitleLoginButton)] [NotNull] IUIButton loginButtonElement,
			[NotNull] IAuthenticationDetailsModel authenticationModel)
		{
			UsernameTextElement = usernameTextElement ?? throw new ArgumentNullException(nameof(usernameTextElement));
			PasswordTextElement = passwordTextElement ?? throw new ArgumentNullException(nameof(passwordTextElement));
			LoginButtonElement = loginButtonElement ?? throw new ArgumentNullException(nameof(loginButtonElement));
			AuthenticationModel = authenticationModel ?? throw new ArgumentNullException(nameof(authenticationModel));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			LoginButtonElement.AddOnClickListener(OnLoginButtonPressed);
			//We can press the button now after registeration button callbacks properly
			LoginButtonElement.IsInteractable = true;

			return Task.CompletedTask;
		}

		private void OnLoginButtonPressed()
		{
			AuthenticationModel.Username = UsernameTextElement.Text;
			AuthenticationModel.Password = PasswordTextElement.Text;

			//Also disable the button interaction.
			LoginButtonElement.IsInteractable = false;
		}
	}
}
