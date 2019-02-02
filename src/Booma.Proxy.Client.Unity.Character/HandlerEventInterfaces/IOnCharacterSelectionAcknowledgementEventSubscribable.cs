using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IOnCharacterSelectionAcknowledgementEventSubscribable
	{
		event EventHandler<CharacterSelectionAckEventArgs> OnCharacterSelectionAcknowledgementRecieved;
	}

	public sealed class CharacterSelectionAckEventArgs : EventArgs
	{
		public CharacterSelectionAckType AckType { get; }

		public byte SlotId { get; }

		/// <inheritdoc />
		public CharacterSelectionAckEventArgs(CharacterSelectionAckType ackType, byte slotId)
		{
			if(!Enum.IsDefined(typeof(CharacterSelectionAckType), ackType)) throw new InvalidEnumArgumentException(nameof(ackType), (int)ackType, typeof(CharacterSelectionAckType));

			AckType = ackType;
			SlotId = slotId;
		}
	}
}
