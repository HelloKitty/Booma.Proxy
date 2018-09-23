using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;
using Nito.AsyncEx;

namespace Booma.Proxy
{
	public sealed class DefaultServerPayloadHandler : IPeerPayloadSpecificMessageHandler<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadClient, IProxiedMessageContext<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer>>
	{
		private ILog Logger { get; }

		private AsyncLock LockObj { get; } = new AsyncLock();

		private BinaryPacketWriter PacketLogger { get; }

		/// <inheritdoc />
		public DefaultServerPayloadHandler([NotNull] ILog logger, [NotNull] BinaryPacketWriter packetLogger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			PacketLogger = packetLogger ?? throw new ArgumentNullException(nameof(packetLogger));
		}

		/// <inheritdoc />
		public async Task HandleMessage(IProxiedMessageContext<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadServer> context, PSOBBGamePacketPayloadServer payload)
		{
			//TODO: Kinda hacky to do it here, but keeping packets ordered this way. Mostly.
			using(await LockObj.LockAsync().ConfigureAwait(false))
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Recieved unhandled server payload Name: {payload.GetType().Name} with OpCode: {(GameNetworkOperationCode)payload.OperationCode} - {payload.OperationCode:X}");

				LogPayloadBytes(payload);

				await context.ProxyConnection.SendMessage(payload)
					.ConfigureAwait(false);
			}

			if(payload is IUnknownPayloadType u)
				await PacketLogger.WritePacketAsync((GameNetworkOperationCode)payload.OperationCode, u, BinaryPacketWriter.PacketType.Server)
					.ConfigureAwait(false);
		}

		private void LogPayloadBytes(PSOBBGamePacketPayloadServer payload)
		{
			if(payload is IUnknownPayloadType u)
			{
				//Some packets are several kilobytes. We DON'T want to write those to the screen.
				if(u.UnknownBytes.Length < 200)
					Logger.Info(GetBytesToString(u.UnknownBytes));
			}
		}

		public static string GetBytesToString(byte[] value)
		{
			//Based on MS SoapHexBinary source
			StringBuilder sb = new StringBuilder(value.Length * 4);

			unchecked
			{
				for(int i = 0; i < value.Length; i++)
				{
					//TODO: This part can probably be sped up
					String s = value[i].ToString("X", CultureInfo.InvariantCulture);

					sb.Append(" 0x");

					if(s.Length == 1)
						sb.Append('0');

					sb.Append(s);
				}
			}

			return sb.ToString();
		}
	}
}
