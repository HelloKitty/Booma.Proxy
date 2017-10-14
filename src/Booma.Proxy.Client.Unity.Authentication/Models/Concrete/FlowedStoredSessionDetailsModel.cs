using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Data model that flows (loads) the session details from storage only.
	/// </summary>
	public sealed class FlowedStoredSessionDetailsModel : SessionDetailsPlayerPrefsStorageModel
	{
		private void Awake()
		{
			//Just all init, the base will handle loading
			//from storage. Setters will handle saving too
			//This one line will allow the session details to flow throughout the process.
			InitializeFromPrefs();
		}
	}
}
