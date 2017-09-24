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
using Reinterpret.Net;

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
	[WireDataContractBaseTypeRuntimeLink(0x19)]
	public sealed class ConnectionRedirectPayload : PSOBBShipPacketPayload, ISerializationEventListener
	{
		//The IP is not always sent. Some servers send it some down.
		//Since it's not possible to deduce Type from differences in packet length we
		//just put it all in a buffer and manually deserialize like it's the early 2000s.
		/// <summary>
		/// The byte buffer of the payload.
		/// </summary>
		[WireMember(1)]
		public byte[] PayloadBytes { get; private set; } //this could be 4 byte IP + 2 byte Port or just 2 byte port. May contain 2 byte padding aswell.

		//We don't need to add the padding because the packet will just stop reading.

		/// <summary>
		/// The <see cref="IPAddress"/> for the endpoint to redirect to.
		/// Will be null if the server didn't want to change the IPAddress for the connected
		/// endpoint connection.
		/// </summary>
		public IPAddress EndpointAddress { get; private set; }

		/// <summary>
		/// The port for the endpoint to redirect to.
		/// </summary>
		public short EndpointPort { get; private set; }

		/// <summary>
		/// Indicates if the redirect points to a new IPAddress.
		/// </summary>
		public bool isNewIpAddressRedirect => EndpointAddress != null;

		/// <summary>
		/// Creates a new redirect to the specified endpoint address and port.
		/// </summary>
		/// <param name="ipAddress">The IP to redirect to.</param>
		/// <param name="port">The port to redirect to.</param>
		public ConnectionRedirectPayload([NotNull] string ipAddress, short port)
		{
			if(string.IsNullOrWhiteSpace(ipAddress)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(ipAddress));

			IPAddress addr = null;
			if(!IPAddress.TryParse(ipAddress, out addr))
				throw new ArgumentException($"Address {nameof(ipAddress)} was in an invalid format.");

			EndpointAddress = addr;
			EndpointPort = port;
		}

		/// <summary>
		/// Creates a new redirect to the specified endpoint address and port.
		/// </summary>
		/// <param name="ipAddress">The IP to redirect to.</param>
		/// <param name="port">The port to redirect to.</param>
		public ConnectionRedirectPayload([NotNull] IPAddress ipAddress, short port)
		{
			if(ipAddress == null) throw new ArgumentNullException(nameof(ipAddress));

			//We have to reverse for endianness
			EndpointAddress = ipAddress;
			EndpointPort = port;
		}

		/// <summary>
		/// Creates a new redirect to the port only.
		/// </summary>
		/// <param name="ipAddress">The IP to redirect to.</param>
		/// <param name="port">The port to redirect to.</param>
		public ConnectionRedirectPayload(short port)
		{
			EndpointPort = port;
		}

		//serializer ctor
		private ConnectionRedirectPayload()
		{
			
		}

		public void OnBeforeSerialization()
		{
			//TODO: Cache the empty byte array

			//Sometimes we don't need or want the IPAddress to be sent
			if(isNewIpAddressRedirect)
				//We have to reverse for endianness
				PayloadBytes = EndpointAddress.GetAddressBytes().Reverse().ToArray();
			else
				PayloadBytes = Enumerable.Empty<byte>().ToArray();

			//we always add the port though
			PayloadBytes = PayloadBytes.Concat(EndpointPort.Reinterpret()).ToArray();
		}

		public void OnAfterDeserialization()
		{
			int position = 0;

			//If it's greater than 4 they sent the IP
			if(PayloadBytes.Length > 4)
			{
				EndpointAddress = new IPAddress(PayloadBytes.Take(4).Reverse().ToArray());
				position = 4;
			}

			//We should ALWAYS have port at least.
			EndpointPort = PayloadBytes.Reinterpret<short>(position);
		}
	}
}
