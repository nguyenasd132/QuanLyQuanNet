using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using QuanLyPhongMay.Models;
using QuanLyPhongMay.Utils;

namespace QuanLyPhongMay.Views
{
    public partial class FormSoDo : Form
    {
        // Dữ liệu
        private List<PhongMay> danhSachPhong = new List<PhongMay>();
        private PhongMay? phongHienTai = null;
        private List<MayTinh> danhSachMay = new List<MayTinh>();
        private MayTinh? mayDangChon = null;

        // Trạng thái khóa vị trí toàn cục
        private bool isGlobalLocked = false;

        public FormSoDo()
        {
            InitializeComponent();
            KhoiTaoKetNoiCSDL();
            TaiDanhSachPhongMay();
        }

        #region Khởi tạo & CSDL

        private void KhoiTaoKetNoiCSDL()
        {
            bool ok = DatabaseHelper.KiemTraKetNoi(out string thongBao);
            CapNhatTrangThaiKetNoi(ok, thongBao);
        }

        private void CapNhatTrangThaiKetNoi(bool ok, string thongBao)
        {
            if (ok)
            {
                lblTrangThaiCSDL.Text = "🟢 SQL Server: Đã kết nối (QL_PhongMay)";
                lblTrangThaiCSDL.ForeColor = Color.FromArgb(5, 150, 105);
                btnKetNoiCSDL.Text = "🟢 CSDL: Online";
                btnKetNoiCSDL.BackColor = Color.FromArgb(236, 253, 245);
                btnKetNoiCSDL.ForeColor = Color.FromArgb(6, 95, 70);
            }
            else
            {
                lblTrangThaiCSDL.Text = "🟡 CSDL: Chế độ Offline / Demo";
                lblTrangThaiCSDL.ForeColor = Color.FromArgb(217, 119, 6);
                btnKetNoiCSDL.Text = "🟡 CSDL: Offline";
                btnKetNoiCSDL.BackColor = Color.FromArgb(254, 243, 199);
                btnKetNoiCSDL.ForeColor = Color.FromArgb(146, 64, 14);
            }
            lblTrangThaiDuLieu.Text = thongBao;
        }

        private void TaiDanhSachPhongMay()
        {
            danhSachPhong = DatabaseHelper.LayDanhSachPhongMay();
            cboPhongMay.Items.Clear();

            foreach (var p in danhSachPhong)
            {
                cboPhongMay.Items.Add(p);
            }

            if (cboPhongMay.Items.Count > 0)
            {
                cboPhongMay.SelectedIndex = 0;
            }
        }

        private void CboPhongMay_SelectedIndexChanged(object? sender, EventArgs e)
        {
            phongHienTai = cboPhongMay.SelectedItem as PhongMay;
            if (phongHienTai != null)
            {
                TaiDuLieuMayTinh();
                VeSoDo();
            }
        }

        #endregion

        #region Xử lý Dữ liệu & Vẽ Sơ đồ

        private void TaiDuLieuMayTinh()
        {
            string maPhong = phongHienTai?.MaPhong ?? "PM01";
            danhSachMay = DatabaseHelper.LayDanhSachMayTinh(maPhong);
            lblTrangThaiDuLieu.Text = $"Đã tải {danhSachMay.Count} máy từ phòng {maPhong}.";
        }

