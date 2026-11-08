@echo off
chcp 65001 >nul
title Cài đặt Quản Lý Phòng Máy (Tạo biểu tượng ngoài Màn hình Desktop & Chạy ngay)
cd /d "%~dp0"

echo ============================================================================
echo   CÀI ĐẶT HỆ THỐNG QUẢN LÝ PHÒNG MÁY (KẾT NỐI TAILSCALE 100.115.111.126,1433)
echo ============================================================================

if not exist "BanChayNgay_Portable\QuanLyPhongMay.exe" (
    echo [1/2] Đang đóng gói file thực thi .exe...
    dotnet build QuanLyPhongMay.csproj -c Release -o "BanChayNgay_Portable"
)

set "TARGET_EXE=%~dp0BanChayNgay_Portable\QuanLyPhongMay.exe"
set "WORK_DIR=%~dp0BanChayNgay_Portable"
set "SHORTCUT_PATH=%USERPROFILE%\Desktop\QuanLyPhongMay.lnk"

echo [2/2] Đang tạo biểu tượng (Shortcut) ngoài màn hình Desktop...
powershell -NoProfile -ExecutionPolicy Bypass -Command "$ws = New-Object -ComObject WScript.Shell; $s = $ws.CreateShortcut('%SHORTCUT_PATH%'); $s.TargetPath = '%TARGET_EXE%'; $s.WorkingDirectory = '%WORK_DIR%'; $s.Description = 'He thong Quan ly So do & Cau hinh Phong May (Tailscale SQL)'; $s.Save()"

echo.
echo [HOÀN TẤT] Đã tạo biểu tượng 'QuanLyPhongMay' ngoài Desktop!
echo Đang mở phần mềm...
start "" "%TARGET_EXE%"
timeout /t 3 >nul
