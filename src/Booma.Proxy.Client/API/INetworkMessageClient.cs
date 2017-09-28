using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Consolidated interface for client that implements reading and writing of the specified generic payloads.
	/// </summary>
	/// <typeparam name="TReadPayloadType"></typeparam>
	/// <typeparam name="TWritePayloadType"></typeparam>
	public interface INetworkMessageClient<TReadPayloadType, TWritePayloadType> : 
		IPacketPayloadReadable<TReadPayloadType>, IPacketPayloadWritable<TWritePayloadType>, IConnectable
		where TReadPayloadType : class
		where TWritePayloadType : class
	{

	}
}
