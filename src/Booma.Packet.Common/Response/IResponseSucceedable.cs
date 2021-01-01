using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Contract for response payloads that can fail or succeed.
	/// </summary>
	public interface IResponseSucceedable
	{
		/// <summary>
		/// Indicates if the response indicates success.
		/// </summary>
		bool isSuccessful { get; }
	}
}
