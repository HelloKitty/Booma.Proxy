using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;
namespace Booma.Proxy
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class BlockNetworkCommand6DEventServerPayload
    {
        public override Type SerializableType => typeof(BlockNetworkCommand6DEventServerPayload);
        public override PSOBBGamePacketPayloadServer Read(Span<byte> buffer, ref int offset)
        {
            BlockNetworkCommand6DEventServerPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(PSOBBGamePacketPayloadServer value, Span<byte> buffer, ref int offset)
        {
            BlockNetworkCommand6DEventServerPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="BlockNetworkCommand6DEventServerPayload"/>
    /// </summary>
    public sealed partial class BlockNetworkCommand6DEventServerPayload_AutoGeneratedTemplateSerializerStrategy
        : BaseAutoGeneratedSerializerStrategy<BlockNetworkCommand6DEventServerPayload_AutoGeneratedTemplateSerializerStrategy, BlockNetworkCommand6DEventServerPayload>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(BlockNetworkCommand6DEventServerPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Read(buffer, ref offset);
            //Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)value.Flags = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Read(buffer, ref offset);
            //Type: BlockNetworkCommand6DEventServerPayload Field: 1 Name: TargetClientIndex Type: Int32;
            value.TargetClientIndex = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
            //Type: BlockNetworkCommand6DEventServerPayload Field: 2 Name: Command Type: BaseSubCommand6D;
            value.Command = BaseSubCommand6D_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(BlockNetworkCommand6DEventServerPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Write(value.OperationCode, buffer, ref offset);
            //Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Write(value.Flags, buffer, ref offset);
            //Type: BlockNetworkCommand6DEventServerPayload Field: 1 Name: TargetClientIndex Type: Int32;
            GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.TargetClientIndex, buffer, ref offset);
            //Type: BlockNetworkCommand6DEventServerPayload Field: 2 Name: Command Type: BaseSubCommand6D;
            BaseSubCommand6D_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.Command, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_4 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 4; }
    }
}