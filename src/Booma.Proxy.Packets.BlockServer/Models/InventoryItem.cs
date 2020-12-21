﻿using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma.Proxy
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
		//TODO: Implement structure.
		[KnownSize(28)]
		[WireMember(1)]
		private byte[] ItemData { get; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private InventoryItem()
		{
			
		}
	}
}
