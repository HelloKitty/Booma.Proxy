using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma
{
	/*typedef struct sylverant_iitem {
	    uint16_t equipped; 2
	    uint16_t tech; 2
	    uint32_t flags; 4
	    
	    union {
	        uint8_t data_b[12]; 12
	        uint16_t data_w[6];
	        uint32_t data_l[3];
	    };
	    
	    uint32_t item_id; 4
	    
	    union {
	        uint8_t data2_b[4]; 4
	        uint16_t data2_w[2];
	        uint32_t data2_l;
	    };
	} PACKED sylverant_iitem_t;*/

	//Based on: https://github.com/Sylverant/libsylverant/blob/7f7e31d90da1b02c8d89d055628540ee3ad59417/include/sylverant/characters.h#L48
	[WireDataContract]
	public sealed class InventoryItem
	{
		//TODO: This may not be correct.
		[IgnoreDataMember]
		public bool IsEquipped => EquippedSlot > 0;

		//Maybe this is the slot its equipped in?
		[WireMember(1)]
		public ushort EquippedSlot { get; internal set; }

		//Don't know what this is
		[WireMember(2)]
		public ushort Technique { get; internal set; }

		[WireMember(3)]
		public uint Flags { get; internal set; }

		[KnownSize(12)]
		[WireMember(4)]
		internal byte[] ItemData1 { get; set; } = Array.Empty<byte>();

		[WireMember(5)]
		public uint ItemId { get; internal set; }

		[KnownSize(4)]
		[WireMember(6)]
		internal byte[] ItemData2 { get; set; } = Array.Empty<byte>();

		public InventoryItem(uint itemId, ushort equippedSlot, ushort technique, uint flags)
		{
			EquippedSlot = equippedSlot;
			Technique = technique;
			Flags = flags;
			ItemId = itemId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public InventoryItem()
		{
			
		}
	}
}
