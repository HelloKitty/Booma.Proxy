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
    /// code for the Type: <see cref="LobbyMenuEntry"/>
    /// </summary>
    public sealed partial class LobbyMenuEntry_Serializer
            : BaseAutoGeneratedSerializerStrategy<LobbyMenuEntry_Serializer, LobbyMenuEntry>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(LobbyMenuEntry value, Span<byte> buffer, ref int offset)
        {
            //Type: MenuItemIdentifier Field: 1 Name: MenuId Type: UInt32;
            value.MenuId = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
            //Type: MenuItemIdentifier Field: 2 Name: ItemId Type: UInt32;
            value.ItemId = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
            //Type: LobbyMenuEntry Field: 1 Name: Padding Type: Int32;
            value.Padding = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(LobbyMenuEntry value, Span<byte> buffer, ref int offset)
        {
            //Type: MenuItemIdentifier Field: 1 Name: MenuId Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.MenuId, buffer, ref offset);
            //Type: MenuItemIdentifier Field: 2 Name: ItemId Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.ItemId, buffer, ref offset);
            //Type: LobbyMenuEntry Field: 1 Name: Padding Type: Int32;
            GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.Padding, buffer, ref offset);
        }
    }
}