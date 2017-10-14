# Character Packets

| Packet OpCode Name | OpCode | Sent by Server | Sent by Client |
| ------------- | ------------- | ------------- | ------------- |
| TIMESTAMP_TYPE | 0x00B1 | [CharacterTimestampEventPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Server/CharacterTimestampEventPayload.cs) | **n/a** |
| BB_OPTION_REQUEST_TYPE | 0x00E0 | **n/a** | [CharacterOptionsRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Client/CharacterOptionsRequestPayload.cs) |
| BB_OPTION_CONFIG_TYPE | 0x00E2 | [CharacterOptionsResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Server/CharacterOptionsResponsePayload.cs) | **n/a** |
| BB_CHARACTER_SELECT_TYPE | 0x00E3 | **n/a** | [CharacterCharacterSelectionRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Client/CharacterCharacterSelectionRequestPayload.cs) |
| BB_CHARACTER_ACK_TYPE | 0x00E4 | [CharacterCharacterSelectionAckPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Server/CharacterCharacterSelectionAckPayload.cs) | **n/a** |
| BB_CHARACTER_UPDATE_TYPE | 0x00E5 | [CharacterCharacterUpdateResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Server/CharacterCharacterUpdateResponsePayload.cs) | **n/a** |
| BB_GUILDCARD_HEADER_TYPE | 0x01DC | [CharacterGuildCardDataHeaderResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Server/CharacterGuildCardDataHeaderResponsePayload.cs) | **n/a** |
| BB_CHECKSUM_TYPE | 0x01E8 | **n/a** | [CharacterChecksumRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Client/CharacterChecksumRequestPayload.cs) |
| BB_PARAM_HEADER_TYPE | 0x01EB | [CharacterDataParametersHeaderResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Server/CharacterDataParametersHeaderResponsePayload.cs) | **n/a** |
| BB_GUILDCARD_CHUNK_TYPE | 0x02DC | [CharacterGuildCardChunkResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Server/CharacterGuildCardChunkResponsePayload.cs) | **n/a** |
| BB_CHECKSUM_ACK_TYPE | 0x02E8 | [CharacterChecksumResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Server/CharacterChecksumResponsePayload.cs) | **n/a** |
| BB_PARAM_CHUNK_TYPE | 0x02EB | [CharacterDataParametersChunkResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Server/CharacterDataParametersChunkResponsePayload.cs) | **n/a** |
| BB_GUILDCARD_CHUNK_REQ_TYPE | 0x03DC | **n/a** | [CharacterGuildCardChunkRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Client/CharacterGuildCardChunkRequestPayload.cs) |
| BB_GUILD_REQUEST_TYPE | 0x03E8 | **n/a** | [CharacterGuildHeaderRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Client/CharacterGuildHeaderRequestPayload.cs) |
| BB_PARAM_CHUNK_REQ_TYPE | 0x03EB | **n/a** | [CharacterDataParametersChunkRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Client/CharacterDataParametersChunkRequestPayload.cs) |
| BB_PARAM_HEADER_REQ_TYPE | 0x04EB | **n/a** | [CharacterDataParametersHeaderRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer/Payloads/Client/CharacterDataParametersHeaderRequestPayload.cs) |


This documentation was automatically generated using the documentation tools.