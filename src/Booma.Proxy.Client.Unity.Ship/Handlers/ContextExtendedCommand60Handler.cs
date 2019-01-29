using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using SceneJect.Common;

namespace Booma.Proxy
{
	/// <summary>
	/// A <see cref="Command60Handler{TSubCommandType}"/> for commands that are <see cref="IMessageContextIdentifiable"/>.
	/// Will provided the <see cref="INetworkPlayer"/> representation to the handler method. Will not dispatch to the child handler
	/// if the client ID is unknown.
	/// </summary>
	/// <typeparam name="TCommandType">The command type to handle.</typeparam>
	/// <typeparam name="TContextType">The type of the context that is required to handle the command.</typeparam>
	[Injectee]
	public abstract class ContextExtendedCommand60Handler<TCommandType, TContextType> : Command60Handler<TCommandType> 
		where TCommandType : BaseSubCommand60, IMessageContextIdentifiable 
		where TContextType : INetworkMessageContext
	{
		/// <summary>
		/// The context factory required to build the context for the message.
		/// </summary>
		private INetworkMessageContextFactory<IMessageContextIdentifiable, TContextType> ContextFactory { get; }

		/// <inheritdoc />
		protected ContextExtendedCommand60Handler(ILog logger, [NotNull] INetworkMessageContextFactory<IMessageContextIdentifiable, TContextType> contextFactory)
			: base(logger)
		{
			ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
		}

		/// <inheritdoc />
		protected override async Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, TCommandType command)
		{
			//Build the context first and check its validity
			TContextType commandContext = ContextFactory.Create(command);

			//Not sure if it's possible to encounter this but we should check to be sure
			if(!commandContext.isValid)
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Recieved Code: {command.OpCodeHexString()} {this.MessageName()} for unknown Id: {command.Identifier}");
				return;
			}

			//Just dispatch with the newly constructed command context
			await HandleSubMessage(context, command, commandContext);
		}

		/// <summary>
		/// Handler methods with the network context, the command, and the command context <typeparamref name="TContextType"/>.
		/// </summary>
		/// <param name="context">The network context.</param>
		/// <param name="command">The command to be handled.</param>
		/// <param name="commandContext">The context for the command.</param>
		/// <returns>An awaitable that completes when the handling is finished.</returns>
		protected abstract Task HandleSubMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, TCommandType command, TContextType commandContext);
	}
}
