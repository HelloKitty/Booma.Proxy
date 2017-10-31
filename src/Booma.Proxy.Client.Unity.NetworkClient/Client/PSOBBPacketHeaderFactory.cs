using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace Booma.Proxy
{
	public sealed class PSOBBPacketHeaderFactory : IPacketHeaderFactory<IPacketPayload>
	{
		/// <inheritdoc />
		public IPacketHeader Create<TPayloadType>(TPayloadType payload, byte[] serializedPayloadData) 
			where TPayloadType : IPacketPayload
		{
			//The packet size is simply the length of the payload plus the header which is 2 bytes
			return new PSOBBPacketHeader(serializedPayloadData.Length + 2);
		}
	}
}
