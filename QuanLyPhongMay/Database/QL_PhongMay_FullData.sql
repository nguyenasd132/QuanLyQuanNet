-- ============================================================================
-- HỆ THỐNG QUẢN LÝ PHÒNG MÁY & SƠ ĐỒ THIẾT BỊ (QL_PhongMay)
-- KỊCH BẢN TẠO CƠ SỞ DỮ LIỆU & BƠM DỮ LIỆU MẪU (AN TOÀN - IGNORE NẾU ĐÃ CÓ)
-- ============================================================================

USE master;
GO

-- 1. TẠO CƠ SỞ DỮ LIỆU NẾU CHƯA CÓ (KHÔNG XÓA NẾU ĐÃ TỒN TẠI)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QL_PhongMay')
BEGIN
    CREATE DATABASE QL_PhongMay;
    PRINT N'Đã tạo mới database [QL_PhongMay].';
END
ELSE
BEGIN
    PRINT N'Database [QL_PhongMay] đã tồn tại, tiếp tục sử dụng.';
END
GO

USE QL_PhongMay;
GO

-- ============================================================================
-- PHẦN I: TẠO BẢNG (IGNORE NẾU ĐÃ TỒN TẠI BẢNG)
-- ============================================================================

-- Bảng 1: Người Dùng
IF OBJECT_ID(N'dbo.NguoiDung', N'U') IS NULL
BEGIN
    CREATE TABLE NguoiDung (
        MaND INT IDENTITY(1,1) PRIMARY KEY,
        TaiKhoan VARCHAR(50) NOT NULL UNIQUE,
        MatKhau VARCHAR(255) NOT NULL,
        HoTen NVARCHAR(100) NOT NULL,
        VaiTro NVARCHAR(50) NOT NULL CHECK (VaiTro IN (N'Quản trị viên', N'Kỹ thuật viên'))
    );
    PRINT N'Đã tạo bảng [NguoiDung].';
END
ELSE
BEGIN
    PRINT N'Bảng [NguoiDung] đã tồn tại (Ignore).';
END
GO

-- Bảng 2: Phòng Máy
IF OBJECT_ID(N'dbo.PhongMay', N'U') IS NULL
BEGIN
    CREATE TABLE PhongMay (
        MaPhong VARCHAR(20) PRIMARY KEY,
        TenPhong NVARCHAR(100) NOT NULL,
        ViTri NVARCHAR(100),
        TrangThai NVARCHAR(50) DEFAULT N'Đang hoạt động',
        HinhNen NVARCHAR(500) NULL
    );
    PRINT N'Đã tạo bảng [PhongMay].';
END
ELSE
BEGIN
    PRINT N'Bảng [PhongMay] đã tồn tại (Ignore).';
    IF COL_LENGTH(N'dbo.PhongMay', N'HinhNen') IS NULL
    BEGIN
        ALTER TABLE PhongMay ADD HinhNen NVARCHAR(500) NULL;
        PRINT N'Đã bổ sung cột [HinhNen] vào bảng [PhongMay].';
    END
END
GO

-- Bảng 3: Máy Tính
IF OBJECT_ID(N'dbo.MayTinh', N'U') IS NULL
BEGIN
    CREATE TABLE MayTinh (
        MaMay VARCHAR(20) PRIMARY KEY,
        MaPhong VARCHAR(20) NOT NULL,
        TenMay NVARCHAR(50) NOT NULL,
        TrangThai NVARCHAR(50) DEFAULT N'Hoạt động',
        ToaDoX INT NULL DEFAULT 20,
        ToaDoY INT NULL DEFAULT 20,
        CONSTRAINT FK_MayTinh_PhongMay FOREIGN KEY (MaPhong) REFERENCES PhongMay(MaPhong) ON DELETE CASCADE
    );
    PRINT N'Đã tạo bảng [MayTinh].';
END
ELSE
BEGIN
    PRINT N'Bảng [MayTinh] đã tồn tại (Ignore).';
END
GO

-- Bảng 4: Loại Thiết Bị
IF OBJECT_ID(N'dbo.LoaiThietBi', N'U') IS NULL
BEGIN
    CREATE TABLE LoaiThietBi (
        MaLoai VARCHAR(20) PRIMARY KEY,
        TenLoai NVARCHAR(100) NOT NULL
    );
    PRINT N'Đã tạo bảng [LoaiThietBi].';
END
ELSE
BEGIN
    PRINT N'Bảng [LoaiThietBi] đã tồn tại (Ignore).';
