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
    /// code for the Type: <see cref="NRelSectionsChunkModel"/>
    /// </summary>
    public sealed partial class NRelSectionsChunkModel_AutoGeneratedTemplateSerializerStrategy
        : BaseAutoGeneratedSerializerStrategy<NRelSectionsChunkModel_AutoGeneratedTemplateSerializerStrategy, NRelSectionsChunkModel>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(NRelSectionsChunkModel value, Span<byte> buffer, ref int offset)
        {
            //Type: NRelSectionsChunkModel Field: 1 Name: _Sections Type: NRelSectionModel[];
            value._Sections = SendSizeComplexArrayTypeSerializerStrategy<NRelSectionModel_AutoGeneratedTemplateSerializerStrategy, NRelSectionModel, Int32>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(NRelSectionsChunkModel value, Span<byte> buffer, ref int offset)
        {
            //Type: NRelSectionsChunkModel Field: 1 Name: _Sections Type: NRelSectionModel[];
            SendSizeComplexArrayTypeSerializerStrategy<NRelSectionModel_AutoGeneratedTemplateSerializerStrategy, NRelSectionModel, Int32>.Instance.Write(value._Sections, buffer, ref offset);
        }
    }
}