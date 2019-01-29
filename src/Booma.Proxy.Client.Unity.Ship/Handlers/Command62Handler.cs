using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace Booma.Proxy
{
	/// <summary>
	/// Implementation of the <see cref="BaseUnityMessageHandler{TIncomingPayloadBaseType,TOutgoingPayloadType,TPayloadType}"/>
	/// that handles specifically <see cref="BlockNetworkCommand62EventClientPayload"/> and dispatches the casted <typeparamref name="TSubCommandType"/> command
	/// to the implementer of this type.
	/// </summary>
	/// <typeparam name="TSubCommandType"></typeparam>
	public abstract class Command62Handler<TSubCommandType> : SubMessageMessageHandler<TSubCommandType, BlockNetworkCommand62EventServerPayload>
		where TSubCommandType : BaseSubCommand62
	{
		/// <inheritdoc />
		protected override bool CheckIsHandlable(BlockNetworkCommand62EventServerPayload payload)
		{
			//Just check if it's the type.
			return payload.Command is TSubCommandType;
		}

		/// <inheritdoc />
		protected override TSubCommandType RetrieveSubMessage(BlockNetworkCommand62EventServerPayload payload)
		{
			//If they ask for it just provide it. Could be null, but not up to us.
			return payload.Command as TSubCommandType;
		}

		/// <inheritdoc />
		protected Command62Handler(ILog logger) 
			: base(logger)
		{

		}
	}
}
