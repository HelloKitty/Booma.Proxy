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
    /// code for the Type: <see cref="Vector3<Single>"/>
    /// </summary>
    public sealed partial class Vector3_Single_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<Vector3_Single_AutoGeneratedTemplateSerializerStrategy, Vector3<Single>>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(Vector3<Single> value, Span<byte> buffer, ref int offset)
        {
            //Type: Vector2 Field: 1 Name: X Type: Single;
            value.X = GenericTypePrimitiveSerializerStrategy<Single>.Instance.Read(buffer, ref offset);
            //Type: Vector2 Field: 2 Name: Y Type: Single;
            value.Y = GenericTypePrimitiveSerializerStrategy<Single>.Instance.Read(buffer, ref offset);
            //Type: Vector3 Field: 1 Name: Z Type: Single;
            value.Z = GenericTypePrimitiveSerializerStrategy<Single>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(Vector3<Single> value, Span<byte> buffer, ref int offset)
        {
            //Type: Vector2 Field: 1 Name: X Type: Single;
            GenericTypePrimitiveSerializerStrategy<Single>.Instance.Write(value.X, buffer, ref offset);
            //Type: Vector2 Field: 2 Name: Y Type: Single;
            GenericTypePrimitiveSerializerStrategy<Single>.Instance.Write(value.Y, buffer, ref offset);
            //Type: Vector3 Field: 1 Name: Z Type: Single;
            GenericTypePrimitiveSerializerStrategy<Single>.Instance.Write(value.Z, buffer, ref offset);
        }
    }
}