﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;

namespace FreecraftCore.Serializer
{
	[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
	//THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
	/// <summary>
	/// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
	/// code for the Type: <see cref = "CharacterBankData"/>
	/// </summary>
	public sealed partial class CharacterBankData_Serializer : BaseAutoGeneratedSerializerStrategy<CharacterBankData_Serializer, CharacterBankData>
	{
		/// <summary>
		/// Auto-generated deserialization/read method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalRead(CharacterBankData value, Span<byte> buffer, ref int offset)
		{
			//Type: CharacterBankData Field: 1 Name: ItemCount Type: Int32
			;
			value.ItemCount = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
			//Type: CharacterBankData Field: 2 Name: Money Type: Int32
			;
			value.Money = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
			//Type: CharacterBankData Field: 3 Name: Items Type: BankItem[]
			;
			value.Items = FixedSizeComplexArrayTypeSerializerStrategy<BankItem_Serializer, BankItem, StaticTypedNumeric_Int32_200>.Instance.Read(buffer, ref offset);
		}

		/// <summary>
		/// Auto-generated serialization/write method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalWrite(CharacterBankData value, Span<byte> buffer, ref int offset)
		{
			//Type: CharacterBankData Field: 1 Name: ItemCount Type: Int32
			;
			GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.ItemCount, buffer, ref offset);
			//Type: CharacterBankData Field: 2 Name: Money Type: Int32
			;
			GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.Money, buffer, ref offset);
			//Type: CharacterBankData Field: 3 Name: Items Type: BankItem[]
			;
			FixedSizeComplexArrayTypeSerializerStrategy<BankItem_Serializer, BankItem, StaticTypedNumeric_Int32_200>.Instance.Write(value.Items, buffer, ref offset);
		}

		private sealed class StaticTypedNumeric_Int32_200 : StaticTypedNumeric<Int32>
		{
			public sealed override Int32 Value => 200;
		}
	}
}