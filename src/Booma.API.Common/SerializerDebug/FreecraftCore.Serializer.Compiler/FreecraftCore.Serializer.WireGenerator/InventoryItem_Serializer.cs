﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="InventoryItem"/>
    /// </summary>
    public sealed partial class InventoryItem_Serializer
            : BaseAutoGeneratedSerializerStrategy<InventoryItem_Serializer, InventoryItem>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(InventoryItem value, Span<byte> buffer, ref int offset)
        {
            //Type: InventoryItem Field: 1 Name: ItemData Type: Byte[];
            value.ItemData = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_28>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(InventoryItem value, Span<byte> buffer, ref int offset)
        {
            //Type: InventoryItem Field: 1 Name: ItemData Type: Byte[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_28>.Instance.Write(value.ItemData, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_28 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 28; }
    }
}