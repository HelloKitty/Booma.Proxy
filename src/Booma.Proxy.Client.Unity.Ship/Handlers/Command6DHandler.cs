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
	/// that handles specifically <see cref="BlockNetworkCommand60EventClientPayload"/> and dispatches the casted <typeparamref name="TSubCommandType"/> command
	/// to the implementer of this type.
	/// </summary>
	/// <typeparam name="TSubCommandType"></typeparam>
	public abstract class Command6DHandler<TSubCommandType> : SubMessageMessageHandler<TSubCommandType, BlockNetworkCommand6DEventServerPayload>
		where TSubCommandType : BaseSubCommand6D
	{
		/// <inheritdoc />
		protected override bool CheckIsHandlable(BlockNetworkCommand6DEventServerPayload payload)
		{
			//Just check if it's the type.
			return payload.Command is TSubCommandType;
		}

		/// <inheritdoc />
		protected override TSubCommandType RetrieveSubMessage(BlockNetworkCommand6DEventServerPayload payload)
		{
			//If they ask for it just provide it. Could be null, but not up to us.
			return payload.Command as TSubCommandType;
		}

		/// <inheritdoc />
		protected Command6DHandler(ILog logger) 
			: base(logger)
		{

		}
	}
}
