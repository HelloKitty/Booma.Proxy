﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;
namespace Booma.Proxy
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class BlockOtherPlayerJoinedLobbyEventPayload
    {
        public override Type SerializableType => typeof(BlockOtherPlayerJoinedLobbyEventPayload);
        public override PSOBBGamePacketPayloadServer Read(Span<byte> buffer, ref int offset)
        {
            BlockOtherPlayerJoinedLobbyEventPayload_Serializer.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(PSOBBGamePacketPayloadServer value, Span<byte> buffer, ref int offset)
        {
            BlockOtherPlayerJoinedLobbyEventPayload_Serializer.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="BlockOtherPlayerJoinedLobbyEventPayload"/>
    /// </summary>
    public sealed partial class BlockOtherPlayerJoinedLobbyEventPayload_Serializer
            : BaseAutoGeneratedSerializerStrategy<BlockOtherPlayerJoinedLobbyEventPayload_Serializer, BlockOtherPlayerJoinedLobbyEventPayload>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(BlockOtherPlayerJoinedLobbyEventPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Read(buffer, ref offset);
            //Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)value.Flags = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Read(buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 1 Name: ClientId Type: Byte;
            value.ClientId = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 2 Name: LeaderId Type: Byte;
            value.LeaderId = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 3 Name: One Type: Byte;
            value.One = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 4 Name: LobbyNumber Type: Byte;
            value.LobbyNumber = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 5 Name: BlockNumber Type: Int16;
            value.BlockNumber = GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Read(buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 6 Name: EventId Type: Int16;
            value.EventId = GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Read(buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 7 Name: Padding Type: Int32;
            value.Padding = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 8 Name: JoinData Type: CharacterJoinData;
            value.JoinData = CharacterJoinData_Serializer.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(BlockOtherPlayerJoinedLobbyEventPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Write(value.OperationCode, buffer, ref offset);
            //Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Write(value.Flags, buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 1 Name: ClientId Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.ClientId, buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 2 Name: LeaderId Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.LeaderId, buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 3 Name: One Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.One, buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 4 Name: LobbyNumber Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.LobbyNumber, buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 5 Name: BlockNumber Type: Int16;
            GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Write(value.BlockNumber, buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 6 Name: EventId Type: Int16;
            GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Write(value.EventId, buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 7 Name: Padding Type: Int32;
            GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.Padding, buffer, ref offset);
            //Type: BlockOtherPlayerJoinedLobbyEventPayload Field: 8 Name: JoinData Type: CharacterJoinData;
            CharacterJoinData_Serializer.Instance.Write(value.JoinData, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_4 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 4; }
    }
}