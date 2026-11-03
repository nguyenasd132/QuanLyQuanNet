using System;
using System.Collections.Generic;

namespace QuanLyPhongMay.Models
{
    public class MayTinh
    {
        public string MaMay { get; set; } = string.Empty;
        public string MaPhong { get; set; } = "PM01";
        public string TenMay { get; set; } = string.Empty;
        public string TrangThai { get; set; } = "Hoạt động"; // 'Hoạt động', 'Hư hỏng', 'Bảo trì'
        public int ViTriX { get; set; } = 20; // ToaDoX
        public int ViTriY { get; set; } = 20; // ToaDoY
        public string GhiChu { get; set; } = string.Empty;
        public bool DaKhoa { get; set; } = false;

        // Danh sách linh kiện / thiết bị gắn vào máy này
        public List<ThietBi> DanhSachThietBi { get; set; } = new List<ThietBi>();

        public MayTinh() { }

        public MayTinh(string maMay, string tenMay, string trangThai, int x = 20, int y = 20, string maPhong = "PM01", string ghiChu = "", bool daKhoa = false)
        {
            MaMay = maMay;
            TenMay = tenMay;
            TrangThai = trangThai;
            ViTriX = x;
            ViTriY = y;
            MaPhong = maPhong;
            GhiChu = ghiChu;
            DaKhoa = daKhoa;
        }

        // Kiểm tra xem máy có linh kiện nào bị hỏng không
        public bool CoThietBiHuHong()
        {
            return DanhSachThietBi.Exists(tb => tb.TrangThai.Equals("Hư hỏng", StringComparison.OrdinalIgnoreCase));
        }

        public int SoThietBiHuHong()
        {
            return DanhSachThietBi.FindAll(tb => tb.TrangThai.Equals("Hư hỏng", StringComparison.OrdinalIgnoreCase)).Count;
        }
    }
}
