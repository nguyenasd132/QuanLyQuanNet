using System;

namespace QuanLyPhongMay.Models
{
    public class MayTinh
    {
        public string MaMay { get; set; }
        public string TenMay { get; set; }
        public string TrangThai { get; set; } // Sẽ có 3 trạng thái: Hoạt động, Hư hỏng, Bảo trì
    }
}