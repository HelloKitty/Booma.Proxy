using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Implementation of the <see cref="BaseUnityMessageHandler{TIncomingPayloadBaseType,TOutgoingPayloadType,TPayloadType}"/>
	/// that handles specifically <see cref="BlockNetworkCommandEventClientPayload"/> and dispatches the casted <typeparamref name="TSubCommandType"/> command
	/// to the implementer of this type.
	/// </summary>
	/// <typeparam name="TSubCommandType"></typeparam>
	public abstract class Command60Handler<TSubCommandType> : BaseUnityMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, BlockNetworkCommandEventServerPayload>
		where TSubCommandType : BaseSubCommand60Server
	{
		/// <inheritdoc />
		public override async Task<bool> TryHandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, PSOBBNetworkIncomingMessage<PSOBBGamePacketPayloadServer> message)
		{
			//Odd design but we override so we can check that this is the payload
			//and then check if we have the right command type
			if(message.Payload is BlockNetworkCommandEventServerPayload payload)
				if(payload.Command is TSubCommandType)
					return await base.TryHandleMessage(context, message);

			//If it's not then we don't want to consume it
			return false;
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, BlockNetworkCommandEventServerPayload payload)
		{
			//We want to grab the command type, we know it's of a specific type at this point
			//so we just want to dispatch it and cast it
			await HandleCommand(context, payload.Command as TSubCommandType);
		}

		/// <summary>
		/// Implemented command handler for the specified <typeparamref name="TSubCommandType"/>.
		/// </summary>
		/// <param name="context">The context of the command.</param>
		/// <param name="command">The command sent.</param>
		/// <returns>An awaitable that completes when the command handling is finished.</returns>
		protected abstract Task HandleCommand(IClientMessageContext<PSOBBGamePacketPayloadClient> context, TSubCommandType command);
	}
}
