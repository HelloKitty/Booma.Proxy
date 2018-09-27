using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	[Injectee]
	public sealed class EditorExposedZoneSettings : SerializedMonoBehaviour, IZoneSettings
	{
		[Inject]
		private IManualInjectionStrategy InjectionService { get; }

		[PropertyTooltip("Should be the ID of the zone. (Ex. 15 for Lobby or 03 for Caves-01)")]
		[SerializeField]
		private short _zoneId;

		/// <inheritdoc />
		public short ZoneId => _zoneId;

		void Start()
		{
			if(InjectionService is null)
				throw new InvalidOperationException("Cannot have zone settings without injection service configuration.");

			//TODO: Is this the best place to do this, it's kidna a hack
			GlobalGameServices.InjectionService = InjectionService;
		}
	}
}