END
GO

-- Bảng 5: Thiết Bị / Linh Kiện
IF OBJECT_ID(N'dbo.ThietBi', N'U') IS NULL
BEGIN
    CREATE TABLE ThietBi (
        MaThietBi VARCHAR(20) PRIMARY KEY,
        MaLoai VARCHAR(20) NOT NULL,
        MaMay VARCHAR(20) NULL,
        TenThietBi NVARCHAR(100) NOT NULL,
        ThongSo NVARCHAR(255),
        HinhAnh NVARCHAR(500) NULL,
        TrangThai NVARCHAR(50) DEFAULT N'Hoạt động',
        CONSTRAINT FK_ThietBi_LoaiThietBi FOREIGN KEY (MaLoai) REFERENCES LoaiThietBi(MaLoai),
        CONSTRAINT FK_ThietBi_MayTinh FOREIGN KEY (MaMay) REFERENCES MayTinh(MaMay) ON DELETE SET NULL
    );
    PRINT N'Đã tạo bảng [ThietBi].';
END
ELSE
BEGIN
    PRINT N'Bảng [ThietBi] đã tồn tại (Ignore).';
END
GO

-- Bảng 6: Phiếu Sự Cố
IF OBJECT_ID(N'dbo.PhieuSuCo', N'U') IS NULL
BEGIN
    CREATE TABLE PhieuSuCo (
        MaSuCo VARCHAR(20) PRIMARY KEY,
        MaMay VARCHAR(20) NOT NULL,
        MaThietBi VARCHAR(20) NULL,
        NguoiLap INT NOT NULL,
        NgayBaoCao DATETIME DEFAULT GETDATE(),
        MoTaLoi NVARCHAR(MAX),
        MucDo NVARCHAR(50) DEFAULT N'Bình thường',
        TinhTrangXuLy NVARCHAR(50) DEFAULT N'Chưa xử lý',
        CONSTRAINT FK_PhieuSuCo_MayTinh FOREIGN KEY (MaMay) REFERENCES MayTinh(MaMay),
        CONSTRAINT FK_PhieuSuCo_ThietBi FOREIGN KEY (MaThietBi) REFERENCES ThietBi(MaThietBi),
        CONSTRAINT FK_PhieuSuCo_NguoiDung FOREIGN KEY (NguoiLap) REFERENCES NguoiDung(MaND)
    );
    PRINT N'Đã tạo bảng [PhieuSuCo].';
END
ELSE
BEGIN
    PRINT N'Bảng [PhieuSuCo] đã tồn tại (Ignore).';
END
GO

-- Bảng 7: Lịch Bảo Trì & Sửa Chữa
IF OBJECT_ID(N'dbo.LichBaoTri_SuaChua', N'U') IS NULL
BEGIN
    CREATE TABLE LichBaoTri_SuaChua (
        MaBaoTri VARCHAR(20) PRIMARY KEY,
        MaSuCo VARCHAR(20) NULL,
        MaMay VARCHAR(20) NOT NULL,
        NguoiThucHien INT NOT NULL,
        NgayBaoTri DATETIME DEFAULT GETDATE(),
        NoiDung NVARCHAR(MAX),
        KetQua NVARCHAR(100) DEFAULT N'Hoàn thành',
        CONSTRAINT FK_LichBaoTri_PhieuSuCo FOREIGN KEY (MaSuCo) REFERENCES PhieuSuCo(MaSuCo),
        CONSTRAINT FK_LichBaoTri_MayTinh FOREIGN KEY (MaMay) REFERENCES MayTinh(MaMay),
        CONSTRAINT FK_LichBaoTri_NguoiDung FOREIGN KEY (NguoiThucHien) REFERENCES NguoiDung(MaND)
    );
    PRINT N'Đã tạo bảng [LichBaoTri_SuaChua].';
END
ELSE
BEGIN
    PRINT N'Bảng [LichBaoTri_SuaChua] đã tồn tại (Ignore).';
END
GO

-- ============================================================================
-- PHẦN II: THÊM DỮ LIỆU MẪU (CHỈ THÊM NẾU BẢN GHI CHƯA TỒN TẠI)
-- ============================================================================

-- 1. BẢNG NGUOIDUNG
IF NOT EXISTS (SELECT 1 FROM NguoiDung WHERE TaiKhoan = 'admin')
    INSERT INTO NguoiDung (TaiKhoan, MatKhau, HoTen, VaiTro) VALUES ('admin', '123456', N'Nguyễn Văn Quản Trị', N'Quản trị viên');
