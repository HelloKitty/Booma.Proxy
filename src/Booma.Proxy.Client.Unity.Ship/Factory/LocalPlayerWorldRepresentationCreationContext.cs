using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Context for creating a local player.
	/// </summary>
	public sealed class LocalPlayerWorldRepresentationCreationContext
	{
		/// <summary>
		/// Data contains spawn information for the local player.
		/// </summary>
		public EntityAssoicatedObject<WorldTransform> SpawnData { get; }

		/// <inheritdoc />
		public LocalPlayerWorldRepresentationCreationContext(int entityId, Vector3 spawnLocation, Quaternion spawnRotation)
		{
			if(EntityGuid.GetEntityType(entityId) != EntityType.Player)
				throw new ArgumentException($"Cannot create: {nameof(LocalPlayerWorldRepresentationCreationContext)} with guid with {nameof(EntityType)}: {EntityGuid.GetEntityType(entityId)}", nameof(entityId));

			SpawnData = new EntityAssoicatedObject<WorldTransform>(entityId, new WorldTransform(spawnLocation, spawnRotation));
		}

		/// <inheritdoc />
		public LocalPlayerWorldRepresentationCreationContext(int entityId, WorldTransform transform)
		{
			if(EntityGuid.GetEntityType(entityId) != EntityType.Player)
				throw new ArgumentException($"Cannot create: {nameof(LocalPlayerWorldRepresentationCreationContext)} with guid with {nameof(EntityType)}: {EntityGuid.GetEntityType(entityId)}", nameof(entityId));

			SpawnData = new EntityAssoicatedObject<WorldTransform>(entityId, transform);
		}

		/// <inheritdoc />
		public LocalPlayerWorldRepresentationCreationContext([NotNull] EntityAssoicatedObject<WorldTransform> data) 
		{
			if(data == null) throw new ArgumentNullException(nameof(data));
			if(EntityGuid.GetEntityType(data.EntityGuid) != EntityType.Player)
				throw new ArgumentException($"Cannot create: {nameof(LocalPlayerWorldRepresentationCreationContext)} with guid with {nameof(EntityType)}: {EntityGuid.GetEntityType(entityId)}", nameof(entityId));

			SpawnData = data
		}
	}
}
