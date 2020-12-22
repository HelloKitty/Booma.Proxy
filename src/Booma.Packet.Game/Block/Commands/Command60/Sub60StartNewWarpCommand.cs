using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload sent when a client is begining a warp to a new area.
	/// Contains client ID information and information about the warp itself.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.GameStartWarpToArea)]
	public sealed class Sub60StartNewWarpCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		//TODO: Is this client id?
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		[WireMember(2)]
		public byte Unused1 { get; internal set; }

		/// <summary>
		/// The zone ID that the user is teleporting to.
		/// </summary>
		[WireMember(3)]
		public short ZoneId { get; internal set; }
		
		//TODO: What is this?
		[WireMember(4)]
		public short Unused2 { get; internal set; }

		/// <inheritdoc />
		public Sub60StartNewWarpCommand(byte identifier, short zoneId)
		{
			Identifier = identifier;
			ZoneId = zoneId;
		}

		public Sub60StartNewWarpCommand()
		{
			//Calc static 32bit size
			CommandSize = 8 / 4;
		}
	}
}
