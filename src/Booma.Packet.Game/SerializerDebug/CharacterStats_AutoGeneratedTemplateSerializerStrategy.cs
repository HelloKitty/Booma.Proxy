using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;

namespace FreecraftCore.Serializer
{
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="CharacterStats"/>
    /// </summary>
    public sealed partial class CharacterStats_AutoGeneratedTemplateSerializerStrategy
        : BaseAutoGeneratedSerializerStrategy<CharacterStats_AutoGeneratedTemplateSerializerStrategy, CharacterStats>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(CharacterStats value, Span<byte> buffer, ref int offset)
        {
            //Type: CharacterStats Field: 1 Name: Stats Type: UInt16[];
            value.Stats = FixedSizePrimitiveArrayTypeSerializerStrategy<ushort, StaticTypedNumeric_Int32_7>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(CharacterStats value, Span<byte> buffer, ref int offset)
        {
            //Type: CharacterStats Field: 1 Name: Stats Type: UInt16[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<ushort, StaticTypedNumeric_Int32_7>.Instance.Write(value.Stats, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_7 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 7; }
    }
}