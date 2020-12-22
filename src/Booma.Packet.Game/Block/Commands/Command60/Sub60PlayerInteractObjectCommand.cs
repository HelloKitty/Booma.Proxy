using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.BeginObjectInteract)]
	public sealed class Sub60PlayerInteractObjectCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }
	
		[WireMember(2)]
		internal byte unk1 { get; set; }

		//TODO: Is this what it is? Matche 0x3F "animationstate" which maye be a checksum too. Probably don't need to send.
		/// <summary>
		/// TODO: What is this
		/// </summary>
		[WireMember(3)]
		public int PositionChecksum { get; internal set; }

		/// <inheritdoc />
		public Sub60PlayerInteractObjectCommand(byte identifier)
			: this()
		{
			Identifier = identifier;
		}

		//Serializer ctor
		private Sub60PlayerInteractObjectCommand()
		{
			CommandSize = 8 / 4;
		}
	}
}
