# Login Packets

| Packet OpCode Name | OpCode | Sent by Server | Sent by Client |
| ------------- | ------------- | ------------- | ------------- |
| BB_WELCOME_TYPE | 0x0003 | [LoginWelcomePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginWelcomePayload.cs) | **n/a** |
| TYPE_05 | 0x0005 | **n/a** | [LoginDisconnectionRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginDisconnectionRequestPayload.cs) |
| MENU_SELECT_TYPE | 0x0010 | **n/a** | [LoginMenuSelectionRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginMenuSelectionRequestPayload.cs) |
| INFO_REPLY_TYPE | 0x0011 | [LoginInfoReplyEventPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginInfoReplyEventPayload.cs) | **n/a** |
| REDIRECT_TYPE | 0x0019 | [LoginConnectionRedirectPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginConnectionRedirectPayload.cs) | **n/a** |
| MESSAGE_BOX_TYPE | 0x001A | [LoginCreateMessageBoxEventPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginCreateMessageBoxEventPayload.cs) | **n/a** |
| LOGIN_93_TYPE | 0x0093 | **n/a** | [LoginLoginRequest93Payload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginLoginRequest93Payload.cs) |
| SHIP_LIST_TYPE | 0x00A0 | [LoginShipListEventPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginShipListEventPayload.cs) | **n/a** |
| TIMESTAMP_TYPE | 0x00B1 | [LoginTimestampEventPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginTimestampEventPayload.cs) | **n/a** |
| BB_OPTION_REQUEST_TYPE | 0x00E0 | **n/a** | [LoginOptionsRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginOptionsRequestPayload.cs) |
| BB_OPTION_CONFIG_TYPE | 0x00E2 | [LoginOptionsResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginOptionsResponsePayload.cs) | **n/a** |
| BB_CHARACTER_SELECT_TYPE | 0x00E3 | **n/a** | [LoginCharacterSelectionRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginCharacterSelectionRequestPayload.cs) |
| BB_CHARACTER_ACK_TYPE | 0x00E4 | [LoginCharacterSelectionAckPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginCharacterSelectionAckPayload.cs) | **n/a** |
| BB_CHARACTER_UPDATE_TYPE | 0x00E5 | [LoginCharacterUpdateResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginCharacterUpdateResponsePayload.cs) | **n/a** |
| BB_SECURITY_TYPE | 0x00E6 | [LoginLoginResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginLoginResponsePayload.cs) | **n/a** |
| BB_SCROLL_MSG_TYPE | 0x00EE | [LoginMarqueeScrollChangeEventPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginMarqueeScrollChangeEventPayload.cs) | **n/a** |
| BB_GUILDCARD_HEADER_TYPE | 0x01DC | [LoginGuildCardDataHeaderResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginGuildCardDataHeaderResponsePayload.cs) | **n/a** |
| BB_CHECKSUM_TYPE | 0x01E8 | **n/a** | [LoginChecksumRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginChecksumRequestPayload.cs) |
| BB_PARAM_HEADER_TYPE | 0x01EB | [LoginDataParametersHeaderResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginDataParametersHeaderResponsePayload.cs) | **n/a** |
| BB_GUILDCARD_CHUNK_TYPE | 0x02DC | [LoginGuildCardChunkResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginGuildCardChunkResponsePayload.cs) | **n/a** |
| BB_CHECKSUM_ACK_TYPE | 0x02E8 | [LoginChecksumResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginChecksumResponsePayload.cs) | **n/a** |
| BB_PARAM_CHUNK_TYPE | 0x02EB | [LoginDataParametersChunkResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginDataParametersChunkResponsePayload.cs) | **n/a** |
| BB_GUILDCARD_CHUNK_REQ_TYPE | 0x03DC | **n/a** | [LoginGuildCardChunkRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginGuildCardChunkRequestPayload.cs) |
| BB_GUILD_REQUEST_TYPE | 0x03E8 | **n/a** | [LoginGuildRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginGuildRequestPayload.cs) |
| BB_PARAM_CHUNK_REQ_TYPE | 0x03EB | **n/a** | [LoginDataParametersChunkRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginDataParametersChunkRequestPayload.cs) |
| BB_PARAM_HEADER_REQ_TYPE | 0x04EB | **n/a** | [LoginDataParametersHeaderRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginDataParametersHeaderRequestPayload.cs) |


This documentation was automatically generated using the documentation tools.