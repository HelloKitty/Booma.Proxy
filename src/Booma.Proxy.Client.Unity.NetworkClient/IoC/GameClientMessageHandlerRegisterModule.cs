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
	public sealed class GameClientMessageHandlerRegisterModule : NonBehaviourDependency
	{
		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			//Foreach handler in the scene we need to register it
			//so that the IoC container can provide the collection of handlers as a potential
			//dependency to other objects.
			IEnumerable<IClientMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>> handlers = FindObjectsOfType<GameObject>()
				.SelectMany(go => go.GetComponents<IClientMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>())
				.Where(c => c != null)
				.Distinct()
				.ToList();

			Debug.Log($"Found Handler Count: {handlers.Count()}");

			foreach(var h in handlers)
				register.RegisterInstance(h)
					.As<IClientMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>();

			register.RegisterType<DefaultPayloadHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>()
				.As<IClientPayloadSpecificMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>()
				.SingleInstance();

			register.RegisterType<MessageHandlerService<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>()
				.As<MessageHandlerService<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient>>();
		}
	}
}
