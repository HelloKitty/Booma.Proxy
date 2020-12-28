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
    /// code for the Type: <see cref="GuildCardData"/>
    /// </summary>
    public sealed partial class GuildCardData_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<GuildCardData_AutoGeneratedTemplateSerializerStrategy, GuildCardData>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(GuildCardData value, Span<byte> buffer, ref int offset)
        {
            //Type: GuildCardData Field: 1 Name: unk1 Type: Byte[];
            value.unk1 = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_276>.Instance.Read(buffer, ref offset);
            //Type: GuildCardData Field: 2 Name: Blocked Type: GuildCardEntry[];
            value.Blocked = FixedSizeComplexArrayTypeSerializerStrategy<GuildCardEntry_AutoGeneratedTemplateSerializerStrategy, GuildCardEntry, StaticTypedNumeric_Int32_29>.Instance.Read(buffer, ref offset);
            //Type: GuildCardData Field: 3 Name: unk2 Type: Byte[];
            value.unk2 = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_120>.Instance.Read(buffer, ref offset);
            //Type: GuildCardData Field: 4 Name: Friends Type: GuildCardFriend[];
            value.Friends = FixedSizeComplexArrayTypeSerializerStrategy<GuildCardFriend_AutoGeneratedTemplateSerializerStrategy, GuildCardFriend, StaticTypedNumeric_Int32_104>.Instance.Read(buffer, ref offset);
            //Type: GuildCardData Field: 5 Name: unk3 Type: Byte[];
            value.unk3 = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_444>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(GuildCardData value, Span<byte> buffer, ref int offset)
        {
            //Type: GuildCardData Field: 1 Name: unk1 Type: Byte[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_276>.Instance.Write(value.unk1, buffer, ref offset);
            //Type: GuildCardData Field: 2 Name: Blocked Type: GuildCardEntry[];
            FixedSizeComplexArrayTypeSerializerStrategy<GuildCardEntry_AutoGeneratedTemplateSerializerStrategy, GuildCardEntry, StaticTypedNumeric_Int32_29>.Instance.Write(value.Blocked, buffer, ref offset);
            //Type: GuildCardData Field: 3 Name: unk2 Type: Byte[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_120>.Instance.Write(value.unk2, buffer, ref offset);
            //Type: GuildCardData Field: 4 Name: Friends Type: GuildCardFriend[];
            FixedSizeComplexArrayTypeSerializerStrategy<GuildCardFriend_AutoGeneratedTemplateSerializerStrategy, GuildCardFriend, StaticTypedNumeric_Int32_104>.Instance.Write(value.Friends, buffer, ref offset);
            //Type: GuildCardData Field: 5 Name: unk3 Type: Byte[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_444>.Instance.Write(value.unk3, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_276 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 276; }
        private sealed class StaticTypedNumeric_Int32_29 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 29; }
        private sealed class StaticTypedNumeric_Int32_120 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 120; }
        private sealed class StaticTypedNumeric_Int32_104 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 104; }
        private sealed class StaticTypedNumeric_Int32_444 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 444; }
    }
}