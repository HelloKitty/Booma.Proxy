using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Command sent by players to the server when the character is entering an operation
	/// that requires it to freeze or has a start and end part of the operation.
	/// Such as dropping an item, dying or chatting with an NPC.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.DropInventoryItem)]
	public sealed partial class Sub60DropInventoryItemCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		//TODO: What is this? Probably client id
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		//TODO: What is this?
		[WireMember(2)]
		public byte Unknown1 { get; internal set; }

		//TODO: What is this?
		[WireMember(3)]
		public short Unknown2 { get; internal set; }

		//TODO: Create an enum? If it possible
		/// <summary>
		/// ID for the zone the character is in.
		/// </summary>
		[WireMember(4)]
		public short ZoneId { get; internal set; }

		/// <summary>
		/// The ID of the item being dropped.
		/// </summary>
		[WireMember(5)]
		public int ItemId { get; internal set; }

		/// <summary>
		/// The position of the item being dropped.
		/// </summary>
		[WireMember(6)]
		public Vector3<float> Position { get; internal set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60DropInventoryItemCommand()
			: base(SubCommand60OperationCode.DropInventoryItem)
		{
			
		}
	}
}
