# Patch Packets

| Packet OpCode Name | OpCode| Sent by Server | Sent by Client |
| ------------- | ------------- | ------------- | ------------- |
| PATCH_WELCOME_TYPE | 0x0002 | [PatchingWelcomePayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingWelcomePayload.cs") | [PatchingWelcomePayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingWelcomePayload.cs") |
| PATCH_LOGIN_TYPE | 0x0004 | [PatchingReadyForLoginRequestPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingReadyForLoginRequestPayload.cs") | [PatchingReadyForLoginRequestPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingReadyForLoginRequestPayload.cs") |
| PATCH_FILE_SEND | 0x0006 | [PatchingFileCheckPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingFileCheckPayload.cs") | [PatchingFileCheckPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingFileCheckPayload.cs") |
| PATCH_DATA_SEND | 0x0007 | [PatchingFileChunkPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingFileChunkPayload.cs") | [PatchingFileChunkPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingFileChunkPayload.cs") |
| PATCH_FILE_DONE | 0x0008 | [PatchingFileDonePayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingFileDonePayload.cs") | [PatchingFileDonePayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingFileDonePayload.cs") |
| PATCH_SET_DIRECTORY | 0x0009 | [PatchingSetDirectoryPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingSetDirectoryPayload.cs") | [PatchingSetDirectoryPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingSetDirectoryPayload.cs") |
| PATCH_ONE_DIR_UP | 0x000A | [PatchingUpOneDirectoryCommandPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingUpOneDirectoryCommandPayload.cs") | [PatchingUpOneDirectoryCommandPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingUpOneDirectoryCommandPayload.cs") |
| PATCH_START_LIST | 0x000B | [PatchingStartPatchPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingStartPatchPayload.cs") | [PatchingStartPatchPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingStartPatchPayload.cs") |
| PATCH_FILE_INFO | 0x000C | **n/a** | **n/a** |
| PATCH_INFO_FINISHED | 0x000D | [PatchingInfoRequestDonePayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingInfoRequestDonePayload.cs") | [PatchingInfoRequestDonePayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingInfoRequestDonePayload.cs") |
| PATCH_FILE_INFO_REPLY | 0x000F | **n/a** | **n/a** |
| PATCH_FILE_LIST_DONE | 0x0010 | **n/a** | **n/a** |
| PATCH_SEND_INFO | 0x0011 | [PatchingPatchInfoPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingPatchInfoPayload.cs") | [PatchingPatchInfoPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingPatchInfoPayload.cs") |
| PATCH_SEND_DONE | 0x0012 | [PatchingDoneCommandPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingDoneCommandPayload.cs") | [PatchingDoneCommandPayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingDoneCommandPayload.cs") |
| PATCH_MESSAGE_TYPE | 0x0013 | [PatchingMessagePayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Server/PatchingMessagePayload.cs") | [PatchingMessagePayload]("../blob/master/src/Booma.Proxy.Packets.PatchServer/Payloads/Client/PatchingMessagePayload.cs") |
| PATCH_REDIRECT_TYPE | 0x0014 | **n/a** | **n/a** |
| PATCH_REDIRECT6_TYPE | 0x0614 | **n/a** | **n/a** |


This documentation was automatically generated using the documentation tools.