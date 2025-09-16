@echo off
setlocal

:: Cấu hình
set PROJECT_PATH=%~dp0
set PROJECT_FILE=YourProject.csproj
set RUNTIME=win-x64
set CONFIG=Release

:: Xóa cache gói riêng lẻ (nếu có)
echo Deleting broken NuGet packages...
rd /s /q "%userprofile%\.nuget\packages\microsoft.windowsdesktop.app.runtime.win-x64"
rd /s /q "%userprofile%\.nuget\packages\microsoft.netcore.app.runtime.win-x64"

:: Dọn toàn bộ cache NuGet
echo Clearing all NuGet caches...
dotnet nuget locals all --clear

:: Chuyển vào thư mục chứa project
cd /d "%PROJECT_PATH%"

:: Restore project
echo Restoring project...
dotnet restore "%PROJECT_FILE%"

:: Build & Publish
echo Publishing project...
dotnet publish "%PROJECT_FILE%" -r %RUNTIME% -c %CONFIG% --self-contained

echo.
echo ✅ Done!
pause
