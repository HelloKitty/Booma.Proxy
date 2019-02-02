using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace Booma.Proxy
{
	//The character selection screen uses this TOO but we don't need them for anything.
	[AdditionalRegisterationAs(typeof(IOnCharacterSelectionAcknowledgementEventSubscribable))]
	[SceneTypeCreate(GameSceneType.PreShipSelectionScene)]
	public sealed class CharacterCharacterSelectionAckResponseHandler : GameMessageHandler<CharacterCharacterSelectionAckPayload>, IOnCharacterSelectionAcknowledgementEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<CharacterSelectionAckEventArgs> OnCharacterSelectionAcknowledgementRecieved;

		/// <inheritdoc />
		public CharacterCharacterSelectionAckResponseHandler(ILog logger) 
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, CharacterCharacterSelectionAckPayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"CharacterAck Id: {payload.Slot} Result: {payload.AckType}");

			//We should now just publish an event about the ack.
			OnCharacterSelectionAcknowledgementRecieved?.Invoke(this, new CharacterSelectionAckEventArgs(payload.AckType, (byte)payload.Slot));

			return Task.CompletedTask;
		}
	}
}
