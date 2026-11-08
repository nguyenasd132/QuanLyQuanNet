using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using QuanLyPhongMay.Models;
using QuanLyPhongMay.Presenters;
using QuanLyPhongMay.Utils;

namespace QuanLyPhongMay.Views
{
    public partial class FormSoDo : Form, ISoDoPhongMayView
    {
        // Tầng Presenter (Mô hình MVP)
        private readonly SoDoPhongMayPresenter _presenter;
        private readonly QuanLyThietBiPresenter _thietBiPresenter;

        // Dữ liệu
        private List<PhongMay> danhSachPhong = new List<PhongMay>();
        private PhongMay? phongHienTai = null;
        private List<MayTinh> danhSachMay = new List<MayTinh>();
        private MayTinh? mayDangChon = null;

        // Trạng thái khóa vị trí toàn cục
        private bool isGlobalLocked = false;
        private bool _isAdjustingLayout = false;

        public FormSoDo()
        {
            InitializeComponent();
            ChuanHoaCauTrucGiaoDien();

            _presenter = new SoDoPhongMayPresenter(this);
            _thietBiPresenter = new QuanLyThietBiPresenter();

            // Đăng ký sự kiện co giãn giao diện động theo kích thước màn hình / cửa sổ
            this.Load += (s, e) => CapNhatBoCucTheoManHinh();
            this.Shown += (s, e) => CapNhatBoCucTheoManHinh();
            this.Resize += (s, e) => CapNhatBoCucTheoManHinh();
            this.DpiChanged += (s, e) => CapNhatBoCucTheoManHinh();
            this.panelSoDo.Resize += (s, e) => CapNhatCoGianSoDo();

            KhoiTaoKetNoiCSDL();
            TaiDanhSachPhongMay();
            CapNhatBoCucTheoManHinh();
        }

        #region Co giãn giao diện động theo kích thước máy (Responsive Layout)

        /// <summary>
        /// Chuẩn hóa cấu trúc Container (loại bỏ FlowLayoutPanel gây tràn/cắt nút trên các màn hình DPI khác nhau)
        /// để toàn bộ nút trên thanh công cụ và thẻ thống kê tự động co giãn mượt theo kích thước máy.
        /// </summary>
        private void ChuanHoaCauTrucGiaoDien()
        {
            this.MinimumSize = new Size(920, 600);

            // 1. Đưa 4 thẻ thống kê trực tiếp vào pnlHeader để luôn hiển thị đầy đủ bên phải
            pnlHeader.Padding = Padding.Empty;
            pnlStats.Visible = false;
            pnlStats.Dock = DockStyle.None;
            pnlStats.Size = Size.Empty;

            foreach (var badge in new[] { lblTongMay, lblHoatDong, lblHuHong, lblBaoTri })
            {
                if (badge.Parent != pnlHeader)
                {
                    badge.Parent = pnlHeader;
                }
                badge.Margin = Padding.Empty;
                badge.AutoSize = true;
                badge.BringToFront();
            }

            // 2. Đưa toàn bộ điều khiển trên thanh công cụ trực tiếp vào pnlToolbar (không qua flowButtons / pnlSearch)
            pnlToolbar.AutoSize = false;
            pnlToolbar.MinimumSize = new Size(0, 40);
            pnlToolbar.Padding = Padding.Empty;

            flowButtons.Visible = false;
            flowButtons.Dock = DockStyle.None;
            flowButtons.Size = Size.Empty;

            pnlSearch.Visible = false;
            pnlSearch.Dock = DockStyle.None;
            pnlSearch.Size = Size.Empty;

            Control[] allToolbarControls =
            {
                lblChonPhong, cboPhongMay, btnThemMay, btnSuaMay, btnXoaMay,
                btnKhoaViTri, btnSapXepLuoi, btnHinhNenPhong, btnInBienBan,
                btnKetNoiCSDL, btnTaiLai, chkHutLuoi, txtTimKiem
            };

            foreach (var ctrl in allToolbarControls)
            {
                if (ctrl.Parent != pnlToolbar)
                {
                    ctrl.Parent = pnlToolbar;
                }
                ctrl.Margin = Padding.Empty;
                ctrl.Dock = DockStyle.None;
                ctrl.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                if (ctrl is Button btn)
                {
                    btn.AutoSize = false;
                    btn.Padding = Padding.Empty;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                }
            }

            // 3. Đảm bảo các ComboBox và ô nhập Mã máy mới hoạt động đầy đủ
            if (cboChiTietTrangThai.Items.Count == 0)
            {
                cboChiTietTrangThai.Items.AddRange(new object[] { "Hoạt động", "Hư hỏng", "Bảo trì" });
            }
            if (cboThemTrangThai.Items.Count == 0)
            {
                cboThemTrangThai.Items.AddRange(new object[] { "Hoạt động", "Hư hỏng", "Bảo trì" });
            }
            if (cboThemTrangThai.SelectedIndex < 0)
            {
                cboThemTrangThai.SelectedIndex = 0;
            }
            txtThemMa.ReadOnly = false;
            txtThemMa.Enabled = true;
        }

