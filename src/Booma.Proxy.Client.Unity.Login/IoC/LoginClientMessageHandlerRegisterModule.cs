using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class LoginClientMessageHandlerRegisterModule : NonBehaviourDependency
	{
		/// <inheritdoc />
		public override void Register(IServiceRegister register)
		{
			//Foreach handler in the scene we need to register it
			//so that the IoC container can provide the collection of handlers as a potential
			//dependency to other objects.
			foreach(IClientMessageHandler<PSOBBLoginPacketPayloadServer, PSOBBLoginPacketPayloadClient> h in Component.FindObjectsOfType(typeof(IClientMessageHandler<PSOBBLoginPacketPayloadServer, PSOBBLoginPacketPayloadClient>)))
			{
				register.RegisterInstance<IClientMessageHandler<PSOBBLoginPacketPayloadServer, PSOBBLoginPacketPayloadClient>, IClientMessageHandler<PSOBBLoginPacketPayloadServer, PSOBBLoginPacketPayloadClient>>(h);
			}

			register.RegisterTransient<MessageHandlerService<PSOBBLoginPacketPayloadServer, PSOBBLoginPacketPayloadClient>, MessageHandlerService<PSOBBLoginPacketPayloadServer, PSOBBLoginPacketPayloadClient>>();
		}
	}
}
