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
	/// code for the Type: <see cref = "CharacterProgressReversed"/>
	/// </summary>
	public sealed partial class CharacterProgressReversed_Serializer : BaseAutoGeneratedSerializerStrategy<CharacterProgressReversed_Serializer, CharacterProgressReversed>
	{
		/// <summary>
		/// Auto-generated deserialization/read method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalRead(CharacterProgressReversed value, Span<byte> buffer, ref int offset)
		{
			//Type: CharacterProgressReversed Field: 1 Name: Experience Type: UInt32
			;
			value.Experience = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
			//Type: CharacterProgressReversed Field: 2 Name: RawLevel Type: UInt32
			;
			value.RawLevel = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
		}

		/// <summary>
		/// Auto-generated serialization/write method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalWrite(CharacterProgressReversed value, Span<byte> buffer, ref int offset)
		{
			//Type: CharacterProgressReversed Field: 1 Name: Experience Type: UInt32
			;
			GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.Experience, buffer, ref offset);
			//Type: CharacterProgressReversed Field: 2 Name: RawLevel Type: UInt32
			;
			GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.RawLevel, buffer, ref offset);
		}
	}
}