        private void VeSoDo()
        {
            panelSoDo.SuspendLayout();
            panelSoDo.Controls.Clear();

            foreach (var may in danhSachMay)
            {
                MayTinhControl ctrl = new MayTinhControl(may)
                {
                    AllowDrag = true,
                    IsGlobalLocked = this.isGlobalLocked,
                    SnapToGrid = chkHutLuoi.Checked
                };

                ctrl.MaySelected += (s, m) => ChonMay(m);
                ctrl.PositionChanged += (s, m) =>
                {
                    if (mayDangChon == m)
                    {
                        numChiTietX.Value = m.ViTriX;
                        numChiTietY.Value = m.ViTriY;
                        lblToaDoConTro.Text = $"Tọa độ: ({m.ViTriX}, {m.ViTriY})";
                    }
                };
                ctrl.DragFinished += (s, m) =>
                {
                    DatabaseHelper.CapNhatToaDo(m.MaMay, m.ViTriX, m.ViTriY);
                    lblTrangThaiDuLieu.Text = $"Đã lưu tọa độ {m.TenMay} tới ({m.ViTriX}, {m.ViTriY}) vào CSDL.";
                };
                ctrl.AttemptDragWhileLocked += (s, m) =>
                {
                    if (isGlobalLocked)
                    {
                        lblTrangThaiDuLieu.Text = "⚠️ Toàn bộ sơ đồ đang bị khóa vị trí. Bấm 'Mở khóa' trên thanh công cụ để di chuyển.";
                    }
                    else if (m.DaKhoa)
                    {
                        lblTrangThaiDuLieu.Text = $"⚠️ '{m.TenMay}' đang bị khóa vị trí riêng. Bấm chuột phải chọn 'Mở khóa' để di chuyển.";
                    }
                };
                ctrl.RequestToggleLock += (s, m) =>
                {
                    if (mayDangChon == m)
                    {
                        chkChiTietKhoa.Checked = m.DaKhoa;
                    }
                    lblTrangThaiDuLieu.Text = m.DaKhoa ? $"Đã khóa vị trí cho {m.TenMay}" : $"Đã mở khóa vị trí cho {m.TenMay}";
                };
                ctrl.RequestEdit += (s, m) => MoDialogSuaMay(m);
                ctrl.RequestManageComponents += (s, m) => MoDialogQuanLyThietBi(m);
                ctrl.RequestDelete += (s, m) => XacNhanVaXoaMay(m);
                ctrl.RequestChangeStatus += (s, args) => DoiTrangThaiNhanh(args.May, args.NewStatus);
                ctrl.RequestPrint += (s, m) => InBienBanSuCo(m);

                panelSoDo.Controls.Add(ctrl);
            }

            panelSoDo.ResumeLayout();
            CapNhatThongKe();

            if (mayDangChon != null)
            {
                var tonTai = danhSachMay.FirstOrDefault(m => m.MaMay == mayDangChon.MaMay);
                ChonMay(tonTai);
            }
            else
            {
                ChonMay(null);
            }
        }

        private void CapNhatThongKe()
        {
            int tong = danhSachMay.Count;
            int hoatDong = danhSachMay.Count(m => m.TrangThai == "Hoạt động");
            int huHong = danhSachMay.Count(m => m.TrangThai == "Hư hỏng");
            int baoTri = danhSachMay.Count(m => m.TrangThai == "Bảo trì");

            lblTongMay.Text = $"Tổng máy: {tong}";
            lblHoatDong.Text = $"🟢 Hoạt động: {hoatDong}";
            lblHuHong.Text = $"🔴 Hư hỏng: {huHong}";
            lblBaoTri.Text = $"🟡 Bảo trì: {baoTri}";
        }

