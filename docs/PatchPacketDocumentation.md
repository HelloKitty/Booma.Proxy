# Patch Packets

| Packet OpCode Name | OpCode| Sent by Server | Sent by Client |
| ------------- | ------------- | ------------- | ------------- |
| PATCH_WELCOME_TYPE | 0x0002 | [PatchingWelcomePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingWelcomePayload.cs) | [PatchingWelcomeAckPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingWelcomeAckPayload.cs) |
| PATCH_LOGIN_TYPE | 0x0004 | [PatchingReadyForLoginRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingReadyForLoginRequestPayload.cs) | [PatchingLoginRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingLoginRequestPayload.cs) |
| PATCH_FILE_SEND | 0x0006 | [PatchingFileCheckPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingFileCheckPayload.cs) | **n/a** |
| PATCH_DATA_SEND | 0x0007 | [PatchingFileChunkPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingFileChunkPayload.cs) | **n/a** |
| PATCH_FILE_DONE | 0x0008 | [PatchingFileDonePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingFileDonePayload.cs) | **n/a** |
| PATCH_SET_DIRECTORY | 0x0009 | [PatchingSetDirectoryPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingSetDirectoryPayload.cs) | **n/a** |
| PATCH_ONE_DIR_UP | 0x000A | [PatchingUpOneDirectoryCommandPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingUpOneDirectoryCommandPayload.cs) | **n/a** |
| PATCH_START_LIST | 0x000B | [PatchingStartPatchPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingStartPatchPayload.cs) | **n/a** |
| PATCH_FILE_INFO | 0x000C | **n/a** | **n/a** |
| PATCH_INFO_FINISHED | 0x000D | [PatchingInfoRequestDonePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingInfoRequestDonePayload.cs) | **n/a** |
| PATCH_FILE_INFO_REPLY | 0x000F | **n/a** | [PatchingFileInformationPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingFileInformationPayload.cs) |
| PATCH_FILE_LIST_DONE | 0x0010 | **n/a** | [PatchingFileInformationDoneAckPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingFileInformationDoneAckPayload.cs) |
| PATCH_SEND_INFO | 0x0011 | [PatchingPatchInfoPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingPatchInfoPayload.cs) | **n/a** |
| PATCH_SEND_DONE | 0x0012 | [PatchingDoneCommandPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingDoneCommandPayload.cs) | **n/a** |
| PATCH_MESSAGE_TYPE | 0x0013 | [PatchingMessagePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingMessagePayload.cs) | **n/a** |
| PATCH_REDIRECT_TYPE | 0x0014 | **n/a** | **n/a** |
| PATCH_REDIRECT6_TYPE | 0x0614 | **n/a** | **n/a** |


This documentation was automatically generated using the documentation tools.