nuget.exe restore Booma.Proxy.sln
dotnet restore Booma.Proxy.sln
dotnet publish tests/Booma.Proxy.Packets.DocumentationGenerator/Booma.Proxy.Packets.DocumentationGenerator.csproj -c release

start tests\Booma.Proxy.Packets.DocumentationGenerator\bin\Release\net46\Booma.Proxy.Packets.DocumentationGenerator.exe \WAIT