        private void CapNhatNhanNutToolbarTheoChieuRong(int W)
        {
            bool compact = W < 1260;
            bool coHinh = panelSoDo.HasBackgroundMap;
            bool isTailscale = DatabaseHelper.IsTailscaleConnected;
            bool isSqlOk = DatabaseHelper.IsConnectedToSql;

            lblChonPhong.Text = compact ? "🏢" : "🏢 Phòng:";
            btnThemMay.Text = compact ? "➕ Thêm" : "➕ Thêm máy";
            btnSuaMay.Text = compact ? "✏️ Sửa" : "✏️ Sửa máy";
            btnXoaMay.Text = compact ? "🗑️ Xóa" : "🗑️ Xóa máy";
            btnKhoaViTri.Text = isGlobalLocked
                ? (compact ? "🔒 Đã khóa" : "🔒 Đã khóa vị trí")
                : (compact ? "🔓 Khóa" : "🔓 Khóa vị trí");
            btnSapXepLuoi.Text = compact ? "📐 Lưới" : "📐 Căn lưới";
            btnHinhNenPhong.Text = coHinh
                ? (compact ? "🗺️ Đã bật" : "🗺️ Nền: Đã bật")
                : (compact ? "🗺️ Bản đồ" : "🗺️ Bản đồ nền");
            btnInBienBan.Text = compact ? "🖨️ In phiếu" : "🖨️ In biên bản";
            btnKetNoiCSDL.Text = isSqlOk
                ? (isTailscale ? (compact ? "🟢 Tailscale" : "🟢 CSDL: Tailscale") : (compact ? "🟢 Online" : "🟢 CSDL: Online"))
                : (compact ? "🟡 Offline" : "🟡 CSDL: Offline");
            btnTaiLai.Text = compact ? "🔄 Tải" : "🔄 Tải lại";
            chkHutLuoi.Text = compact ? "🧲 Lưới" : "🧲 Hút lưới (20px)";
        }

