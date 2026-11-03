using System;

namespace QuanLyPhongMay.Models
{
    public class LoaiThietBi
    {
        public string MaLoai { get; set; } = string.Empty;
        public string TenLoai { get; set; } = string.Empty;

        public LoaiThietBi() { }

        public LoaiThietBi(string maLoai, string tenLoai)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
        }

        public override string ToString()
        {
            return TenLoai;
        }
    }
}
