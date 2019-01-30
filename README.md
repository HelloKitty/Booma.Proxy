## Discord: https://discord.gg/Qk8HpmG

# Booma.Proxy

Booma.Proxy is actually not a Proxy. It is a collection of C#/.NET libraries for communication, understanding and emulating Phantasy Star Online Blue Burst. These can be used to construct an emulated PSOBB server or an emulated PSOBB client. In fact this repository contains an in-development implementation of a client in Unity3D.

This project is built on top of 17 years of the reverse engineering work done by many in the community, Sodaboy's proxy, [Sylverant's opensource C++ DC/BB/GC server implementation](https://github.com/Sylverant/) and one of the [most recent public Tethella releases](https://github.com/justnoxx/psobb-tethealla/).

Special thanks to Soly for implementing and explaining the cryptography involved!

## How and Why

Booma.Proxy's libraries relies on the extendable and flexible metadata based serializer that I built for the World of Warcraft protocol, [FreecraftCore.Serializer](https://github.com/FreecraftCore/FreecraftCore.Serializer). FreecraftCore.Serializer was chosen because of the productivity, readable and usability it provides. Although it originally started as a WoW serializer inspired by Blizzard's on internal serializer it has become a staple for any emulation work that I do. Rewriting the same manual serialization that has been written 1,000 times since the Dreamcast isn't helpful or useful to anyone anymore so I feel it's time to step it up a notch and bring industry grade serialization techniques and workflows to PSOBB packets.

## Structure

There are 5 main services in Phantasy Star Online Blue Burst. These are [**Patch**](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.PatchServer), [**Login**](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.LoginServer), [**Character**](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.CharacterServer), [**Ship**](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.ShipServer) and [**Block**](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.BlockServer). They each contain a set of service specific operation codes and payloads (the data part of the packet). Additionally many of the game services have [shared packets](https://github.com/HelloKitty/Booma.Proxy/tree/master/src/Booma.Proxy.Packets.SharedServer) that they use.

Some of these projects are empty, such as Login, or nearly empty. This is either because the majority of the packets are shared between the other services or they have yet to be implemented.

Each packet has a 2 byte operation code. For the Patch service that operation code is called [PatchNetworkOperationCode](https://github.com/HelloKitty/Booma.Proxy/blob/master/src/Booma.Proxy.Packets.Common/OperationCodes/PatchNetworkOperationCode.cs) and for the Game services that operation code is called [GameNetworkOperationCode](https://github.com/HelloKitty/Booma.Proxy/blob/master/src/Booma.Proxy.Packets.Common/OperationCodes/GameNetworkOperationCode.cs).

Packets, or payloads as their data is referred to in the project, are usually dispatched to handlers that can handle them. There can be more than one handler per-payload defined. Depending on the situation you may want to use one or the other. See an example of a handler here that's called [SharedWelcomePayloadHandler](https://github.com/HelloKitty/Booma.Proxy/blob/master/src/Booma.Proxy.Client.Unity.Authentication/Handlers/SharedWelcomePayloadHandler.cs). It handles a welcome message and initializes the provided encryption IVs.

TODO continue doc

## Builds

TODO

## Tests

AppVeyor: [![Build status](https://ci.appveyor.com/api/projects/status/fo39keq6deuwrerm/branch/master?svg=true)](https://ci.appveyor.com/project/HelloKitty/booma-proxy/branch/master)

## License

Contributions including pull requests, commits, notes, dumps, gists or anything else in the repository are licensed under the below licensing terms.

AGPL with an unrestricted perpetual non-exclusive license granted to [HelloKitty](www.github.com/HelloKitty)/AndrewBlakely
