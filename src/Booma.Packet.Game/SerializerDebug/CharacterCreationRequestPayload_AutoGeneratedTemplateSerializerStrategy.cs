using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;
namespace Booma.Proxy
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class CharacterCreationRequestPayload
    {
        public override Type SerializableType => typeof(CharacterCreationRequestPayload);
        public override PSOBBGamePacketPayloadClient Read(Span<byte> buffer, ref int offset)
        {
            CharacterCreationRequestPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(PSOBBGamePacketPayloadClient value, Span<byte> buffer, ref int offset)
        {
            CharacterCreationRequestPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="CharacterCreationRequestPayload"/>
    /// </summary>
    public sealed partial class CharacterCreationRequestPayload_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<CharacterCreationRequestPayload_AutoGeneratedTemplateSerializerStrategy, CharacterCreationRequestPayload>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(CharacterCreationRequestPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadClient Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, UInt16>.Instance.Read(buffer, ref offset);
            //Type: PSOBBGamePacketPayloadClient Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)value.Flags = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Read(buffer, ref offset);
            //Type: CharacterCreationRequestPayload Field: 1 Name: SlotSelected Type: Byte;
            value.SlotSelected = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: CharacterCreationRequestPayload Field: 2 Name: unused Type: Byte[];
            value.unused = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_3>.Instance.Read(buffer, ref offset);
            //Type: CharacterCreationRequestPayload Field: 3 Name: CharacterData Type: PlayerCharacterDataModel;
            value.CharacterData = PlayerCharacterDataModel_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(CharacterCreationRequestPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadClient Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, UInt16>.Instance.Write(value.OperationCode, buffer, ref offset);
            //Type: PSOBBGamePacketPayloadClient Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Write(value.Flags, buffer, ref offset);
            //Type: CharacterCreationRequestPayload Field: 1 Name: SlotSelected Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.SlotSelected, buffer, ref offset);
            //Type: CharacterCreationRequestPayload Field: 2 Name: unused Type: Byte[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_3>.Instance.Write(value.unused, buffer, ref offset);
            //Type: CharacterCreationRequestPayload Field: 3 Name: CharacterData Type: PlayerCharacterDataModel;
            PlayerCharacterDataModel_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.CharacterData, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_3 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 3; }
        private sealed class StaticTypedNumeric_Int32_4 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 4; }
    }
}