﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Enumeration of all the game servers
	/// operation codes.
	/// </summary>
	public enum GameNetworkOperationCode : short
	{
		UNKNOWN = 0x0000,
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

		GAME_LIST_TYPE = 0x0008,

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
		BLOCK_LIST_TYPE = 0x0007, //Tethella seems to send 07. not a A1 like Soly's ump
		INFO_REPLY_TYPE = 0x0011,
		MENU_SELECT_TYPE = 0x0010,

		/// <summary>
		/// This is 05 opcode.
		/// It's basically a "Hey, disconnect me."
		/// </summary>
		TYPE_05 = 0x0005,

		/// <summary>
		/// Message sent by the client to init its character data
		/// on the block server.
		/// </summary>
		BLOCK_SET_CHAR_DATA_TYPE = 0x0061,

		//Sent when a player joins the lobby
		LOBBY_ADD_PLAYER_TYPE = 0x0068,

		LOBBY_LEAVE_TYPE = 0x0069,

		//List of the lobby?
		LOBBY_LIST_TYPE = 0x0083,

		LOBBY_CHANGE_TYPE = 0x0084,

		//Big 14,000 byte chunk of character data including inventory and everything
		BB_FULL_CHARACTER_TYPE = 0x00E7,

		//Simple message that asks the client for some character data
		CHAR_DATA_REQUEST_TYPE = 0x0095,

		//Sent to the client to tell it to join a lobby
		LOBBY_JOIN_TYPE = 0x0067,

		GAME_JOIN_TYPE = 0x0064,

		//Sent to the client when another player leaves
		GAME_LEAVE_TYPE = 0x0066,

		GAME_COMMAND0_TYPE = 0x0060,

		GAME_COMMAND2_TYPE = 0x0062,

		/// <summary>
		/// 0x6D subcommand. Sent mostly at game join.
		/// </summary>
		GAME_COMMANDD_TYPE = 0x006D,

		PING_TYPE = 0x001D,

		CHAT_TYPE = 0x0006,

		DONE_BURSTING_TYPE = 0x006F,

		/// <summary>
		/// Opcode sent by the client when it wants to create a game
		/// on a block.
		/// </summary>
		GAME_CREATE_TYPE = 0x00C1,

		/// <summary>
		/// Opcode sent by the server when a client
		/// is joining a lobby.
		/// </summary>
		GAME_ADD_PLAYER_TYPE = 0x0065,

		//Can't find this on on Sylverant
		//It's related to character data I think
		SERVER_CHARACTER_DATA_TYPE = 0x15EA,

		/// <summary>
		/// Opcode sent when player joins the lobby about challenge mode rank.
		/// </summary>
		C_RANK_TYPE = 0x00C5,

		/// <summary>
		/// Opcode sent to set player lobby arrows.
		/// </summary>
		LOBBY_ARROW_LIST_TYPE = 0x0088,

		/// <summary>
		/// Opcode sent when a player leaves a game.
		/// Same structure as CHAR_DATA_TYPE.
		/// </summary>
		LEAVE_GAME_PL_DATA_TYPE = 0x0098,

		/// <summary>
		/// Opcode sent by the client to the Ship when it updates its config/customization/keybinds.
		/// </summary>
		BB_UPDATE_CONFIG = 0x07ED,
	}
}