IF NOT EXISTS (SELECT 1 FROM NguoiDung WHERE TaiKhoan = 'kythuat01')
    INSERT INTO NguoiDung (TaiKhoan, MatKhau, HoTen, VaiTro) VALUES ('kythuat01', '123456', N'Trần Văn Kỹ Thuật', N'Kỹ thuật viên');
IF NOT EXISTS (SELECT 1 FROM NguoiDung WHERE TaiKhoan = 'kythuat02')
    INSERT INTO NguoiDung (TaiKhoan, MatKhau, HoTen, VaiTro) VALUES ('kythuat02', '123456', N'Lê Minh Hoàng', N'Kỹ thuật viên');
IF NOT EXISTS (SELECT 1 FROM NguoiDung WHERE TaiKhoan = 'gv_nguyenanh')
    INSERT INTO NguoiDung (TaiKhoan, MatKhau, HoTen, VaiTro) VALUES ('gv_nguyenanh', '123456', N'Nguyễn Hoàng Anh', N'Quản trị viên');
GO

-- 2. BẢNG PHONGMAY
INSERT INTO PhongMay (MaPhong, TenPhong, ViTri, TrangThai)
SELECT src.MaPhong, src.TenPhong, src.ViTri, src.TrangThai
FROM (VALUES 
    ('PM01', N'Phòng Thực Hành Lập Trình C/C++', N'Tòa A - Lầu 1 (Phòng A101)', N'Đang hoạt động'),
    ('PM02', N'Phòng Mạng Máy Tính & An Toàn TT', N'Tòa A - Lầu 2 (Phòng A203)', N'Đang hoạt động'),
    ('PM03', N'Phòng Đồ Họa & Trí Tuệ Nhân Tạo (AI)', N'Tòa B - Lầu 3 (Phòng B305)', N'Bảo trì định kỳ')
) AS src(MaPhong, TenPhong, ViTri, TrangThai)
WHERE NOT EXISTS (SELECT 1 FROM PhongMay WHERE PhongMay.MaPhong = src.MaPhong);
GO

-- 3. BẢNG LOAITHIETBI
INSERT INTO LoaiThietBi (MaLoai, TenLoai)
SELECT src.MaLoai, src.TenLoai
FROM (VALUES 
    ('L01', N'Màn hình'),
    ('L02', N'Bàn phím'),
    ('L03', N'Chuột'),
    ('L04', N'RAM'),
    ('L05', N'CPU'),
    ('L06', N'Card đồ họa (VGA)'),
    ('L07', N'Ổ cứng SSD/HDD'),
    ('L08', N'Tai nghe')
) AS src(MaLoai, TenLoai)
WHERE NOT EXISTS (SELECT 1 FROM LoaiThietBi WHERE LoaiThietBi.MaLoai = src.MaLoai);
GO

-- 4. BẢNG MAYTINH (Tọa độ chuẩn lưới 20px)
INSERT INTO MayTinh (MaMay, MaPhong, TenMay, TrangThai, ToaDoX, ToaDoY)
SELECT src.MaMay, src.MaPhong, src.TenMay, src.TrangThai, src.ToaDoX, src.ToaDoY
FROM (VALUES 
    -- Phòng PM01 (12 máy)
    ('PC01', 'PM01', N'Máy 01', N'Hoạt động', 20, 20),
    ('PC02', 'PM01', N'Máy 02', N'Hư hỏng',   200, 20),
    ('PC03', 'PM01', N'Máy 03', N'Bảo trì',   380, 20),
    ('PC04', 'PM01', N'Máy 04', N'Hoạt động', 560, 20),
    ('PC05', 'PM01', N'Máy 05', N'Hoạt động', 20, 240),
    ('PC06', 'PM01', N'Máy 06', N'Hoạt động', 200, 240),
    ('PC07', 'PM01', N'Máy 07', N'Hư hỏng',   380, 240),
    ('PC08', 'PM01', N'Máy 08', N'Hoạt động', 560, 240),
    ('PC09', 'PM01', N'Máy 09', N'Hoạt động', 20, 460),
    ('PC10', 'PM01', N'Máy 10', N'Hoạt động', 200, 460),
    ('PC11', 'PM01', N'Máy 11', N'Bảo trì',   380, 460),
    ('PC12', 'PM01', N'Máy 12', N'Hoạt động', 560, 460),
    -- Phòng PM02 (6 máy)
    ('PC13', 'PM02', N'Máy Cisco 01', N'Hoạt động', 40, 40),
    ('PC14', 'PM02', N'Máy Cisco 02', N'Hoạt động', 220, 40),
    ('PC15', 'PM02', N'Máy Cisco 03', N'Hư hỏng',   400, 40),
    ('PC16', 'PM02', N'Máy Cisco 04', N'Hoạt động', 40, 260),
    ('PC17', 'PM02', N'Máy Cisco 05', N'Bảo trì',   220, 260),
    ('PC18', 'PM02', N'Máy Cisco 06', N'Hoạt động', 400, 260),
    -- Phòng PM03 (4 máy đồ họa)
    ('PC19', 'PM03', N'Máy Render 01', N'Hoạt động', 40, 40),
    ('PC20', 'PM03', N'Máy Render 02', N'Hoạt động', 240, 40),
    ('PC21', 'PM03', N'Máy Render 03', N'Bảo trì',   40, 260),
    ('PC22', 'PM03', N'Máy Render 04', N'Hoạt động', 240, 260)
) AS src(MaMay, MaPhong, TenMay, TrangThai, ToaDoX, ToaDoY)
WHERE NOT EXISTS (SELECT 1 FROM MayTinh WHERE MayTinh.MaMay = src.MaMay);
GO

