using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;
namespace Booma.Proxy
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class Sub60PlayerFreezeCommand
    {
        public override Type SerializableType => typeof(Sub60PlayerFreezeCommand);
        public override BaseSubCommand60 Read(Span<byte> buffer, ref int offset)
        {
            Sub60PlayerFreezeCommand_AutoGeneratedTemplateSerializerStrategy.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(BaseSubCommand60 value, Span<byte> buffer, ref int offset)
        {
            Sub60PlayerFreezeCommand_AutoGeneratedTemplateSerializerStrategy.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="Sub60PlayerFreezeCommand"/>
    /// </summary>
    public sealed partial class Sub60PlayerFreezeCommand_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<Sub60PlayerFreezeCommand_AutoGeneratedTemplateSerializerStrategy, Sub60PlayerFreezeCommand>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(Sub60PlayerFreezeCommand value, Span<byte> buffer, ref int offset)
        {
            //Type: BaseSubCommand60 Field: 1 Name: CommandOperationCode Type: SubCommand60OperationCode;
            value.CommandOperationCode = GenericPrimitiveEnumTypeSerializerStrategy<SubCommand60OperationCode, Byte>.Instance.Read(buffer, ref offset);
            //Type: BaseSubCommand60 Field: 2 Name: CommandSize Type: Byte;
            if (value.isSizeSerialized)value.CommandSize = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: Sub60PlayerFreezeCommand Field: 1 Name: Identifier Type: Byte;
            value.Identifier = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: Sub60PlayerFreezeCommand Field: 2 Name: Unknown1 Type: Byte;
            value.Unknown1 = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: Sub60PlayerFreezeCommand Field: 3 Name: Reason Type: FreezeReason;
            value.Reason = GenericPrimitiveEnumTypeSerializerStrategy<Booma.Proxy.Sub60PlayerFreezeCommand.FreezeReason, UInt32>.Instance.Read(buffer, ref offset);
            //Type: Sub60PlayerFreezeCommand Field: 4 Name: Position Type: Vector2;
            value.Position = Vector2_Single_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: Sub60PlayerFreezeCommand Field: 5 Name: Unknown2 Type: Int32;
            value.Unknown2 = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(Sub60PlayerFreezeCommand value, Span<byte> buffer, ref int offset)
        {
            //Type: BaseSubCommand60 Field: 1 Name: CommandOperationCode Type: SubCommand60OperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<SubCommand60OperationCode, Byte>.Instance.Write(value.CommandOperationCode, buffer, ref offset);
            //Type: BaseSubCommand60 Field: 2 Name: CommandSize Type: Byte;
            if (value.isSizeSerialized)BytePrimitiveSerializerStrategy.Instance.Write(value.CommandSize, buffer, ref offset);
            //Type: Sub60PlayerFreezeCommand Field: 1 Name: Identifier Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.Identifier, buffer, ref offset);
            //Type: Sub60PlayerFreezeCommand Field: 2 Name: Unknown1 Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.Unknown1, buffer, ref offset);
            //Type: Sub60PlayerFreezeCommand Field: 3 Name: Reason Type: FreezeReason;
            GenericPrimitiveEnumTypeSerializerStrategy<Booma.Proxy.Sub60PlayerFreezeCommand.FreezeReason, UInt32>.Instance.Write(value.Reason, buffer, ref offset);
            //Type: Sub60PlayerFreezeCommand Field: 4 Name: Position Type: Vector2;
            Vector2_Single_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.Position, buffer, ref offset);
            //Type: Sub60PlayerFreezeCommand Field: 5 Name: Unknown2 Type: Int32;
            GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.Unknown2, buffer, ref offset);
        }
    }
}