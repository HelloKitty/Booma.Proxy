using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Booma.Proxy
{
	public static class UnityExtended
	{
		public static SynchronizationContext UnityMainThreadContext { get; private set; }

		/// <summary>
		/// Sets the <see cref="UnityMainThreadContext"/> with the current
		/// thread's (caller thread) sync context.
		/// </summary>
		public static void InitializeSyncContext()
		{
			UnityMainThreadContext = SynchronizationContext.Current;
		}
	}
}