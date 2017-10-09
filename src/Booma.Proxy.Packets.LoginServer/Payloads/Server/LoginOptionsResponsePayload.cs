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
	/// Payload that responds to <see cref="LoginOptionsRequestPayload"/> with options saved on the
	/// server for the specified client.
	/// </summary>
	[WireDataContract]
	[LoginServerPacketPayload(LoginNetworkOperationCodes.BB_OPTION_CONFIG_TYPE)]
	public sealed class LoginOptionsResponsePayload : PSOBBLoginPacketPayloadServer
	{
		/// <summary>
		/// Unknown bytes.
		/// </summary>
		[KnownSize(276)]
		[WireMember(1)]
		private byte[] unk { get; }

		/// <summary>
		/// Binding configuration.
		/// </summary>
		[WireMember(2)]
		public BindingsConfig Bindings { get; }

		/// <summary>
		/// The guild card for the account.
		/// </summary>
		[WireMember(3)]
		public uint GuildCard { get; }

		/// <summary>
		/// The team information.
		/// </summary>
		[WireMember(4)]
		public AccountTeamInformation TeamInfo { get; }

		public LoginOptionsResponsePayload()
		{
			
		}
	}
}
