using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	//TODO: This is a test component.
	public class MapObjectTestComponent : SerializedMonoBehaviour
	{
		/// <summary>
		/// Should be set 
		/// </summary>
		public MapDataFormatObjectEntry MapObject;

		[Button]
		public void DebugLogObjectDetails()
		{
			Debug.Log($"Object: {name} \n{MapObject.ToFullString()}");
		}
	}
}