        private void CapNhatBoCucTheoManHinh()
        {
            if (_isAdjustingLayout || this.WindowState == FormWindowState.Minimized) return;
            int W = this.ClientSize.Width;
            int H = this.ClientSize.Height;
            if (W < 400 || H < 300) return;

            _isAdjustingLayout = true;
            try
            {
                float scaleW = Math.Clamp(W / 1400f, 0.65f, 1.40f);
                float scaleH = Math.Clamp(H / 820f, 0.68f, 1.35f);

                // 1. HEADER: Co giãn chiều cao, font chữ và 4 badge thống kê bên phải
                int headerH = Math.Clamp((int)Math.Round(64 * scaleH), 50, 82);
                pnlHeader.Height = headerH;

                float titleFontPt = Math.Clamp(11.8f * scaleW, 8.8f, 14.5f);
                float subFontPt = Math.Clamp(8.0f * scaleW, 6.5f, 9.8f);
                lblTitle.Font = new Font("Segoe UI", titleFontPt, FontStyle.Bold);
                lblSubtitle.Font = new Font("Segoe UI", subFontPt, FontStyle.Regular);
                lblTitle.Location = new Point((int)Math.Round(12 * scaleW), Math.Max(4, (int)Math.Round(6 * scaleH)));
                lblSubtitle.Location = new Point((int)Math.Round(14 * scaleW), lblTitle.Bottom + 1);

                CapNhatViTriBadgesHeader(W, headerH, scaleW, scaleH);

                // 2. TOOLBAR: Co giãn toàn bộ Button, ComboBox, CheckBox, SearchBox theo đúng kích thước màn hình
                CapNhatNhanNutToolbarTheoChieuRong(W);

                int toolbarH = Math.Clamp((int)Math.Round(48 * scaleH), 38, 58);
                pnlToolbar.Height = toolbarH;
                int btnH = Math.Clamp(toolbarH - 10, 26, 44);
                int btnY = (toolbarH - btnH) / 2;

                float btnFontPt = Math.Clamp(8.2f * scaleW, 6.6f, 10.0f);
                Font btnFont = new Font("Segoe UI", btnFontPt, FontStyle.Bold);
                Font cboFont = new Font("Segoe UI", Math.Clamp(8.4f * scaleW, 7.0f, 10.0f), FontStyle.Bold);
                Font chkFont = new Font("Segoe UI", Math.Clamp(7.9f * scaleW, 6.6f, 9.5f), FontStyle.Regular);

                lblChonPhong.Font = cboFont;
                cboPhongMay.Font = cboFont;
                chkHutLuoi.Font = chkFont;
                txtTimKiem.Font = new Font("Segoe UI", Math.Clamp(8.4f * scaleW, 7.0f, 10.0f), FontStyle.Regular);

                int padX = Math.Clamp((int)Math.Round(7 * scaleW), 4, 12);
                int gap = Math.Clamp((int)Math.Round(4 * scaleW), 2, 7);

                lblChonPhong.Location = new Point(padX, (toolbarH - lblChonPhong.Height) / 2);
                int startX = lblChonPhong.Right + gap;
                int availW = Math.Max(500, W - startX - padX);

                // Trọng số chiều rộng tỷ lệ cho 12 phần tử trên thanh công cụ
                (Control ctrl, float weight)[] toolbarItems = new (Control, float)[]
                {
                    (cboPhongMay, 1.52f),
                    (btnThemMay, 0.94f),
                    (btnSuaMay, 0.84f),
                    (btnXoaMay, 0.84f),
                    (btnKhoaViTri, 1.00f),
                    (btnSapXepLuoi, 0.86f),
                    (btnHinhNenPhong, 1.04f),
                    (btnInBienBan, 1.00f),
                    (btnKetNoiCSDL, 1.16f),
                    (btnTaiLai, 0.76f),
                    (chkHutLuoi, 0.96f),
                    (txtTimKiem, 1.22f)
                };

                float totalWeight = toolbarItems.Sum(i => i.weight);
                int totalGaps = (toolbarItems.Length - 1) * gap;
                int netWidth = Math.Max(380, availW - totalGaps);

                int curX = startX;
                for (int i = 0; i < toolbarItems.Length; i++)
                {
                    var (ctrl, weight) = toolbarItems[i];
                    int itemW = (i == toolbarItems.Length - 1)
                        ? Math.Max(55, W - padX - curX)
                        : Math.Max(46, (int)Math.Round(netWidth * (weight / totalWeight)));

                    if (ctrl is Button btn)
                    {
                        btn.AutoSize = false;
                        btn.Font = btnFont;
                        btn.Padding = Padding.Empty;
                        btn.SetBounds(curX, btnY, itemW, btnH);
                    }
                    else if (ctrl is ComboBox cbo)
                    {
                        cbo.Width = itemW;
                        cbo.Location = new Point(curX, (toolbarH - cbo.Height) / 2);
                    }
                    else if (ctrl is CheckBox chk)
                    {
                        chk.AutoSize = false;
                        chk.SetBounds(curX, btnY, itemW, btnH);
                    }
                    else if (ctrl is TextBox txt)
                    {
                        txt.Width = itemW;
                        txt.Location = new Point(curX, (toolbarH - txt.Height) / 2);
                    }

                    curX += itemW + gap;
                }

                // 3. SPLIT CONTAINER & SIDEBAR: Co giãn tỷ lệ chiều rộng & chiều cao Sidebar
                int sidebarTargetW = Math.Clamp((int)Math.Round(W * 0.27f), 295, 420);
                int targetSplitterDist = Math.Max(420, W - sidebarTargetW - splitMain.SplitterWidth);
                if (splitMain.Width > 500 && Math.Abs(splitMain.SplitterDistance - targetSplitterDist) > 4)
                {
                    splitMain.SplitterDistance = targetSplitterDist;
                }

                CapNhatBoCucSidebar(scaleW, scaleH);

                // 4. SƠ ĐỒ PHÒNG MÁY: Co giãn kích thước & vị trí các máy tính trên bản đồ
                CapNhatCoGianSoDo();
            }
            catch { }
            finally
            {
                _isAdjustingLayout = false;
            }
        }

