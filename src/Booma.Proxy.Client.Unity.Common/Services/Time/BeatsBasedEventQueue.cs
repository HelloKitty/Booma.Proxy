using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// An event queue based on Beats timing for scheduling
	/// and dispatching.
	/// </summary>
	public sealed class BeatsBasedEventQueue : IBeatsEventQueueDispatchable, IBeatsEventQueueRegisterable
	{
		/// <summary>
		/// The function that can produce the current beats time.
		/// </summary>
		private Func<double> CurrentBeatsTimeFunction { get; }

		/// <summary>
		/// The internally managed event queue.
		/// </summary>
		private C5.IntervalHeap<IBeatEvent> EventQueue { get; }

		/// <summary>
		/// The syncronization object.
		/// </summary>
		private readonly object syncObj = new object();

		/// <inheritdoc />
		public bool isBeatEventReady
		{
			get
			{
				lock(syncObj)
				{
					if(EventQueue.IsEmpty)
						return false;

					IBeatEvent beatsEvent = EventQueue.FindMin();

					//If the scheduled time exceeds the beats time then one is ready.
					return CurrentBeatsTimeFunction() >= beatsEvent.ScheduledBeatTime;
				}
			}
		}

		/// <summary>
		/// Creates a new empty beats event queue with the specified way to compute current time.
		/// </summary>
		/// <param name="currentBeatsTimeFunction">The func for computing the current beats time.</param>
		public BeatsBasedEventQueue(Func<double> currentBeatsTimeFunction)
		{
			if(currentBeatsTimeFunction == null) throw new ArgumentNullException(nameof(currentBeatsTimeFunction));

			CurrentBeatsTimeFunction = currentBeatsTimeFunction;
			EventQueue = new C5.IntervalHeap<IBeatEvent>(10, new IBeatsEventComparer());
		}

		/// <inheritdoc />
		public void RegisterEvent(double scheduledDispatchBeatTime, Action eventToDispatch)
		{
			if(scheduledDispatchBeatTime < CurrentBeatsTimeFunction())
				throw new InvalidOperationException($"Cannot register event with scheduled Time: {scheduledDispatchBeatTime} because that time has already passed.");

			IBeatEvent beatsEvent = new ScheduledBeatsEvent(scheduledDispatchBeatTime, eventToDispatch);

			//Just register the beats event
			lock(syncObj)
				EventQueue.Add(beatsEvent);
		}

		/// <inheritdoc />
		public void DispatchNext()
		{
			IBeatEvent beatEvent = null;

			//Must lock around the isReady check to prevent potential dequeue races.
			lock(syncObj)
			{
				//Check if we're ready
				if(!isBeatEventReady)
					throw new InvalidOperationException($"Requested a beat event dispatch but {nameof(isBeatEventReady)} was false. Must check first.");

				//Remove and delete
				beatEvent = EventQueue.FindMin();
				EventQueue.DeleteMin();
			}
				
			if(beatEvent == null)
				throw new InvalidOperationException($"Failed to read beat event from the Beat {nameof(EventQueue)}.");

			//Just dispatch the beat event
			beatEvent.Dispatch();
		}
	}
}
