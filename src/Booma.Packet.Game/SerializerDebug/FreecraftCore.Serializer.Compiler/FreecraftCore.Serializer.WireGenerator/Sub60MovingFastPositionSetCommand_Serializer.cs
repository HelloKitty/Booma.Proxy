﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;
namespace Booma
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class Sub60MovingFastPositionSetCommand
    {
        public override Type SerializableType => typeof(Sub60MovingFastPositionSetCommand);
        public override BaseSubCommand60 Read(Span<byte> buffer, ref int offset)
        {
            Sub60MovingFastPositionSetCommand_Serializer.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(BaseSubCommand60 value, Span<byte> buffer, ref int offset)
        {
            Sub60MovingFastPositionSetCommand_Serializer.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="Sub60MovingFastPositionSetCommand"/>
    /// </summary>
    public sealed partial class Sub60MovingFastPositionSetCommand_Serializer
            : BaseAutoGeneratedSerializerStrategy<Sub60MovingFastPositionSetCommand_Serializer, Sub60MovingFastPositionSetCommand>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(Sub60MovingFastPositionSetCommand value, Span<byte> buffer, ref int offset)
        {
            //Type: BaseSubCommand60 Field: 1 Name: CommandOperationCode Type: SubCommand60OperationCode;
            value.CommandOperationCode = GenericPrimitiveEnumTypeSerializerStrategy<SubCommand60OperationCode, Byte>.Instance.Read(buffer, ref offset);
            //Type: BaseSubCommand60 Field: 2 Name: CommandSize Type: Byte;
            if (value.isSizeSerialized)value.CommandSize = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: Sub60MovingFastPositionSetCommand Field: 1 Name: Identifier Type: Byte;
            value.Identifier = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: Sub60MovingFastPositionSetCommand Field: 2 Name: unused Type: Byte;
            value.unused = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: Sub60MovingFastPositionSetCommand Field: 3 Name: Position Type: Vector2;
            value.Position = Vector2_Single_Serializer.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(Sub60MovingFastPositionSetCommand value, Span<byte> buffer, ref int offset)
        {
            //Type: BaseSubCommand60 Field: 1 Name: CommandOperationCode Type: SubCommand60OperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<SubCommand60OperationCode, Byte>.Instance.Write(value.CommandOperationCode, buffer, ref offset);
            //Type: BaseSubCommand60 Field: 2 Name: CommandSize Type: Byte;
            if (value.isSizeSerialized)BytePrimitiveSerializerStrategy.Instance.Write(value.CommandSize, buffer, ref offset);
            //Type: Sub60MovingFastPositionSetCommand Field: 1 Name: Identifier Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.Identifier, buffer, ref offset);
            //Type: Sub60MovingFastPositionSetCommand Field: 2 Name: unused Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.unused, buffer, ref offset);
            //Type: Sub60MovingFastPositionSetCommand Field: 3 Name: Position Type: Vector2;
            Vector2_Single_Serializer.Instance.Write(value.Position, buffer, ref offset);
        }
    }
}