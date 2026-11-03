using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyPhongMay.Models;

namespace QuanLyPhongMay.Utils
{
    public static class DatabaseHelper
    {
        // Danh sách chuỗi kết nối thử nghiệm tự động
        private static readonly string[] CandidateConnectionStrings = new string[]
        {
            @"Server=.\SQLEXPRESS;Database=QL_PhongMay;Integrated Security=True;TrustServerCertificate=True;",
            @"Server=localhost;Database=QL_PhongMay;Integrated Security=True;TrustServerCertificate=True;",
            @"Server=(local);Database=QL_PhongMay;Integrated Security=True;TrustServerCertificate=True;",
            @"Server=localhost\SQLEXPRESS;Database=QL_PhongMay;Integrated Security=True;TrustServerCertificate=True;"
        };

        public static string CurrentConnectionString { get; set; } = CandidateConnectionStrings[0];
        public static bool IsConnectedToSql { get; private set; } = false;

        public static bool KiemTraKetNoi(out string thongBao)
        {
            // Thử chuỗi hiện tại trước
            if (ThuKetNoi(CurrentConnectionString, out thongBao))
            {
                IsConnectedToSql = true;
                return true;
            }

            // Nếu thất bại, tự động dò các server candidates
            foreach (var connStr in CandidateConnectionStrings)
            {
                if (connStr != CurrentConnectionString && ThuKetNoi(connStr, out string msg))
                {
                    CurrentConnectionString = connStr;
                    IsConnectedToSql = true;
                    thongBao = $"Đã tự động kết nối qua: {connStr}";
                    return true;
                }
            }

            IsConnectedToSql = false;
            thongBao = "Không thể kết nối tới SQL Server localhost. Ứng dụng sẽ hoạt động ở chế độ Demo/Offline.";
            return false;
        }

