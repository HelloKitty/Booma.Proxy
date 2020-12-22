using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Based on the specification from: http://sharnoth.com/psodevwiki/format/dat
	/// </summary>
	[WireDataContract]
	public sealed class MapDatFormatModel
	{
		[WireMember(1)]
		internal MapDatFormatTableEntryContainer[] _Entries { get; set; }

		public IEnumerable<MapDatFormatTableEntryContainer> Entries => _Entries;

		//Serializer ctor
		protected MapDatFormatModel()
		{
			
		}
	}
}
