using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Booma.Proxy
{
	/// <summary>
	/// Handles <see cref="LoginBlockListEventPayload"/> and dispatches to the
	/// UI controller service.
	/// </summary>
	public sealed class LoginBlockListListHandler : LoginMessageHandler<LoginBlockListEventPayload>
	{
		/// <summary>
		/// The menu registeration service.
		/// </summary>
		[Required]
		[OdinSerialize]
		private IMenuListingRegisterable BlockListingRegisterService { get; set; }

		/// <inheritdoc />
		public override Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, LoginBlockListEventPayload payload)
		{
			foreach(MenuListing m in payload.Blocks)
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Block Listing: {m.ItemName}");

				BlockListingRegisterService.RegisterMenuItem(m);
			}

			return Task.CompletedTask;
		}
	}
}
