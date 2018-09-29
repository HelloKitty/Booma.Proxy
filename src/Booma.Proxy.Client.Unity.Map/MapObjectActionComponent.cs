using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class MapObjectActionComponent : SerializedMonoBehaviour
	{
		[ReadOnly]
		[SerializeField]
		private short _ObjectActionId;

		/// <summary>
		/// The action identifier sent in the packet
		/// for when a player interacts with the object.
		/// </summary>
		public short ObjectActionId => _ObjectActionId;

		internal void SetFromEntry(MapDataFormatObjectEntry objectEntry)
		{
			_ObjectActionId = objectEntry.ObjectInteractionId;
		}
	}
}
