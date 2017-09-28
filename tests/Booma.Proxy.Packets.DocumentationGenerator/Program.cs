using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy.Packets.DocumentationGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			//Overwrite the original file
			Directory.CreateDirectory(@"docs");
			File.Create(@"docs\PatchPacketDocumentation.md").Close();

			StringBuilder builder = new StringBuilder();

			//Generate the patch packet documentation
			builder.Append(BuildHeader(1, "Patch Packets"));
			InsertLineBreak(builder);
			InsertLineBreak(builder);

			builder.Append(BuildPacketHeaderRow());
			InsertLineBreak(builder);

			//TODO: Cache or sort for perf
			IEnumerable<Type> PatchPacketTypes = PacketPatchServerMetadataMarker.SerializableTypes;

			foreach(PatchNetworkOperationCodes opcode in Enum.GetValues(typeof(PatchNetworkOperationCodes)))
			{
				string row = BuildPacketInformationRow(opcode.ToString(), $"0x{String.Format("{0:X4}",(int)opcode)}",
					PatchPacketTypes.FirstOrDefault(p => HasOpCodeAttribute<PatchServerPacketPayloadAttribute>(p, opcode)),
					PatchPacketTypes.FirstOrDefault(p => HasOpCodeAttribute<PatchClientPacketPayloadAttribute>(p, opcode)));

				builder.Append(row);
				InsertLineBreak(builder);
			}

			File.WriteAllText(@"docs\PatchPacketDocumentation.md", builder.ToString());
		}

		private static string BuildPacketInformationRow(string opcodeName, string opcodeValueAsString, Type optionalServerPayloadType, Type optionalClientPayloadType)
		{
			return $"| {opcodeName} | {opcodeValueAsString} | {BuildUrlLinkFromName(optionalServerPayloadType?.Name)} | {BuildUrlLinkFromName(optionalServerPayloadType?.Name)} |";
		}

		private static object BuildUrlLinkFromName(string name)
		{
			//TODO: add URL linking to the files
			return string.IsNullOrWhiteSpace(name) ? @"**n/a**" : $"[{name}](\"todo\")";
		}

		private static bool HasOpCodeAttribute<TWireLinkBaseAttributeType>(Type t, PatchNetworkOperationCodes opcode)
			where TWireLinkBaseAttributeType : WireDataContractBaseLinkAttribute
		{
			TWireLinkBaseAttributeType attri = t.GetCustomAttribute<TWireLinkBaseAttributeType>();

			if(attri == null)
				return false;

			return attri.Index == (int)opcode;
		}

		private static string BuildPacketHeaderRow()
		{
			return @"| Packet OpCode Name | OpCode| Sent by Server | Sent by Client |" + "\r\n" + @"| ------------- | ------------- | ------------- | ------------- |";
		}

		/*| Packet OpCode Name | OpCode| Sent by Server | Sent by Client |
| ------------- | ------------- | ------------- | ------------- |
| [SEND_FILE]("link")  | 0x11 | [SomePayloadType]("")  | N/A |
| Content Cell  | Content Cell  |*/

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
