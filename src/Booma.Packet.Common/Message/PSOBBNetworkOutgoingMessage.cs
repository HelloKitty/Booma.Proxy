using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
{
	/// <summary>
	/// Full message for a PSOBB 
	/// </summary>
	[WireDataContract]
	public sealed class PSOBBNetworkOutgoingMessage
	{
		/// <summary>
		/// The header for the outgoing message.
		/// </summary>
		[WireMember(1)]
		public PSOBBPacketHeader Header { get; internal set; }

		/// <summary>
		/// The byte representation of the payload.
		/// </summary>
		[ReadToEnd]
		[WireMember(2)]
		public byte[] Payload { get; internal set; }

		/// <summary>
		/// Creates a new outgoing message envelope. Represents a complete
		/// logical packet including the header and the payload byte representation.
		/// </summary>
		/// <param name="payload">The payload.</param>
		public PSOBBNetworkOutgoingMessage(byte[] payload)
		{
			if(payload == null) throw new ArgumentNullException(nameof(payload));

			Payload = payload;
			Header = new PSOBBPacketHeader(payload.Length + 2);
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public PSOBBNetworkOutgoingMessage()
		{
			
		}
	}
}
