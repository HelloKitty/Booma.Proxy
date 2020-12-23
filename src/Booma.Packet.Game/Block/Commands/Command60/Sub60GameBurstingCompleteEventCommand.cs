using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//Seems this is sent to all clients in the party after a joined player has finished bursting
	//I think clients shortly after send a 0x60 0x20 Area/Warp Ack.
	/// <summary>
	/// Sent by the server in response to a 6F being sent during bursting into a game.
	/// (no idea what is in it at the moment)
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.GameBurstingComplete)]
	public sealed partial class Sub60GameBurstingCompleteEventCommand : BaseSubCommand60
	{
		//See: https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L13792
		//TODO: Investigate what this is
		//Opensource emulators always send this for some reason.
		//Unknown meaning.
		//pkt->data[0] = 0x18;
		//pkt->data[1] = 0x08;
		/// <summary>
		/// Unknown value.
		/// Emulators always send: 0x18
		/// </summary>
		[WireMember(1)]
		public byte Unk1 { get; internal set; } = 0x18;

		/// <summary>
		/// Unknown value.
		/// Emulators always send: 0x08
		/// </summary>
		[WireMember(2)]
		public byte Unk2 { get; internal set; } = 0x08;

		public Sub60GameBurstingCompleteEventCommand(byte unk1, byte unk2)
			: this()
		{
			Unk1 = unk1;
			Unk2 = unk2;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60GameBurstingCompleteEventCommand()
			: base(SubCommand60OperationCode.GameBurstingComplete)
		{
			
		}
	}
}
