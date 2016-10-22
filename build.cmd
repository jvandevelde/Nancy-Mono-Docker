REM set msbuild.exe=
REM for /D %%D in (%SYSTEMROOT%\Microsoft.NET\Framework\v4*) do set msbuild.exe=%%D\MSBuild.exe
REM %msbuild.exe% ./NancyMonoDemo.sln /t:Build /p:Configuration=Release /p:TargetFrameworkVersion=v4.6

set MSBUILD_PATH="%ProgramFiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe"
%MSBUILD_PATH% ./NancyMonoDemo.sln /t:Build /p:Configuration=Release /p:TargetFrameworkVersion=v4.6