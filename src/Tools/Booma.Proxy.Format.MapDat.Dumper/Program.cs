using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using Reinterpret.Net;

namespace Booma
{
	class Program
	{
		//MapData can be gathered from Tethealla. xx_xx_o.dat contains the variant object data.
		//TODO: Support multiple types. Only does object right now
		static void Main(string[] args)
		{
			Console.WriteLine("Enter Filename:");
			string fileName = Console.ReadLine();

			SerializerService serializer = new SerializerService();

			serializer.RegisterType<MapDatFormatGenericBodyModel<MapDataFormatObjectEntry>>();
			serializer.Compile();

			byte[] bytes = File.ReadAllBytes(fileName);

			int count = bytes.Length / 68;

			//TODO: Find a way to deal with count without having to do this
			bytes = count.Reinterpret().Concat(bytes).ToArray();

			Console.WriteLine("Deserializing.");

			MapDatFormatGenericBodyModel<MapDataFormatObjectEntry> model = serializer.Deserialize<MapDatFormatGenericBodyModel<MapDataFormatObjectEntry>>(bytes);

			Console.WriteLine("Done Deserializing.");

			List<string> linesToWrite = new List<string>(count);

			int index = 0;
			foreach(var m in model)
			{
				string log = $"Index: {index} {m.ToString()}";

				Console.WriteLine(log);
				linesToWrite.Add(log);

				index++;
			}

			File.WriteAllLines($"{Path.GetFileNameWithoutExtension(fileName)}_out.txt", linesToWrite);

			Console.WriteLine("Press any key to close...");
			Console.ReadKey();
		}
	}

	[WireDataContract]
	public class Test
	{
		[SendSize(PrimitiveSizeType.Int32)]
		[DontTerminate]
		[Encoding(EncodingType.UTF16)]
		[WireMember(1)]
		public string TestString { get; internal set; }

		[SendSize(PrimitiveSizeType.Byte)]
		[WireMember(2)]
		public short[] Shorts { get; internal set; }

		/// <inheritdoc />
		public Test(string testString, short[] shorts)
		{
			TestString = testString;
			Shorts = shorts;
		}

		//Serializer ctorp
		protected Test()
		{
			
		}
	}
}