        private static bool ThuKetNoi(string connStr, out string loi)
        {
            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    loi = "Kết nối thành công!";
                    return true;
                }
            }
            catch (Exception ex)
            {
                loi = ex.Message;
                return false;
            }
        }

        #region Phòng Máy

        public static List<PhongMay> LayDanhSachPhongMay()
        {
            List<PhongMay> list = new List<PhongMay>();
            if (!IsConnectedToSql) return LayPhongMayMau();

            try
            {
                using (var conn = new SqlConnection(CurrentConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT MaPhong, TenPhong, ViTri, TrangThai FROM PhongMay";
                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PhongMay(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.IsDBNull(2) ? "" : reader.GetString(2),
                                reader.IsDBNull(3) ? "Đang hoạt động" : reader.GetString(3)
                            ));
                        }
                    }
                }
            }
            catch (Exception)
            {
                return LayPhongMayMau();
            }

            if (list.Count == 0) list = LayPhongMayMau();
            return list;
        }

        private static List<PhongMay> LayPhongMayMau()
        {
            return new List<PhongMay>
            {
                new PhongMay("PM01", "Phòng Thực Hành C/C++", "Lầu 1 - Tòa A", "Đang hoạt động"),
                new PhongMay("PM02", "Phòng Mạng Máy Tính", "Lầu 2 - Tòa A", "Đang hoạt động")
            };
        }

        #endregion

        #region Máy Tính & Thiết Bị

        public static List<MayTinh> LayDanhSachMayTinh(string maPhong)
        {
            List<MayTinh> listMay = new List<MayTinh>();
            if (!IsConnectedToSql) return LayMayTinhMau(maPhong);

            try
            {
                using (var conn = new SqlConnection(CurrentConnectionString))
                {
                    conn.Open();

                    // 1. Lấy danh sách máy tính
                    string sqlMay = "SELECT MaMay, MaPhong, TenMay, TrangThai, ToaDoX, ToaDoY FROM MayTinh WHERE MaPhong = @maPhong";
                    using (var cmd = new SqlCommand(sqlMay, conn))
                    {
                        cmd.Parameters.AddWithValue("@maPhong", maPhong);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string ma = reader.GetString(0);
                                string phong = reader.GetString(1);
                                string ten = reader.GetString(2);
                                string trangThai = reader.IsDBNull(3) ? "Hoạt động" : reader.GetString(3);
                                int x = reader.IsDBNull(4) ? 20 : reader.GetInt32(4);
                                int y = reader.IsDBNull(5) ? 20 : reader.GetInt32(5);

                                listMay.Add(new MayTinh(ma, ten, trangThai, x, y, phong, "", false));
                            }
                        }
                    }

                    // 2. Lấy toàn bộ thiết bị gắn vào các máy trong phòng này
                    string sqlTB = @"SELECT t.MaThietBi, t.MaLoai, t.MaMay, t.TenThietBi, t.ThongSo, t.HinhAnh, t.TrangThai, l.TenLoai
                                     FROM ThietBi t
                                     LEFT JOIN LoaiThietBi l ON t.MaLoai = l.MaLoai
                                     WHERE t.MaMay IN (SELECT MaMay FROM MayTinh WHERE MaPhong = @maPhong)";
                    using (var cmdTB = new SqlCommand(sqlTB, conn))
                    {
                        cmdTB.Parameters.AddWithValue("@maPhong", maPhong);
                        using (var readerTB = cmdTB.ExecuteReader())
                        {
                            while (readerTB.Read())
                            {
                                string maTB = readerTB.GetString(0);
                                string maLoai = readerTB.GetString(1);
                                string maMay = readerTB.IsDBNull(2) ? "" : readerTB.GetString(2);
                                string tenTB = readerTB.GetString(3);
                                string thongSo = readerTB.IsDBNull(4) ? "" : readerTB.GetString(4);
                                string? hinhAnh = readerTB.IsDBNull(5) ? null : readerTB.GetString(5);
                                string trangThaiTB = readerTB.IsDBNull(6) ? "Hoạt động" : readerTB.GetString(6);
                                string tenLoai = readerTB.IsDBNull(7) ? "" : readerTB.GetString(7);

                                ThietBi tb = new ThietBi(maTB, maLoai, maMay, tenTB, thongSo, hinhAnh, trangThaiTB, tenLoai);
                                var may = listMay.Find(m => m.MaMay == maMay);
                                if (may != null)
                                {
                                    may.DanhSachThietBi.Add(tb);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return LayMayTinhMau(maPhong);
            }

            if (listMay.Count == 0) listMay = LayMayTinhMau(maPhong);
            return listMay;
        }

        public static bool CapNhatToaDo(string maMay, int x, int y)
        {
            if (!IsConnectedToSql) return true;

            try
            {
                using (var conn = new SqlConnection(CurrentConnectionString))
                {
                    conn.Open();
                    string sql = "UPDATE MayTinh SET ToaDoX = @x, ToaDoY = @y WHERE MaMay = @ma";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@x", x);
                        cmd.Parameters.AddWithValue("@y", y);
                        cmd.Parameters.AddWithValue("@ma", maMay);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool CapNhatThongTinMay(MayTinh may)
        {
            if (!IsConnectedToSql) return true;

            try
            {
                using (var conn = new SqlConnection(CurrentConnectionString))
                {
                    conn.Open();
                    string sql = "UPDATE MayTinh SET TenMay = @ten, TrangThai = @tt, ToaDoX = @x, ToaDoY = @y WHERE MaMay = @ma";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ten", may.TenMay);
                        cmd.Parameters.AddWithValue("@tt", may.TrangThai);
                        cmd.Parameters.AddWithValue("@x", may.ViTriX);
                        cmd.Parameters.AddWithValue("@y", may.ViTriY);
                        cmd.Parameters.AddWithValue("@ma", may.MaMay);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool ThemMayTinh(MayTinh may)
        {
            if (!IsConnectedToSql) return true;

            try
            {
                using (var conn = new SqlConnection(CurrentConnectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO MayTinh (MaMay, MaPhong, TenMay, TrangThai, ToaDoX, ToaDoY) VALUES (@ma, @phong, @ten, @tt, @x, @y)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", may.MaMay);
                        cmd.Parameters.AddWithValue("@phong", may.MaPhong);
                        cmd.Parameters.AddWithValue("@ten", may.TenMay);
                        cmd.Parameters.AddWithValue("@tt", may.TrangThai);
                        cmd.Parameters.AddWithValue("@x", may.ViTriX);
                        cmd.Parameters.AddWithValue("@y", may.ViTriY);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool XoaMayTinh(string maMay)
        {
            if (!IsConnectedToSql) return true;

            try
            {
                using (var conn = new SqlConnection(CurrentConnectionString))
                {
                    conn.Open();
                    // Giải phóng thiết bị trước khi xóa máy
                    string sqlGiaiPhong = "UPDATE ThietBi SET MaMay = NULL WHERE MaMay = @ma";
                    using (var cmdGP = new SqlCommand(sqlGiaiPhong, conn))
                    {
                        cmdGP.Parameters.AddWithValue("@ma", maMay);
                        cmdGP.ExecuteNonQuery();
                    }

                    string sql = "DELETE FROM MayTinh WHERE MaMay = @ma";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", maMay);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool CapNhatTrangThaiThietBi(string maTB, string trangThaiMoi)
        {
            if (!IsConnectedToSql) return true;

            try
            {
                using (var conn = new SqlConnection(CurrentConnectionString))
                {
                    conn.Open();
                    string sql = "UPDATE ThietBi SET TrangThai = @tt WHERE MaThietBi = @ma";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tt", trangThaiMoi);
                        cmd.Parameters.AddWithValue("@ma", maTB);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool GanThietBiVaoMay(string maTB, string? maMay)
        {
            if (!IsConnectedToSql) return true;

            try
            {
                using (var conn = new SqlConnection(CurrentConnectionString))
                {
                    conn.Open();
                    string sql = "UPDATE ThietBi SET MaMay = @may WHERE MaThietBi = @tb";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@may", (object?)maMay ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@tb", maTB);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool TaoPhieuSuCo(PhieuSuCo phieu)
        {
            if (!IsConnectedToSql) return true;

            try
            {
                using (var conn = new SqlConnection(CurrentConnectionString))
                {
                    conn.Open();
                    string sql = @"INSERT INTO PhieuSuCo (MaSuCo, MaMay, MaThietBi, NguoiLap, NgayBaoCao, MoTaLoi, MucDo, TinhTrangXuLy) 
                                   VALUES (@maSC, @maMay, @maTB, @nguoiLap, @ngay, @moTa, @mucDo, @tinhTrang)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maSC", phieu.MaSuCo);
                        cmd.Parameters.AddWithValue("@maMay", phieu.MaMay);
                        cmd.Parameters.AddWithValue("@maTB", (object?)phieu.MaThietBi ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@nguoiLap", phieu.NguoiLap);
                        cmd.Parameters.AddWithValue("@ngay", phieu.NgayBaoCao);
                        cmd.Parameters.AddWithValue("@moTa", phieu.MoTaLoi);
                        cmd.Parameters.AddWithValue("@mucDo", phieu.MucDo);
                        cmd.Parameters.AddWithValue("@tinhTrang", phieu.TinhTrangXuLy);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static List<ThietBi> LayDanhSachThietBiKho()
        {
            List<ThietBi> list = new List<ThietBi>();
            if (!IsConnectedToSql) return list;

            try
            {
                using (var conn = new SqlConnection(CurrentConnectionString))
                {
                    conn.Open();
                    string sql = @"SELECT t.MaThietBi, t.MaLoai, t.MaMay, t.TenThietBi, t.ThongSo, t.HinhAnh, t.TrangThai, l.TenLoai
                                   FROM ThietBi t
                                   LEFT JOIN LoaiThietBi l ON t.MaLoai = l.MaLoai
                                   WHERE t.MaMay IS NULL";
                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ThietBi(
                                reader.GetString(0),
                                reader.GetString(1),
                                null,
                                reader.GetString(3),
                                reader.IsDBNull(4) ? "" : reader.GetString(4),
                                reader.IsDBNull(5) ? null : reader.GetString(5),
                                reader.IsDBNull(6) ? "Hoạt động" : reader.GetString(6),
                                reader.IsDBNull(7) ? "" : reader.GetString(7)
                            ));
                        }
                    }
                }
            }
            catch { }
            return list;
        }

        private static List<MayTinh> LayMayTinhMau(string maPhong)
        {
            List<MayTinh> list = new List<MayTinh>();
            int chieuRongO = 150;
            int chieuCaoO = 160;
            int khoangCach = 20;
            int soCot = 5;

            for (int i = 1; i <= 10; i++)
            {
                string ma = $"PC{i:D2}";
                string ten = $"Máy {i:D2}";
                string trangThai = "Hoạt động";
                if (i == 2 || i == 6) trangThai = "Hư hỏng";
                if (i == 5) trangThai = "Bảo trì";

                int hang = (i - 1) / soCot;
                int cot = (i - 1) % soCot;
                int x = khoangCach + cot * (chieuRongO + khoangCach);
                int y = khoangCach + hang * (chieuCaoO + khoangCach);

                MayTinh may = new MayTinh(ma, ten, trangThai, x, y, maPhong, "", false);

                // Thêm linh kiện mẫu
                may.DanhSachThietBi.Add(new ThietBi($"TB{i}1", "L01", ma, "Màn hình Dell 24 inch", "1920x1080 75Hz", null, "Hoạt động", "Màn hình"));
                may.DanhSachThietBi.Add(new ThietBi($"TB{i}2", "L02", ma, "Bàn phím cơ DareU", "Blue Switch RGB", null, "Hoạt động", "Bàn phím"));
                may.DanhSachThietBi.Add(new ThietBi($"TB{i}3", "L03", ma, "Chuột Logitech G102", "8000 DPI", null, (i == 2 ? "Hư hỏng" : "Hoạt động"), "Chuột"));
                may.DanhSachThietBi.Add(new ThietBi($"TB{i}4", "L04", ma, "RAM Kingston Fury 16GB", "DDR4 3200MHz", null, (i == 6 ? "Hư hỏng" : "Hoạt động"), "RAM"));

                list.Add(may);
            }

            return list;
        }

        #endregion
    }
}
