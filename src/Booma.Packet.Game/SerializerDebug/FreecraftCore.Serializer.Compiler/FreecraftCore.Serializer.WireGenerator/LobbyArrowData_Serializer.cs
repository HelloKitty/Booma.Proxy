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
    /// code for the Type: <see cref="LobbyArrowData"/>
    /// </summary>
    public sealed partial class LobbyArrowData_Serializer
            : BaseAutoGeneratedSerializerStrategy<LobbyArrowData_Serializer, LobbyArrowData>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(LobbyArrowData value, Span<byte> buffer, ref int offset)
        {
            //Type: LobbyArrowData Field: 1 Name: Tag Type: Int32;
            value.Tag = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
            //Type: LobbyArrowData Field: 2 Name: GuildCardNumber Type: UInt32;
            value.GuildCardNumber = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
            //Type: LobbyArrowData Field: 3 Name: Arrow Type: UInt32;
            value.Arrow = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(LobbyArrowData value, Span<byte> buffer, ref int offset)
        {
            //Type: LobbyArrowData Field: 1 Name: Tag Type: Int32;
            GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.Tag, buffer, ref offset);
            //Type: LobbyArrowData Field: 2 Name: GuildCardNumber Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.GuildCardNumber, buffer, ref offset);
            //Type: LobbyArrowData Field: 3 Name: Arrow Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.Arrow, buffer, ref offset);
        }
    }
}