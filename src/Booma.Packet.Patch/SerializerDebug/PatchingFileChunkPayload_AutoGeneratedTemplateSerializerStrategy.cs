using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;
namespace Booma.Proxy
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class PatchingFileChunkPayload
    {
        public override Type SerializableType => typeof(PatchingFileChunkPayload);
        public override PSOBBPatchPacketPayloadServer Read(Span<byte> buffer, ref int offset)
        {
            PatchingFileChunkPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(PSOBBPatchPacketPayloadServer value, Span<byte> buffer, ref int offset)
        {
            PatchingFileChunkPayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="PatchingFileChunkPayload"/>
    /// </summary>
    public sealed partial class PatchingFileChunkPayload_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<PatchingFileChunkPayload_AutoGeneratedTemplateSerializerStrategy, PatchingFileChunkPayload>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(PatchingFileChunkPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBPatchPacketPayloadServer Field: 1 Name: OperationCode Type: PatchNetworkOperationCode;
            value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<PatchNetworkOperationCode, Int16>.Instance.Read(buffer, ref offset);
            //Type: PatchingFileChunkPayload Field: 1 Name: PatchFileChunkIndex Type: Int32;
            value.PatchFileChunkIndex = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
            //Type: PatchingFileChunkPayload Field: 2 Name: PatchFileChunkChecksum Type: UInt32;
            value.PatchFileChunkChecksum = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
            //Type: PatchingFileChunkPayload Field: 3 Name: PatchFileChunkData Type: Byte[];
            value.PatchFileChunkData = SendSizePrimitiveArrayTypeSerializerStrategy<byte, Int32>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(PatchingFileChunkPayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBPatchPacketPayloadServer Field: 1 Name: OperationCode Type: PatchNetworkOperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<PatchNetworkOperationCode, Int16>.Instance.Write(value.OperationCode, buffer, ref offset);
            //Type: PatchingFileChunkPayload Field: 1 Name: PatchFileChunkIndex Type: Int32;
            GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.PatchFileChunkIndex, buffer, ref offset);
            //Type: PatchingFileChunkPayload Field: 2 Name: PatchFileChunkChecksum Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.PatchFileChunkChecksum, buffer, ref offset);
            //Type: PatchingFileChunkPayload Field: 3 Name: PatchFileChunkData Type: Byte[];
            SendSizePrimitiveArrayTypeSerializerStrategy<byte, Int32>.Instance.Write(value.PatchFileChunkData, buffer, ref offset);
        }
    }
}