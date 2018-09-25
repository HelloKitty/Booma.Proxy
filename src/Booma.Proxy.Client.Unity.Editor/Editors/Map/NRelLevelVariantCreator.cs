using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Booma.Proxy.Editor
{
	public sealed class NRelLevelVariantCreator : EditorWindow
	{
		private Episode1Map DesiredMap { get; set; }

		private int BaseMap { get; set; }

		private int Variant { get; set; }
 
		[MenuItem("Booma/Map/NRelVariantCreator")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow<NRelLevelVariantCreator>();
		}

		void OnGUI()
		{
			DesiredMap = (Episode1Map)EditorGUILayout.EnumPopup(DesiredMap, "The map.");
			BaseMap = EditorGUILayout.IntField(BaseMap, "The base map id (Ex. (_XX_00) where base map id is XX");
			Variant = EditorGUILayout.IntField(Variant, "The variant number of the map (ex. (_00_XX) where the variant is XX");

			if(GUILayout.Button("Generate Variant"))
			{
				MapPathBuilder pathbuilder = new MapPathBuilder(".bytes");

				string objectPath = pathbuilder.GenerateDataPath(DesiredMap, BaseMap, Variant);
				string sectionPath = pathbuilder.GenerateSectionDataPath(DesiredMap, BaseMap);

				DynamicMapSceneBuilder sceneBuilder = new DynamicMapSceneBuilder(new DefaultPSOScaleUnitScalerStrategy());

				MapFileDeserializer mapSerializer = new MapFileDeserializer();
				sceneBuilder.AddObjectsToScene(mapSerializer.LoadObjects(objectPath), mapSerializer.LoadSections(sectionPath).Sections.ToDictionary(model => (int)model.SecionId));
			}
		}
	}
}
