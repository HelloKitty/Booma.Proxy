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
	public sealed class EditorExposedZoneSettings : SerializedMonoBehaviour, IZoneSettings
	{
		[PropertyTooltip("Should be the ID of the zone. (Ex. 15 for Lobby or 03 for Caves-01)")]
		[SerializeField]
		private short _zoneId;

		/// <inheritdoc />
		public short ZoneId => _zoneId;
	}
}
