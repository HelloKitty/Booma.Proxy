using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using SceneJect.Common;
using Sirenix.OdinInspector;

namespace Booma.Proxy
{
	/// <summary>
	/// Base for network request senders.
	/// </summary>
	[Injectee]
	public abstract class NetworkRequestSender : SerializedMonoBehaviour
	{
		/// <summary>
		/// The network send service used to send the messages
		/// in the request sender.
		/// </summary>
		[Inject]
		protected IPeerPayloadSendService<PSOBBGamePacketPayloadClient> SendService { get; }

		/// <summary>
		/// The network send service used to send the messages
		/// in the request sender.
		/// </summary>
		[Inject]
		protected IPeerRequestSendService<PSOBBGamePacketPayloadClient> SendServiceAsync { get; }
	}
}
