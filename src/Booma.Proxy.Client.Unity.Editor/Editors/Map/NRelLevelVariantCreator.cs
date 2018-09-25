using System;
using System.Collections.Generic;
using System.IO;
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
			EditorGUILayout.LabelField("The map.", EditorStyles.boldLabel);
			DesiredMap = (Episode1Map)EditorGUILayout.EnumPopup(DesiredMap);

			EditorGUILayout.LabelField("The base map id (Ex. (_XX_00) where base map id is XX", EditorStyles.boldLabel);
			BaseMap = EditorGUILayout.IntField(BaseMap);

			EditorGUILayout.LabelField("The variant number of the map (ex. (_00_XX) where the variant is XX", EditorStyles.boldLabel);
			Variant = EditorGUILayout.IntField(Variant);

			if(GUILayout.Button("Generate Variant"))
			{
				MapPathBuilder pathbuilder = new MapPathBuilder(".bytes");

				string objectPath = pathbuilder.GenerateObjectDataPath(DesiredMap, BaseMap, Variant);
				string sectionPath = pathbuilder.GenerateSectionDataPath(DesiredMap, BaseMap);

				DynamicMapSceneBuilder sceneBuilder = new DynamicMapSceneBuilder(new DefaultPSOScaleUnitScalerStrategy());

				MapFileDeserializer mapSerializer = new MapFileDeserializer();
				sceneBuilder.AddObjectsToScene(mapSerializer.LoadObjects(objectPath), mapSerializer.LoadSections(sectionPath).Sections.ToDictionary(model => (int)model.SecionId));
			}
		}
	}
}
