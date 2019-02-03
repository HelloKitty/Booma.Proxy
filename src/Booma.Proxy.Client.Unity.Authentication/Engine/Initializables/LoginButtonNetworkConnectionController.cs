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
	[SceneTypeCreate(GameSceneType.TitleScreen)]
	public sealed class LoginButtonNetworkConnectionController : IGameInitializable
	{
		/// <summary>
		/// UI element reference for the login button.
		/// </summary>
		private IUIButton LoginButtonElement { get; }

		private IGameConnectionEndpointDetails ConnectDetails { get; }

		private IConnectionService ConnectionService { get; }

		/// <inheritdoc />
		public LoginButtonNetworkConnectionController(
			[KeyFilter(UnityUIRegisterationKey.TitleLoginButton)] [NotNull] IUIButton loginButtonElement, 
			[NotNull] IGameConnectionEndpointDetails connectDetails,
			IConnectionService connectionService)
		{
			LoginButtonElement = loginButtonElement ?? throw new ArgumentNullException(nameof(loginButtonElement));
			ConnectDetails = connectDetails ?? throw new ArgumentNullException(nameof(connectDetails));
			ConnectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			LoginButtonElement.AddOnClickListenerAsync(OnLoginButtonPresedAsync);
			return Task.CompletedTask;
		}

		private Task OnLoginButtonPresedAsync()
		{
			//Assume that the connection details of this model have been validated before we
			//reach this point, not this objects responsiability
			return ConnectionService.ConnectAsync(ConnectDetails.IpAddress, ConnectDetails.Port);
		}
	}
}
