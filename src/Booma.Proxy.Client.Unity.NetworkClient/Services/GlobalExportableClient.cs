using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//TODO: This is sooo a hack but I don't have the patience to deal with this right now.
	public sealed class GlobalExportableClient : INetworkClientExportable
	{
		/// <inheritdoc />
		public bool isClientExported { get; } = GlobalNetwork.CurrentExportableClient.isClientExported;

		/// <inheritdoc />
		public void ExportmanagedClient()
		{
			GlobalNetwork.CurrentExportableClient.ExportmanagedClient();
		}
	}
}
