using System.Collections.Generic;
using QuanLyPhongMay.Models;

namespace QuanLyPhongMay.Views
{
    /// <summary>
    /// Giao diện View (trong mô hình MVP) cho màn hình Sơ đồ Vị trí & Cấu hình Thiết bị Phòng máy.
    /// </summary>
    public interface ISoDoPhongMayView
    {
        void HienThiTrangThaiKetNoi(bool isConnected, string thongBao);
        void CapNhatDanhSachPhong(List<PhongMay> danhSachPhong);
        void CapNhatSoDoMayTinh(List<MayTinh> danhSachMay);
        void HienThiThongTinMayChon(MayTinh? may);
        void HienThiThongBaoTrangThai(string thongDiep);
    }
}
