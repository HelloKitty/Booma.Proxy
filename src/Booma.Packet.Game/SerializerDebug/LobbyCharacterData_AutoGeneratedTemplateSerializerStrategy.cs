using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;

namespace FreecraftCore.Serializer
{
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="LobbyCharacterData"/>
    /// </summary>
    public sealed partial class LobbyCharacterData_AutoGeneratedTemplateSerializerStrategy
        : BaseAutoGeneratedSerializerStrategy<LobbyCharacterData_AutoGeneratedTemplateSerializerStrategy, LobbyCharacterData>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(LobbyCharacterData value, Span<byte> buffer, ref int offset)
        {
            //Type: LobbyCharacterData Field: 1 Name: Stats Type: CharacterStats;
            value.Stats = CharacterStats_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 2 Name: unk1 Type: UInt16;
            value.unk1 = GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 3 Name: unk2 Type: UInt64;
            value.unk2 = GenericTypePrimitiveSerializerStrategy<UInt64>.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 4 Name: Progress Type: CharacterProgress;
            value.Progress = CharacterProgress_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 5 Name: Meseta Type: Int32;
            value.Meseta = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 6 Name: GuildCard Type: String;
            value.GuildCard = FixedSizeStringTypeSerializerStrategy<ASCIIStringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 7 Name: unk3 Type: UInt32[];
            value.unk3 = FixedSizePrimitiveArrayTypeSerializerStrategy<uint, StaticTypedNumeric_Int32_2>.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 8 Name: Special Type: CharacterSpecialCustomInfo;
            value.Special = CharacterSpecialCustomInfo_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 9 Name: SectionId Type: SectionId;
            value.SectionId = GenericPrimitiveEnumTypeSerializerStrategy<SectionId, Byte>.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 10 Name: ClassRace Type: CharacterClassRace;
            value.ClassRace = GenericPrimitiveEnumTypeSerializerStrategy<CharacterClassRace, Byte>.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 11 Name: VersionData Type: CharacterVersionData;
            value.VersionData = CharacterVersionData_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 12 Name: CustomizationInfo Type: CharacterCustomizationInfo;
            value.CustomizationInfo = CharacterCustomizationInfo_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 13 Name: CharacterName Type: String;
            value.CharacterName = FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 14 Name: ActionBarSettings Type: Byte[];
            value.ActionBarSettings = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_232>.Instance.Read(buffer, ref offset);
            //Type: LobbyCharacterData Field: 15 Name: Techniques Type: Byte[];
            value.Techniques = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_20>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(LobbyCharacterData value, Span<byte> buffer, ref int offset)
        {
            //Type: LobbyCharacterData Field: 1 Name: Stats Type: CharacterStats;
            CharacterStats_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.Stats, buffer, ref offset);
            //Type: LobbyCharacterData Field: 2 Name: unk1 Type: UInt16;
            GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Write(value.unk1, buffer, ref offset);
            //Type: LobbyCharacterData Field: 3 Name: unk2 Type: UInt64;
            GenericTypePrimitiveSerializerStrategy<UInt64>.Instance.Write(value.unk2, buffer, ref offset);
            //Type: LobbyCharacterData Field: 4 Name: Progress Type: CharacterProgress;
            CharacterProgress_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.Progress, buffer, ref offset);
            //Type: LobbyCharacterData Field: 5 Name: Meseta Type: Int32;
            GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.Meseta, buffer, ref offset);
            //Type: LobbyCharacterData Field: 6 Name: GuildCard Type: String;
            FixedSizeStringTypeSerializerStrategy<ASCIIStringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Write(value.GuildCard, buffer, ref offset);
            //Type: LobbyCharacterData Field: 7 Name: unk3 Type: UInt32[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<uint, StaticTypedNumeric_Int32_2>.Instance.Write(value.unk3, buffer, ref offset);
            //Type: LobbyCharacterData Field: 8 Name: Special Type: CharacterSpecialCustomInfo;
            CharacterSpecialCustomInfo_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.Special, buffer, ref offset);
            //Type: LobbyCharacterData Field: 9 Name: SectionId Type: SectionId;
            GenericPrimitiveEnumTypeSerializerStrategy<SectionId, Byte>.Instance.Write(value.SectionId, buffer, ref offset);
            //Type: LobbyCharacterData Field: 10 Name: ClassRace Type: CharacterClassRace;
            GenericPrimitiveEnumTypeSerializerStrategy<CharacterClassRace, Byte>.Instance.Write(value.ClassRace, buffer, ref offset);
            //Type: LobbyCharacterData Field: 11 Name: VersionData Type: CharacterVersionData;
            CharacterVersionData_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.VersionData, buffer, ref offset);
            //Type: LobbyCharacterData Field: 12 Name: CustomizationInfo Type: CharacterCustomizationInfo;
            CharacterCustomizationInfo_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.CustomizationInfo, buffer, ref offset);
            //Type: LobbyCharacterData Field: 13 Name: CharacterName Type: String;
            FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Write(value.CharacterName, buffer, ref offset);
            //Type: LobbyCharacterData Field: 14 Name: ActionBarSettings Type: Byte[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_232>.Instance.Write(value.ActionBarSettings, buffer, ref offset);
            //Type: LobbyCharacterData Field: 15 Name: Techniques Type: Byte[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_20>.Instance.Write(value.Techniques, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_16 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 16; }
        private sealed class StaticTypedNumeric_Int32_2 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 2; }
        private sealed class StaticTypedNumeric_Int32_232 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 232; }
        private sealed class StaticTypedNumeric_Int32_20 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 20; }
    }
}