using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Data model for the client session details.
	/// </summary>
	public interface IClientSessionDetails
	{
		/// <summary>
		/// The session id (indicates what part of the process we're in).
		/// Is known on Syl as TeamId or on Teth as 32bit security id.
		/// </summary>
		int SessionId { get; set; }

		/// <summary>
		/// 40 byte session verification data
		/// used to validate a client session.
		/// </summary>
		byte[] SessionVerificationData { get; set; }

		/// <summary>
		/// Client card number
		/// is apart of the session as well.
		/// </summary>
		uint GuildCardNumber { get; set; }
	}
}
