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
    /// code for the Type: <see cref="PSOBBNetworkOutgoingMessage"/>
    /// </summary>
    public sealed partial class PSOBBNetworkOutgoingMessage_Serializer
            : BaseAutoGeneratedSerializerStrategy<PSOBBNetworkOutgoingMessage_Serializer, PSOBBNetworkOutgoingMessage>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(PSOBBNetworkOutgoingMessage value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBNetworkOutgoingMessage Field: 1 Name: Header Type: PSOBBPacketHeader;
            value.Header = PSOBBPacketHeader_Serializer.Instance.Read(buffer, ref offset);
            //Type: PSOBBNetworkOutgoingMessage Field: 2 Name: Payload Type: Byte[];
            value.Payload = PrimitiveArrayTypeSerializerStrategy<byte>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(PSOBBNetworkOutgoingMessage value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBNetworkOutgoingMessage Field: 1 Name: Header Type: PSOBBPacketHeader;
            PSOBBPacketHeader_Serializer.Instance.Write(value.Header, buffer, ref offset);
            //Type: PSOBBNetworkOutgoingMessage Field: 2 Name: Payload Type: Byte[];
            PrimitiveArrayTypeSerializerStrategy<byte>.Instance.Write(value.Payload, buffer, ref offset);
        }
    }
}