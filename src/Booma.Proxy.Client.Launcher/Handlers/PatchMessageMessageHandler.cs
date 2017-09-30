using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public sealed class PatchMessageMessageHandler : IClientPayloadSpecificMessageHandler<PatchingMessagePayload, PSOBBPatchPacketPayloadClient>, INotifyPropertyChanged
	{
		private string _patchNotesText;

		/// <summary>
		/// Property that the external UI
		/// should listen on to recieve the patch notes
		/// message sent from the patch server.
		/// </summary>
		public string PatchNotesText
		{
			get => _patchNotesText;
			set
			{
				_patchNotesText = value;
				OnPropertyChanged();
			}
		}

		/// <inheritdoc />
		public async Task HandleMessage(IClientMessageContext<PSOBBPatchPacketPayloadClient> context, PatchingMessagePayload payload)
		{
			//When we get the payload just initialize the patchnotes text
			PatchNotesText = payload.Message;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
