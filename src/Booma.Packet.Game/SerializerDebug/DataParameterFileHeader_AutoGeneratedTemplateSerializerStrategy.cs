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
    /// code for the Type: <see cref="DataParameterFileHeader"/>
    /// </summary>
    public sealed partial class DataParameterFileHeader_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<DataParameterFileHeader_AutoGeneratedTemplateSerializerStrategy, DataParameterFileHeader>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(DataParameterFileHeader value, Span<byte> buffer, ref int offset)
        {
            //Type: DataParameterFileHeader Field: 1 Name: FileSize Type: UInt32;
            value.FileSize = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
            //Type: DataParameterFileHeader Field: 2 Name: Checksum Type: UInt32;
            value.Checksum = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
            //Type: DataParameterFileHeader Field: 3 Name: Offset Type: UInt32;
            value.Offset = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
            //Type: DataParameterFileHeader Field: 4 Name: FileName Type: String;
            value.FileName = FixedSizeStringTypeSerializerStrategy<ASCIIStringTypeSerializerStrategy, StaticTypedNumeric_Int32_64>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(DataParameterFileHeader value, Span<byte> buffer, ref int offset)
        {
            //Type: DataParameterFileHeader Field: 1 Name: FileSize Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.FileSize, buffer, ref offset);
            //Type: DataParameterFileHeader Field: 2 Name: Checksum Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.Checksum, buffer, ref offset);
            //Type: DataParameterFileHeader Field: 3 Name: Offset Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.Offset, buffer, ref offset);
            //Type: DataParameterFileHeader Field: 4 Name: FileName Type: String;
            FixedSizeStringTypeSerializerStrategy<ASCIIStringTypeSerializerStrategy, StaticTypedNumeric_Int32_64>.Instance.Write(value.FileName, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_64 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 64; }
    }
}