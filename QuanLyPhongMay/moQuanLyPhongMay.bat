@echo off
chcp 65001 >nul
title Khởi chạy Hệ thống Quản lý Phòng Máy (Tailscale SQL: 100.115.111.126,1433)
cd /d "%~dp0"

if exist "BanChayNgay_Portable\QuanLyPhongMay.exe" (
    echo [OK] Đang khởi động QuanLyPhongMay.exe từ bản Portable...
    start "" "BanChayNgay_Portable\QuanLyPhongMay.exe"
    exit /b 0
)

if exist "bin\Release\net10.0-windows\QuanLyPhongMay.exe" (
    echo [OK] Đang khởi động QuanLyPhongMay.exe (Release)...
    start "" "bin\Release\net10.0-windows\QuanLyPhongMay.exe"
    exit /b 0
)

if exist "bin\Debug\net10.0-windows\QuanLyPhongMay.exe" (
    echo [OK] Đang khởi động QuanLyPhongMay.exe (Debug)...
    start "" "bin\Debug\net10.0-windows\QuanLyPhongMay.exe"
    exit /b 0
)

echo [INFO] Đang biên dịch nhanh bản chạy ngay...
dotnet build QuanLyPhongMay.csproj -c Release -o "BanChayNgay_Portable"
if exist "BanChayNgay_Portable\QuanLyPhongMay.exe" (
    start "" "BanChayNgay_Portable\QuanLyPhongMay.exe"
) else (
    pause
)
