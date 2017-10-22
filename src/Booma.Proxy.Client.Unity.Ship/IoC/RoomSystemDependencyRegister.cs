using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class RoomSystemDependencyRegister : NonBehaviourDependency
	{
		//TODO: When we actually have a room system redo this
		[SerializeField]
		public TestingRoomSystem RoomSystem;

		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			register.RegisterInstance(RoomSystem)
				.As<IRoomQueryable>()
				.As<IRoomCollection>()
				.SingleInstance();
		}
	}
}
