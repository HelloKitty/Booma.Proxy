using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Uses the stored connection details that were stored
	/// for the purposes of flowing through the session.
	/// </summary>
	public sealed class FlowedStoredConnectionDetailsModel : ConnectionDetailsPlayerPrefsStorageModel
	{
		private void Awake()
		{
			//Init the details from prefs
			InitializeFromPrefs();
		}
	}
}
