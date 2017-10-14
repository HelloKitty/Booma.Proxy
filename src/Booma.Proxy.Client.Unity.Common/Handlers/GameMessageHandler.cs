using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Simplified type of <see cref="BaseUnityMessageHandler{TIncomingPayloadBaseType,TOutgoingPayloadType,TPayloadType}"/>
	/// for game message handlers.
	/// </summary>
	/// <typeparam name="TPayloadType"></typeparam>
	public abstract class GameMessageHandler<TPayloadType> : BaseUnityMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, TPayloadType> 
		where TPayloadType : PSOBBGamePacketPayloadServer
	{

	}
}
