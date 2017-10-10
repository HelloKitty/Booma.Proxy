using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for a managed network client that provides
	/// a high level networking API to consumers.
	/// </summary>
	/// <typeparam name="TPayloadWriteType">The type of payload it should write.</typeparam>
	/// <typeparam name="TPayloadReadType">The type of payload it should read.</typeparam>
	public interface IManagedNetworkClient<TPayloadWriteType, TPayloadReadType> : IClientPayloadSendService<TPayloadWriteType>, IConnectionService,
		INetworkMessageProducer<TPayloadReadType>, IPayloadInterceptable
		where TPayloadWriteType : class
		where TPayloadReadType : class
	{
		//Just a conslidated interface for easier consuming and Typing
	}
}
