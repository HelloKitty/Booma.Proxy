using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/*typedef struct sylverant_bitem {
	    union {
	        uint8_t data_b[12];
	        uint16_t data_w[6];
	        uint32_t data_l[3];
	    };

	    uint32_t item_id;

	    union {
	        uint8_t data2_b[4];
	        uint16_t data2_w[2];
	        uint32_t data2_l;
	    };

	    uint16_t amount;
	    uint16_t flags;
	} PACKED sylverant_bitem_t;*/

	//Based on: https://github.com/Sylverant/libsylverant/blob/206b44f906054c081f47627e546ea19dc00322b4/include/sylverant/characters.h#L67
	[WireDataContract]
	public sealed class BankItem
	{
		//TODO: Implement structure.
		[KnownSize(24)]
		[WireMember(1)]
		internal byte[] ItemData { get; set; } = Array.Empty<byte>();

		public BankItem(byte[] itemData)
		{
			ItemData = itemData ?? throw new ArgumentNullException(nameof(itemData));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public BankItem()
		{
			
		}
	}
}
