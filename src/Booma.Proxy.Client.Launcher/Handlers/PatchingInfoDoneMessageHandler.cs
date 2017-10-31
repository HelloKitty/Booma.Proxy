using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public sealed class PatchingInfoDoneMessageHandler : IPeerPayloadSpecificMessageHandler<PatchingInfoRequestDonePayload, PSOBBPatchPacketPayloadClient>, INotifyPropertyChanged
	{
		private bool _isPatchingDone = false;

		public bool isPatchingDone
		{
			get => _isPatchingDone;
			set
			{
				_isPatchingDone = value;
				OnPropertyChanged();
			}
		}

		/// <inheritdoc />
		public async Task HandleMessage(IPeerMessageContext<PSOBBPatchPacketPayloadClient> context, PatchingInfoRequestDonePayload payload)
		{
			//This payload means patching is complete
			isPatchingDone = true;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
