using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Object that can build a filename from map and variation.
	/// </summary>
	public sealed class MapPathBuilder
	{
		public string RootPath { get; }

		//WARNING: Must use .bytes extension in Unity3D if we want to load it as a binary file.
		/// <summary>
		/// The extension of the map file.
		/// Recommended to use ".bytes" for Unity3D asset loading.
		/// </summary>
		public string Extension { get; }

		public MapPathBuilder(string rootPath = @"/maps/", string extension = ".dat")
		{
			if(string.IsNullOrWhiteSpace(extension)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(extension));
			if(string.IsNullOrWhiteSpace(rootPath)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(rootPath));

			RootPath = rootPath;
			Extension = extension;
		}

		//TODO: Should we make generic? Should we add overloads?
		public string GenerateFileName(Episode1Map map, int variation)
		{
			return $"map_{map.ToString().ToLower()}_{variation:00}";
		}

		public string GeneratePath(Episode1Map map, int variation)
		{
			return Path.Combine(RootPath, $"{GenerateFileName(map, variation)}{Extension}");
		}
	}
}
