using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Contract for a response payload that contains a result/response code.
	/// </summary>
	/// <typeparam name="TResponseCodeType">The type of response code.</typeparam>
	public interface IResponseCodePayload<TResponseCodeType>
		where TResponseCodeType : struct
	{
		/// <summary>
		/// The code indicates the result of the response.
		/// </summary>
		TResponseCodeType ResponseCode { get; }
	}
}
