using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace Booma.Proxy
{
	/// <summary>
	/// Implementation of the message handler
	/// that handles specifically <see cref="BlockNetworkCommand60EventClientPayload"/> and dispatches the casted <typeparamref name="TSubCommandType"/> command
	/// to the implementer of this type.
	/// </summary>
	/// <typeparam name="TSubCommandType"></typeparam>
	public abstract class InteropCommand60Handler<TSubCommandType> : BaseInteropSubMessageMessageHandler<TSubCommandType, BlockNetworkCommand60EventServerPayload>
		where TSubCommandType : BaseSubCommand60
	{
		/// <inheritdoc />
		protected InteropCommand60Handler(ILog logger)
			: base(logger)
		{

		}

		/// <inheritdoc />
		protected override bool CheckIsHandlable(BlockNetworkCommand60EventServerPayload payload)
		{
			//Just check if it's the type.
			return payload.Command is TSubCommandType;
		}

		/// <inheritdoc />
		protected override TSubCommandType RetrieveSubMessage(BlockNetworkCommand60EventServerPayload payload)
		{
			//If they ask for it just provide it. Could be null, but not up to us.
			return payload.Command as TSubCommandType;
		}

		public override Task HandleMessage(InteropPSOBBPeerMessageContext context, BlockNetworkCommand60EventServerPayload payload)
		{
			//Because this is technically unsanitized input from remote clients, psobb servers just forward it
			//we don't want exceptions in this area to bubble up
			//if they do the network client will disconnect itself
			//therefore we supress and log these exceptions
			try
			{
				return base.HandleMessage(context, payload);
			}
			catch (Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Encountered Sub60 message Exception: \n {e.ToString()}");

				return Task.CompletedTask;
			}
		}
	}
}
