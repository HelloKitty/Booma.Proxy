﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="TechniqueDefinitionData"/>
    /// </summary>
    public sealed partial class TechniqueDefinitionData_Serializer
            : BaseAutoGeneratedSerializerStrategy<TechniqueDefinitionData_Serializer, TechniqueDefinitionData>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(TechniqueDefinitionData value, Span<byte> buffer, ref int offset)
        {
            //Type: TechniqueDefinitionData Field: 1 Name: TechniqueId Type: Int16;
            value.TechniqueId = GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Read(buffer, ref offset);
            //Type: TechniqueDefinitionData Field: 2 Name: Level Type: Byte;
            value.Level = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(TechniqueDefinitionData value, Span<byte> buffer, ref int offset)
        {
            //Type: TechniqueDefinitionData Field: 1 Name: TechniqueId Type: Int16;
            GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Write(value.TechniqueId, buffer, ref offset);
            //Type: TechniqueDefinitionData Field: 2 Name: Level Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.Level, buffer, ref offset);
        }
    }
}