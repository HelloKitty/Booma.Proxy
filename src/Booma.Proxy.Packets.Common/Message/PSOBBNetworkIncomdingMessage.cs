using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Full message for a PSOBB incoming packet.
	/// </summary>
	public sealed class PSOBBNetworkIncomdingMessage<TPayloadType>
		where TPayloadType : class
	{
		/// <summary>
		/// The header for the incoming message.
		/// </summary>
		public IPacketHeader Header { get; }

		/// <summary>
		/// The deserialized payload for the incomding header.
		/// </summary>
		public TPayloadType Payload { get; }

		/// <summary>
		/// Creates a new outgoing message envelope. Represents a complete
		/// logical packet including the header and the payload byte representation.
		/// </summary>
		/// <param name="payload">The payload.</param>
		public PSOBBNetworkIncomdingMessage([NotNull] IPacketHeader header, [NotNull] TPayloadType payload)
		{
			if(header == null) throw new ArgumentNullException(nameof(header));
			if(payload == null) throw new ArgumentNullException(nameof(payload));

			Header = header;
			Payload = payload;
		}

		//Serializer ctor
		private PSOBBNetworkIncomdingMessage()
		{
			
		}
	}
}
