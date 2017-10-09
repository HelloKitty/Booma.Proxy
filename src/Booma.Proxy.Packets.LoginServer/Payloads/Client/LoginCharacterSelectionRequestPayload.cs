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
		public byte SlotSelected { get; }

		//TODO: Why is this here? Is it really padding?
		[KnownSize(3)]
		[WireMember(2)]
		private byte[] padding1 { get; } = new byte[3];

		/// <summary>
		/// Indicates the type of selection to be done.
		/// (Ex. Preview or Play)
		/// </summary>
		[WireMember(3)]
		public CharacterSelectionType SelectionType { get; }

		[KnownSize(3)]
		[WireMember(4)]
		private byte[] padding2 { get; } = new byte[3];

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
