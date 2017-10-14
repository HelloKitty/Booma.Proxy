using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using NUnit.Framework;

namespace Booma.Proxy.Packets.Tests.UnitTests
{
	[TestFixture]
	public class AllPacketsTests
	{
		public static IEnumerable<Type> PayloadTypes { get; } = PacketLoginServerMetadataMarker.SerializableTypes
			.Concat(PacketPatchServerMetadataMarker.SerializableTypes)
			.Concat(PacketSharedMetadataMarker.SerializableTypes)
			.Concat(PacketBlockServerMetadataMarker.SerializableTypes)
			.Concat(PacketShipServerMetadataMarker.SerializableTypes)
			.Concat(PacketCharacterServerMetadataMarker.SerializableTypes);

		[Test]
		[TestCaseSource(nameof(PayloadTypes))]
		public void Test_No_Other_Packet_Shares_BaseType_And_OpCode(Type t)
		{
			//arrange
			WireDataContractBaseLinkAttribute attri = t.GetCustomAttribute<WireDataContractBaseLinkAttribute>();

			foreach(Type payloadType in PayloadTypes)
			{
				if(payloadType == t)
					continue;

				//If it is the same base type we should check opcode
				if(payloadType.BaseType != t.BaseType)
					continue;

				//Check for non-duplicate opcode on same basetype
				Assert.AreNotEqual(attri.Index, payloadType.GetCustomAttribute<WireDataContractBaseLinkAttribute>().Index, $"Found duplicate OpCode: 0x{attri.Index:X} on Type: {t.Name} and Type: {payloadType.Name}.");
			}
		}
	}
}
