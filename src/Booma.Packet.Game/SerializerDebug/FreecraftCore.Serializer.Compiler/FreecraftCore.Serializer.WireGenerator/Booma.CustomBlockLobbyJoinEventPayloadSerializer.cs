﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;
namespace Booma
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class BlockLobbyJoinEventPayload
    {
        public override Type SerializableType => typeof(BlockLobbyJoinEventPayload);
        public override PSOBBGamePacketPayloadServer Read(Span<byte> buffer, ref int offset)
        {
            Booma.CustomBlockLobbyJoinEventPayloadSerializer.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public override void Write(PSOBBGamePacketPayloadServer value, Span<byte> buffer, ref int offset)
        {
            Booma.CustomBlockLobbyJoinEventPayloadSerializer.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}