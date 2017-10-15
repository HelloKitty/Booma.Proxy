# Block Packets

| Packet OpCode Name | OpCode | Sent by Server | Sent by Client |
| ------------- | ------------- | ------------- | ------------- |
| PING_TYPE | 0x001D | [BlockClientPingEventPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.BlockServer/Payloads/Server/BlockClientPingEventPayload.cs) | [BlockClientPingResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.BlockServer/Payloads/Client/BlockClientPingResponsePayload.cs) |
| GAME_COMMAND0_TYPE | 0x0060 | [BlockNetworkCommandEventServerPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.BlockServer/Payloads/Server/BlockNetworkCommandEventServerPayload.cs) | [BlockNetworkCommandEventClientPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.BlockServer/Payloads/Client/BlockNetworkCommandEventClientPayload.cs) |
| BLOCK_SET_CHAR_DATA_TYPE | 0x0061 | **n/a** | [BlockCharacterDataInitializeClientResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.BlockServer/Payloads/Client/BlockCharacterDataInitializeClientResponsePayload.cs) |
| LOBBY_JOIN_TYPE | 0x0067 | [BlockLobbyJoinEventPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.BlockServer/Payloads/Server/BlockLobbyJoinEventPayload.cs) | **n/a** |
| CHAR_DATA_REQUEST_TYPE | 0x0095 | [BlockCharacterDataInitializationServerRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.BlockServer/Payloads/Server/BlockCharacterDataInitializationServerRequestPayload.cs) | **n/a** |


This documentation was automatically generated using the documentation tools.