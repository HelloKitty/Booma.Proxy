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
    /// code for the Type: <see cref="CharacterCustomizationInfo"/>
    /// </summary>
    public sealed partial class CharacterCustomizationInfo_Serializer
            : BaseAutoGeneratedSerializerStrategy<CharacterCustomizationInfo_Serializer, CharacterCustomizationInfo>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(CharacterCustomizationInfo value, Span<byte> buffer, ref int offset)
        {
            //Type: CharacterCustomizationInfo Field: 1 Name: CostumeId Type: UInt16;
            value.CostumeId = GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Read(buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 2 Name: SkinId Type: UInt16;
            value.SkinId = GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Read(buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 3 Name: FaceId Type: UInt16;
            value.FaceId = GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Read(buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 4 Name: HeadId Type: UInt16;
            value.HeadId = GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Read(buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 5 Name: HairId Type: UInt16;
            value.HairId = GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Read(buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 6 Name: HairColor Type: Vector3;
            value.HairColor = Vector3_UInt16_Serializer.Instance.Read(buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 7 Name: Proportions Type: Vector2;
            value.Proportions = Vector2_Single_Serializer.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(CharacterCustomizationInfo value, Span<byte> buffer, ref int offset)
        {
            //Type: CharacterCustomizationInfo Field: 1 Name: CostumeId Type: UInt16;
            GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Write(value.CostumeId, buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 2 Name: SkinId Type: UInt16;
            GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Write(value.SkinId, buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 3 Name: FaceId Type: UInt16;
            GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Write(value.FaceId, buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 4 Name: HeadId Type: UInt16;
            GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Write(value.HeadId, buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 5 Name: HairId Type: UInt16;
            GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Write(value.HairId, buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 6 Name: HairColor Type: Vector3;
            Vector3_UInt16_Serializer.Instance.Write(value.HairColor, buffer, ref offset);
            //Type: CharacterCustomizationInfo Field: 7 Name: Proportions Type: Vector2;
            Vector2_Single_Serializer.Instance.Write(value.Proportions, buffer, ref offset);
        }
    }
}