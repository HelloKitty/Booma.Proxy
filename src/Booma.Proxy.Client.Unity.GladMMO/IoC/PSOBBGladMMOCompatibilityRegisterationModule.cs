using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GladMMO;
using GladNet;
using SceneJect.Common;

namespace Booma.Proxy
{
	public sealed class PSOBBGladMMOCompatibilityRegisterationModule : NonBehaviourDependency
	{
		/// <inheritdoc />
		public override void Register(ContainerBuilder builder)
		{
			//This is required to catch outgoing client packets
			//and convert them to PSOBB packets and send them
			builder.RegisterType<GladMMONetworkClientPSOBBPayloadIntercepterSendService>()
				.As<IPeerPayloadSendService<GameClientPacketPayload>>()
				.SingleInstance();
		}
	}
}