        private void ChonMay(MayTinh? may)
        {
            mayDangChon = may;

            foreach (Control ctrl in panelSoDo.Controls)
            {
                if (ctrl is MayTinhControl mc)
                {
                    mc.IsSelected = (may != null && mc.Data.MaMay == may.MaMay);
                }
            }

            if (may != null)
            {
                string textKhoa = (isGlobalLocked || may.DaKhoa) ? " [🔒 Đã khóa vị trí]" : "";
                bool coLoiTB = may.CoThietBiHuHong();
                string alertTB = coLoiTB ? " ⚠️ (Có linh kiện hỏng)" : "";

                lblChiTietBanner.Text = $"💻 {may.TenMay} [{may.MaMay}]{textKhoa}\nTrạng thái: {may.TrangThai}{alertTB}";

                if (may.TrangThai == "Hoạt động")
                {
                    if (coLoiTB)
                    {
                        lblChiTietBanner.BackColor = Color.FromArgb(254, 226, 226);
                        lblChiTietBanner.ForeColor = Color.FromArgb(185, 28, 28);
                    }
                    else
                    {
                        lblChiTietBanner.BackColor = Color.FromArgb(209, 250, 229);
                        lblChiTietBanner.ForeColor = Color.FromArgb(6, 95, 70);
                    }
                }
                else if (may.TrangThai == "Hư hỏng")
                {
                    lblChiTietBanner.BackColor = Color.FromArgb(254, 202, 202);
                    lblChiTietBanner.ForeColor = Color.FromArgb(153, 27, 27);
                }
                else
                {
                    lblChiTietBanner.BackColor = Color.FromArgb(253, 230, 138);
                    lblChiTietBanner.ForeColor = Color.FromArgb(146, 64, 14);
                }

                txtChiTietMa.Text = may.MaMay;
                txtChiTietTen.Text = may.TenMay;
                cboChiTietTrangThai.SelectedItem = may.TrangThai;
                numChiTietX.Value = Math.Max(0, may.ViTriX);
                numChiTietY.Value = Math.Max(0, may.ViTriY);
                chkChiTietKhoa.Checked = may.DaKhoa;

                btnChiTietCapNhat.Enabled = true;
                btnChiTietXoa.Enabled = true;
                btnChiTietInPhieu.Enabled = true;
                btnSidebarGanLinhKien.Enabled = true;
                btnSidebarDoiStatusTB.Enabled = true;
                btnSidebarQuanLyTB.Enabled = true;

                // Load danh sách linh kiện vào DataGridView trên Sidebar
                dgvLinhKienMay.Rows.Clear();
                foreach (var tb in may.DanhSachThietBi)
                {
                    dgvLinhKienMay.Rows.Add(tb.MaThietBi, $"{tb.GetIcon()} {tb.TenThietBi}", tb.TrangThai);
                }

                lblThongTinMayChon.Text = $"Đang chọn: {may.TenMay} ({may.MaMay}) - {may.TrangThai} | Số linh kiện: {may.DanhSachThietBi.Count}";
                lblToaDoConTro.Text = $"Tọa độ: ({may.ViTriX}, {may.ViTriY})";
            }
            else
            {
                lblChiTietBanner.Text = "Chưa chọn máy nào\n(Nhấp chuột vào máy trên sơ đồ)";
                lblChiTietBanner.BackColor = Color.FromArgb(241, 245, 249);
                lblChiTietBanner.ForeColor = Color.FromArgb(100, 116, 139);

                txtChiTietMa.Text = "";
                txtChiTietTen.Text = "";
                cboChiTietTrangThai.SelectedIndex = -1;
                numChiTietX.Value = 0;
                numChiTietY.Value = 0;
                chkChiTietKhoa.Checked = false;

                dgvLinhKienMay.Rows.Clear();

                btnChiTietCapNhat.Enabled = false;
                btnChiTietXoa.Enabled = false;
                btnChiTietInPhieu.Enabled = false;
                btnSidebarGanLinhKien.Enabled = false;
                btnSidebarDoiStatusTB.Enabled = false;
                btnSidebarQuanLyTB.Enabled = false;

                lblThongTinMayChon.Text = "Chưa chọn máy nào";
                lblToaDoConTro.Text = "Tọa độ: -";
            }
        }

