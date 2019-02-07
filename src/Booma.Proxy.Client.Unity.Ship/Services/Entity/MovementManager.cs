using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Movement manager is the manager class that drives movement logic.
	/// It uses a queue-based system to step through movement generators.
	/// </summary>
	public sealed class MovementManager : IMovementGenerator<int>
	{
		/// <summary>
		/// A non-sorted non-priority-based queue of movement states.
		/// </summary>
		private Queue<IMovementGeneratorState> MovementStates { get; }

		/// <summary>
		/// The current movement state.
		/// </summary>
		private IMovementGeneratorState CurrentState { get; set; }

		private readonly object SyncObj = new object();

		private IReadonlyEntityGuidMappable<GameObject> EntityWorldObjectMap { get; }

		public MovementManager([NotNull] IReadonlyEntityGuidMappable<GameObject> entityWorldObjectMap)
		{
			EntityWorldObjectMap = entityWorldObjectMap ?? throw new ArgumentNullException(nameof(entityWorldObjectMap));
			MovementStates = new Queue<IMovementGeneratorState>(3);
		}

		public void RegisterState([NotNull] IMovementGeneratorState state)
		{
			if(state == null) throw new ArgumentNullException(nameof(state));

			lock(SyncObj)
				MovementStates.Enqueue(state);
		}

		/// <inheritdoc />
		public void Update(int entity, long currentTime)
		{
			//We might then we should not block here for so long but
			//the issue is if we didn't then someone could carefully stream movement packets
			//to players in the lobby and put them in an endless loop trying to skip skippable packets
			lock(SyncObj)
			{
				//If we have no current state we have to get one.
				if(CurrentState == null)
				{
					//But if there are no states we should give up.
					if(MovementStates.Count == 0)
						return;

					CurrentState = MovementStates.Dequeue();

					//This prevents an endless loop
					//Though this should never happen
					//Reentrant lock will also prevent anything from changing
					//so this is safe.
					if(CurrentState == null)
						Update(entity, currentTime);
				}

				//TODO: We should cache finished call maybe
				//The case where we have a non-null currentstate is done
				//or we we have queued up movement states and the current state is low priority/skippable.
				if(CurrentState.isFinished || (CurrentState.isSkippable && MovementStates.Count == 0))
				{
					CurrentState = MovementStates.Dequeue();

					//This may seem weird, but calling into the same method again here recursively makes sense
					//because it'll check if the next state is finished (probably not) or is skippable and we have more.
					//It will eventually run out of skippable states or we'll find one that isn't skippable
					Update(entity, currentTime);
				}

				//At this point the CurrentState is either finished, unskippable or we've got no more states left.
				//If it's finished then we have no reason to call it.
				if(CurrentState.isFinished)
				{
					//Setting this null here will allow
					//for the next call to basically check if it's null and if we have no more and it can leave
					//quick.
					CurrentState = null;
					return;
				}
			}

			//TODO: Is it safe to assume that an object exists for the entity Update is calling with?
			//Once we have the current state though and we can use it THEN we can release the lock.
			GameObject entityWorldObject = EntityWorldObjectMap[entity];
			CurrentState.Update(entityWorldObject, currentTime);
		}
	}
}
