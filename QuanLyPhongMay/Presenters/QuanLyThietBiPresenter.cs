using System;
using System.Collections.Generic;
using QuanLyPhongMay.Models;
using QuanLyPhongMay.Utils;

namespace QuanLyPhongMay.Presenters
{
    /// <summary>
    /// Tầng Presenter (trong mô hình MVP) phụ trách nghiệp vụ quản lý linh kiện thiết bị
    /// và lập phiếu báo cáo sự cố tự động cho từng máy trạm.
    /// </summary>
    public class QuanLyThietBiPresenter
    {
        public List<ThietBi> LayThietBiTrongKho()
        {
            return DatabaseHelper.LayDanhSachThietBiKho();
        }

        public bool GanThietBiVaoMay(ThietBi thietBi, MayTinh mayTinh)
        {
            bool ok = DatabaseHelper.GanThietBiVaoMay(thietBi.MaThietBi, mayTinh.MaMay);
            if (ok)
            {
                thietBi.MaMay = mayTinh.MaMay;
                if (!mayTinh.DanhSachThietBi.Contains(thietBi))
                {
                    mayTinh.DanhSachThietBi.Add(thietBi);
                }
            }
            return ok;
        }

        public bool GoThietBiKhoiMay(ThietBi thietBi, MayTinh mayTinh)
        {
            bool ok = DatabaseHelper.GanThietBiVaoMay(thietBi.MaThietBi, null);
            if (ok)
            {
                thietBi.MaMay = null;
                mayTinh.DanhSachThietBi.Remove(thietBi);
            }
            return ok;
        }

        public string ChuyenDoiTrangThaiThietBi(ThietBi thietBi, MayTinh mayTinh)
        {
            string trangThaiMoi = thietBi.TrangThai.Equals("Hoạt động", StringComparison.OrdinalIgnoreCase)
                ? "Hư hỏng"
                : "Hoạt động";

            thietBi.TrangThai = trangThaiMoi;
            DatabaseHelper.CapNhatTrangThaiThietBi(thietBi.MaThietBi, trangThaiMoi);

            if (trangThaiMoi == "Hư hỏng")
            {
                string maSC = "SC" + DateTime.Now.ToString("ddHHmmss");
                PhieuSuCo phieu = new PhieuSuCo(
                    maSC,
                    mayTinh.MaMay,
                    thietBi.MaThietBi,
                    1,
                    $"Sự cố thiết bị {thietBi.TenThietBi} hỏng tại {mayTinh.TenMay}");
                DatabaseHelper.TaoPhieuSuCo(phieu);

                mayTinh.TrangThai = "Hư hỏng";
                DatabaseHelper.CapNhatThongTinMay(mayTinh);
            }

            return trangThaiMoi;
        }
    }
}
