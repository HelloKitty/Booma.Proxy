# Login Packets

| Packet OpCode Name | OpCode | Sent by Server | Sent by Client |
| ------------- | ------------- | ------------- | ------------- |
| BB_WELCOME_TYPE | 0x0003 | [LoginWelcomePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginWelcomePayload.cs) | **n/a** |
| REDIRECT_TYPE | 0x0019 | [LoginConnectionRedirectPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginConnectionRedirectPayload.cs) | **n/a** |
| MESSAGE_BOX_TYPE | 0x001A | [LoginCreateMessageBoxEventPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginCreateMessageBoxEventPayload.cs) | **n/a** |
| LOGIN_93_TYPE | 0x0093 | **n/a** | [LoginLoginRequest93Payload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginLoginRequest93Payload.cs) |
| BB_OPTION_REQUEST_TYPE | 0x00E0 | **n/a** | [LoginOptionsRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginOptionsRequestPayload.cs) |
| BB_OPTION_CONFIG_TYPE | 0x00E2 | [LoginOptionsResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginOptionsResponsePayload.cs) | **n/a** |
| BB_CHARACTER_SELECT_TYPE | 0x00E3 | **n/a** | [LoginCharacterSelectionRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginCharacterSelectionRequestPayload.cs) |
| BB_CHARACTER_ACK_TYPE | 0x00E4 | [LoginCharacterSelectionAckPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginCharacterSelectionAckPayload.cs) | **n/a** |
| BB_CHARACTER_UPDATE_TYPE | 0x00E5 | [LoginCharacterUpdateResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginCharacterUpdateResponsePayload.cs) | **n/a** |
| BB_SECURITY_TYPE | 0x00E6 | [LoginLoginResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginLoginResponsePayload.cs) | **n/a** |
| BB_GUILDCARD_HEADER_TYPE | 0x01DC | [LoginGuildResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginGuildResponsePayload.cs) | **n/a** |
| BB_CHECKSUM_TYPE | 0x01E8 | **n/a** | [LoginChecksumRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginChecksumRequestPayload.cs) |
| BB_GUILDCARD_CHUNK_TYPE | 0x02DC | [LoginGuildCardChunkResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginGuildCardChunkResponsePayload.cs) | **n/a** |
| BB_CHECKSUM_ACK_TYPE | 0x02E8 | [LoginChecksumResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginChecksumResponsePayload.cs) | **n/a** |
| BB_GUILDCARD_CHUNK_REQ_TYPE | 0x03DC | **n/a** | [LoginGuildCardChunkRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginGuildCardChunkRequestPayload.cs) |
| BB_GUILD_REQUEST_TYPE | 0x03E8 | **n/a** | [LoginGuildRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginGuildRequestPayload.cs) |


This documentation was automatically generated using the documentation tools.