-- 5. BẢNG THIETBI
INSERT INTO ThietBi (MaThietBi, MaLoai, MaMay, TenThietBi, ThongSo, HinhAnh, TrangThai)
SELECT src.MaThietBi, src.MaLoai, src.MaMay, src.TenThietBi, src.ThongSo, src.HinhAnh, src.TrangThai
FROM (VALUES 
    ('TB01_MH',  'L01', 'PC01', N'Màn hình Dell P2419H', N'24 inch FHD IPS 60Hz', 'Images/dell24.png', N'Hoạt động'),
    ('TB01_BP',  'L02', 'PC01', N'Bàn phím DareU LK145', N'Giả cơ, cổng USB', 'Images/dareu_kb.png', N'Hoạt động'),
    ('TB01_CH',  'L03', 'PC01', N'Chuột Logitech G102 Lightsync', N'8000 DPI Optical', 'Images/g102.png', N'Hoạt động'),
    ('TB01_RAM', 'L04', 'PC01', N'RAM Kingston Fury Beast 16GB', N'DDR4 3200MHz (2x8GB)', 'Images/ram_kingston.png', N'Hoạt động'),
    ('TB01_CPU', 'L05', 'PC01', N'CPU Intel Core i5-12400', N'6 nhân 12 luồng, Turbo 4.4GHz', 'Images/i5.png', N'Hoạt động'),
    ('TB02_MH',  'L01', 'PC02', N'Màn hình Dell P2419H', N'24 inch FHD IPS 60Hz', 'Images/dell24.png', N'Hoạt động'),
    ('TB02_BP',  'L02', 'PC02', N'Bàn phím DareU LK145', N'Giả cơ, cổng USB', 'Images/dareu_kb.png', N'Hoạt động'),
    ('TB02_CH',  'L03', 'PC02', N'Chuột Logitech G102 Lightsync', N'Liệt nút click trái, đứt dây', 'Images/g102.png', N'Hư hỏng'),
    ('TB02_RAM', 'L04', 'PC02', N'RAM Kingston Fury Beast 8GB', N'Cháy chip nhớ, dump xanh', 'Images/ram_kingston.png', N'Hư hỏng'),
    ('TB02_CPU', 'L05', 'PC02', N'CPU Intel Core i5-12400', N'6 nhân 12 luồng, Turbo 4.4GHz', 'Images/i5.png', N'Hoạt động'),
    ('TB03_MH',  'L01', 'PC03', N'Màn hình Samsung Curved 24 inch', N'Bị sọc ngang hiển thị', 'Images/samsung24.png', N'Bảo trì'),
    ('TB03_BP',  'L02', 'PC03', N'Bàn phím DareU LK145', N'Bình thường', 'Images/dareu_kb.png', N'Hoạt động'),
    ('TB03_CH',  'L03', 'PC03', N'Chuột Logitech G102 Lightsync', N'Bình thường', 'Images/g102.png', N'Hoạt động'),
    ('TB03_CPU', 'L05', 'PC03', N'CPU Intel Core i5-12400', N'6 nhân 12 luồng', 'Images/i5.png', N'Hoạt động'),
    ('TB04_MH',  'L01', 'PC04', N'Màn hình LG 24MP500', N'24 inch IPS 75Hz', 'Images/lg24.png', N'Hoạt động'),
    ('TB04_BP',  'L02', 'PC04', N'Bàn phím Fuhlen L411', N'Chống nước văn phòng', 'Images/fuhlen.png', N'Hoạt động'),
    ('TB04_CH',  'L03', 'PC04', N'Chuột Fuhlen L102', N'1000 DPI Optical', 'Images/fuhlen_mouse.png', N'Hoạt động'),
    ('TB05_MH',  'L01', 'PC05', N'Màn hình Asus VP249QGR', N'24 inch 144Hz IPS', 'Images/asus24.png', N'Hoạt động'),
    ('TB05_CH',  'L03', 'PC05', N'Chuột Razer DeathAdder Essential', N'6400 DPI', 'Images/razer.png', N'Hoạt động'),
    ('TB06_MH',  'L01', 'PC06', N'Màn hình Dell P2419H', N'24 inch FHD', 'Images/dell24.png', N'Hoạt động'),
    ('TB06_CH',  'L03', 'PC06', N'Chuột Logitech G102', N'Bình thường', 'Images/g102.png', N'Hoạt động'),
    ('TB07_MH',  'L01', 'PC07', N'Màn hình Dell P2419H', N'Mất nguồn không lên đèn', 'Images/dell24.png', N'Hư hỏng'),
    ('TB07_BP',  'L02', 'PC07', N'Bàn phím DareU LK145', N'Kẹt phím Space và Enter', 'Images/dareu_kb.png', N'Hư hỏng'),
    ('TB07_CH',  'L03', 'PC07', N'Chuột Logitech G102', N'Bình thường', 'Images/g102.png', N'Hoạt động'),
    ('TB08_MH',  'L01', 'PC08', N'Màn hình LG 24MP500', N'24 inch IPS', 'Images/lg24.png', N'Hoạt động'),
    ('TB08_BP',  'L02', 'PC08', N'Bàn phím Fuhlen L411', N'Bình thường', 'Images/fuhlen.png', N'Hoạt động'),
    ('TB11_MH',  'L01', 'PC11', N'Màn hình AOC 24 inch', N'75Hz Full HD', 'Images/aoc24.png', N'Hoạt động'),
    ('TB11_VGA', 'L06', 'PC11', N'VGA NVIDIA GTX 1660 Super', N'6GB GDDR6 - Quạt kêu to, quá nhiệt', 'Images/gtx1660.png', N'Bảo trì'),
    ('TB19_MH',  'L01', 'PC19', N'Màn hình Dell UltraSharp U2722D', N'27 inch 2K QHD IPS', 'Images/dell_u27.png', N'Hoạt động'),
    ('TB19_CPU', 'L05', 'PC19', N'CPU Intel Core i9-13900K', N'24 nhân 32 luồng, Turbo 5.8GHz', 'Images/i9.png', N'Hoạt động'),
    ('TB19_VGA', 'L06', 'PC19', N'VGA NVIDIA RTX 4070 Ti 12GB', N'12GB GDDR6X Chuyên Render 3D', 'Images/rtx4070.png', N'Hoạt động'),
    ('TB19_RAM', 'L04', 'PC19', N'RAM Corsair Vengeance 64GB', N'DDR5 5600MHz (2x32GB)', 'Images/corsair64.png', N'Hoạt động'),
    ('TB19_SSD', 'L07', 'PC19', N'SSD Samsung 980 Pro 2TB', N'PCIe 4.0 NVMe Read 7000MB/s', 'Images/ssd_samsung.png', N'Hoạt động'),
    ('TB_KHO_01', 'L01', NULL, N'Màn hình Dell P2419H (Kho dự phòng)', N'24 inch FHD Mới 100%', 'Images/dell24.png', N'Hoạt động'),
    ('TB_KHO_02', 'L03', NULL, N'Chuột Logitech G102 Lightsync (Kho)', N'Mới nguyên hộp', 'Images/g102.png', N'Hoạt động'),
    ('TB_KHO_03', 'L02', NULL, N'Bàn phím DareU LK145 (Kho dự phòng)', N'Mới nguyên hộp', 'Images/dareu_kb.png', N'Hoạt động'),
    ('TB_KHO_04', 'L04', NULL, N'RAM Kingston Fury 8GB DDR4 (Kho)', N'Bus 3200MHz New', 'Images/ram_kingston.png', N'Hoạt động'),
    ('TB_KHO_05', 'L08', NULL, N'Tai nghe Gaming Zidli ZH7 (Kho)', N'Âm thanh 7.1 có mic', 'Images/zidli.png', N'Hoạt động')
) AS src(MaThietBi, MaLoai, MaMay, TenThietBi, ThongSo, HinhAnh, TrangThai)
WHERE NOT EXISTS (SELECT 1 FROM ThietBi WHERE ThietBi.MaThietBi = src.MaThietBi);
GO