        private void PanelSoDo_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ChonMay(null);
            }
        }

        private void DgvLinhKienMay_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLinhKienMay.Columns[e.ColumnIndex].Name == "colTrangThaiTB" && e.Value != null)
            {
                string status = e.Value.ToString() ?? "";
                if (status.Equals("Hư hỏng", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle!.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    e.CellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle!.ForeColor = Color.FromArgb(5, 150, 105);
                    e.CellStyle.BackColor = Color.FromArgb(236, 253, 245);
                    e.CellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                }
            }
        }

        #endregion

        #region Thao tác Khóa / Mở Khóa

        private void ToggleKhoaViTri()
        {
            isGlobalLocked = !isGlobalLocked;
            CapNhatTrangThaiKhoa();
        }

        private void CapNhatTrangThaiKhoa()
        {
            if (isGlobalLocked)
            {
                btnKhoaViTri.Text = "🔒 Đã khóa vị trí";
                btnKhoaViTri.BackColor = Color.FromArgb(239, 68, 68);
                btnKhoaViTri.ForeColor = Color.White;
                btnKhoaViTri.FlatAppearance.BorderSize = 0;
                lblTrangThaiDuLieu.Text = "🔒 Đã KHÓA vị trí toàn bộ sơ đồ (không thể kéo thả).";
            }
            else
            {
                btnKhoaViTri.Text = "🔓 Khóa vị trí";
                btnKhoaViTri.BackColor = Color.FromArgb(241, 245, 249);
                btnKhoaViTri.ForeColor = Color.FromArgb(30, 41, 59);
                btnKhoaViTri.FlatAppearance.BorderSize = 1;
                lblTrangThaiDuLieu.Text = "🔓 Đã MỞ KHÓA vị trí (có thể kéo thả tự do).";
            }

            foreach (Control ctrl in panelSoDo.Controls)
            {
                if (ctrl is MayTinhControl mc)
                {
                    mc.IsGlobalLocked = isGlobalLocked;
                    mc.Invalidate();
                }
            }
        }

        #endregion

        #region Thao tác Thêm / Xóa / Sửa / Linh Kiện / In ấn

        private void MoDialogThemMay(int defaultX = 20, int defaultY = 20)
        {
            string maPhong = phongHienTai?.MaPhong ?? "PM01";
            int soThuTu = danhSachMay.Count + 1;
            string maGoiY = $"PC{soThuTu:D2}";
            while (danhSachMay.Any(m => m.MaMay.Equals(maGoiY, StringComparison.OrdinalIgnoreCase)))
            {
                soThuTu++;
                maGoiY = $"PC{soThuTu:D2}";
            }

            MayTinh mayMoiGoiY = new MayTinh(maGoiY, $"Máy {soThuTu:D2}", "Hoạt động", defaultX, defaultY, maPhong, "", false);

            using (FormThemSuaMay formThem = new FormThemSuaMay(mayMoiGoiY, defaultX, defaultY))
            {
                if (formThem.ShowDialog(this) == DialogResult.OK)
                {
                    MayTinh mayMoi = formThem.MayTinhData;
                    mayMoi.MaPhong = maPhong;

                    if (danhSachMay.Any(m => m.MaMay.Equals(mayMoi.MaMay, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show($"Mã máy '{mayMoi.MaMay}' đã tồn tại!", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DatabaseHelper.ThemMayTinh(mayMoi);
                    danhSachMay.Add(mayMoi);
                    VeSoDo();
                    ChonMay(mayMoi);
                    MessageBox.Show($"Đã thêm máy '{mayMoi.TenMay}' vào {maPhong}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void MoDialogSuaMay(MayTinh may)
        {
            using (FormThemSuaMay formSua = new FormThemSuaMay(may))
            {
                if (formSua.ShowDialog(this) == DialogResult.OK)
                {
                    MayTinh duLieuMoi = formSua.MayTinhData;
                    may.TenMay = duLieuMoi.TenMay;
                    may.TrangThai = duLieuMoi.TrangThai;
                    may.ViTriX = duLieuMoi.ViTriX;
                    may.ViTriY = duLieuMoi.ViTriY;
                    may.DaKhoa = duLieuMoi.DaKhoa;

                    DatabaseHelper.CapNhatThongTinMay(may);
                    VeSoDo();
                    ChonMay(may);
                }
            }
        }

        private void MoDialogQuanLyThietBi(MayTinh may)
        {
            using (FormQuanLyThietBi frm = new FormQuanLyThietBi(may))
            {
                frm.ShowDialog(this);
                if (frm.DataChanged)
                {
                    VeSoDo();
                    ChonMay(may);
                }
            }
        }

        private void BtnSidebarGanLinhKien_Click(object? sender, EventArgs e)
        {
            if (mayDangChon != null) MoDialogQuanLyThietBi(mayDangChon);
        }

        private void BtnSidebarQuanLyTB_Click(object? sender, EventArgs e)
        {
            if (mayDangChon != null) MoDialogQuanLyThietBi(mayDangChon);
        }

        private void BtnSidebarDoiStatusTB_Click(object? sender, EventArgs e)
        {
            if (mayDangChon == null || dgvLinhKienMay.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn linh kiện trong bảng trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maTB = dgvLinhKienMay.CurrentRow.Cells["colMaTB"].Value?.ToString() ?? "";
            var tb = mayDangChon.DanhSachThietBi.Find(t => t.MaThietBi == maTB);
            if (tb == null) return;

            string trangThaiMoi = tb.TrangThai.Equals("Hoạt động", StringComparison.OrdinalIgnoreCase) ? "Hư hỏng" : "Hoạt động";
            tb.TrangThai = trangThaiMoi;

            DatabaseHelper.CapNhatTrangThaiThietBi(tb.MaThietBi, trangThaiMoi);

            if (trangThaiMoi == "Hư hỏng")
            {
                string maSC = "SC" + DateTime.Now.ToString("ddHHmmss");
                PhieuSuCo phieu = new PhieuSuCo(maSC, mayDangChon.MaMay, tb.MaThietBi, 1, $"Sự cố thiết bị {tb.TenThietBi} hỏng tại {mayDangChon.TenMay}");
                DatabaseHelper.TaoPhieuSuCo(phieu);
                mayDangChon.TrangThai = "Hư hỏng";
                DatabaseHelper.CapNhatThongTinMay(mayDangChon);
            }

            VeSoDo();
            ChonMay(mayDangChon);
        }

        private void XacNhanVaXoaMay(MayTinh may)
        {
            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa máy '{may.TenMay}' (Mã: {may.MaMay}) khỏi CSDL và sơ đồ phòng máy không?",
                "Xác nhận xóa máy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                DatabaseHelper.XoaMayTinh(may.MaMay);
                danhSachMay.Remove(may);
                if (mayDangChon == may) mayDangChon = null;

                VeSoDo();
                MessageBox.Show($"Đã xóa máy '{may.TenMay}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DoiTrangThaiNhanh(MayTinh may, string trangThaiMoi)
        {
            may.TrangThai = trangThaiMoi;
            DatabaseHelper.CapNhatThongTinMay(may);
            VeSoDo();
            ChonMay(may);
            lblTrangThaiDuLieu.Text = $"Đã chuyển {may.TenMay} sang trạng thái: {trangThaiMoi}";
        }

        private void InBienBanSuCo(MayTinh may)
        {
            string dsLinhKien = "";
            var tbHong = may.DanhSachThietBi.FindAll(t => t.TrangThai.Equals("Hư hỏng", StringComparison.OrdinalIgnoreCase));
            if (tbHong.Count > 0)
            {
                dsLinhKien = "\n- Linh kiện gặp sự cố: " + string.Join(", ", tbHong.Select(t => $"{t.TenThietBi} ({t.MaThietBi})"));
            }

            string noiDung = $"- Mã thiết bị: {may.MaMay}\n- Tên máy: {may.TenMay}\n- Phòng: {may.MaPhong}\n- Tình trạng máy: {may.TrangThai}\n- Tọa độ: ({may.ViTriX}, {may.ViTriY}){dsLinhKien}";
            InBienBan inPhieu = new InBienBan(noiDung);
            inPhieu.HienThiBanIn();
        }

        private void BtnThemMay_Click(object? sender, EventArgs e)
        {
            MoDialogThemMay();
        }

        private void BtnSuaMay_Click(object? sender, EventArgs e)
        {
            if (mayDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một máy tính trên sơ đồ trước khi sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MoDialogSuaMay(mayDangChon);
        }

        private void BtnXoaMay_Click(object? sender, EventArgs e)
        {
            if (mayDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn máy cần xóa trên sơ đồ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            XacNhanVaXoaMay(mayDangChon);
        }

        private void BtnKhoaViTri_Click(object? sender, EventArgs e)
        {
            ToggleKhoaViTri();
        }

        private void BtnSapXepLuoi_Click(object? sender, EventArgs e)
        {
            SapXepLuoiTuDong();
        }

        private void BtnInBienBan_Click(object? sender, EventArgs e)
        {
            if (mayDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn máy tính cần lập biên bản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            InBienBanSuCo(mayDangChon);
        }

        private void BtnKetNoiCSDL_Click(object? sender, EventArgs e)
        {
            KhoiTaoKetNoiCSDL();
            TaiDanhSachPhongMay();
        }

        private void BtnTaiLai_Click(object? sender, EventArgs e)
        {
            TaiDuLieuMayTinh();
            VeSoDo();
        }

        private void ChkHutLuoi_CheckedChanged(object? sender, EventArgs e)
        {
            foreach (Control ctrl in panelSoDo.Controls)
            {
                if (ctrl is MayTinhControl mc)
                {
                    mc.SnapToGrid = chkHutLuoi.Checked;
                }
            }
        }

        private void ItemThemTaiCho_Click(object? sender, EventArgs e)
        {
            Point mousePos = panelSoDo.PointToClient(Cursor.Position);
            int x = mousePos.X - panelSoDo.AutoScrollPosition.X;
            int y = mousePos.Y - panelSoDo.AutoScrollPosition.Y;
            MoDialogThemMay(x, y);
        }

        private void ItemKhoaTatCa_Click(object? sender, EventArgs e)
        {
            ToggleKhoaViTri();
        }

        private void BtnChiTietCapNhat_Click(object? sender, EventArgs e)
        {
            if (mayDangChon == null) return;

            string tenMoi = txtChiTietTen.Text.Trim();
            if (string.IsNullOrEmpty(tenMoi))
            {
                MessageBox.Show("Tên máy không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mayDangChon.TenMay = tenMoi;
            mayDangChon.TrangThai = cboChiTietTrangThai.SelectedItem?.ToString() ?? "Hoạt động";
            mayDangChon.ViTriX = (int)numChiTietX.Value;
            mayDangChon.ViTriY = (int)numChiTietY.Value;
            mayDangChon.DaKhoa = chkChiTietKhoa.Checked;

            DatabaseHelper.CapNhatThongTinMay(mayDangChon);
            VeSoDo();
            ChonMay(mayDangChon);
            MessageBox.Show($"Đã cập nhật thông tin máy '{mayDangChon.TenMay}' vào CSDL!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnChiTietInPhieu_Click(object? sender, EventArgs e)
        {
            if (mayDangChon != null) InBienBanSuCo(mayDangChon);
        }

        private void BtnChiTietXoa_Click(object? sender, EventArgs e)
        {
            if (mayDangChon != null) XacNhanVaXoaMay(mayDangChon);
        }

        private void BtnThemNhanh_Click(object? sender, EventArgs e)
        {
            string ma = txtThemMa.Text.Trim();
            string ten = txtThemTen.Text.Trim();

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên máy!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (danhSachMay.Any(m => m.MaMay.Equals(ma, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show($"Mã máy '{ma}' đã tồn tại!", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int chieuRongO = 165;
            int chieuCaoO = 195;
            int khoangCach = 20;
            int soCot = 5;

            int index = danhSachMay.Count;
            int hang = index / soCot;
            int cot = index % soCot;
            int x = khoangCach + cot * (chieuRongO + khoangCach);
            int y = khoangCach + hang * (chieuCaoO + khoangCach);

            string maPhong = phongHienTai?.MaPhong ?? "PM01";
            MayTinh mayMoi = new MayTinh(ma, ten, cboThemTrangThai.SelectedItem?.ToString() ?? "Hoạt động", x, y, maPhong, "", chkThemKhoa.Checked);

            DatabaseHelper.ThemMayTinh(mayMoi);
            danhSachMay.Add(mayMoi);

            txtThemMa.Text = "";
            txtThemTen.Text = "";
            chkThemKhoa.Checked = false;

            VeSoDo();
            ChonMay(mayMoi);
            MessageBox.Show($"Đã thêm máy '{mayMoi.TenMay}' vào sơ đồ phòng máy!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SapXepLuoiTuDong()
        {
            if (isGlobalLocked)
            {
                MessageBox.Show("Sơ đồ đang ở chế độ KHÓA VỊ TRÍ. Vui lòng mở khóa trước khi căn chỉnh lại lưới!", "Đang khóa vị trí", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (danhSachMay.Count == 0) return;

            DialogResult dr = MessageBox.Show(
                "Bạn có muốn tự động căn chỉnh toàn bộ máy tính thành hàng ngũ 5 cột ngay ngắn không?",
                "Căn lưới tự động",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            int chieuRongO = 165;
            int chieuCaoO = 195;
            int khoangCach = 20;
            int soCot = 5;

            for (int i = 0; i < danhSachMay.Count; i++)
            {
                if (!danhSachMay[i].DaKhoa)
                {
                    int hang = i / soCot;
                    int cot = i % soCot;

                    danhSachMay[i].ViTriX = khoangCach + cot * (chieuRongO + khoangCach);
                    danhSachMay[i].ViTriY = khoangCach + hang * (chieuCaoO + khoangCach);
                    DatabaseHelper.CapNhatToaDo(danhSachMay[i].MaMay, danhSachMay[i].ViTriX, danhSachMay[i].ViTriY);
                }
            }

            VeSoDo();
            lblTrangThaiDuLieu.Text = "Đã tự động căn lưới cho toàn bộ máy tính và cập nhật CSDL.";
        }

        private void TxtTimKiem_TextChanged(object? sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) return;

            var match = danhSachMay.FirstOrDefault(m =>
                m.TenMay.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                m.MaMay.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                m.DanhSachThietBi.Any(tb => tb.TenThietBi.Contains(keyword, StringComparison.OrdinalIgnoreCase)));

            if (match != null)
            {
                ChonMay(match);
                foreach (Control ctrl in panelSoDo.Controls)
                {
                    if (ctrl is MayTinhControl mc && mc.Data.MaMay == match.MaMay)
                    {
                        panelSoDo.ScrollControlIntoView(mc);
                        break;
                    }
                }
            }
        }

        #endregion
    }
}
