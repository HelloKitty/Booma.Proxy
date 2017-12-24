using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	public sealed class Vector4<T> : Vector3<T>
	{
		/// <summary>
		/// X value.
		/// </summary>
		[WireMember(1)]
		public T W { get; }

		/// <inheritdoc />
		public Vector4(T x, T y, T z, T w) 
			: base(x, y, z)
		{
			W = w;
		}

		private Vector4()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"{base.ToString()} W: {W}";
		}
	}
}
