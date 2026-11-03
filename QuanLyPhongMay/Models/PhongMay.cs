using System;

namespace QuanLyPhongMay.Models
{
    public class PhongMay
    {
        public string MaPhong { get; set; } = string.Empty;
        public string TenPhong { get; set; } = string.Empty;
        public string ViTri { get; set; } = string.Empty;
        public string TrangThai { get; set; } = "Đang hoạt động";

        public PhongMay() { }

        public PhongMay(string maPhong, string tenPhong, string viTri, string trangThai)
        {
            MaPhong = maPhong;
            TenPhong = tenPhong;
            ViTri = viTri;
            TrangThai = trangThai;
        }

        public override string ToString()
        {
            return $"{MaPhong} - {TenPhong}";
        }
    }
}
