using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	//WARNING: This is technically not a model of the original data. I added the length prefix size for easier reading
	/// <summary>
	/// Format of the custom n.bytes format.
	/// Used in Unity3D to load section information.
	/// </summary>
	[WireDataContract]
	public sealed class NRelSectionsChunkModel
	{
		[SendSize(SendSizeAttribute.SizeType.Int32)]
		[WireMember(1)]
		private NRelSectionModel[] _Sections { get; }

		/// <summary>
		/// NRel sections data.
		/// </summary>
		public IEnumerable<NRelSectionModel> Sections => _Sections;

		public NRelSectionsChunkModel()
		{
			
		}
	}
}