-- 6. BẢNG PHIEUSUCO
INSERT INTO PhieuSuCo (MaSuCo, MaMay, MaThietBi, NguoiLap, NgayBaoCao, MoTaLoi, MucDo, TinhTrangXuLy)
SELECT src.MaSuCo, src.MaMay, src.MaThietBi, src.NguoiLap, src.NgayBaoCao, src.MoTaLoi, src.MucDo, src.TinhTrangXuLy
FROM (VALUES 
    ('SC01', 'PC02', 'TB02_CH',  2, '2026-11-01 08:30:00', N'Chuột bị chập chờn, liệt chuột trái không thao tác được', N'Bình thường', N'Chưa xử lý'),
    ('SC02', 'PC02', 'TB02_RAM', 2, '2026-11-01 09:15:00', N'Máy tính bị dump màn hình xanh liên tục mã lỗi Memory Management', N'Nghiêm trọng', N'Đang xử lý'),
    ('SC03', 'PC03', 'TB03_MH',  1, '2026-11-02 14:00:00', N'Màn hình bị sọc ngang màu xanh lá che mất thanh taskbar', N'Bình thường', N'Đang xử lý'),
    ('SC04', 'PC07', 'TB07_MH',  2, '2026-11-03 10:20:00', N'Màn hình không sáng đèn tín hiệu nguồn, nghi hỏng bo nguồn adapter', N'Nghiêm trọng', N'Chưa xử lý'),
    ('SC05', 'PC11', 'TB11_VGA', 3, '2026-11-04 15:45:00', N'Card đồ họa quạt kêu to bất thường, nhiệt độ 95 độ C tự ngắt', N'Nghiêm trọng', N'Đang xử lý')
) AS src(MaSuCo, MaMay, MaThietBi, NguoiLap, NgayBaoCao, MoTaLoi, MucDo, TinhTrangXuLy)
WHERE NOT EXISTS (SELECT 1 FROM PhieuSuCo WHERE PhieuSuCo.MaSuCo = src.MaSuCo);
GO

