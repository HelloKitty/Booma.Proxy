using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	public sealed class CharacterStats
	{
		/*  uint16_t atp;
		    uint16_t mst;
		    uint16_t evp;
		    uint16_t hp;
		    uint16_t dfp;
		    uint16_t ata;
		    uint16_t lck;*/

		//TODO: Provide a better API for accessing the stats data.
		[KnownSize(7)]
		[WireMember(1)]
		public ushort[] Stats { get; internal set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private CharacterStats()
		{
			
		}
	}
}
