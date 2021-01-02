﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;
namespace Booma
{
    [AutoGeneratedWireMessageImplementationAttribute]
    public partial class PSOBBGamePacketPayloadServer : IWireMessage<PSOBBGamePacketPayloadServer>
    {
        public virtual Type SerializableType => typeof(PSOBBGamePacketPayloadServer);
        public virtual PSOBBGamePacketPayloadServer Read(Span<byte> buffer, ref int offset)
        {
            PSOBBGamePacketPayloadServer_Serializer.Instance.InternalRead(this, buffer, ref offset);
            return this;
        }
        public virtual void Write(PSOBBGamePacketPayloadServer value, Span<byte> buffer, ref int offset)
        {
            PSOBBGamePacketPayloadServer_Serializer.Instance.InternalWrite(this, buffer, ref offset);
        }
    }
}

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="PSOBBGamePacketPayloadServer"/>
    /// </summary>
    public sealed partial class PSOBBGamePacketPayloadServer_Serializer
            : BasePolymorphicAutoGeneratedSerializerStrategy<PSOBBGamePacketPayloadServer_Serializer, PSOBBGamePacketPayloadServer, UInt16>
    {
        protected override PSOBBGamePacketPayloadServer CreateType(int key)
        {
            switch (key)
            {
                case (int)Booma.GameNetworkOperationCode.CHAR_DATA_REQUEST_TYPE:
                    return new BlockCharacterDataInitializationServerRequestPayload();
                case (int)Booma.GameNetworkOperationCode.PING_TYPE:
                    return new BlockClientPingEventPayload();
                case (int)Booma.GameNetworkOperationCode.GAME_JOIN_TYPE:
                    return new BlockGameJoinEventPayload();
                case (int)Booma.GameNetworkOperationCode.GAME_LIST_TYPE:
                    return new BlockGameListResponsePayload();
                case (int)Booma.GameNetworkOperationCode.GAME_ADD_PLAYER_TYPE:
                    return new BlockGamePlayerJoinedEventPayload();
                case (int)Booma.GameNetworkOperationCode.LOBBY_JOIN_TYPE:
                    return new BlockLobbyJoinEventPayload();
                case (int)Booma.GameNetworkOperationCode.GAME_COMMAND0_TYPE:
                    return new BlockNetworkCommand60EventServerPayload();
                case (int)Booma.GameNetworkOperationCode.GAME_COMMAND2_TYPE:
                    return new BlockNetworkCommand62EventServerPayload();
                case (int)Booma.GameNetworkOperationCode.GAME_COMMANDD_TYPE:
                    return new BlockNetworkCommand6DEventServerPayload();
                case (int)Booma.GameNetworkOperationCode.LOBBY_ADD_PLAYER_TYPE:
                    return new BlockOtherPlayerJoinedLobbyEventPayload();
                case (int)Booma.GameNetworkOperationCode.GAME_LEAVE_TYPE:
                    return new BlockOtherPlayerLeaveGameEventPayload();
                case (int)Booma.GameNetworkOperationCode.LOBBY_LEAVE_TYPE:
                    return new BlockOtherPlayerLeaveLobbyEventPayload();
                case (int)Booma.GameNetworkOperationCode.SERVER_CHARACTER_DATA_TYPE:
                    return new BlockSetCharacterDataEventPayload();
                case (int)Booma.GameNetworkOperationCode.CHAT_TYPE:
                    return new BlockTextChatMessageEventPayload();
                case (int)Booma.GameNetworkOperationCode.C_RANK_TYPE:
                    return new ChallengeModeRankDataEventPayload();
                case (int)Booma.GameNetworkOperationCode.BB_FULL_CHARACTER_TYPE:
                    return new InitializeCharacterDataEventPayload();
                case (int)Booma.GameNetworkOperationCode.LOBBY_LIST_TYPE:
                    return new LobbyListEventPayload();
                case (int)Booma.GameNetworkOperationCode.LOBBY_ARROW_LIST_TYPE:
                    return new SetLobbyArrowsEventPayload();
                case (int)Booma.GameNetworkOperationCode.BB_CHARACTER_ACK_TYPE:
                    return new CharacterCharacterSelectionAckPayload();
                case (int)Booma.GameNetworkOperationCode.BB_CHARACTER_UPDATE_TYPE:
                    return new CharacterCharacterUpdateResponsePayload();
                case (int)Booma.GameNetworkOperationCode.BB_CHECKSUM_ACK_TYPE:
                    return new CharacterChecksumResponsePayload();
                case (int)Booma.GameNetworkOperationCode.BB_PARAM_CHUNK_TYPE:
                    return new CharacterDataParametersChunkResponsePayload();
                case (int)Booma.GameNetworkOperationCode.BB_PARAM_HEADER_TYPE:
                    return new CharacterDataParametersHeaderResponsePayload();
                case (int)Booma.GameNetworkOperationCode.BB_GUILDCARD_CHUNK_TYPE:
                    return new CharacterGuildCardChunkResponsePayload();
                case (int)Booma.GameNetworkOperationCode.BB_GUILDCARD_HEADER_TYPE:
                    return new CharacterGuildCardDataHeaderResponsePayload();
                case (int)Booma.GameNetworkOperationCode.BB_OPTION_CONFIG_TYPE:
                    return new CharacterOptionsResponsePayload();
                case (int)Booma.GameNetworkOperationCode.TIMESTAMP_TYPE:
                    return new CharacterTimestampEventPayload();
                case (int)Booma.GameNetworkOperationCode.REDIRECT_TYPE:
                    return new SharedConnectionRedirectPayload();
                case (int)Booma.GameNetworkOperationCode.MESSAGE_BOX_TYPE:
                    return new SharedCreateMessageBoxEventPayload();
                case (int)Booma.GameNetworkOperationCode.INFO_REPLY_TYPE:
                    return new SharedInfoReplyEventPayload();
                case (int)Booma.GameNetworkOperationCode.BB_SECURITY_TYPE:
                    return new SharedLoginResponsePayload();
                case (int)Booma.GameNetworkOperationCode.BB_SCROLL_MSG_TYPE:
                    return new SharedMarqueeScrollChangeEventPayload();
                case (int)Booma.GameNetworkOperationCode.SHIP_LIST_TYPE:
                    return new SharedShipListEventPayload();
                case (int)Booma.GameNetworkOperationCode.BB_WELCOME_TYPE:
                    return new SharedWelcomePayload();
                case (int)Booma.GameNetworkOperationCode.BLOCK_LIST_TYPE:
                    return new ShipBlockListEventPayload();
                default:
                    return new UnknownServerGamePayload();
            }
        }
    }
}