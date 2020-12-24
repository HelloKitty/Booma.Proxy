using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;
namespace Booma.Proxy
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class PatchingInfoRequestDonePayload
    {
        public override Type SerializableType => typeof(PatchingInfoRequestDonePayload);
        public override PSOBBPatchPacketPayloadServer Read(Span<byte> buffer, ref int offset)
        {
            PatchingInfoRequestDonePayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(PSOBBPatchPacketPayloadServer value, Span<byte> buffer, ref int offset)
        {
            PatchingInfoRequestDonePayload_AutoGeneratedTemplateSerializerStrategy.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="PatchingInfoRequestDonePayload"/>
    /// </summary>
    public sealed partial class PatchingInfoRequestDonePayload_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<PatchingInfoRequestDonePayload_AutoGeneratedTemplateSerializerStrategy, PatchingInfoRequestDonePayload>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(PatchingInfoRequestDonePayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBPatchPacketPayloadServer Field: 1 Name: OperationCode Type: PatchNetworkOperationCode;
            value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<PatchNetworkOperationCode, Int16>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(PatchingInfoRequestDonePayload value, Span<byte> buffer, ref int offset)
        {
            //Type: PSOBBPatchPacketPayloadServer Field: 1 Name: OperationCode Type: PatchNetworkOperationCode;
            GenericPrimitiveEnumTypeSerializerStrategy<PatchNetworkOperationCode, Int16>.Instance.Write(value.OperationCode, buffer, ref offset);
        }
    }
}