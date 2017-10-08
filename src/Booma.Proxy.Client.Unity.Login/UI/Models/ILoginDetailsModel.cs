using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for login details models.
	/// </summary>
	public interface ILoginDetailsModel
	{
		/// <summary>
		/// Clears the model.
		/// </summary>
		void Clear();

		/// <summary>
		/// The username
		/// </summary>
		string Username { get; set; }

		/// <summary>
		/// The password.
		/// </summary>
		string Password { get; set; }
	}
}
