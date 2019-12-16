nuget.exe restore Booma.Proxy.sln
dotnet restore Booma.Proxy.sln
dotnet publish src/Booma.Proxy.Client.Unity.BuildAll/Booma.Proxy.Client.Unity.BuildAll.csproj -c release

if not exist "build\client\release" mkdir build\client\release
xcopy src\Booma.Proxy.Client.Unity.BuildAll\bin\Release\netstandard2.0\publish build\client\release /Y /q /EXCLUDE:BuildExclude.txt

PAUSE