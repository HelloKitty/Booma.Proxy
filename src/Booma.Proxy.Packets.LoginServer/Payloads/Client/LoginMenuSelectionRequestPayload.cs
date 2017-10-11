using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload for selecting a menu option.
	/// </summary>
	[WireDataContract]
	[LoginClientPacketPayload(LoginNetworkOperationCodes.MENU_SELECT_TYPE)]
	public sealed class LoginMenuSelectionRequestPayload : PSOBBLoginPacketPayloadClient
	{
		/// <summary>
		/// The id of the menu selecting from.
		/// </summary>
		[WireMember(1)]
		public MenuItemIdentifier Selection { get; }

		/// <inheritdoc />
		public LoginMenuSelectionRequestPayload([NotNull] MenuItemIdentifier selection)
		{
			if(selection == null) throw new ArgumentNullException(nameof(selection));

			Selection = selection;
		}

		public LoginMenuSelectionRequestPayload()
		{
			
		}
	}
}
