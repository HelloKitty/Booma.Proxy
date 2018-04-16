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

			string path = pathbuilder.GeneratePath(Episode1Map.Forest01, 0, 0);

			DynamicMapSceneBuilder sceneBuilder = new DynamicMapSceneBuilder(new DefaultPSOScaleUnitScalerStrategy());

			sceneBuilder.AddObjectsToScene(new MapFileDeserializer().LoadObjects(path));
		}
	}
}
