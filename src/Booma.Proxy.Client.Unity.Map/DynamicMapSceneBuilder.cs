using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booma.Proxy;
using SceneJect.Common;
using UnityEngine;

namespace Booma
{
	[Injectee]
	public class DynamicMapSceneBuilder
	{
		private IUnitScalerStrategy Scaler { get; }

		public DynamicMapSceneBuilder(IUnitScalerStrategy scaler)
		{
			if(scaler == null) throw new ArgumentNullException(nameof(scaler));

			Scaler = scaler;
		}

		public void AddObjectsToScene(IEnumerable<MapDataFormatObjectEntry> objects)
		{
			int index = 0;

			Dictionary<int, GameObject> SectionRootObject = new Dictionary<int, GameObject>();

			foreach(var entry in objects)
			{
				GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;

				o.name = $"{index}_{entry.ObjectType.ToString()}_{entry.Identifier}";

				o.transform.position = Scaler.Scale(entry.Position.ToUnityVector3());

				//TODO: Is this right?
				o.transform.rotation = Quaternion.AngleAxis(Scaler.ScaleYRotation(entry.Rotation.Y), Vector3.up);

				if(!SectionRootObject.ContainsKey(entry.Section))
					SectionRootObject[entry.Section] = new GameObject($"Section_{entry.Section}_Root");

				o.transform.parent = SectionRootObject[entry.Section].transform;


				index++;
			}
		}
	}
}
