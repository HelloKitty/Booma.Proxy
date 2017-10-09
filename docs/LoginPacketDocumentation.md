# Login Packets

| Packet OpCode Name | OpCode | Sent by Server | Sent by Client |
| ------------- | ------------- | ------------- | ------------- |
| BB_WELCOME_TYPE | 0x0003 | [LoginWelcomePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginWelcomePayload.cs) | **n/a** |
| REDIRECT_TYPE | 0x0019 | [LoginConnectionRedirectPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginConnectionRedirectPayload.cs) | **n/a** |
| MESSAGE_BOX_TYPE | 0x001A | [LoginCreateMessageBoxEventPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginCreateMessageBoxEventPayload.cs) | **n/a** |
| LOGIN_93_TYPE | 0x0093 | **n/a** | [LoginLoginRequest93Payload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginLoginRequest93Payload.cs) |
| BB_OPTION_REQUEST_TYPE | 0x00E0 | **n/a** | [LoginOptionsRequestPayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Client/LoginOptionsRequestPayload.cs) |
| BB_OPTION_CONFIG_TYPE | 0x00E2 | [LoginOptionsResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginOptionsResponsePayload.cs) | **n/a** |
| BB_SECURITY_TYPE | 0x00E6 | [LoginLoginResponsePayload](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer/Payloads/Server/LoginLoginResponsePayload.cs) | **n/a** |


This documentation was automatically generated using the documentation tools.