using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
{
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.ChangeObjectState)]
	public sealed partial class Sub60PlayerChangedObjectStateCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		public byte Identifier => ObjectIdentifier.Identifier;

		[WireMember(1)]
		public MapObjectIdentifier ObjectIdentifier { get; internal set; }

		//TODO: What is this? Always seems to be 0 in this packet
		/// <summary>
		/// TODO: ?
		/// </summary>
		[WireMember(2)]
		internal int unk1 { get; set; } = 0;
		
		//This value is linked to whatever the target object is too. Laser switch with ID 5 will point to Laser Gate ID 5.
		//This is NOT the same for Forest doors, for some reaosn though.
		/// <summary>
		/// The ID of the object interaction.
		/// This is NOT the ObjectId/Index. It is a 2 byte field later in the map object format.
		/// </summary>
		[WireMember(3)]
		public short ObjectInteractionId { get; internal set; }

		[WireMember(4)]
		public byte Unk3 { get; internal set; } //seems to be 1, at least when pressing a button.

		/// <summary>
		/// Indicates if the object should be set active.
		/// (Ex. Switched that have been used should send 1)
		/// </summary>
		[WireMember(5)]
		public bool isActive { get; internal set; } //seems to be 1, at least when pressing a button.

		/// <inheritdoc />
		public Sub60PlayerChangedObjectStateCommand([NotNull] MapObjectIdentifier objectIdentifier, short unk2, byte unk3, bool isActive)
			: this()
		{
			ObjectIdentifier = objectIdentifier ?? throw new ArgumentNullException(nameof(objectIdentifier));
			ObjectInteractionId = unk2;
			Unk3 = unk3;
			this.isActive = isActive;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60PlayerChangedObjectStateCommand()
			: base(SubCommand60OperationCode.ChangeObjectState)
		{
			CommandSize = 12 / 4;
		}
	}
}