-- 7. BẢNG LICHBAOTRI_SUACHUA
INSERT INTO LichBaoTri_SuaChua (MaBaoTri, MaSuCo, MaMay, NguoiThucHien, NgayBaoTri, NoiDung, KetQua)
SELECT src.MaBaoTri, src.MaSuCo, src.MaMay, src.NguoiThucHien, src.NgayBaoTri, src.NoiDung, src.KetQua
FROM (VALUES 
    ('BT01', 'SC01', 'PC02', 2, '2026-11-02 10:00:00', N'Đã tiếp nhận chuột lỗi, xuất kho chuột mới TB_KHO_02 chuẩn bị thay thế', N'Đang sửa chữa'),
    ('BT02', 'SC03', 'PC03', 2, '2026-11-03 08:30:00', N'Đã tháo màn hình gửi hãng Samsung bảo hành thay tấm nền', N'Chờ linh kiện'),
    ('BT03', NULL,   'PC01', 3, '2026-10-25 14:00:00', N'Bảo trì định kỳ: Vệ sinh case, tra keo tản nhiệt CPU, cài đặt lại Windows', N'Hoạt động'),
    ('BT04', NULL,   'PC19', 2, '2026-10-30 16:00:00', N'Kiểm tra định kỳ dàn máy AI Render, cập nhật NVIDIA Studio Driver mới nhất', N'Hoạt động')
) AS src(MaBaoTri, MaSuCo, MaMay, NguoiThucHien, NgayBaoTri, NoiDung, KetQua)
WHERE NOT EXISTS (SELECT 1 FROM LichBaoTri_SuaChua WHERE LichBaoTri_SuaChua.MaBaoTri = src.MaBaoTri);
GO

PRINT N'====================================================================';
PRINT N'✅ HOÀN TẤT KIỂM TRA & NẠP DỮ LIỆU [QL_PhongMay] (BẢO LƯU DỮ LIỆU CŨ)!';
PRINT N'====================================================================';
GO
