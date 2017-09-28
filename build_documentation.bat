nuget.exe restore Booma.Proxy.sln
dotnet build Booma.Proxy.sln -c Debug
PAUSE

start tests\Booma.Proxy.Packets.DocumentationGenerator\bin\Debug\Booma.Proxy.Packets.DocumentationGenerator.exe \WAIT