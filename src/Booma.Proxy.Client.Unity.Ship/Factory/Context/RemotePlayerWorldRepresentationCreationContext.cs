using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Context for creating a remote player.
	/// </summary>
	public sealed class RemotePlayerWorldRepresentationCreationContext
	{
		/// <summary>
		/// Data contains spawn information for the remote player.
		/// </summary>
		public EntityAssoicatedObject<WorldTransform> SpawnData { get; }

		/// <inheritdoc />
		public RemotePlayerWorldRepresentationCreationContext(int entityId, Vector3 spawnLocation, Quaternion spawnRotation)
		{
			if(EntityGuid.GetEntityType(entityId) != EntityType.Player)
				throw new ArgumentException($"Cannot create: {nameof(LocalPlayerWorldRepresentationCreationContext)} with guid with {nameof(EntityType)}: {EntityGuid.GetEntityType(entityId)}", nameof(entityId));

			SpawnData = new EntityAssoicatedObject<WorldTransform>(entityId, new WorldTransform(spawnLocation, spawnRotation));
		}

		/// <inheritdoc />
		public RemotePlayerWorldRepresentationCreationContext(int entityId, WorldTransform transform)
		{
			if(EntityGuid.GetEntityType(entityId) != EntityType.Player)
				throw new ArgumentException($"Cannot create: {nameof(LocalPlayerWorldRepresentationCreationContext)} with guid with {nameof(EntityType)}: {EntityGuid.GetEntityType(entityId)}", nameof(entityId));

			SpawnData = new EntityAssoicatedObject<WorldTransform>(entityId, transform);
		}

		/// <inheritdoc />
		public RemotePlayerWorldRepresentationCreationContext([NotNull] EntityAssoicatedObject<WorldTransform> data) 
		{
			if(data == null) throw new ArgumentNullException(nameof(data));
			if(EntityGuid.GetEntityType(data.EntityGuid) != EntityType.Player)
				throw new ArgumentException($"Cannot create: {nameof(LocalPlayerWorldRepresentationCreationContext)} with guid with {nameof(EntityType)}: {EntityGuid.GetEntityType(data.EntityGuid)}", nameof(data));

			SpawnData = data;
		}

		public RemotePlayerWorldRepresentationCreationContext(int entityId, Transform transform)
		{
			if(EntityGuid.GetEntityType(entityId) != EntityType.Player)
				throw new ArgumentException($"Cannot create: {nameof(LocalPlayerWorldRepresentationCreationContext)} with guid with {nameof(EntityType)}: {EntityGuid.GetEntityType(entityId)}", nameof(entityId));

			SpawnData = new EntityAssoicatedObject<WorldTransform>(entityId, new WorldTransform(transform.position, transform.rotation));
		}
	}
}
