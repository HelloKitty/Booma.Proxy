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
	/// <summary>
	/// Raw .REL data component attached to objects
	/// generated from PSO REL data.
	/// </summary>
	public class MapObjectDataComponent : SerializedMonoBehaviour
	{
		/// <summary>
		/// Should be set 
		/// </summary>
		[OdinSerialize]
		public MapDataFormatObjectEntry MapObject { get; set; } //needs to be publicly settable for creation time

		[Button]
		public void DebugLogObjectDetails()
		{
			Debug.Log($"Object: {name} \n{MapObject.ToFullString()}");
		}
	}
}
