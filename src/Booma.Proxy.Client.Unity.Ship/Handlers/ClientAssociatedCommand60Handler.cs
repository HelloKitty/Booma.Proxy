using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;

namespace Booma.Proxy
{
	/// <summary>
	/// A <see cref="Command60Handler{TSubCommandType}"/> for commands that are <see cref="ICommandClientIdentifiable"/>.
	/// Will provided the <see cref="INetworkPlayer"/> representation to the handler method. Will not dispatch to the child handler
	/// if the client ID is unknown.
	/// </summary>
	/// <typeparam name="TCommandType"></typeparam>
	[Injectee]
	public abstract class ClientAssociatedCommand60Handler<TCommandType> : Command60Handler<TCommandType> 
		where TCommandType : BaseSubCommand60, ICommandClientIdentifiable
	{
		/// <summary>
		/// The indextable collection of <see cref="INetworkPlayer"/>s.
		/// </summary>
		[Inject]
		protected INetworkPlayerCollection PlayerCollection { get; }

		//This handler will exit if the model (the command) doesn't contain a valid client id.
		/// <inheritdoc />
		protected override async Task HandleSubMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, TCommandType command)
		{
			//Not sure if it's possible to encounter this but we should check to be sure
			if(!PlayerCollection.ContainsId(command.ClientId))
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Recieved Code: {command.OpCodeHexString()} {this.MessageName()} for unknown Id: {command.ClientId}");
				return;
			}

			await HandleClientMessage(context, command, PlayerCollection[command.ClientId]);
		}

		/// <summary>
		/// Handler methods with the network context, the command and the <see cref="INetworkPlayer"/> that the command
		/// is related to.
		/// </summary>
		/// <param name="context">The network context.</param>
		/// <param name="command">The command to be handled.</param>
		/// <param name="player">The play associated with the command.</param>
		/// <returns>An awaitable that completes when the handling is finished.</returns>
		protected abstract Task HandleClientMessage(IClientMessageContext<PSOBBGamePacketPayloadClient> context, TCommandType command, INetworkPlayer player);
	}
}