        private void CapNhatViTriBadgesHeader(int headerW, int headerH, float scaleW, float scaleH)
        {
            float statFontPt = Math.Clamp(8.4f * scaleW, 7.0f, 10.0f);
            Font statFont = new Font("Segoe UI", statFontPt, FontStyle.Bold);
            int padX = Math.Clamp((int)Math.Round(8 * scaleW), 4, 12);
            int padY = Math.Clamp((int)Math.Round(4 * scaleH), 2, 7);
            int gap = Math.Clamp((int)Math.Round(6 * scaleW), 3, 10);

            Label[] badgesRightToLeft = { lblBaoTri, lblHuHong, lblHoatDong, lblTongMay };
            int rightX = headerW - (int)Math.Round(12 * scaleW);

            foreach (var lbl in badgesRightToLeft)
            {
                lbl.Font = statFont;
                lbl.Padding = new Padding(padX, padY, padX, padY);
                lbl.AutoSize = true;
                int y = Math.Max(3, (headerH - lbl.Height) / 2);
                rightX -= lbl.Width;
                lbl.Location = new Point(rightX, y);
                rightX -= gap;
            }
        }

        private void CapNhatBoCucSidebar(float scaleW, float scaleH)
        {
            int sideW = pnlSidebar.ClientSize.Width;
            int sideH = pnlSidebar.ClientSize.Height;
            if (sideW < 200 || sideH < 300) return;

            int pad = Math.Clamp((int)Math.Round(6 * scaleW), 4, 10);
            int grpW = Math.Max(235, sideW - pad * 2);
            int sideBtnH = Math.Clamp((int)Math.Round(30 * scaleH), 25, 38);
            float sideBtnFontPt = Math.Clamp(8.0f * scaleW, 7.0f, 9.2f);
            Font sideBtnFont = new Font("Segoe UI", sideBtnFontPt, FontStyle.Bold);
            Font lblFont = new Font("Segoe UI", Math.Clamp(8.2f * scaleW, 7.2f, 9.2f), FontStyle.Regular);
            Font inputFont = new Font("Segoe UI", Math.Clamp(8.4f * scaleW, 7.4f, 9.4f), FontStyle.Regular);

            foreach (var b in new[] { btnChiTietCapNhat, btnChiTietInPhieu, btnChiTietXoa, btnSidebarGanLinhKien, btnSidebarDoiStatusTB, btnSidebarQuanLyTB, btnThemNhanh })
            {
                b.Font = sideBtnFont;
            }

            // Chiều cao phần thêm nhanh ở dưới (co giãn vừa khít mọi màn hình)
            int grpThemH = Math.Clamp((int)Math.Round(166 * scaleH), 144, 195);
            int grpChiTietH = Math.Max(290, sideH - grpThemH - pad * 3);

            grpChiTiet.SetBounds(pad, pad, grpW, grpChiTietH);
            grpThem.SetBounds(pad, grpChiTiet.Bottom + pad, grpW, grpThemH);

            int innerX = 10;
            int innerW = grpW - 20;
            int labelColW = Math.Clamp((int)Math.Round(82 * scaleW), 68, 95);
            int fieldX = innerX + labelColW;
            int fieldW = Math.Max(120, grpW - fieldX - 10);

            // Bố cục bên trong grpChiTiet
            int bannerH = Math.Clamp((int)Math.Round(42 * scaleH), 34, 52);
            lblChiTietBanner.Font = new Font("Segoe UI", Math.Clamp(8.3f * scaleW, 7.2f, 9.5f), FontStyle.Bold);
            lblChiTietBanner.SetBounds(innerX, 20, innerW, bannerH);

            int rowStep = Math.Clamp((int)Math.Round(29 * scaleH), 24, 34);
            int curY = lblChiTietBanner.Bottom + 6;

            lblMa.Font = lblFont;
            txtChiTietMa.Font = inputFont;
            lblMa.Location = new Point(innerX, curY + 3);
            txtChiTietMa.SetBounds(fieldX, curY, fieldW, 24);
            curY += rowStep;

            lblTen.Font = lblFont;
            txtChiTietTen.Font = inputFont;
            lblTen.Location = new Point(innerX, curY + 3);
            txtChiTietTen.SetBounds(fieldX, curY, fieldW, 24);
            curY += rowStep;

            lblTT.Font = lblFont;
            cboChiTietTrangThai.Font = inputFont;
            lblTT.Location = new Point(innerX, curY + 3);
            cboChiTietTrangThai.SetBounds(fieldX, curY, fieldW, 24);
            curY += rowStep;

            lblToaDo.Font = lblFont;
            numChiTietX.Font = inputFont;
            numChiTietY.Font = inputFont;
            lblToaDo.Location = new Point(innerX, curY + 3);
            tblToaDo.SetBounds(fieldX, curY, fieldW, 26);
            curY += rowStep;

            lblKhoaMay.Font = lblFont;
            chkChiTietKhoa.Font = lblFont;
            lblKhoaMay.Location = new Point(innerX, curY + 2);
            chkChiTietKhoa.Location = new Point(fieldX, curY);
            curY += Math.Clamp((int)Math.Round(24 * scaleH), 20, 28);

            lblListTB.Font = new Font("Segoe UI", Math.Clamp(8.2f * scaleW, 7.2f, 9.2f), FontStyle.Bold);
            lblListTB.Location = new Point(innerX, curY);

            tblNutHanhDong.SetBounds(innerX, grpChiTietH - sideBtnH - 6, innerW, sideBtnH);
            tblNutThietBi.SetBounds(innerX, tblNutHanhDong.Top - sideBtnH - 4, innerW, sideBtnH);

            int dgvTop = lblListTB.Bottom + 3;
            int dgvH = Math.Max(55, tblNutThietBi.Top - dgvTop - 5);
            dgvLinhKienMay.SetBounds(innerX, dgvTop, innerW, dgvH);

            // Bố cục bên trong grpThem (Thêm máy nhanh)
            int tRowStep = Math.Clamp((int)Math.Round(28 * scaleH), 23, 33);
            int tY = 20;

            lblTMa.Font = lblFont;
            txtThemMa.Font = inputFont;
            lblTMa.Location = new Point(innerX, tY + 3);
            txtThemMa.SetBounds(fieldX, tY, fieldW, 24);
            tY += tRowStep;

            lblTTen.Font = lblFont;
            txtThemTen.Font = inputFont;
            lblTTen.Location = new Point(innerX, tY + 3);
            txtThemTen.SetBounds(fieldX, tY, fieldW, 24);
            tY += tRowStep;

            lblTTT.Font = lblFont;
            cboThemTrangThai.Font = inputFont;
            lblTTT.Location = new Point(innerX, tY + 3);
            cboThemTrangThai.SetBounds(fieldX, tY, fieldW, 24);
            tY += tRowStep;

            chkThemKhoa.Font = lblFont;
            chkThemKhoa.Location = new Point(fieldX, tY);

            btnThemNhanh.SetBounds(fieldX, grpThemH - sideBtnH - 6, fieldW, sideBtnH);
        }

