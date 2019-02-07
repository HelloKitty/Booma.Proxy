using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	[SceneTypeCreate(GameSceneType.LobbyDefault)]
	public sealed class MovementManagerTickable : IGameTickable
	{
		private IReadonlyEntityGuidMappable<MovementManager> MovementManagerMappable { get; }

		/// <inheritdoc />
		public MovementManagerTickable([NotNull] IReadonlyEntityGuidMappable<MovementManager> movementManagerMappable)
		{
			MovementManagerMappable = movementManagerMappable ?? throw new ArgumentNullException(nameof(movementManagerMappable));
		}

		/// <inheritdoc />
		public void Tick()
		{
			//So we just step through each movement manager
			foreach(var kvp in MovementManagerMappable)
			{
				//TODO: We don't currently support or have time like VRGuardians. So we can just provide a 0.
				kvp.Value.Update(kvp.Key, 0);
			}
		}
	}
}
