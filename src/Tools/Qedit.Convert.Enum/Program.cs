using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Qedit.Convert.Enum
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Enter Filename:");
			string fileName = Console.ReadLine();

			//0	Player Set	-	Rotation Y
			Regex columMatchRegex = new Regex(@"^([^\t]*)");
			Regex whitespaceRemovalRegex = new Regex(@"\s+");
			Regex invalidEnumnameRegex = new Regex(@"(\?|\-|,|" + "\"" + @"|\(|\)|\/|\')+");

			File.WriteAllLines($"{Path.GetFileNameWithoutExtension(fileName)}_out.txt", File.ReadAllLines(fileName)
				.Select(s =>
				{
					string number = whitespaceRemovalRegex.Replace(columMatchRegex.Match(s).Value, "");
					string type = whitespaceRemovalRegex.Replace(columMatchRegex.Match(s.Replace($"{number}\t", "")).Value, "");

					//Replace invalid characters after replacing some we want to do
					type = invalidEnumnameRegex.Replace(type, "_");

					if(char.IsDigit(type.First()))
						type = $"_{type}";

					return $"{type} = {number},";
				}));
		}
	}
}
