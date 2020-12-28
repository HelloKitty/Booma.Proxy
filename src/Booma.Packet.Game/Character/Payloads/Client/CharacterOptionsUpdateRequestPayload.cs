using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	//See: https://github.com/Sylverant/libsylverant/blob/7f7e31d90da1b02c8d89d055628540ee3ad59417/include/sylverant/characters.h#L167
	//Based on Sylverant's sylverant_bb_key_team_config_t
	/// <summary>
	/// Payload that is similar to <see cref="CharacterOptionsResponsePayload"/> with options data.
	/// Sent by the client in cases including when <see cref="CharacterOptionsResponsePayload"/> data is incorrect.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.BB_OPTION_CONFIG_TYPE)]
	public sealed partial class CharacterOptionsUpdateRequestPayload : PSOBBGamePacketPayloadClient
	{
		/// <summary>
		/// Unknown bytes.
		/// </summary>
		[KnownSize(276)]
		[WireMember(1)]
		internal byte[] unk { get; set; } = Array.Empty<byte>();

		/// <summary>
		/// Binding configuration.
		/// </summary>
		[WireMember(2)]
		public BindingsConfig Bindings { get; internal set; }

		/// <summary>
		/// The guild card for the account.
		/// </summary>
		[WireMember(3)]
		public uint GuildCard { get; internal set; }

		/// <summary>
		/// The team information.
		/// </summary>
		[WireMember(4)]
		public AccountTeamInformation TeamInfo { get; internal set; }

		public CharacterOptionsUpdateRequestPayload(BindingsConfig bindings, uint guildCard, AccountTeamInformation teamInfo) 
			: this()
		{
			Bindings = bindings;
			GuildCard = guildCard;
			TeamInfo = teamInfo;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterOptionsUpdateRequestPayload()
			: base(GameNetworkOperationCode.BB_OPTION_CONFIG_TYPE)
		{
			
		}
	}
}
