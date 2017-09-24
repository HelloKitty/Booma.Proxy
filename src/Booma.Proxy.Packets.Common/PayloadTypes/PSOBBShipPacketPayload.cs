﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// The base type for PSOBB ship packet payloads. This isn't for patch/login.
	/// Contains the <see cref="Flags"/> optional byte chunk and maps to child
	/// types based on a 2 byte opcode <see cref="ushort"/> that comes over the network.
	/// </summary>
	[WireDataContract(WireDataContractAttribute.KeyType.UShort, true)]
	public abstract class PSOBBShipPacketPayload
	{
		/// <summary>
		/// The optional flags field.
		/// This value is different for some packets than others.
		/// </summary>
		[KnownSize(4)] //always 4 bytes
		[WireMember(1)]
		public byte[] Flags { get; } = new byte[4]; //we can initialize new flags every payload since they're always there
	}
}
