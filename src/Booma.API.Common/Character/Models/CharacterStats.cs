using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Structure that represents a character's stats.
	/// </summary>
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

		/// <summary>
		/// Internal stats storage array.
		/// </summary>
		[KnownSize(7)]
		[WireMember(1)]
		public ushort[] Stats { get; internal set; } = Array.Empty<ushort>();

		/// <summary>
		/// Gets the specified <see cref="stat"/> value.
		/// </summary>
		/// <param name="stat"></param>
		/// <returns></returns>
		public ushort this[CharacterStatType stat] => Stats[(int) stat];

		public CharacterStats(ushort[] stats)
		{
			Stats = stats ?? throw new ArgumentNullException(nameof(stats));
		}

		public CharacterStats(IEnumerable<KeyValuePair<CharacterStatType, ushort>> stats)
		{
			//TODO: Constant
			Stats = new ushort[7];

			foreach (var kvp in stats)
				Stats[(int) kvp.Key] = kvp.Value;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterStats()
		{
			
		}
	}
}
