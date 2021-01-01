using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma;
using FreecraftCore.Serializer;

namespace Booma
{
	//See: https://github.com/Sylverant/libsylverant/blob/206b44f906054c081f47627e546ea19dc00322b4/include/sylverant/characters.h#L81
	[WireDataContract]
	public sealed class CharacterBankData
	{
		[WireMember(1)]
		public int ItemCount { get; internal set; }

		[WireMember(2)]
		public int Money { get; internal set; }

		//sylverant_bitem_t items[200];
		[KnownSize(200)]
		[WireMember(3)]
		public BankItem[] Items { get; set; }

		public CharacterBankData(int money, BankItem[] items)
		{
			ItemCount = items.Length;
			Money = money;

			//Adjust to the expected fixed size.
			if (items.Length == 200)
				Items = items;
			else
				Items = items
					.Concat(Enumerable.Repeat(new BankItem(), 200 - items.Length))
					.ToArray();
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterBankData()
		{
			
		}
	}
}
