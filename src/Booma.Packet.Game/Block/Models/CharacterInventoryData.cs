using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma
{
	/*typedef struct sylverant_inventory {
		uint8_t item_count;
		uint8_t hpmats_used;
		uint8_t tpmats_used;
		uint8_t language;
		sylverant_iitem_t items[30];
	} PACKED sylverant_inventory_t;*/

	//Based on: https://github.com/Sylverant/libsylverant/blob/7f7e31d90da1b02c8d89d055628540ee3ad59417/include/sylverant/characters.h#L75
	[WireDataContract]
	public sealed class CharacterInventoryData
	{
		[WireMember(1)]
		public byte ItemCount { get; internal set; }

		[WireMember(2)]
		public byte HpMaterialsUsed { get; internal set; }

		[WireMember(3)]
		public byte TpMaterialsUsed { get; internal set; }

		[WireMember(4)]
		internal byte Language { get; set; }

		[WireMember(5)]
		[KnownSize(30)]
		public InventoryItem[] Items { get; internal set; }

		public CharacterInventoryData(byte itemCount, byte hpMaterialsUsed, byte tpMaterialsUsed, byte language, InventoryItem[] items)
		{
			ItemCount = itemCount;
			HpMaterialsUsed = hpMaterialsUsed;
			TpMaterialsUsed = tpMaterialsUsed;
			Language = language;
			Items = items;
		}

		public CharacterInventoryData(byte itemCount, byte hpMaterialsUsed, byte tpMaterialsUsed, byte language)
		{
			ItemCount = itemCount;
			HpMaterialsUsed = hpMaterialsUsed;
			TpMaterialsUsed = tpMaterialsUsed;
			Language = language;
			Items = new InventoryItem[30];
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterInventoryData()
		{
			
		}
	}
}
