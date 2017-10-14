using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Enumeration of sub-operation codes for 0x60 packet
	/// <see cref="BlockNetworkCommandEventClientPayload"/> and ... TODO server
	/// </summary>
	public enum SubCommand60OperationCode : byte //it's sent as byte from server
	{
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

		//Syl: https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x3F
		//Tethella: https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L8356
		/// <summary>
		/// Seems to set a position and used for teleporting/wraping without scene changes(?)?
		/// </summary>
		TeleportToPosition = 0x3F,

		//Syl: https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/subcmd.h#L847
		//Tethella: https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L10215
		//TODO: What is this? Sent at lobby join.
		/// <summary>
		/// Some sort of burst command/event.
		/// </summary>
		BurstType5 = 0x6F

		//l->clientx[client->clientID] = *(unsigned *) &client->decryptbuf[0x14];
		//l->clienty[client->clientID] = *(unsigned*)&client->decryptbuf[0x1C];
	}
}
