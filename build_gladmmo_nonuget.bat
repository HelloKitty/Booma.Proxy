dotnet publish src/Booma.Proxy.Client.Unity.GladMMO/Booma.Proxy.Client.Unity.GladMMO.csproj -c release

if not exist "build\gladmmo\release" mkdir build\gladmmo\release
xcopy src\Booma.Proxy.Client.Unity.GladMMO\bin\Release\netstandard2.0\publish build\gladmmo\release /Y /q /EXCLUDE:BuildExclude.txt