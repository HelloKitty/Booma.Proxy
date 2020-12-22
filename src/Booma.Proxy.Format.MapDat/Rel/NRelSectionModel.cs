using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booma.Proxy;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Data model for the n.rel section.
	/// </summary>
	[WireDataContract]
	public sealed class NRelSectionModel
	{
		[WireMember(1)]
		public uint SecionId { get; internal set; }

		[WireMember(2)]
		public Vector3<float> Position { get; internal set; }

		[WireMember(3)]
		internal int unk1 { get; set; }

		[WireMember(4)]
		public uint YRotation { get; internal set; }

		[WireMember(5)]
		internal int unk2 { get; set; }

		[WireMember(6)]
		internal float unk3 { get; set; }

		[WireMember(7)]
		public uint SimpleGeometryPointer { get; internal set; }

		[WireMember(8)]
		public uint AnimatedGeometryPointer { get; internal set; }

		[WireMember(9)]
		public uint SimpleGeometryCount { get; internal set; }

		[WireMember(10)]
		public uint AnimatedGeometryCount { get; internal set; }

		[WireMember(11)]
		internal int unk4 { get; set; }

		public NRelSectionModel()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"SectionId: {SecionId} Position: {Position}";
		}
	}
}
