using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	public static class MathPSO
	{
		/// <summary>
		/// Converts the provided <see cref="networkRotationRepresentation"/> 2byte (dword)
		/// into a y-axis singular rotation value.
		/// </summary>
		/// <param name="networkRotationRepresentation">The network representation.</param>
		/// <returns>The y-axis singular float rotation.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float FromNetworkRotationToYAxisRotation(this short networkRotationRepresentation)
		{
			return (networkRotationRepresentation & 0xffff) / 182.04444f;
		}

		/// <summary>
		/// Converts the provided <see cref="yaxisRotation"/> 4bytes
		/// into the PSO expected network 2byte representation.
		/// </summary>
		/// <param name="yaxisRotation">The yaxis rotation..</param>
		/// <returns>The 2byte network representation of the provided float.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ToNetworkRotation(this float yaxisRotation)
		{
			return (short)(yaxisRotation * 182.04444f);
		}
	}
}
