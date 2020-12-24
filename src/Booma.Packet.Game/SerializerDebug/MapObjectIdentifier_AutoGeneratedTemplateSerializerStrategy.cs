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
    /// code for the Type: <see cref="MapObjectIdentifier"/>
    /// </summary>
    public sealed partial class MapObjectIdentifier_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<MapObjectIdentifier_AutoGeneratedTemplateSerializerStrategy, MapObjectIdentifier>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(MapObjectIdentifier value, Span<byte> buffer, ref int offset)
        {
            //Type: MapObjectIdentifier Field: 2 Name: ObjectIdentifier Type: Int16;
            value.ObjectIdentifier = GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(MapObjectIdentifier value, Span<byte> buffer, ref int offset)
        {
            //Type: MapObjectIdentifier Field: 2 Name: ObjectIdentifier Type: Int16;
            GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Write(value.ObjectIdentifier, buffer, ref offset);
        }
    }
}