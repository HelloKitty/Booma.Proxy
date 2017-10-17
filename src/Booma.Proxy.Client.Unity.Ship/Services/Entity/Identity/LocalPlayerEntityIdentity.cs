using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// A model for the local player's identity.
	/// </summary>
	public class LocalPlayerEntityIdentity : PlayerEntityIdentity
	{
		/// <inheritdoc />
		public LocalPlayerEntityIdentity(ICharacterSlotSelectedModel slotModel) 
			: base(slotModel.SlotSelected)
		{
			//Just pass the selected slot to the base entity identity ctor
		}
	}
}
