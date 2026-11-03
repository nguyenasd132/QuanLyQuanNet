using System;

namespace QuanLyPhongMay.Models
{
    public class PhieuSuCo
    {
        public string MaSuCo { get; set; } = string.Empty;
        public string MaMay { get; set; } = string.Empty;
        public string? MaThietBi { get; set; } = null;
        public int NguoiLap { get; set; } = 1;
        public DateTime NgayBaoCao { get; set; } = DateTime.Now;
        public string MoTaLoi { get; set; } = string.Empty;
        public string MucDo { get; set; } = "Bình thường";
        public string TinhTrangXuLy { get; set; } = "Chưa xử lý";

        public PhieuSuCo() { }

        public PhieuSuCo(string maSuCo, string maMay, string? maThietBi, int nguoiLap, string moTaLoi, string mucDo = "Bình thường", string tinhTrangXuLy = "Chưa xử lý")
        {
            MaSuCo = maSuCo;
            MaMay = maMay;
            MaThietBi = maThietBi;
            NguoiLap = nguoiLap;
            NgayBaoCao = DateTime.Now;
            MoTaLoi = moTaLoi;
            MucDo = mucDo;
            TinhTrangXuLy = tinhTrangXuLy;
        }
    }
}
