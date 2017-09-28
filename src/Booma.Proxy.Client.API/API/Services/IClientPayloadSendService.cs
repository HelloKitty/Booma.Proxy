using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for services that provide the ability to send payloads.
	/// </summary>
	/// <typeparam name="TPayloadBaseType"></typeparam>
	public interface IClientPayloadSendService<TPayloadBaseType>
		where TPayloadBaseType : class
	{
		//TODO: We will implement a priority or immediate sending overloads in the future

		/// <summary>
		/// Sends the provided <see cref="payload"/>
		/// </summary>
		/// <typeparam name="TPayloadType"></typeparam>
		/// <param name="payload"></param>
		/// <returns></returns>
		Task<SendResult> SendMessage<TPayloadType>(TPayloadType payload)
			where TPayloadType : class, TPayloadBaseType;
	}
}
