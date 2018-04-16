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
		public uint SecionId { get; }

		[WireMember(2)]
		public Vector3<int> Position { get; }

		[WireMember(3)]
		private int unk1 { get; }

		[WireMember(4)]
		public uint YRotation { get; }

		[WireMember(5)]
		private int unk2 { get; }

		[WireMember(6)]
		private float unk3 { get; }

		[WireMember(7)]
		public uint SimpleGeometryPointer { get; }

		[WireMember(8)]
		public uint AnimatedGeometryPointer { get; }

		[WireMember(9)]
		public uint SimpleGeometryCount { get; }

		[WireMember(10)]
		public uint AnimatedGeometryCount { get; }

		[WireMember(11)]
		private int unk4 { get; }

		public NRelSectionModel()
		{
			
		}
	}
}