        private void CapNhatCoGianSoDo()
        {
            if (panelSoDo.ClientSize.Width < 200 || panelSoDo.ClientSize.Height < 150) return;

            // Kích thước chuẩn tham chiếu của vùng vẽ sơ đồ (5 cột x 3 hàng máy tính)
            float scaleX = Math.Clamp(panelSoDo.ClientSize.Width / 960f, 0.68f, 1.45f);
            float scaleY = Math.Clamp(panelSoDo.ClientSize.Height / 640f, 0.68f, 1.45f);

            foreach (Control ctrl in panelSoDo.Controls)
            {
                if (ctrl is MayTinhControl mc)
                {
                    mc.ApplyCanvasScale(scaleX, scaleY);
                }
            }
            panelSoDo.Invalidate();
        }

        #endregion

        #region Khởi tạo & CSDL (Triển khai ISoDoPhongMayView - MVP)

        public void HienThiTrangThaiKetNoi(bool isConnected, string thongBao)
        {
            CapNhatTrangThaiKetNoi(isConnected, thongBao);
        }

        public void CapNhatDanhSachPhong(List<PhongMay> dsPhong)
        {
            danhSachPhong = dsPhong;
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

        public void CapNhatSoDoMayTinh(List<MayTinh> dsMay)
        {
            danhSachMay = dsMay;
            VeSoDo();
        }

        public void HienThiThongTinMayChon(MayTinh? may)
        {
            ChonMay(may);
        }

        public void HienThiThongBaoTrangThai(string thongDiep)
        {
            lblTrangThaiDuLieu.Text = thongDiep;
        }

        private void KhoiTaoKetNoiCSDL()
        {
            _presenter.KiemTraKetNoiCSDL(out _);
        }

        private void CapNhatTrangThaiKetNoi(bool ok, string thongBao)
        {
            if (ok)
            {
                bool isTailscale = DatabaseHelper.IsTailscaleConnected;
                lblTrangThaiCSDL.Text = isTailscale
                    ? $"🟢 SQL Tailscale: {DatabaseHelper.TailscaleServerEndpoint} ({DatabaseHelper.DefaultDatabaseName})"
                    : "🟢 SQL Server: Đã kết nối (QL_PhongMay)";
                lblTrangThaiCSDL.ForeColor = Color.FromArgb(5, 150, 105);
                btnKetNoiCSDL.Text = isTailscale ? "🟢 CSDL: Tailscale" : "🟢 CSDL: Online";
                btnKetNoiCSDL.BackColor = Color.FromArgb(236, 253, 245);
                btnKetNoiCSDL.ForeColor = Color.FromArgb(6, 95, 70);
            }
            else
            {
                lblTrangThaiCSDL.Text = $"🟡 CSDL: Offline (Chờ Tailscale {DatabaseHelper.TailscaleServerEndpoint})";
                lblTrangThaiCSDL.ForeColor = Color.FromArgb(217, 119, 6);
                btnKetNoiCSDL.Text = "🟡 CSDL: Offline";
                btnKetNoiCSDL.BackColor = Color.FromArgb(254, 243, 199);
                btnKetNoiCSDL.ForeColor = Color.FromArgb(146, 64, 14);
            }
            lblTrangThaiDuLieu.Text = thongBao;
        }

        private void TaiDanhSachPhongMay()
        {
            _presenter.TaiDanhSachPhongMay();
        }

        private void CboPhongMay_SelectedIndexChanged(object? sender, EventArgs e)
        {
            phongHienTai = cboPhongMay.SelectedItem as PhongMay;
            if (phongHienTai != null)
            {
                TaiHinhNenPhongHienTai();
                TaiDuLieuMayTinh();
                VeSoDo();
            }
        }

        private void TaiHinhNenPhongHienTai()
        {
            if (phongHienTai == null)
            {
                panelSoDo.ClearBackgroundMap();
                CapNhatGiaoDienNutHinhNen();
                return;
            }

            if (!string.IsNullOrWhiteSpace(phongHienTai.HinhNen))
            {
                bool loaded = panelSoDo.LoadBackgroundMap(phongHienTai.HinhNen);
                // Nếu thành viên nhóm clone từ GitHub về mà chưa có file bản đồ mẫu cục bộ thì tự động khởi tạo
                if (!loaded && phongHienTai.HinhNen.Contains("FloorPlan_", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        string bgFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Backgrounds");
                        Directory.CreateDirectory(bgFolder);
                        string destFullPath = Path.Combine(bgFolder, $"FloorPlan_{phongHienTai.MaPhong}.png");
                        MapCanvasPanel.GenerateSampleFloorPlanImage(phongHienTai.MaPhong, phongHienTai.TenPhong, destFullPath);
                        panelSoDo.LoadBackgroundMap(destFullPath);
                    }
                    catch { }
                }
            }
            else
            {
                panelSoDo.ClearBackgroundMap();
            }

            CapNhatGiaoDienNutHinhNen();
        }

        private void CapNhatGiaoDienNutHinhNen()
        {
            bool coHinh = panelSoDo.HasBackgroundMap;
            itemXoaHinhNen.Enabled = coHinh;

            if (coHinh)
            {
                btnHinhNenPhong.Text = "🗺️ Nền: Đã bật";
                btnHinhNenPhong.BackColor = Color.FromArgb(2, 132, 199);
            }
            else
            {
                btnHinhNenPhong.Text = "🗺️ Bản đồ nền";
                btnHinhNenPhong.BackColor = Color.FromArgb(14, 165, 233);
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
            CapNhatCoGianSoDo();
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

            if (this.ClientSize.Width > 400 && pnlHeader.Height > 30)
            {
                float scaleW = Math.Clamp(this.ClientSize.Width / 1400f, 0.68f, 1.40f);
                float scaleH = Math.Clamp(this.ClientSize.Height / 800f, 0.72f, 1.35f);
                CapNhatViTriBadgesHeader(this.ClientSize.Width, pnlHeader.Height, scaleW, scaleH);
            }
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

        private string TaoMaMayGoiY(string maPhong, out int soThuTu)
        {
            soThuTu = danhSachMay.Count + 1;
            string prefix = "PC";
            if (maPhong.Equals("PM02", StringComparison.OrdinalIgnoreCase)) prefix = "PC2";
            else if (maPhong.Equals("PM03", StringComparison.OrdinalIgnoreCase)) prefix = "PC3";

            string maGoiY = $"{prefix}{soThuTu:D2}";
            while (danhSachMay.Any(m => m.MaMay.Equals(maGoiY, StringComparison.OrdinalIgnoreCase)) ||
                   DatabaseHelper.KiemTraMaMayTonTai(maGoiY))
            {
                soThuTu++;
                maGoiY = $"{prefix}{soThuTu:D2}";
            }
            return maGoiY;
        }

        private void MoDialogThemMay(int defaultX = 20, int defaultY = 20)
        {
            string maPhong = phongHienTai?.MaPhong ?? "PM01";
            string maGoiY = TaoMaMayGoiY(maPhong, out int soThuTu);

            // Nếu thêm từ nút trên thanh công cụ (20,20), tự động tính vị trí ô lưới trống tiếp theo
            if (defaultX == 20 && defaultY == 20 && danhSachMay.Count > 0)
            {
                int index = danhSachMay.Count;
                int hang = index / 5;
                int cot = index % 5;
                defaultX = 20 + cot * (165 + 20);
                defaultY = 20 + hang * (195 + 20);
            }

            MayTinh mayMoiGoiY = new MayTinh(maGoiY, $"Máy {soThuTu:D2}", "Hoạt động", defaultX, defaultY, maPhong, "", false);

            // Truyền isEditMode: false để cho phép người dùng tự do chỉnh sửa Mã máy
            using (FormThemSuaMay formThem = new FormThemSuaMay(mayMoiGoiY, defaultX, defaultY, isEditMode: false))
            {
                if (formThem.ShowDialog(this) == DialogResult.OK)
                {
                    MayTinh mayMoi = formThem.MayTinhData;
                    mayMoi.MaPhong = maPhong;

                    if (danhSachMay.Any(m => m.MaMay.Equals(mayMoi.MaMay, StringComparison.OrdinalIgnoreCase)) ||
                        DatabaseHelper.KiemTraMaMayTonTai(mayMoi.MaMay))
                    {
                        MessageBox.Show($"Mã máy '{mayMoi.MaMay}' đã tồn tại trong hệ thống CSDL! Vui lòng nhập mã máy khác.", "Trùng mã máy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!DatabaseHelper.ThemMayTinh(mayMoi))
                    {
                        MessageBox.Show($"Không thể thêm máy '{mayMoi.MaMay}' vào CSDL SQL Server.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    danhSachMay.Add(mayMoi);
                    VeSoDo();
                    ChonMay(mayMoi);
                    MessageBox.Show($"Đã thêm máy '{mayMoi.TenMay}' (Mã: {mayMoi.MaMay}) vào phòng {maPhong}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void MoDialogSuaMay(MayTinh may)
        {
            using (FormThemSuaMay formSua = new FormThemSuaMay(may, may.ViTriX, may.ViTriY, isEditMode: true))
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

        private void BtnHinhNenPhong_Click(object? sender, EventArgs e)
        {
            ChonAnhBanDoNenChoPhong();
        }

        private void ItemChonHinhNen_Click(object? sender, EventArgs e)
        {
            ChonAnhBanDoNenChoPhong();
        }

        private void ItemTaoBanDoMau_Click(object? sender, EventArgs e)
        {
            TaoBanDoKienTrucMauChoPhong();
        }

        private void ItemXoaHinhNen_Click(object? sender, EventArgs e)
        {
            XoaAnhBanDoNenPhong();
        }

        private void ChonAnhBanDoNenChoPhong()
        {
            if (phongHienTai == null)
            {
                MessageBox.Show("Vui lòng chọn phòng máy trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = $"Chọn ảnh bản đồ mặt bằng địa lý cho phòng {phongHienTai.MaPhong} - {phongHienTai.TenPhong}";
                ofd.Filter = "Tệp hình ảnh (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|Tất cả các tệp (*.*)|*.*";
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string bgFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Backgrounds");
                        Directory.CreateDirectory(bgFolder);

                        string ext = Path.GetExtension(ofd.FileName);
                        string safeFileName = $"Map_{phongHienTai.MaPhong}_{DateTime.Now:yyyyMMdd_HHmmss}{ext}";
                        string destFullPath = Path.Combine(bgFolder, safeFileName);
                        File.Copy(ofd.FileName, destFullPath, true);

                        string relativePath = Path.Combine("Images", "Backgrounds", safeFileName);
                        phongHienTai.HinhNen = relativePath;
                        DatabaseHelper.CapNhatHinhNenPhong(phongHienTai.MaPhong, relativePath);

                        TaiHinhNenPhongHienTai();
                        lblTrangThaiDuLieu.Text = $"🗺️ Đã cập nhật ảnh bản đồ mặt bằng cho phòng {phongHienTai.MaPhong}. Hãy kéo thả máy tính vào đúng vị trí thực tế!";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể nạp ảnh bản đồ nền:\n{ex.Message}", "Lỗi nạp ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void TaoBanDoKienTrucMauChoPhong()
        {
            if (phongHienTai == null) return;

            try
            {
                string bgFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Backgrounds");
                Directory.CreateDirectory(bgFolder);

                string safeFileName = $"FloorPlan_{phongHienTai.MaPhong}.png";
                string destFullPath = Path.Combine(bgFolder, safeFileName);

                MapCanvasPanel.GenerateSampleFloorPlanImage(phongHienTai.MaPhong, phongHienTai.TenPhong, destFullPath);

                string relativePath = Path.Combine("Images", "Backgrounds", safeFileName);
                phongHienTai.HinhNen = relativePath;
                DatabaseHelper.CapNhatHinhNenPhong(phongHienTai.MaPhong, relativePath);

                TaiHinhNenPhongHienTai();
                lblTrangThaiDuLieu.Text = $"📐 Đã tạo và áp dụng sơ đồ mặt bằng kiến trúc mẫu cho phòng {phongHienTai.MaPhong}.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tạo bản đồ mẫu:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaAnhBanDoNenPhong()
        {
            if (phongHienTai == null || !panelSoDo.HasBackgroundMap) return;

            DialogResult dr = MessageBox.Show(
                $"Bạn có muốn gỡ bỏ ảnh bản đồ nền của phòng '{phongHienTai.TenPhong}' ({phongHienTai.MaPhong}) để quay về nền lưới mặc định không?",
                "Xóa bản đồ nền",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                phongHienTai.HinhNen = null;
                DatabaseHelper.CapNhatHinhNenPhong(phongHienTai.MaPhong, null);
                TaiHinhNenPhongHienTai();
                lblTrangThaiDuLieu.Text = $"🗑️ Đã gỡ bỏ ảnh bản đồ nền của phòng {phongHienTai.MaPhong}.";
            }
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

            if (danhSachMay.Any(m => m.MaMay.Equals(ma, StringComparison.OrdinalIgnoreCase)) ||
                DatabaseHelper.KiemTraMaMayTonTai(ma))
            {
                MessageBox.Show($"Mã máy '{ma}' đã tồn tại trong hệ thống CSDL! Vui lòng nhập mã khác.", "Trùng mã máy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtThemMa.Focus();
                txtThemMa.SelectAll();
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

            if (!DatabaseHelper.ThemMayTinh(mayMoi))
            {
                MessageBox.Show($"Không thể thêm máy '{mayMoi.MaMay}' vào CSDL SQL Server.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            danhSachMay.Add(mayMoi);

            txtThemMa.Text = "";
            txtThemTen.Text = "";
            chkThemKhoa.Checked = false;

            VeSoDo();
            ChonMay(mayMoi);
            MessageBox.Show($"Đã thêm máy '{mayMoi.TenMay}' (Mã: {mayMoi.MaMay}) vào sơ đồ phòng máy!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
