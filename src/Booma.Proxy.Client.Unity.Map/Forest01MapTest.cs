using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booma.Proxy;
using UnityEngine;

namespace Booma
{
	public class Forest01MapTest : MonoBehaviour
	{
		public void Awake()
		{
			MapPathBuilder pathbuilder = new MapPathBuilder(".bytes");

			string objectPath = pathbuilder.GenerateDataPath(Episode1Map.Forest01, 0, 0);
			string sectionPath = pathbuilder.GenerateSectionDataPath(Episode1Map.Forest01, 0);

			DynamicMapSceneBuilder sceneBuilder = new DynamicMapSceneBuilder(new DefaultPSOScaleUnitScalerStrategy());

			MapFileDeserializer mapSerializer = new MapFileDeserializer();
			sceneBuilder.AddObjectsToScene(mapSerializer.LoadObjects(objectPath), mapSerializer.LoadSections(sectionPath).Sections.ToDictionary(model => (int)model.SecionId));
		}
	}
}
