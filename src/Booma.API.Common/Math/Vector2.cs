using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Generic 2-dimensional vector.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	[WireDataContract]
	public class Vector2<T>
	{
		/// <summary>
		/// X value.
		/// </summary>
		[WireMember(1)]
		public T X { get; internal set; }

		/// <summary>
		/// Y value.
		/// </summary>
		[WireMember(2)]
		public T Y { get; internal set; }

		/// <inheritdoc />
		public Vector2(T x, T y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Vector2()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"X: {X} Y: {Y}";
		}
	}
}
