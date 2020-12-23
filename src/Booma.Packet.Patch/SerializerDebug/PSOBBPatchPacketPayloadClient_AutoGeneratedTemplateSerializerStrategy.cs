using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;
namespace Booma.Proxy
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class PSOBBPatchPacketPayloadClient : IWireMessage<PSOBBPatchPacketPayloadClient>
    {
        public virtual Type SerializableType => typeof(PSOBBPatchPacketPayloadClient);
        public virtual PSOBBPatchPacketPayloadClient Read(Span<byte> buffer, ref int offset)
        {
            PSOBBPatchPacketPayloadClient_AutoGeneratedTemplateSerializerStrategy.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public virtual void Write(PSOBBPatchPacketPayloadClient value, Span<byte> buffer, ref int offset)
        {
            PSOBBPatchPacketPayloadClient_AutoGeneratedTemplateSerializerStrategy.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="PSOBBPatchPacketPayloadClient"/>
    /// </summary>
    public sealed partial class PSOBBPatchPacketPayloadClient_AutoGeneratedTemplateSerializerStrategy
        : BasePolymorphicAutoGeneratedSerializerStrategy<PSOBBPatchPacketPayloadClient_AutoGeneratedTemplateSerializerStrategy, PSOBBPatchPacketPayloadClient, UInt16>
    {
        protected override PSOBBPatchPacketPayloadClient CreateType(int key)
        {
            switch (key)
            {
                case (int)Booma.Proxy.PatchNetworkOperationCode.PATCH_FILE_LIST_DONE:
                    return new PatchingFileInformationDoneAckPayload();
                case (int)Booma.Proxy.PatchNetworkOperationCode.PATCH_FILE_INFO_REPLY:
                    return new PatchingFileInformationReplyPayload();
                case (int)Booma.Proxy.PatchNetworkOperationCode.PATCH_LOGIN_TYPE:
                    return new PatchingLoginRequestPayload();
                case (int)Booma.Proxy.PatchNetworkOperationCode.PATCH_WELCOME_TYPE:
                    return new PatchingWelcomeAckPayload();
                default:
                    throw new NotImplementedException($"Encountered unimplemented sub-type for Type: {nameof(PSOBBPatchPacketPayloadClient)} with Key: {key}");
            }
        }
    }
}