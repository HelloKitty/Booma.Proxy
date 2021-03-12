﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;
namespace Booma
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class GameCharacterDataInitializeClientResponsePayload
    {
        public override Type SerializableType => typeof(GameCharacterDataInitializeClientResponsePayload);
        public override PSOBBGamePacketPayloadClient Read(Span<byte> buffer, ref int offset)
        {
            GameCharacterDataInitializeClientResponsePayload_Serializer.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(PSOBBGamePacketPayloadClient value, Span<byte> buffer, ref int offset)
        {
            GameCharacterDataInitializeClientResponsePayload_Serializer.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="GameCharacterDataInitializeClientResponsePayload"/>
    /// </summary>
    public sealed partial class GameCharacterDataInitializeClientResponsePayload_Serializer
            : BaseAutoGeneratedSerializerStrategy<GameCharacterDataInitializeClientResponsePayload_Serializer, GameCharacterDataInitializeClientResponsePayload>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(GameCharacterDataInitializeClientResponsePayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadClient Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, UInt16>.Instance.Read(buffer, ref offset);
            //Type: PSOBBGamePacketPayloadClient Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)value.Flags = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Read(buffer, ref offset);
            //Type: GameCharacterDataInitializeClientResponsePayload Field: 1 Name: Bytes Type: Byte[];
            value.Bytes = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_2088>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(GameCharacterDataInitializeClientResponsePayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadClient Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, UInt16>.Instance.Write(value.OperationCode, buffer, ref offset);
            //Type: PSOBBGamePacketPayloadClient Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Write(value.Flags, buffer, ref offset);
            //Type: GameCharacterDataInitializeClientResponsePayload Field: 1 Name: Bytes Type: Byte[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_2088>.Instance.Write(value.Bytes, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_2088 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 2088; }
        private sealed class StaticTypedNumeric_Int32_4 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 4; }
    }
}