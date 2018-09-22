using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using Reinterpret.Net;
using UnityEngine;

namespace Booma
{
	public sealed class MapFileDeserializer
	{
		private static ISerializerService Serializer { get; } = CreateNewSerializer();

		private static ISerializerService CreateNewSerializer()
		{
			SerializerService serializer = new SerializerService();

			serializer.RegisterType<MapDatFormatGenericBodyModel<MapDataFormatObjectEntry>>();
			serializer.RegisterType<NRelSectionsChunkModel>();
			serializer.Compile();

			return serializer;
		}

		public MapDatFormatGenericBodyModel<MapDataFormatObjectEntry> LoadObjects(string path)
		{
			if(string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));

			TextAsset map = Resources.Load<TextAsset>(Path.ChangeExtension(path, null));

			if(map == null)
				throw new InvalidOperationException($"Unable load map from resources Path: {path}");

			//TODO: Redo sizing
			MapDatFormatGenericBodyModel<MapDataFormatObjectEntry> entries = Serializer.Deserialize<MapDatFormatGenericBodyModel<MapDataFormatObjectEntry>>(new DefaultStreamReaderStrategy(map.bytes).PreprendWithBytes((map.bytes.Length / 68).Reinterpret()));

			Resources.UnloadAsset(map);

			return entries;
		}

		public NRelSectionsChunkModel LoadSections(string path)
		{
			if(string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));

			TextAsset map = Resources.Load<TextAsset>(Path.ChangeExtension(path, null));

			if(map == null)
				throw new InvalidOperationException($"Unable load map from resources Path: {path}");

			NRelSectionsChunkModel model = Serializer.Deserialize<NRelSectionsChunkModel>(map.bytes);

			Resources.UnloadAsset(map);

			return model;
		}
	}
}
