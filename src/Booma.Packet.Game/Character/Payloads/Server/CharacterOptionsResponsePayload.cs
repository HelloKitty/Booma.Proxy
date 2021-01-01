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
	/// Payload that responds to <see cref="CharacterOptionsRequestPayload"/> with options saved on the
	/// server for the specified client.
	/// </summary>
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.BB_OPTION_CONFIG_TYPE)]
	public sealed partial class CharacterOptionsResponsePayload : PSOBBGamePacketPayloadServer
	{
		[WireMember(1)]
		public CharacterOptionsConfiguration Config { get; internal set; }

		public CharacterOptionsResponsePayload(CharacterOptionsConfiguration config) 
			: this()
		{
			Config = config ?? throw new ArgumentNullException(nameof(config));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterOptionsResponsePayload()
			: base(GameNetworkOperationCode.BB_OPTION_CONFIG_TYPE)
		{
			
		}
	}
}
