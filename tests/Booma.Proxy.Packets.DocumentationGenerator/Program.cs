using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Generic.Math;
using JetBrains.Annotations;

namespace Booma.Proxy.Packets.DocumentationGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			//Overwrite the original file
			Directory.CreateDirectory(@"docs");
			File.Create(@"docs\PatchPacketDocumentation.md").Close();
			File.Create(@"docs\LoginPacketDocumentation.md").Close();
			File.Create(@"docs\ShipPacketDocumentation.md").Close();
			File.Create(@"docs\BlockPacketDocumentation.md").Close();
			File.Create(@"docs\CharacterPacketDocumentation.md").Close();

			string patchDocString = 
				BuildPacketDocumentation<PatchClientPacketPayloadAttribute, PatchServerPacketPayloadAttribute, PatchNetworkOperationCode>(PacketPatchServerMetadataMarker.SerializableTypes, "Patch");

			string loginDocString =
				BuildPacketDocumentation<GameClientPacketPayloadAttribute, GameServerPacketPayloadAttribute, GameNetworkOperationCode>(PacketLoginServerMetadataMarker.SerializableTypes, "Login");

			string characterDocString =
				BuildPacketDocumentation<GameClientPacketPayloadAttribute, GameServerPacketPayloadAttribute, GameNetworkOperationCode>(PacketCharacterServerMetadataMarker.SerializableTypes, "Character");

			string blockDocString =
				BuildPacketDocumentation<GameClientPacketPayloadAttribute, GameServerPacketPayloadAttribute, GameNetworkOperationCode>(PacketBlockServerMetadataMarker.SerializableTypes, "Block");

			string shipDocString =
				BuildPacketDocumentation<GameClientPacketPayloadAttribute, GameServerPacketPayloadAttribute, GameNetworkOperationCode>(PacketShipServerMetadataMarker.SerializableTypes, "Ship");

			File.WriteAllText(@"docs\PatchPacketDocumentation.md", patchDocString);
			File.WriteAllText(@"docs\LoginPacketDocumentation.md", loginDocString);
			File.WriteAllText(@"docs\CharacterPacketDocumentation.md", characterDocString);
			File.WriteAllText(@"docs\BlockPacketDocumentation.md", blockDocString);
			File.WriteAllText(@"docs\ShipPacketDocumentation.md", shipDocString);
		}

		public static string BuildPacketDocumentation<TOutgoingPayloadAttributeType, TIncomingPayloadAttributeType, TOpcodeType>([NotNull] IEnumerable<Type> packets, string packetType)
			where TOpcodeType : struct
			where TOutgoingPayloadAttributeType : WireDataContractBaseLinkAttribute
			where TIncomingPayloadAttributeType : WireDataContractBaseLinkAttribute
		{
			if(packets == null) throw new ArgumentNullException(nameof(packets));
			packets = packets.ToList();

			StringBuilder builder = new StringBuilder();

			//Generate the patch packet documentation
			builder.Append(BuildHeader(1, $"{packetType} Packets"));
			InsertLineBreak(builder);
			InsertLineBreak(builder);

			builder.Append(BuildPacketHeaderRow());
			InsertLineBreak(builder);

			foreach(TOpcodeType opcode in Enum.GetValues(typeof(TOpcodeType)))
			{
				Type serverPayloadType = packets.FirstOrDefault(p => HasOpCodeAttribute<TIncomingPayloadAttributeType, TOpcodeType>(p, opcode));
				Type clientPayloadType = packets.FirstOrDefault(p => HasOpCodeAttribute<TOutgoingPayloadAttributeType, TOpcodeType>(p, opcode));

				//Skip if there are no payloads for this OpCode
				if(serverPayloadType == null && clientPayloadType == null)
					continue;

				string row = BuildPacketInformationRow(opcode.ToString(), $"0x{String.Format("{0:X4}", GenericMath.Convert<TOpcodeType, int>(opcode))}",
						serverPayloadType, clientPayloadType, packetType);

				builder.Append(row);
				InsertLineBreak(builder);
			}

			InsertLineBreak(builder);
			InsertLineBreak(builder);
			builder.Append("This documentation was automatically generated using the documentation tools.");

			return builder.ToString();
		}

		private static string BuildPacketInformationRow(string opcodeName, string opcodeValueAsString, Type optionalServerPayloadType, Type optionalClientPayloadType, string packetType)
		{
			return $"| {opcodeName} | {opcodeValueAsString} | {BuildUrlLinkFromName(optionalServerPayloadType?.Name, "Server", packetType)} | {BuildUrlLinkFromName(optionalClientPayloadType?.Name, "Client", packetType)} |";
		}

		private static object BuildUrlLinkFromName(string name, string subdirName, string packetType)
		{
			//TODO: add URL linking to the files
			return string.IsNullOrWhiteSpace(name) ? @"**n/a**" : $"[{name}]({@"https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets."}{packetType}{@"Server/Payloads"}/{subdirName}/{name}.cs)";
		}

		private static bool HasOpCodeAttribute<TWireLinkBaseAttributeType, TOpcodeType>(Type t, TOpcodeType opcode)
			where TWireLinkBaseAttributeType : WireDataContractBaseLinkAttribute
		{
			TWireLinkBaseAttributeType attri = t.GetCustomAttribute<TWireLinkBaseAttributeType>();

			if(attri == null)
				return false;

			return attri.Index == GenericMath.Convert<TOpcodeType, int>(opcode);
		}

		private static string BuildPacketHeaderRow()
		{
			return @"| Packet OpCode Name | OpCode | Sent by Server | Sent by Client |" + "\r\n" + @"| ------------- | ------------- | ------------- | ------------- |";
		}

		public static void InsertLineBreak(StringBuilder builder)
		{
			builder.Append("\r\n");
		}

		public static string BuildHeader(int depth, string header)
		{
			return $"{new string(Enumerable.Repeat('#', depth).ToArray())} {header}";
		}
	}
}
