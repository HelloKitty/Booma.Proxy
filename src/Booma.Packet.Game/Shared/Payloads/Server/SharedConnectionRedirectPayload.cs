using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/*typedef struct bb_redirect
	{
		bb_pkt_hdr_t hdr;
		uint32_t ip_addr;       //Big-endian
		uint16_t port;          //Little-endian
		uint8_t padding[2];
	}
	PACKED bb_redirect_pkt;*/

	//Syl sent: REDIRECT_TYPE https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/login_packets.c#L351
	//Syl struct: https://github.com/Sylverant/login_server/blob/d275702120ade56ce0b8b826a6c549753587d7e1/src/packets.h#L321
	[WireDataContract]
	[GameServerPacketPayload(GameNetworkOperationCode.REDIRECT_TYPE)]
	public sealed class SharedConnectionRedirectPayload : PSOBBGamePacketPayloadServer
	{
		/// <summary>
		/// The IPAddress that should be reidrected to.
		/// </summary>
		[KnownSize(4)]
		[WireMember(1)]
		internal byte[] IpAddressBytes { get; set; }

		/// <summary>
		/// The port for the endpoint to redirect to.
		/// </summary>
		[WireMember(2)]
		public short EndpointerPort { get; internal set; }

		//TODO: What and why?
		/// <summary>
		/// ?
		/// </summary>
		[KnownSize(2)]
		[WireMember(3)]
		internal byte[] padding { get; set; } = new byte[2];

		//Lazy cache of the computed ipaddr
		private Lazy<IPAddress> _EndpointAddress { get; }

		/// <summary>
		/// The <see cref="IPAddress"/> for the endpoint to redirect to.
		/// </summary>
		public IPAddress EndpointAddress => _EndpointAddress.Value;

		/// <summary>
		/// Creates a new redirect to the specified endpoint address and port.
		/// </summary>
		/// <param name="ipAddress">The IP to redirect to.</param>
		/// <param name="port">The port to redirect to.</param>
		public SharedConnectionRedirectPayload([NotNull] string ipAddress, short port)
			: this()
		{
			if(string.IsNullOrWhiteSpace(ipAddress)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(ipAddress));

			IPAddress addr = null;
			if(!IPAddress.TryParse(ipAddress, out addr))
				throw new ArgumentException($"Address {nameof(ipAddress)} was in an invalid format.");

			//We have to reverse for endianness
			IpAddressBytes = addr.GetAddressBytes();
			EndpointerPort = port;
		}

		/// <summary>
		/// Creates a new redirect to the specified endpoint address and port.
		/// </summary>
		/// <param name="ipAddress">The IP to redirect to.</param>
		/// <param name="port">The port to redirect to.</param>
		public SharedConnectionRedirectPayload([NotNull] IPAddress ipAddress, short port)
			: this()
		{
			if(ipAddress == null) throw new ArgumentNullException(nameof(ipAddress));

			//We have to reverse for endianness
			IpAddressBytes = ipAddress.GetAddressBytes();
			EndpointerPort = port;
		}

		public SharedConnectionRedirectPayload()
		{
			_EndpointAddress = new Lazy<IPAddress>(() => new IPAddress(IpAddressBytes), true);
		}
	}
}
