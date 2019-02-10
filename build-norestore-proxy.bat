dotnet msbuild src/Tools/Booma.Proxy.Proxy -p:Configuration=Debug

if not exist "build\proxy" mkdir build\proxy

xcopy src\Tools\Booma.Proxy.Proxy\bin\Debug\net462 build\proxy
EXIT 0