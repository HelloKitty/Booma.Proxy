using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Contract for messages that contain a context identifier.
	/// </summary>
	public interface IMessageContextIdentifiable
	{
		/// <summary>
		/// The ID associated with the message.
		/// </summary>
		byte Identifier { get; }
	}
}
