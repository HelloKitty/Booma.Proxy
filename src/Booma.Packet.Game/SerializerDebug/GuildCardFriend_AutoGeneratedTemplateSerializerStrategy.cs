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
    /// code for the Type: <see cref="GuildCardFriend"/>
    /// </summary>
    public sealed partial class GuildCardFriend_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<GuildCardFriend_AutoGeneratedTemplateSerializerStrategy, GuildCardFriend>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(GuildCardFriend value, Span<byte> buffer, ref int offset)
        {
            //Type: GuildCardFriend Field: 1 Name: Data Type: GuildCardEntry;
            value.Data = GuildCardEntry_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: GuildCardFriend Field: 2 Name: unk1 Type: Int32;
            value.unk1 = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
            //Type: GuildCardFriend Field: 3 Name: Comment Type: String;
            value.Comment = FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_88>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(GuildCardFriend value, Span<byte> buffer, ref int offset)
        {
            //Type: GuildCardFriend Field: 1 Name: Data Type: GuildCardEntry;
            GuildCardEntry_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.Data, buffer, ref offset);
            //Type: GuildCardFriend Field: 2 Name: unk1 Type: Int32;
            GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.unk1, buffer, ref offset);
            //Type: GuildCardFriend Field: 3 Name: Comment Type: String;
            FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_88>.Instance.Write(value.Comment, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_88 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 88; }
    }
}