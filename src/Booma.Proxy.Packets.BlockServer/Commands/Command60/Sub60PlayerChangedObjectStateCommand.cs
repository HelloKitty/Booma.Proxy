using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.ChangeObjectState)]
	public sealed class Sub60PlayerChangedObjectStateCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		public byte Identifier => ObjectIdentifier.Identifier;

		[WireMember(1)]
		public MapObjectIdentifier ObjectIdentifier { get; }

		//TODO: What is this? Always seems to be 0 in this packet
		/// <summary>
		/// TODO: ?
		/// </summary>
		[WireMember(2)]
		private int unk1 { get; } = 0;

		//This is actually the ID of the ObjectActionID that should be fired. Weird design, but each interactable object has this interact id defined.
		/// <summary>
		/// TODO: Unknown value, seems to be related to the object type hit.
		/// Maybe the new animation state?
		/// </summary>
		[WireMember(3)]
		public short Unk2 { get; }

		[WireMember(4)]
		public byte Unk3 { get; } //seems to be 1, at least when pressing a button.

		[WireMember(5)]
		public byte Unk4 { get; } //seems to be 1, at least when pressing a button.

		/// <inheritdoc />
		public Sub60PlayerChangedObjectStateCommand([NotNull] MapObjectIdentifier objectIdentifier, short unk2, byte unk3, byte unk4)
			: this()
		{
			ObjectIdentifier = objectIdentifier ?? throw new ArgumentNullException(nameof(objectIdentifier));
			Unk2 = unk2;
			Unk3 = unk3;
			Unk4 = unk4;
		}

		//Serializer ctor
		private Sub60PlayerChangedObjectStateCommand()
		{
			CommandSize = 12 / 4;
		}
	}
}
