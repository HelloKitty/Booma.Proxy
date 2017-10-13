using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Enumeration of all the login server
	/// operation codes.
	/// </summary>
	public enum LoginNetworkOperationCodes : short
	{
		//TODO: Format
		BB_SECURITY_TYPE = 0x00E6,
		BB_WELCOME_TYPE = 0x0003,
		REDIRECT_TYPE = 0x0019,
		LOGIN_93_TYPE = 0x0093,
		MESSAGE_BOX_TYPE = 0x001A,
		BB_OPTION_REQUEST_TYPE = 0x00E0,
		BB_OPTION_CONFIG_TYPE = 0x00E2,
		BB_CHARACTER_SELECT_TYPE = 0x00E3,
		BB_CHARACTER_ACK_TYPE = 0x00E4,
		BB_CHARACTER_UPDATE_TYPE = 0x00E5,
		BB_CHECKSUM_TYPE = 0x01E8,
		BB_CHECKSUM_ACK_TYPE = 0x02E8,
		BB_GUILD_REQUEST_TYPE = 0x03E8,

		BB_GUILDCARD_HEADER_TYPE = 0x01DC,
		BB_GUILDCARD_CHUNK_TYPE  = 0x02DC,
		BB_GUILDCARD_CHUNK_REQ_TYPE = 0x03DC,

		BB_PARAM_HEADER_REQ_TYPE = 0x04EB,
		BB_PARAM_HEADER_TYPE = 0x01EB,
		BB_PARAM_CHUNK_REQ_TYPE = 0x03EB,
		BB_PARAM_CHUNK_TYPE = 0x02EB,
		TIMESTAMP_TYPE = 0x00B1,
		BB_SCROLL_MSG_TYPE = 0x00EE,

		SHIP_LIST_TYPE = 0x00A0,
		BLOCK_LIST_TYPE = 0x00A1,
		INFO_REPLY_TYPE = 0x0011,
		MENU_SELECT_TYPE = 0x0010,

		/// <summary>
		/// This is 05 opcode.
		/// It's basically a "Hey, disconnect me."
		/// </summary>
		TYPE_05 = 0x0005
	}
}
