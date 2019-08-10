for /f %%i in ('dir /b/a-d/od/t:c') do set LAST=%%i
echo The most recently created file is %LAST%

pause


set version=%DATE:~-4,4%%DATE:~-7,2%%DATE:~-10,2%.%TIME:~0,2%%TIME:~3,2%%TIME:~6,2%.0

nuget pack Package.nuspec -version %version% &
dotnet nuget push Chresimos.%version%.nupkg

pause