using System;
using System.Collections.Generic;
using System.Text;

namespace Booma.Proxy
{
	public sealed class DefaultCharacterSlotModel : ICharacterSlotSelectedModel
	{
		public byte SlotSelected { get; set; }
	}
}
