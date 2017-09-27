# Booma.Proxy

Yet another PSOBB proxy but this time with time in C#/.NET!

Built on top of 17 years of the reverse engineering work done by many in the community, Sodaboy's proxy, [Sylverant's opensource C++ DC/BB/GC server implementation](https://github.com/Sylverant/) and one of the [most recent public Tethella releases](https://github.com/justnoxx/psobb-tethealla/).

## How and Why

Booma.Proxy relies on the extendable and flexible metadata based serializer that I build for the World of Warcraft protocol, [FreecraftCore.Serializer](https://github.com/FreecraftCore/FreecraftCore.Serializer). FreecraftCore.Serializer was chosen because of the productivity, readable and usability it provides. Rewriting the same manual serialization that has been written 1,000 times since the Dreamcast isn't helpful or useful to anyone anymore.

Booma.Proxy will have a first class understanding of the data being sent, has the potential to provide high quality logging and packet breakdowns through Type-safe descriptors and will provide a generic metadata based API for generating UI/GUI forms for packet structures for spoofing purposes.

## Structure

TODO

## Builds

TODO

## Tests

AppVeyor: [![Build status](https://ci.appveyor.com/api/projects/status/fo39keq6deuwrerm/branch/master?svg=true)](https://ci.appveyor.com/project/HelloKitty/booma-proxy/branch/master)

## License

AGPL with an unrestricted perpetual non-exclusive license granted to [HelloKitty](www.github.com/HelloKitty)/AndrewBlakely
