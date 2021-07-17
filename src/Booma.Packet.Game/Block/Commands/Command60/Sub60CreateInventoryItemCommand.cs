using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Command sent by the server when an inventory item is added to a character's inventory.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.CreateInventoryItem)]
	public sealed partial class Sub60CreateInventoryItemCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		//TODO: What is this? Probably client id
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		//TODO: What is this?
		[WireMember(2)]
		internal byte Unknown1 { get; set; } = 0; //Sylv sets to 0 explicitly (not required in C# though)

		//We don't use EMPTY here so it's easier to interact with these chunks.
		[KnownSize(12)]
		[WireMember(3)]
		public byte[] ItemData1 { get; set; } = new byte[12];

		[WireMember(4)]
		public uint ItemId { get; internal set; }

		//We don't use EMPTY here so it's easier to interact with these chunks.
		[KnownSize(4)]
		[WireMember(5)]
		public byte[] ItemData2 { get; set; } = new byte[4];

		//TODO: What is this?
		[WireMember(6)]
		internal uint Unknown2 { get; set; } = 0; //Sylv sets to 0 explicitly (not required in C# though)

		public Sub60CreateInventoryItemCommand(byte identifier, uint itemId, byte[] itemData1, byte[] itemData2) 
			: this()
		{
			Identifier = identifier;
			ItemId = itemId;
			ItemData1 = itemData1 ?? throw new ArgumentNullException(nameof(itemData1));
			ItemData2 = itemData2 ?? throw new ArgumentNullException(nameof(itemData2));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60CreateInventoryItemCommand()
			: base(SubCommand60OperationCode.CreateInventoryItem)
		{
			//Sylv sets this size
			CommandSize = 0x07;
		}
	}
}
