using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Proxy
{
	/// <summary>
	/// Handle that recieves the E4 ack with the character selection result.
	/// </summary>
	public sealed class PreShipListCharacterSelectionAckHandler : GameMessageHandler<CharacterCharacterSelectionAckPayload>
	{
		//TODO: Broadcast code with it.
		/// <summary>
		/// Called on the selection is non-existant.
		/// </summary>
		[SerializeField]
		private UnityEvent OnSelectionFailed;

		/// <summary>
		/// Called when the connection is successful.
		/// </summary>
		[SerializeField]
		private UnityEvent OnSelectionSuccess;

		/// <inheritdoc />
		public PreShipListCharacterSelectionAckHandler(ILog logger) 
			: base(logger)
		{
			
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<PSOBBGamePacketPayloadClient> context, CharacterCharacterSelectionAckPayload payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Recieved Selection Response: {payload.AckType}");

			if(payload.AckType == CharacterSelectionAckType.BB_CHAR_ACK_NONEXISTANT)
				OnSelectionFailed?.Invoke();

			//Call success and let someone who setup the scene handle this.
			OnSelectionSuccess?.Invoke();
		}
	}
}
