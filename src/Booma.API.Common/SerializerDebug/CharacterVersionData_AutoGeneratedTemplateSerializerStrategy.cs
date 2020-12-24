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
    /// code for the Type: <see cref="CharacterVersionData"/>
    /// </summary>
    public sealed partial class CharacterVersionData_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<CharacterVersionData_AutoGeneratedTemplateSerializerStrategy, CharacterVersionData>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(CharacterVersionData value, Span<byte> buffer, ref int offset)
        {
            //Type: CharacterVersionData Field: 1 Name: V2Flags Type: Byte;
            value.V2Flags = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: CharacterVersionData Field: 2 Name: Version Type: Byte;
            value.Version = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: CharacterVersionData Field: 3 Name: V1Flags Type: UInt32;
            value.V1Flags = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(CharacterVersionData value, Span<byte> buffer, ref int offset)
        {
            //Type: CharacterVersionData Field: 1 Name: V2Flags Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.V2Flags, buffer, ref offset);
            //Type: CharacterVersionData Field: 2 Name: Version Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.Version, buffer, ref offset);
            //Type: CharacterVersionData Field: 3 Name: V1Flags Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.V1Flags, buffer, ref offset);
        }
    }
}