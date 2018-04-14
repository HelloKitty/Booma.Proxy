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

			foreach(var m in model)
			{
				string log = m.ToString();

				Console.WriteLine(log);
				linesToWrite.Add(log);
			}

			File.WriteAllLines($"{Path.GetFileNameWithoutExtension(fileName)}_out.txt", linesToWrite);

			Console.WriteLine("Press any key to close...");
			Console.ReadKey();
		}
	}

	[WireDataContract]
	public class Test
	{
		[SendSize(SendSizeAttribute.SizeType.Int32)]
		[DontTerminate]
		[Encoding(EncodingType.UTF16)]
		[WireMember(1)]
		public string TestString { get; }

		[SendSize(SendSizeAttribute.SizeType.Byte)]
		[WireMember(2)]
		public short[] Shorts { get; }

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
