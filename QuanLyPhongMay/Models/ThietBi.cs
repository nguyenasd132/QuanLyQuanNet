using System;

namespace QuanLyPhongMay.Models
{
    public class ThietBi
    {
        public string MaThietBi { get; set; } = string.Empty;
        public string MaLoai { get; set; } = string.Empty;
        public string? MaMay { get; set; } = null;
        public string TenThietBi { get; set; } = string.Empty;
        public string ThongSo { get; set; } = string.Empty;
        public string? HinhAnh { get; set; } = null;
        public string TrangThai { get; set; } = "Hoạt động"; // "Hoạt động", "Hư hỏng"

        // Thuộc tính phụ trợ hiển thị (từ JOIN LoaiThietBi)
        public string TenLoai { get; set; } = string.Empty;

        public ThietBi() { }

        public ThietBi(string maTB, string maLoai, string? maMay, string tenTB, string thongSo, string? hinhAnh, string trangThai, string tenLoai = "")
        {
            MaThietBi = maTB;
            MaLoai = maLoai;
            MaMay = maMay;
            TenThietBi = tenTB;
            ThongSo = thongSo;
            HinhAnh = hinhAnh;
            TrangThai = trangThai;
            TenLoai = tenLoai;
        }

        // Biểu tượng icon theo loại thiết bị
        public string GetIcon()
        {
            string loai = (TenLoai + " " + TenThietBi).ToLower();
            if (loai.Contains("màn hình") || loai.Contains("monitor") || loai.Contains("dell") || loai.Contains("samsung")) return "🖥️";
            if (loai.Contains("chuột") || loai.Contains("mouse") || loai.Contains("logitech")) return "🖱️";
            if (loai.Contains("bàn phím") || loai.Contains("phím") || loai.Contains("keyboard")) return "⌨️";
            if (loai.Contains("ram") || loai.Contains("memory")) return "💾";
            if (loai.Contains("cpu") || loai.Contains("chip") || loai.Contains("intel") || loai.Contains("amd")) return "🔲";
            if (loai.Contains("vga") || loai.Contains("card") || loai.Contains("gtx") || loai.Contains("rtx")) return "🎮";
            if (loai.Contains("nguồn") || loai.Contains("psu")) return "⚡";
            if (loai.Contains("tai nghe") || loai.Contains("headphone")) return "🎧";
            return "🔌";
        }
    }
}
