using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using Reinterpret.Net;

namespace Booma.Proxy.Format.MapNRel.Dumper
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Enter Filename:");
			string fileName = Console.ReadLine();

			SerializerService serializer = new SerializerService();

			serializer.RegisterType<NRelMainBlockModel>();
			serializer.RegisterType<NRelTrailerModel>();
			serializer.RegisterType<NRelSectionModel>();
			serializer.RegisterType<NRelSectionsChunkModel>();
			serializer.Compile();

			byte[] bytes = File.ReadAllBytes(fileName);

			//Trailer is at -16 bytes from EOF.
			NRelTrailerModel trailer = serializer
				.Deserialize<NRelTrailerModel>(new DefaultStreamReaderStrategy(new MemoryStream(bytes, bytes.Length - 16, 16)));

			NRelMainBlockModel mainBlock = serializer
				.Deserialize<NRelMainBlockModel>(new DefaultStreamReaderStrategy(new MemoryStream(bytes, (int)trailer.MainBlockPointer, (int)(bytes.Length - trailer.MainBlockPointer))));

			NRelSectionsChunkModel sections = serializer
				.Deserialize<NRelSectionsChunkModel>(
					new DefaultStreamReaderStrategy(new MemoryStream(bytes, (int)mainBlock.SectionPointer, (int)(bytes.Length - mainBlock.SectionPointer)))
					.PreprendWithBytes(((int)mainBlock.SectionCount).Reinterpret()));

			List<string> linesToWrite = new List<string>(sections.Sections.Count());

			foreach(var s in sections.Sections)
			{
				string log = s.ToString();

				Console.WriteLine(log);
				linesToWrite.Add(log);
			}

			File.WriteAllLines($"{Path.GetFileNameWithoutExtension(fileName)}_out.txt", linesToWrite);
			File.WriteAllBytes($"{Path.GetFileNameWithoutExtension(fileName)}.bytes", serializer.Serialize(sections));

			Console.WriteLine("Press any key to close...");
			Console.ReadKey();
		}
	}
}
