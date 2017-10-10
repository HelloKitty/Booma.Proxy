using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// The base type for PSOBB login payloads that the client sends. This isn't for login/ship.
	/// Contains the <see cref="Flags"/> optional byte chunk and maps to child
	/// types based on a 2 byte opcode <see cref="ushort"/> that comes over the network.
	/// </summary>
	[WireDataContract(WireDataContractAttribute.KeyType.UShort, true)]
	public abstract class PSOBBLoginPacketPayloadClient : IPacketPayload
	{
		/// <summary>
		/// The optional flags field.
		/// This value is different for some packets than others.
		/// </summary>
		[KnownSize(4)] //always 4 bytes
		[WireMember(2)]
		public byte[] Flags { get; } = new byte[4]; //we can initialize new flags every payload since they're always there

		/// <summary>
		/// Parameterless ctor.
		/// Flags will be 0.
		/// </summary>
		protected PSOBBLoginPacketPayloadClient()
		{
			
		}

		/// <summary>
		/// Optional ctor that allows for setting the flags field.
		/// Will throw if the length is greater than 4 or null.
		/// </summary>
		/// <param name="flags">The flags to set.</param>
		protected PSOBBLoginPacketPayloadClient([NotNull] byte[] flags)
		{
			if(flags == null) throw new ArgumentNullException(nameof(flags));

			//Limit size
			if(flags.Length > 4) throw new ArgumentException("Value cannot be a collection with size  greater than 4.", nameof(flags));

			//If it's 4 then we should just set it
			//Otherwise we need to extend the array to size of 4
			if(flags.Length == 4)
				Flags = flags;
			else
				Flags = flags.Concat(Enumerable.Repeat((byte)0, 4 - flags.Length)).ToArray();
		}
	}
}
