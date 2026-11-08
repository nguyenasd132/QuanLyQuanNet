using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyPhongMay.Models;
using QuanLyPhongMay.Utils;
using QuanLyPhongMay.Views;

namespace QuanLyPhongMay.Presenters
{
    /// <summary>
    /// Tầng Presenter (trong mô hình MVP - Model-View-Presenter) điều phối nghiệp vụ
    /// giữa giao diện Sơ đồ phòng máy (View) và tầng Dữ liệu / Models.
    /// </summary>
    public class SoDoPhongMayPresenter
    {
        private readonly ISoDoPhongMayView _view;

        public SoDoPhongMayPresenter(ISoDoPhongMayView view)
        {
            _view = view;
        }

        public bool KiemTraKetNoiCSDL(out string thongBao)
        {
            bool ok = DatabaseHelper.KiemTraKetNoi(out thongBao);
            _view.HienThiTrangThaiKetNoi(ok, thongBao);
            return ok;
        }

        public List<PhongMay> TaiDanhSachPhongMay()
        {
            var dsPhong = DatabaseHelper.LayDanhSachPhongMay();
            _view.CapNhatDanhSachPhong(dsPhong);
            return dsPhong;
        }

        public List<MayTinh> TaiDanhSachMayTheoPhong(string maPhong)
        {
            var dsMay = DatabaseHelper.LayDanhSachMayTinh(maPhong);
            _view.HienThiThongBaoTrangThai($"Đã tải {dsMay.Count} máy từ phòng {maPhong}.");
            return dsMay;
        }

        public bool LuuToaDoMay(MayTinh may, int x, int y)
        {
            may.ViTriX = x;
            may.ViTriY = y;
            bool ok = DatabaseHelper.CapNhatToaDo(may.MaMay, x, y);
            _view.HienThiThongBaoTrangThai($"Đã lưu tọa độ {may.TenMay} tới ({x}, {y}) vào CSDL.");
            return ok;
        }

        public bool ThemMayTinhMoi(MayTinh mayMoi, IEnumerable<MayTinh> danhSachHienTai, out string thongBaoLoi)
        {
            if (string.IsNullOrWhiteSpace(mayMoi.MaMay) || string.IsNullOrWhiteSpace(mayMoi.TenMay))
            {
                thongBaoLoi = "Mã máy và Tên máy không được để trống!";
                return false;
            }

            if (danhSachHienTai.Any(m => m.MaMay.Equals(mayMoi.MaMay, StringComparison.OrdinalIgnoreCase)) ||
                DatabaseHelper.KiemTraMaMayTonTai(mayMoi.MaMay))
            {
                thongBaoLoi = $"Mã máy '{mayMoi.MaMay}' đã tồn tại trong hệ thống CSDL!";
                return false;
            }

            if (!DatabaseHelper.ThemMayTinh(mayMoi))
            {
                thongBaoLoi = $"Không thể thêm máy '{mayMoi.MaMay}' vào CSDL SQL Server.";
                return false;
            }

            thongBaoLoi = string.Empty;
            _view.HienThiThongBaoTrangThai($"Đã thêm máy {mayMoi.TenMay} ({mayMoi.MaMay}) vào CSDL.");
            return true;
        }

        public bool CapNhatMayTinh(MayTinh may)
        {
            bool ok = DatabaseHelper.CapNhatThongTinMay(may);
            _view.HienThiThongBaoTrangThai($"Đã cập nhật thông tin máy {may.TenMay} ({may.MaMay}).");
            return ok;
        }

        public bool XoaMayTinh(string maMay)
        {
            bool ok = DatabaseHelper.XoaMayTinh(maMay);
            _view.HienThiThongBaoTrangThai($"Đã xóa máy {maMay} khỏi phòng máy.");
            return ok;
        }

        public bool CapNhatBanDoNenPhong(PhongMay phong, string? duongDanTuongDoi)
        {
            phong.HinhNen = duongDanTuongDoi;
            return DatabaseHelper.CapNhatHinhNenPhong(phong.MaPhong, duongDanTuongDoi);
        }
    }
}
