using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using GladNet;

namespace Booma.Proxy
{
	class Program
	{
		public static async Task Main(string[] args)
		{
			PsobbHandlerRegisterationModule psobbHandlerModules = new PsobbHandlerRegisterationModule();

			psobbHandlerModules.AddServerHandlerModule(new PsobbProxyTestSessionMessageHandlerRegisterationModule());
			psobbHandlerModules.AddClientHanderModule(new PsobbProxyTestClientMessageHandlerRegisterationModule());

			PsobbProxyApplicationBase appBase = new PsobbProxyApplicationBase(new NetworkAddressInfo(IPAddress.Parse("127.0.0.1"), 5055),
				new NetworkAddressInfo(IPAddress.Parse("127.0.0.1"), 12000), new ConsoleLogger(LogLevel.All), psobbHandlerModules,
				new PsobbNetworkSerializers());

			if(!appBase.StartServer())
			{
				Console.WriteLine("Failed to start proxy. Press any key to close.");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Starting proxy.");

			await appBase.BeginListening();
		}
	}
}
