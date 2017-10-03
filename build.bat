nuget.exe restore Booma.Proxy.sln
dotnet restore Booma.Proxy.sln
dotnet publish src/Booma.Proxy.Client.Unity.Login/Booma.Proxy.Client.Unity.Login.csproj -c release
dotnet publish src/Booma.Proxy.Client.Unity.Editor/Booma.Proxy.Client.Unity.Editor.csproj -c release

if not exist "build\client\release" mkdir build\client\release
xcopy src\Booma.Proxy.Client.Unity.Editor\bin\Release\net46\publish build\client\release /Y /q /EXCLUDE:BuildExclude.txt
xcopy src\Booma.Proxy.Client.Unity.Login\bin\Release\net46\publish build\client\release /Y /q /EXCLUDE:BuildExclude.txt

PAUSE