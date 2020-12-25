using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Static constants Type for patch packets.
	/// </summary>
	public static class NetworkPatchPacketConstants
	{
		/// <summary>
		/// Patch packet headers are 2 byte Uint16 values of packet length.
		/// </summary>
		public const int PATCH_PACKET_HEADER_SIZE = 2;

		/// <summary>
		/// Patch packet's maximum size allowed.
		/// </summary>
		public const int PATCH_PACKET_MAXIMUM_SIZE = 1024;
	}
}
