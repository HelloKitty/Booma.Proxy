using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booma.Proxy;

namespace Booma
{
	/// <summary>
	/// Contract for types/objects that are operation-coded.
	/// </summary>
	/// <typeparam name="TOperationCodeType">The operation code type.</typeparam>
	public interface IOperationCodeable<out TOperationCodeType>
		where TOperationCodeType : Enum
	{
		/// <summary>
		/// Operation code for the object.
		/// </summary>
		TOperationCodeType OperationCode { get; }
	}
}
