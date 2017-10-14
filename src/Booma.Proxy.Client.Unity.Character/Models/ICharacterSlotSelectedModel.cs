using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for a data model that contains the slot selected.
	/// </summary>
	public interface ICharacterSlotSelectedModel
	{
		/// <summary>
		/// The character slot selected.
		/// </summary>
		int SlotSelected { get; set; }
	}
}
