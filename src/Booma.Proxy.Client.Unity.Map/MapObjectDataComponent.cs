using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	//This component exists only for debugging.
	public class MapObjectDataComponent : SerializedMonoBehaviour
	{
		[ShowInInspector]
		[ReadOnly]
		[OdinSerialize]
		private MapDataFormatObjectEntry _MapObject;

		/// <summary>
		/// Should be set 
		/// </summary>
		public MapDataFormatObjectEntry MapObject
		{
			get => _MapObject;
			set => _MapObject = value;
		} //needs to be publicly settable for creation time

		[Button]
		public void DebugLogObjectDetails()
		{
			Debug.Log($"Object: {name} \n{MapObject.ToFullString()}");
		}
	}
}
