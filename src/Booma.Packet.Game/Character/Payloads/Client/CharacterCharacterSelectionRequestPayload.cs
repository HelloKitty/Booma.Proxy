using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Payload sent by the client to select or recieve preview information for a
	/// character in the specified <see cref="SlotSelected"/>.
	/// </summary>
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.BB_CHARACTER_SELECT_TYPE)]
	public sealed partial class CharacterCharacterSelectionRequestPayload : PSOBBGamePacketPayloadClient
	{
		/// <summary>
		/// The slot being selected.
		/// </summary>
		[WireMember(1)]
		public int SlotSelected { get; internal set; }

		/// <summary>
		/// Indicates the type of selection to be done.
		/// (Ex. Preview or Play)
		/// </summary>
		[WireMember(2)]
		public CharacterSelectionType SelectionType { get; internal set; }

		/// <inheritdoc />
		public CharacterCharacterSelectionRequestPayload(byte slotSelected, CharacterSelectionType selectionType)
			: this()
		{
			if(!Enum.IsDefined(typeof(CharacterSelectionType), selectionType)) throw new InvalidEnumArgumentException(nameof(selectionType), (int)selectionType, typeof(CharacterSelectionType));

			//TODO: Should we do slot number validation?
			SlotSelected = slotSelected;
			SelectionType = selectionType;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterCharacterSelectionRequestPayload()
			: base(GameNetworkOperationCode.BB_CHARACTER_SELECT_TYPE)
		{
			
		}
	}
}
