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

			//Login server
			/*PsobbProxyApplicationBase loginAppBase = new PsobbProxyApplicationBase(new NetworkAddressInfo(IPAddress.Parse("127.0.0.1"), 12000),
				new NetworkAddressInfo(IPAddress.Parse("192.168.0.3"), 12000), new ConsoleLogger(LogLevel.All), psobbHandlerModules,
				new PsobbNetworkSerializers());

			PsobbProxyApplicationBase characterAppBase = new PsobbProxyApplicationBase(new NetworkAddressInfo(IPAddress.Parse("127.0.0.1"), 12001),
				new NetworkAddressInfo(IPAddress.Parse("192.168.0.3"), 12001), new ConsoleLogger(LogLevel.All), psobbHandlerModules,
				new PsobbNetworkSerializers());

			//5278
			PsobbProxyApplicationBase shipAppBase = new PsobbProxyApplicationBase(new NetworkAddressInfo(IPAddress.Parse("127.0.0.1"), 5002),
				new NetworkAddressInfo(IPAddress.Parse("192.168.0.12"), 5002), new ConsoleLogger(LogLevel.All), psobbHandlerModules,
				new PsobbNetworkSerializers());*/

			PsobbProxyApplicationBase block1AppBase = new PsobbProxyApplicationBase(new NetworkAddressInfo(IPAddress.Parse("127.0.0.1"), 5003),
				new NetworkAddressInfo(IPAddress.Parse("192.168.0.12"), 5003), new ConsoleLogger(LogLevel.All), psobbHandlerModules,
				new PsobbNetworkSerializers());

			/*if(!loginAppBase.StartServer())
			{
				Console.WriteLine("Failed to start login proxy. Press any key to close.");
				Console.ReadKey();
				return;
			}

			if(!characterAppBase.StartServer())
			{
				Console.WriteLine("Failed to start character proxy. Press any key to close.");
				Console.ReadKey();
				return;
			}

			if(!shipAppBase.StartServer())
			{
				Console.WriteLine("Failed to start ship proxy. Press any key to close.");
				Console.ReadKey();
				return;
			}*/
			
			if(!block1AppBase.StartServer())
			{
				Console.WriteLine("Failed to start block proxy. Press any key to close.");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Starting proxy.");

			/*Task.Run(loginAppBase.BeginListening)
				.ConfigureAwait(false);

			Task.Run(characterAppBase.BeginListening)
				.ConfigureAwait(false);

			Task.Run(shipAppBase.BeginListening)
				.ConfigureAwait(false);*/

			Task.Run(block1AppBase.BeginListening)
				.ConfigureAwait(false);

			await Task.Delay(int.MaxValue)
				.ConfigureAwait(false);
		}
	}
}
