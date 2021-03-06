using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
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
		[WireMember(1)]
		public CharacterOptionsConfiguration Config { get; internal set; }

		public CharacterOptionsUpdateRequestPayload(CharacterOptionsConfiguration config)
			: this()
		{
			Config = config ?? throw new ArgumentNullException(nameof(config));
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
