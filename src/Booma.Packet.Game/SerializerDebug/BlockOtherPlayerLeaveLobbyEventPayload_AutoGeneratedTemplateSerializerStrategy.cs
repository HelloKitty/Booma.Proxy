using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;
namespace Booma.Proxy
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class BlockOtherPlayerLeaveLobbyEventPayload
    {
        public override Type SerializableType => typeof(BlockOtherPlayerLeaveLobbyEventPayload);
        public override PSOBBGamePacketPayloadServer Read(Span<byte> buffer, ref int offset)
        {
            BlockOtherPlayerLeaveLobbyEventPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(PSOBBGamePacketPayloadServer value, Span<byte> buffer, ref int offset)
        {
            BlockOtherPlayerLeaveLobbyEventPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="BlockOtherPlayerLeaveLobbyEventPayload"/>
    /// </summary>
    public sealed partial class BlockOtherPlayerLeaveLobbyEventPayload_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<BlockOtherPlayerLeaveLobbyEventPayload_AutoGeneratedTemplateSerializerStrategy, BlockOtherPlayerLeaveLobbyEventPayload>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(BlockOtherPlayerLeaveLobbyEventPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Read(buffer, ref offset);
            //Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)value.Flags = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Read(buffer, ref offset);
            //Type: BlockOtherPlayerLeaveLobbyEventPayload Field: 1 Name: ClientId Type: Byte;
            value.ClientId = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: BlockOtherPlayerLeaveLobbyEventPayload Field: 2 Name: LeaderId Type: Byte;
            value.LeaderId = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(BlockOtherPlayerLeaveLobbyEventPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Write(value.OperationCode, buffer, ref offset);
            //Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Write(value.Flags, buffer, ref offset);
            //Type: BlockOtherPlayerLeaveLobbyEventPayload Field: 1 Name: ClientId Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.ClientId, buffer, ref offset);
            //Type: BlockOtherPlayerLeaveLobbyEventPayload Field: 2 Name: LeaderId Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.LeaderId, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_4 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 4; }
    }
}