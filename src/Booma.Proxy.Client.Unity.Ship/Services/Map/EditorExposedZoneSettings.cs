using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Booma.Proxy
{
	public sealed class EditorExposedZoneSettings : SerializedMonoBehaviour, IZoneSettings
	{
		/// <inheritdoc />
		[PropertyTooltip("Should be the ID of the zone. (Ex. 15 for Lobby or 03 for Caves-01)")]
		[OdinSerialize]
		public short ZoneId { get; private set; }
	}
}
