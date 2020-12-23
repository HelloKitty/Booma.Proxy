using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;

namespace FreecraftCore.Serializer
{
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="MapDatFormatModel"/>
    /// </summary>
    public sealed partial class MapDatFormatModel_AutoGeneratedTemplateSerializerStrategy
        : BaseAutoGeneratedSerializerStrategy<MapDatFormatModel_AutoGeneratedTemplateSerializerStrategy, MapDatFormatModel>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(MapDatFormatModel value, Span<byte> buffer, ref int offset)
        {
            //Type: MapDatFormatModel Field: 1 Name: _Entries Type: MapDatFormatTableEntryContainer[];
            value._Entries = ComplexArrayTypeSerializerStrategy<MapDatFormatTableEntryContainer_AutoGeneratedTemplateSerializerStrategy, MapDatFormatTableEntryContainer>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(MapDatFormatModel value, Span<byte> buffer, ref int offset)
        {
            //Type: MapDatFormatModel Field: 1 Name: _Entries Type: MapDatFormatTableEntryContainer[];
            ComplexArrayTypeSerializerStrategy<MapDatFormatTableEntryContainer_AutoGeneratedTemplateSerializerStrategy, MapDatFormatTableEntryContainer>.Instance.Write(value._Entries, buffer, ref offset);
        }
    }
}