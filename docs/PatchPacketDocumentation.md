# Patch Packets

| Packet OpCode Name | OpCode| Sent by Server | Sent by Client |
| ------------- | ------------- | ------------- | ------------- |
| PATCH_WELCOME_TYPE | 0x0002 | [PatchingWelcomePayload]("todo") | [PatchingWelcomePayload]("todo") |
| PATCH_LOGIN_TYPE | 0x0004 | [PatchingReadyForLoginRequestPayload]("todo") | [PatchingReadyForLoginRequestPayload]("todo") |
| PATCH_FILE_SEND | 0x0006 | [PatchingFileCheckPayload]("todo") | [PatchingFileCheckPayload]("todo") |
| PATCH_DATA_SEND | 0x0007 | [PatchingFileChunkPayload]("todo") | [PatchingFileChunkPayload]("todo") |
| PATCH_FILE_DONE | 0x0008 | [PatchingFileDonePayload]("todo") | [PatchingFileDonePayload]("todo") |
| PATCH_SET_DIRECTORY | 0x0009 | [PatchingSetDirectoryPayload]("todo") | [PatchingSetDirectoryPayload]("todo") |
| PATCH_ONE_DIR_UP | 0x000A | [PatchingUpOneDirectoryCommandPayload]("todo") | [PatchingUpOneDirectoryCommandPayload]("todo") |
| PATCH_START_LIST | 0x000B | [PatchingStartPatchPayload]("todo") | [PatchingStartPatchPayload]("todo") |
| PATCH_FILE_INFO | 0x000C | **n/a** | **n/a** |
| PATCH_INFO_FINISHED | 0x000D | [PatchingInfoRequestDonePayload]("todo") | [PatchingInfoRequestDonePayload]("todo") |
| PATCH_FILE_INFO_REPLY | 0x000F | **n/a** | **n/a** |
| PATCH_FILE_LIST_DONE | 0x0010 | **n/a** | **n/a** |
| PATCH_SEND_INFO | 0x0011 | [PatchingPatchInfoPayload]("todo") | [PatchingPatchInfoPayload]("todo") |
| PATCH_SEND_DONE | 0x0012 | [PatchingDoneCommandPayload]("todo") | [PatchingDoneCommandPayload]("todo") |
| PATCH_MESSAGE_TYPE | 0x0013 | [PatchingMessagePayload]("todo") | [PatchingMessagePayload]("todo") |
| PATCH_REDIRECT_TYPE | 0x0014 | **n/a** | **n/a** |
| PATCH_REDIRECT6_TYPE | 0x0614 | **n/a** | **n/a** |
