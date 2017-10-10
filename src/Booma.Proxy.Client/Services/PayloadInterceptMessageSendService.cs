using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Service that can generate awaitable responses
	/// for recieveing a payload of a specific type.
	/// </summary>
	public sealed class PayloadInterceptMessageSendService<TPayloadBaseType> : IClientRequestSendService<TPayloadBaseType> 
		where TPayloadBaseType : class
	{
		/// <summary>
		/// Service that allows for interception of payloads.
		/// </summary>
		private IPayloadInterceptable InterceptionService { get; }

		/// <summary>
		/// The client send service.
		/// </summary>
		private IClientPayloadSendService<TPayloadBaseType> SendService { get; }

		/// <inheritdoc />
		public PayloadInterceptMessageSendService([NotNull] IPayloadInterceptable interceptionService, [NotNull] IClientPayloadSendService<TPayloadBaseType> sendService)
		{
			if(interceptionService == null) throw new ArgumentNullException(nameof(interceptionService));
			if(sendService == null) throw new ArgumentNullException(nameof(sendService));

			InterceptionService = interceptionService;
			SendService = sendService;
		}

		/// <inheritdoc />
		public async Task<TResponseType> SendRequestAsync<TResponseType>(TPayloadBaseType request) 
			where TResponseType : IPacketPayload
		{
			//TODO: There is a design race condition here. No matter the order.
			//We opt for this particular race condition because it would be better to recieve
			//responses from slightly before us sending the request than to miss them due to a race
			//before registering the interception.
			Task<TResponseType> resulTask = InterceptionService.InterceptPayload<TResponseType>();

			await SendService.SendMessage(request);

			return await resulTask;
		}
	}
}
