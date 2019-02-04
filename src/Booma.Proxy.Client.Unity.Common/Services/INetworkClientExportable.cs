using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	public interface INetworkClientExportable
	{
		/// <summary>
		/// Indicates if the network client has been exported.
		/// </summary>
		bool isClientExported { get; }

		void ExportmanagedClient();
	}
}
