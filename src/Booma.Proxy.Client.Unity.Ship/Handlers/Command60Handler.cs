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
	public abstract class Command60Handler<TSubCommandType> : SubMessageMessageHandler<TSubCommandType, BlockNetworkCommandEventServerPayload>
		where TSubCommandType : BaseSubCommand60
	{
		/// <inheritdoc />
		protected override bool CheckIsHandlable(BlockNetworkCommandEventServerPayload payload)
		{
			//Just check if it's the type.
			return payload.Command is TSubCommandType;
		}

		/// <inheritdoc />
		protected override TSubCommandType RetrieveSubMessage(BlockNetworkCommandEventServerPayload payload)
		{
			//If they ask for it just provide it. Could be null, but not up to us.
			return payload.Command as TSubCommandType;
		}
	}
}
