using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Data model for the login session details.
	/// </summary>
	public interface ILoginSessionDetails
	{
		/// <summary>
		/// The session id (indicates what part of the process we're in).
		/// Is known on Syl as TeamId or on Teth as 32bit security id.
		/// </summary>
		int SessionId { get; set; }

		/// <summary>
		/// 40 byte session verification data
		/// used to validate a login session.
		/// </summary>
		byte[] SessionVerificationData { get; set; }
	}
}
