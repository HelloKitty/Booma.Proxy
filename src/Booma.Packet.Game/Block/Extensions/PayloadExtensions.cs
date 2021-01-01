using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Booma
{
	public static class PayloadExtensions
	{
		/// <summary>
		/// Converts the message into a hex logging string.
		/// </summary>
		/// <param name="payload">The payload to generate a string for.</param>
		/// <returns>A hex string for the payload.</returns>
		public static string OpCodeHexString([NotNull] this PSOBBGamePacketPayloadServer payload)
		{
			if(payload == null) throw new ArgumentNullException(nameof(payload));

			return $"{payload.OperationCode:X}";
		}
	}
}
