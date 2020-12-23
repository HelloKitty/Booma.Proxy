using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;
namespace Booma.Proxy
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class BlockNetworkCommand62EventClientPayload
    {
        public override Type SerializableType => typeof(BlockNetworkCommand62EventClientPayload);
        public override PSOBBGamePacketPayloadClient Read(Span<byte> buffer, ref int offset)
        {
            BlockNetworkCommand62EventClientPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(PSOBBGamePacketPayloadClient value, Span<byte> buffer, ref int offset)
        {
            BlockNetworkCommand62EventClientPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="BlockNetworkCommand62EventClientPayload"/>
    /// </summary>
    public sealed partial class BlockNetworkCommand62EventClientPayload_AutoGeneratedTemplateSerializerStrategy
        : BaseAutoGeneratedSerializerStrategy<BlockNetworkCommand62EventClientPayload_AutoGeneratedTemplateSerializerStrategy, BlockNetworkCommand62EventClientPayload>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(BlockNetworkCommand62EventClientPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadClient Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, UInt16>.Instance.Read(buffer, ref offset);
            //Type: PSOBBGamePacketPayloadClient Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)value.Flags = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Read(buffer, ref offset);
            //Type: BlockNetworkCommand62EventClientPayload Field: 1 Name: Command Type: BaseSubCommand62;
            value.Command = BaseSubCommand62_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(BlockNetworkCommand62EventClientPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadClient Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, UInt16>.Instance.Write(value.OperationCode, buffer, ref offset);
            //Type: PSOBBGamePacketPayloadClient Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Write(value.Flags, buffer, ref offset);
            //Type: BlockNetworkCommand62EventClientPayload Field: 1 Name: Command Type: BaseSubCommand62;
            BaseSubCommand62_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.Command, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_4 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 4; }
    }
}