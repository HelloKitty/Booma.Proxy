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

	//Tethealla
	//unsigned char data[12]; // the standard $setitem1 - $setitem3 fare
	//unsigned itemid; // player item id
	//unsigned char data2[4]; // $setitem4 (mag use only)

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

		//We don't use EMPTY here so it's easier to interact with these chunks.
		[KnownSize(12)]
		[WireMember(4)]
		public byte[] ItemData1 { get; set; } = new byte[12];

		[WireMember(5)]
		public uint ItemId { get; internal set; }

		//We don't use EMPTY here so it's easier to interact with these chunks.
		[KnownSize(4)]
		[WireMember(6)]
		public byte[] ItemData2 { get; set; } = new byte[4];

		public InventoryItem(uint itemId, ushort equippedSlot, ushort technique, uint flags, byte[] itemData1, byte[] itemData2)
		{
			EquippedSlot = equippedSlot;
			Technique = technique;
			Flags = flags;
			ItemData1 = itemData1 ?? throw new ArgumentNullException(nameof(itemData1));
			ItemData2 = itemData2 ?? throw new ArgumentNullException(nameof(itemData2));
			ItemId = itemId;
		}

		public InventoryItem(uint itemId, ushort equippedSlot, ushort technique, uint flags)
		{
			EquippedSlot = equippedSlot;
			Technique = technique;
			Flags = flags;
			ItemId = itemId;
		}

		public void SetWeaponType(byte type)
		{
			ItemData1[1] = type;
		}

		public void SetEmpty()
		{
			SetWeaponType(0xFF);
			ItemId = 0xFFFFFFFF;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public InventoryItem()
		{
			//TODO: Serializer will call this pointlessly.
			SetEmpty();
		}
	}
}
