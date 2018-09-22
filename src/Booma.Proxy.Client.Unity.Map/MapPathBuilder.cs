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

		public MapPathBuilder(string extension = ".dat", string rootPath = @"maps/")
		{
			if(string.IsNullOrWhiteSpace(extension)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(extension));
			if(string.IsNullOrWhiteSpace(rootPath)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(rootPath));

			RootPath = rootPath;
			Extension = extension;
		}

		//TODO: Should we make generic? Should we add overloads?
		public string GenerateDataFileName(Episode1Map map, int basemap, int variation)
		{
			return $"map_{map.ToString().ToLower()}_{basemap:00}_{variation:00}";
		}

		public string GenerateDataPath(Episode1Map map, int basemap, int variation)
		{
			return Path.Combine(RootPath, $"{GenerateDataFileName(map, basemap, variation)}{Extension}");
		}

		//TODO: Refactor/doc
		/// <summary>
		/// Builds the file name path for the n.rel (n.bytes now) that contains
		/// section information.
		/// </summary>
		/// <param name="map"></param>
		/// <param name="basemap"></param>
		/// <returns></returns>
		public string GenerateSectionDataPath(Episode1Map map, int basemap)
		{
			return Path.Combine(RootPath, $"map_{map.ToString().ToLower()}_{basemap:00}n{Extension}");
		}
	}
}
