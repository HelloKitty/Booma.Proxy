﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Enumeration of sub-operation codes for 0x60 packet
	/// <see cref="BlockNetworkCommand60EventClientPayload"/> and ... TODO server
	/// </summary>
	public enum SubCommand60OperationCode : byte //it's sent as byte from server
	{
		Unknown = 0x00,

		//TODO: Is this right?
		//Syl: https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x1F
		/// <summary>
		/// Sent to the server to request a warp. (?)
		/// </summary>
		WrapToNewArea = 0x1F,

		//TODO: Document
		/// <summary>
		/// 
		/// </summary>
		EnterFreshlyWrappedZoneCommand = 0x23,

		//TODO: Is any of this true?
		/// <summary>
		/// Sent the server when we encounter a player we haven't seen
		/// who has just finished warping. 
		/// </summary>
		AlertFreshlyWarpedClients = 0x20,

		/// <summary>
		/// Sent the client to begin teleporting
		/// to a different zone. Is rebroadcasted to others.
		/// </summary>
		BeginZoneTeleporting = 0x22,

		//Syl: https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x3F
		//Tethella: https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L8356
		/// <summary>
		/// Seems to set a position and used for teleporting/wraping without scene changes(?)?
		/// </summary>
		TeleportToPosition = 0x3F,

		SetFinalMovingPosition = 0x3E,

		//SUBCMD_DEL_MAP_ITEM
		PickupItem = 0x59,

		//Syl: https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/subcmd.h#L847
		//Tethella: https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L10215
		//TODO: What is this? Sent at lobby join.
		/// <summary>
		/// Some sort of burst command/event.
		/// </summary>
		BurstType5 = 0x6F,

		/// <summary>
		/// Finished loading the map
		/// </summary>
		SUBCMD_LOAD_3B = 0x3B,

		/// <summary>
		/// Subcommand that sets the player's X and Y position while running.
		/// </summary>
		MovingSlowPositionChanged = 0x40,

		/// <summary>
		/// Subcommand that 
		/// </summary>
		MovingFastPositionChanged = 0x42,

		LobbyBallMove = 0x79,

		SetExperienceRate = 0xDD,

		//TODO: Is this a good name?
		//SUBCMD_TALK_NPC
		FreezePlayer = 0x2C,

		//SUBCMD_DROP_ITEM
		DropInventoryItem = 0x2A,

		//SUBCMD_BURST_DONE   
		GameBurstingComplete = 0x72,

		/// <summary>
		/// Opcode used to packets
		/// about players leaving a game.
		/// </summary>
		GAME_LEAVE_TYPE = 0x66,

		//l->clientx[client->clientID] = *(unsigned *) &client->decryptbuf[0x14];
		//l->clienty[client->clientID] = *(unsigned*)&client->decryptbuf[0x1C];
		/// <summary>
		/// Sent when a client leaves a map and begins to warp to a new area.
		/// </summary>
		GameStartWarpToArea = 0x21,

		//TODO: Is this right?
		//https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L9212
		PlayerInventorySet = 0xDE,

		/// <summary>
		/// Opcode for event that lets clients know a box has been hit.
		/// </summary>
		GameBoxHit = 0x0B,

		/// <summary>
		/// Opcode for when a user sits down in a photon chair.
		/// </summary>
		PhotonChairSit = 0xAB,

		/// <summary>
		/// Opcode for when a step in an attack finished.
		/// This packet will contain the results of an attack step.
		/// </summary>
		AttackStepFinished = 0x46,

		/// <summary>
		/// Opcode for when the first step of an attack is occuring for
		/// the player sending it.
		/// </summary>
		AttackStepOne = 0x43,

		/// <summary>
		/// Opcode sent around the time the player tries to interact with an object.
		/// </summary>
		BeginObjectInteract = 0x50,

		/// <summary>
		/// Opcode sent after a player interacts with an object.
		/// TODO: I think this changes the state of an object, due to interaction. Research.
		/// </summary>
		ChangeObjectState = 0x05,

		/// <summary>
		/// Opcode sent when a player begings casting a technique.
		/// </summary>
		BeginTechniqueCast = 0x8D,

		/// <summary>
		/// Opcode sent when the player has actually started to cast the technique.
		/// Contains the actual spell cast info.
		/// </summary>
		CastingTechiqueCast = 0x47,

		/// <summary>
		/// Opcode sent when the player finished casting a technique.
		/// Contains spell cast information.
		/// </summary>
		FinishedTechniqueCast = 0x48,

		/// <summary>
		/// Opcode sent when an enemy is hit with damage (maybe objects too).
		/// Clients/Players don't own the damage they deal, they are not in the packet.
		/// </summary>
		ObjectDamageHit = 0x0A,

		//TODO: Implement
		/// <summary>
		/// Opcode sent when the client has encountered a dead creature
		/// </summary>
		RequestCreatureExperience = 0xC8,

		/// <summary>
		/// Opcode sent when a creature dies on a client, broadcasted
		/// to everyone else that it has died.
		/// </summary>
		CreatureDeathEvent = 0x76,

		CreateInventoryItem = 0xBE
	}
}
