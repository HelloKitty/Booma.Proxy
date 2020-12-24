## Discord: https://discord.psobb2.com

# Booma.Proxy

Booma.Proxy is actually not a Proxy. It is a collection of C#/.NET libraries for communication, understanding and emulating Phantasy Star Online Blue Burst. These can be used to construct an emulated PSOBB server or an emulated PSOBB client.

This project is built on top of 17 years of the reverse engineering work done by many in the community, Sodaboy's proxy, [Sylverant's opensource C++ DC/BB/GC server implementation](https://github.com/Sylverant/) and one of the [most recent public Tethella releases](https://github.com/justnoxx/psobb-tethealla/).

Special thanks to Soly for implementing and explaining the cryptography involved!

## How and Why

Booma.Proxy's relies on the extendable and flexible metadata-based serializer that I built for the World of Warcraft protocol, [FreecraftCore.Serializer](https://github.com/FreecraftCore/FreecraftCore.Serializer). FreecraftCore.Serializer was chosen because of the productivity, readable, performance and usability it provides. It allows for modeling binary structures like packets as DTOs directly as C# Types at a high level.

## Builds

[Booma.API.Common](https://www.nuget.org/packages/Booma.API.Common/)

[Booma.Crypto.Common](https://www.nuget.org/packages/Booma.Crypto.Common/)

[Booma.Packet.Common](https://www.nuget.org/packages/Booma.Packet.Common/)

[Booma.Packet.Patch](https://www.nuget.org/packages/Booma.Packet.Patch/)

[Booma.Patch.Game](https://www.nuget.org/packages/Booma.Patch.Game/)

## License

Contributions including pull requests, commits, notes, dumps, gists or anything else in the repository are licensed under the below licensing terms.

AGPL 3.0 with a seperate unrestricted, non-exclusive, perpetual, and irrevocable license also granted to [Andrew Blakely](www.github.com/HelloKitty)
