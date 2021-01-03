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
    /// code for the Type: <see cref="SecurityData"/>
    /// </summary>
    public sealed partial class SecurityData_Serializer
            : BaseAutoGeneratedSerializerStrategy<SecurityData_Serializer, SecurityData>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(SecurityData value, Span<byte> buffer, ref int offset)
        {
            //Type: SecurityData Field: 1 Name: Magic Type: UInt32;
            value.Magic = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
            //Type: SecurityData Field: 2 Name: Slot Type: Byte;
            value.Slot = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: SecurityData Field: 3 Name: SelectedCharacter Type: Boolean;
            value.SelectedCharacter = GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Read(buffer, ref offset);
            //Type: SecurityData Field: 3 Name: Unknown Type: Byte[];
            value.Unknown = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_34>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(SecurityData value, Span<byte> buffer, ref int offset)
        {
            //Type: SecurityData Field: 1 Name: Magic Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.Magic, buffer, ref offset);
            //Type: SecurityData Field: 2 Name: Slot Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.Slot, buffer, ref offset);
            //Type: SecurityData Field: 3 Name: SelectedCharacter Type: Boolean;
            GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Write(value.SelectedCharacter, buffer, ref offset);
            //Type: SecurityData Field: 3 Name: Unknown Type: Byte[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_34>.Instance.Write(value.Unknown, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_34 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 34; }
    }
}