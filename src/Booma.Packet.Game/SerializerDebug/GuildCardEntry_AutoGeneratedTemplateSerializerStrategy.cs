using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="GuildCardEntry"/>
    /// </summary>
    public sealed partial class GuildCardEntry_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<GuildCardEntry_AutoGeneratedTemplateSerializerStrategy, GuildCardEntry>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(GuildCardEntry value, Span<byte> buffer, ref int offset)
        {
            //Type: GuildCardEntry Field: 1 Name: GuildCard Type: UInt32;
            value.GuildCard = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
            //Type: GuildCardEntry Field: 2 Name: Name Type: String;
            value.Name = FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_24>.Instance.Read(buffer, ref offset);
            //Type: GuildCardEntry Field: 3 Name: TeamName Type: String;
            value.TeamName = FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Read(buffer, ref offset);
            //Type: GuildCardEntry Field: 4 Name: Description Type: String;
            value.Description = FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_88>.Instance.Read(buffer, ref offset);
            //Type: GuildCardEntry Field: 5 Name: unk1 Type: Byte;
            value.unk1 = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: GuildCardEntry Field: 6 Name: Language Type: Byte;
            value.Language = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: GuildCardEntry Field: 7 Name: SectionId Type: SectionId;
            value.SectionId = GenericPrimitiveEnumTypeSerializerStrategy<SectionId, Byte>.Instance.Read(buffer, ref offset);
            //Type: GuildCardEntry Field: 8 Name: ClassType Type: CharacterClass;
            value.ClassType = GenericPrimitiveEnumTypeSerializerStrategy<CharacterClass, Byte>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(GuildCardEntry value, Span<byte> buffer, ref int offset)
        {
            //Type: GuildCardEntry Field: 1 Name: GuildCard Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.GuildCard, buffer, ref offset);
            //Type: GuildCardEntry Field: 2 Name: Name Type: String;
            FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_24>.Instance.Write(value.Name, buffer, ref offset);
            //Type: GuildCardEntry Field: 3 Name: TeamName Type: String;
            FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Write(value.TeamName, buffer, ref offset);
            //Type: GuildCardEntry Field: 4 Name: Description Type: String;
            FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_88>.Instance.Write(value.Description, buffer, ref offset);
            //Type: GuildCardEntry Field: 5 Name: unk1 Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.unk1, buffer, ref offset);
            //Type: GuildCardEntry Field: 6 Name: Language Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.Language, buffer, ref offset);
            //Type: GuildCardEntry Field: 7 Name: SectionId Type: SectionId;
            GenericPrimitiveEnumTypeSerializerStrategy<SectionId, Byte>.Instance.Write(value.SectionId, buffer, ref offset);
            //Type: GuildCardEntry Field: 8 Name: ClassType Type: CharacterClass;
            GenericPrimitiveEnumTypeSerializerStrategy<CharacterClass, Byte>.Instance.Write(value.ClassType, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_24 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 24; }
        private sealed class StaticTypedNumeric_Int32_16 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 16; }
        private sealed class StaticTypedNumeric_Int32_88 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 88; }
    }
}