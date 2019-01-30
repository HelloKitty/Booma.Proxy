using System;
using System.Security;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//Based on source from: https://github.com/dotnet/corefx/blob/4a58e005144f023bf176f2ba5f87d32688f10d85/src/Common/src/CoreLib/System/Runtime/CompilerServices/YieldAwaitable.cs
	/// <summary>Provides an awaitable context for switching into a target environment.</summary>
	/// <remarks>This type is intended for compiler use only.</remarks>
	public readonly struct UnityYieldAwaitable
	{
		/// <summary>Gets an awaiter for this <see cref="YieldAwaitable"/>.</summary>
		/// <returns>An awaiter for this awaitable.</returns>
		/// <remarks>This method is intended for compiler user rather than use directly in code.</remarks>
		public UnityYieldAwaiter GetAwaiter() { return new UnityYieldAwaiter(); }

		/// <summary>Provides an awaiter that switches into a target environment.</summary>
		/// <remarks>This type is intended for compiler use only.</remarks>
		public readonly struct UnityYieldAwaiter : ICriticalNotifyCompletion
		{
			/// <summary>Gets whether a yield is not required.</summary>
			/// <remarks>This property is intended for compiler user rather than use directly in code.</remarks>
			public bool IsCompleted => false;// yielding is always required for YieldAwaiter, hence false

			/// <summary>Posts the <paramref name="continuation"/> back to the current context.</summary>
			/// <param name="continuation">The action to invoke asynchronously.</param>
			/// <exception cref="System.ArgumentNullException">The <paramref name="continuation"/> argument is null (Nothing in Visual Basic).</exception>
			public void OnCompleted(Action continuation)
			{
				QueueContinuation(continuation, flowContext: true);
			}

			/// <summary>Posts the <paramref name="continuation"/> back to the current context.</summary>
			/// <param name="continuation">The action to invoke asynchronously.</param>
			/// <exception cref="System.ArgumentNullException">The <paramref name="continuation"/> argument is null (Nothing in Visual Basic).</exception>
			public void UnsafeOnCompleted(Action continuation)
			{
				QueueContinuation(continuation, flowContext: false);
			}

			/// <summary>Posts the <paramref name="continuation"/> back to the current context.</summary>
			/// <param name="continuation">The action to invoke asynchronously.</param>
			/// <param name="flowContext">true to flow ExecutionContext; false if flowing is not required.</param>
			/// <exception cref="System.ArgumentNullException">The <paramref name="continuation"/> argument is null (Nothing in Visual Basic).</exception>
			private static void QueueContinuation(Action continuation, bool flowContext)
			{
				// Validate arguments
				if(continuation == null) throw new ArgumentNullException(nameof(continuation));

				//TODO: Check that the thread context was set.
				UnityExtended.UnityMainThreadContext.Post(s_sendOrPostCallbackRunAction, continuation);
			}

			/// <summary>SendOrPostCallback that invokes the Action supplied as object state.</summary>
			private static readonly SendOrPostCallback s_sendOrPostCallbackRunAction = RunAction;

			/// <summary>Runs an Action delegate provided as state.</summary>
			/// <param name="state">The Action delegate to invoke.</param>
			private static void RunAction(object state) { ((Action)state)(); }

			/// <summary>Ends the await operation.</summary>
			public void GetResult() { } // Nop. It exists purely because the compiler pattern demands it.
		}
	}
}