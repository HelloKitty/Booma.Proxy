using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;
using Reinterpret.Net;

namespace Booma
{
	//https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x76
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.CreatureDeathEvent)]
	public sealed partial class Sub60CreatureDeathEventCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		public byte Identifier => ObjectIdentifier.Identifier;

		[WireMember(1)]
		public MapObjectIdentifier ObjectIdentifier { get; internal set; }

		//TODO: What is this?
		//Unknown, is 01 00 08 00 sometimes.
		[WireMember(2)]
		public int Unk1 { get; internal set; } = new byte[4] {01, 00, 08, 00}.Reinterpret<int>();

		/// <inheritdoc />
		public Sub60CreatureDeathEventCommand([NotNull] MapObjectIdentifier objectIdentifier)
			: this()
		{
			ObjectIdentifier = objectIdentifier ?? throw new ArgumentNullException(nameof(objectIdentifier));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60CreatureDeathEventCommand()
			: base(SubCommand60OperationCode.CreatureDeathEvent)
		{
			CommandSize = 8 / 4;
		}
	}
}
