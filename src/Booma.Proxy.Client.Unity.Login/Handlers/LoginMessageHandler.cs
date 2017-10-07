using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Simplified type of <see cref="BaseUnityMessageHandler{TIncomingPayloadBaseType,TOutgoingPayloadType,TPayloadType}"/>
	/// for login message handlers.
	/// </summary>
	/// <typeparam name="TPayloadType"></typeparam>
	public abstract class LoginMessageHandler<TPayloadType> : BaseUnityMessageHandler<PSOBBLoginPacketPayloadServer, PSOBBLoginPacketPayloadClient, TPayloadType> 
		where TPayloadType : PSOBBLoginPacketPayloadServer
	{

	}
}
