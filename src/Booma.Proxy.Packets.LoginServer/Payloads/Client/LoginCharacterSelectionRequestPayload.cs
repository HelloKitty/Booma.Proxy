using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Payload sent by the client to select or recieve preview information for a
	/// character in the specified <see cref="SlotSelected"/>.
	/// </summary>
	[WireDataContract]
	[LoginClientPacketPayload(LoginNetworkOperationCodes.BB_CHARACTER_SELECT_TYPE)]
	public sealed class LoginCharacterSelectionRequestPayload : PSOBBLoginPacketPayloadClient
	{
		/// <summary>
		/// The slot being selected.
		/// </summary>
		[WireMember(1)]
		public int SlotSelected { get; }

		/// <summary>
		/// Indicates the type of selection to be done.
		/// (Ex. Preview or Play)
		/// </summary>
		[WireMember(2)]
		public CharacterSelectionType SelectionType { get; }

		/// <inheritdoc />
		public LoginCharacterSelectionRequestPayload(byte slotSelected, CharacterSelectionType selectionType)
		{
			if(!Enum.IsDefined(typeof(CharacterSelectionType), selectionType)) throw new InvalidEnumArgumentException(nameof(selectionType), (int)selectionType, typeof(CharacterSelectionType));

			//TODO: Should we do slot number validation?
			SlotSelected = slotSelected;
			SelectionType = selectionType;
		}

		//serializer ctor
		private LoginCharacterSelectionRequestPayload()
		{
			
		}
	}
}
