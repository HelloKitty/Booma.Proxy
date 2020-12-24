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
    /// code for the Type: <see cref="MenuListing"/>
    /// </summary>
    public sealed partial class MenuListing_AutoGeneratedTemplateSerializerStrategy
        : BaseAutoGeneratedSerializerStrategy<MenuListing_AutoGeneratedTemplateSerializerStrategy, MenuListing>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(MenuListing value, Span<byte> buffer, ref int offset)
        {
            //Type: MenuListing Field: 1 Name: Selection Type: MenuItemIdentifier;
            value.Selection = MenuItemIdentifier_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: MenuListing Field: 2 Name: Flags Type: UInt16;
            value.Flags = GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Read(buffer, ref offset);
            //Type: MenuListing Field: 3 Name: ItemName Type: String;
            value.ItemName = FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_17>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(MenuListing value, Span<byte> buffer, ref int offset)
        {
            //Type: MenuListing Field: 1 Name: Selection Type: MenuItemIdentifier;
            MenuItemIdentifier_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.Selection, buffer, ref offset);
            //Type: MenuListing Field: 2 Name: Flags Type: UInt16;
            GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Write(value.Flags, buffer, ref offset);
            //Type: MenuListing Field: 3 Name: ItemName Type: String;
            FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_17>.Instance.Write(value.ItemName, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_17 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 17; }
    }
}