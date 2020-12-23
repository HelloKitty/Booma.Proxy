using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;
namespace Booma.Proxy
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class SharedShipListEventPayload
    {
        public override Type SerializableType => typeof(SharedShipListEventPayload);
        public override PSOBBGamePacketPayloadServer Read(Span<byte> buffer, ref int offset)
        {
            SharedShipListEventPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(PSOBBGamePacketPayloadServer value, Span<byte> buffer, ref int offset)
        {
            SharedShipListEventPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="SharedShipListEventPayload"/>
    /// </summary>
    public sealed partial class SharedShipListEventPayload_AutoGeneratedTemplateSerializerStrategy
        : BaseAutoGeneratedSerializerStrategy<SharedShipListEventPayload_AutoGeneratedTemplateSerializerStrategy, SharedShipListEventPayload>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(SharedShipListEventPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Read(buffer, ref offset);
            //Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)value.Flags = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Read(buffer, ref offset);
            //Type: SharedShipListEventPayload Field: 1 Name: _MenuListings Type: MenuListing[];
            value._MenuListings = SendSizeComplexArrayTypeSerializerStrategy<MenuListing_AutoGeneratedTemplateSerializerStrategy, MenuListing, Int32>.Instance.Read(buffer, ref offset);
            //Type: SharedShipListEventPayload Field: 2 Name: LastMenuListing Type: MenuListing;
            value.LastMenuListing = MenuListing_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            value.OnAfterDeserialization();
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(SharedShipListEventPayload value, Span<byte> buffer, ref int offset)
        {
            value.OnBeforeSerialization();
            //Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Write(value.OperationCode, buffer, ref offset);
            //Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[];
            if (value.isFlagsSerialized)FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Write(value.Flags, buffer, ref offset);
            //Type: SharedShipListEventPayload Field: 1 Name: _MenuListings Type: MenuListing[];
            SendSizeComplexArrayTypeSerializerStrategy<MenuListing_AutoGeneratedTemplateSerializerStrategy, MenuListing, Int32>.Instance.Write(value._MenuListings, buffer, ref offset);
            //Type: SharedShipListEventPayload Field: 2 Name: LastMenuListing Type: MenuListing;
            MenuListing_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.LastMenuListing, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_4 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 4; }
    }
}