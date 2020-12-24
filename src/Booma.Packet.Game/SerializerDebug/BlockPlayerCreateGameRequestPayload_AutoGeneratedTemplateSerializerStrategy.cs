using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;
namespace Booma.Proxy
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class BlockPlayerCreateGameRequestPayload
    {
        public override Type SerializableType => typeof(BlockPlayerCreateGameRequestPayload);
        public override PSOBBGamePacketPayloadClient Read(Span<byte> buffer, ref int offset)
        {
            BlockPlayerCreateGameRequestPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(PSOBBGamePacketPayloadClient value, Span<byte> buffer, ref int offset)
        {
            BlockPlayerCreateGameRequestPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="BlockPlayerCreateGameRequestPayload"/>
    /// </summary>
    public sealed partial class BlockPlayerCreateGameRequestPayload_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<BlockPlayerCreateGameRequestPayload_AutoGeneratedTemplateSerializerStrategy, BlockPlayerCreateGameRequestPayload>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(BlockPlayerCreateGameRequestPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadClient Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, UInt16>.Instance.Read(buffer, ref offset);
            //Type: PSOBBGamePacketPayloadClient Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)value.Flags = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Read(buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 1 Name: unk1 Type: Int64;
            value.unk1 = GenericTypePrimitiveSerializerStrategy<Int64>.Instance.Read(buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 2 Name: GameName Type: String;
            value.GameName = FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Read(buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 3 Name: Password Type: String;
            value.Password = FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Read(buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 4 Name: PartyDifficulty Type: DifficultyType;
            value.PartyDifficulty = GenericPrimitiveEnumTypeSerializerStrategy<DifficultyType, Byte>.Instance.Read(buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 5 Name: isBattleModeEnabled Type: Boolean;
            value.isBattleModeEnabled = GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Read(buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 6 Name: isChallengeModeEnabled Type: Boolean;
            value.isChallengeModeEnabled = GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Read(buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 7 Name: PartyEpisode Type: EpisodeType;
            value.PartyEpisode = GenericPrimitiveEnumTypeSerializerStrategy<EpisodeType, Byte>.Instance.Read(buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 8 Name: isSinglePlayerModeEnabled Type: Boolean;
            value.isSinglePlayerModeEnabled = GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(BlockPlayerCreateGameRequestPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadClient Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, UInt16>.Instance.Write(value.OperationCode, buffer, ref offset);
            //Type: PSOBBGamePacketPayloadClient Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Write(value.Flags, buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 1 Name: unk1 Type: Int64;
            GenericTypePrimitiveSerializerStrategy<Int64>.Instance.Write(value.unk1, buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 2 Name: GameName Type: String;
            FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Write(value.GameName, buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 3 Name: Password Type: String;
            FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Write(value.Password, buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 4 Name: PartyDifficulty Type: DifficultyType;
            GenericPrimitiveEnumTypeSerializerStrategy<DifficultyType, Byte>.Instance.Write(value.PartyDifficulty, buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 5 Name: isBattleModeEnabled Type: Boolean;
            GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Write(value.isBattleModeEnabled, buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 6 Name: isChallengeModeEnabled Type: Boolean;
            GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Write(value.isChallengeModeEnabled, buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 7 Name: PartyEpisode Type: EpisodeType;
            GenericPrimitiveEnumTypeSerializerStrategy<EpisodeType, Byte>.Instance.Write(value.PartyEpisode, buffer, ref offset);
            //Type: BlockPlayerCreateGameRequestPayload Field: 8 Name: isSinglePlayerModeEnabled Type: Boolean;
            GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Write(value.isSinglePlayerModeEnabled, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_16 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 16; }
        private sealed class StaticTypedNumeric_Int32_4 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 4; }
    }
}