using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// TODO
	/// </summary>
	/// <typeparam name="TBodyType"></typeparam>
	[WireDataContract]
	public sealed class MapDatFormatGenericBodyModel<TBodyType> : IEnumerable<TBodyType>
		where TBodyType : IBodySizable
	{
		[SendSize(PrimitiveSizeType.Int32)]
		[WireMember(1)]
		internal TBodyType[] _Entries { get; set; }

		/// <summary>
		/// Body entries.
		/// </summary>
		public IEnumerable<TBodyType> Entries => _Entries;

		/// <inheritdoc />
		public IEnumerator<TBodyType> GetEnumerator()
		{
			return Entries.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return _Entries.GetEnumerator();
		}
	}
}
