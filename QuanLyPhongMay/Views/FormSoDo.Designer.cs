namespace QuanLyPhongMay.Views
{
    partial class FormSoDo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null!;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlHeader = new Panel();
            pnlStats = new FlowLayoutPanel();
            lblBaoTri = new Label();
            lblHuHong = new Label();
            lblHoatDong = new Label();
            lblTongMay = new Label();
            lblSubtitle = new Label();
            lblTitle = new Label();
            pnlToolbar = new Panel();
            pnlSearch = new Panel();
            txtTimKiem = new TextBox();
            flowButtons = new FlowLayoutPanel();
            lblChonPhong = new Label();
            cboPhongMay = new ComboBox();
            btnThemMay = new Button();
            btnSuaMay = new Button();
            btnXoaMay = new Button();
            btnKhoaViTri = new Button();
            btnSapXepLuoi = new Button();
            btnInBienBan = new Button();
            btnKetNoiCSDL = new Button();
            btnTaiLai = new Button();
            chkHutLuoi = new CheckBox();
            splitMain = new SplitContainer();
            panelSoDo = new MapCanvasPanel();
            mapContextMenu = new ContextMenuStrip(components);
            itemThemTaiCho = new ToolStripMenuItem();
            itemKhoaTatCa = new ToolStripMenuItem();
            pnlSidebar = new Panel();
            grpThem = new GroupBox();
            btnThemNhanh = new Button();
            chkThemKhoa = new CheckBox();
            cboThemTrangThai = new ComboBox();
            lblTTT = new Label();
            txtThemTen = new TextBox();
            lblTTen = new Label();
            txtThemMa = new TextBox();
            lblTMa = new Label();
            grpChiTiet = new GroupBox();
            tblNutHanhDong = new TableLayoutPanel();
            btnChiTietCapNhat = new Button();
            btnChiTietInPhieu = new Button();
            btnChiTietXoa = new Button();
            tblNutThietBi = new TableLayoutPanel();
            btnSidebarGanLinhKien = new Button();
            btnSidebarDoiStatusTB = new Button();
            btnSidebarQuanLyTB = new Button();
            dgvLinhKienMay = new DataGridView();
            colMaTB = new DataGridViewTextBoxColumn();
            colTenTB = new DataGridViewTextBoxColumn();
            colTrangThaiTB = new DataGridViewTextBoxColumn();
            lblListTB = new Label();
            chkChiTietKhoa = new CheckBox();
            lblKhoaMay = new Label();
            tblToaDo = new TableLayoutPanel();
            numChiTietX = new NumericUpDown();
            numChiTietY = new NumericUpDown();
            lblToaDo = new Label();
            cboChiTietTrangThai = new ComboBox();
            lblTT = new Label();
            txtChiTietTen = new TextBox();
            lblTen = new Label();
            txtChiTietMa = new TextBox();
            lblMa = new Label();
            lblChiTietBanner = new Label();
            statusStrip = new StatusStrip();
            lblTrangThaiCSDL = new ToolStripStatusLabel();
            lblTrangThaiDuLieu = new ToolStripStatusLabel();
            lblThongTinMayChon = new ToolStripStatusLabel();
            lblToaDoConTro = new ToolStripStatusLabel();
            pnlHeader.SuspendLayout();
            pnlStats.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlSearch.SuspendLayout();
            flowButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            mapContextMenu.SuspendLayout();
            pnlSidebar.SuspendLayout();
            grpThem.SuspendLayout();
            grpChiTiet.SuspendLayout();
            tblNutHanhDong.SuspendLayout();
            tblNutThietBi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLinhKienMay).BeginInit();
            tblToaDo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numChiTietX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numChiTietY).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(30, 41, 59);
            pnlHeader.Controls.Add(pnlStats);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(18, 11, 18, 11);
            pnlHeader.Size = new Size(1554, 93);
            pnlHeader.TabIndex = 0;
            // 
            // pnlStats
            // 
            pnlStats.BackColor = Color.Transparent;
            pnlStats.Controls.Add(lblBaoTri);
            pnlStats.Controls.Add(lblHuHong);
            pnlStats.Controls.Add(lblHoatDong);
            pnlStats.Controls.Add(lblTongMay);
            pnlStats.Dock = DockStyle.Right;
            pnlStats.FlowDirection = FlowDirection.RightToLeft;
            pnlStats.Location = new Point(873, 11);
            pnlStats.Margin = new Padding(3, 4, 3, 4);
            pnlStats.Name = "pnlStats";
            pnlStats.Size = new Size(663, 71);
            pnlStats.TabIndex = 2;
            pnlStats.WrapContents = false;
            // 
            // lblBaoTri
            // 
            lblBaoTri.AutoSize = true;
            lblBaoTri.BackColor = Color.FromArgb(254, 243, 199);
            lblBaoTri.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblBaoTri.ForeColor = Color.FromArgb(245, 158, 11);
            lblBaoTri.Location = new Point(552, 16);
            lblBaoTri.Margin = new Padding(6, 16, 6, 0);
            lblBaoTri.Name = "lblBaoTri";
            lblBaoTri.Padding = new Padding(14, 8, 14, 8);
            lblBaoTri.Size = new Size(105, 37);
            lblBaoTri.TabIndex = 0;
            lblBaoTri.Text = "Bảo trì: 0";
            // 
            // lblHuHong
            // 
            lblHuHong.AutoSize = true;
            lblHuHong.BackColor = Color.FromArgb(254, 226, 226);
            lblHuHong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblHuHong.ForeColor = Color.FromArgb(239, 68, 68);
            lblHuHong.Location = new Point(418, 16);
            lblHuHong.Margin = new Padding(6, 16, 6, 0);
            lblHuHong.Name = "lblHuHong";
            lblHuHong.Padding = new Padding(14, 8, 14, 8);
            lblHuHong.Size = new Size(122, 37);
            lblHuHong.TabIndex = 1;
            lblHuHong.Text = "Hư hỏng: 0";
            // 
            // lblHoatDong
            // 
            lblHoatDong.AutoSize = true;
            lblHoatDong.BackColor = Color.FromArgb(209, 250, 229);
            lblHoatDong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblHoatDong.ForeColor = Color.FromArgb(16, 185, 129);
            lblHoatDong.Location = new Point(270, 16);
            lblHoatDong.Margin = new Padding(6, 16, 6, 0);
            lblHoatDong.Name = "lblHoatDong";
            lblHoatDong.Padding = new Padding(14, 8, 14, 8);
            lblHoatDong.Size = new Size(136, 37);
            lblHoatDong.TabIndex = 2;
            lblHoatDong.Text = "Hoạt động: 0";
            // 
            // lblTongMay
            // 
            lblTongMay.AutoSize = true;
            lblTongMay.BackColor = Color.FromArgb(219, 234, 254);
            lblTongMay.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTongMay.ForeColor = Color.FromArgb(59, 130, 246);
            lblTongMay.Location = new Point(127, 16);
            lblTongMay.Margin = new Padding(6, 16, 6, 0);
            lblTongMay.Name = "lblTongMay";
            lblTongMay.Padding = new Padding(14, 8, 14, 8);
            lblTongMay.Size = new Size(131, 37);
            lblTongMay.TabIndex = 3;
            lblTongMay.Text = "Tổng máy: 0";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Location = new Point(18, 51);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(748, 20);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Hiển thị trực tiếp cấu hình linh kiện và tình trạng thiết bị • Kéo thả vị trí • Kết nối CSDL SQL Server QL_PhongMay";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(16, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(542, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🖥️ SƠ ĐỒ VỊ TRÍ & CẤU HÌNH THIẾT BỊ PHÒNG MÁY";
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = Color.White;
            pnlToolbar.Controls.Add(pnlSearch);
            pnlToolbar.Controls.Add(flowButtons);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(0, 93);
            pnlToolbar.Margin = new Padding(3, 4, 3, 4);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Padding = new Padding(11, 11, 11, 11);
            pnlToolbar.Size = new Size(1554, 69);
            pnlToolbar.TabIndex = 1;
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(txtTimKiem);
            pnlSearch.Dock = DockStyle.Right;
            pnlSearch.Location = new Point(1286, 11);
            pnlSearch.Margin = new Padding(3, 4, 3, 4);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(257, 47);
            pnlSearch.TabIndex = 1;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Font = new Font("Segoe UI", 9.5F);
            txtTimKiem.Location = new Point(6, 5);
            txtTimKiem.Margin = new Padding(3, 4, 3, 4);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "🔍 Tìm kiếm mã/tên máy...";
            txtTimKiem.Size = new Size(245, 29);
            txtTimKiem.TabIndex = 0;
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            // 
            // flowButtons
            // 
            flowButtons.AutoSize = true;
            flowButtons.Controls.Add(lblChonPhong);
            flowButtons.Controls.Add(cboPhongMay);
            flowButtons.Controls.Add(btnThemMay);
            flowButtons.Controls.Add(btnSuaMay);
            flowButtons.Controls.Add(btnXoaMay);
            flowButtons.Controls.Add(btnKhoaViTri);
            flowButtons.Controls.Add(btnSapXepLuoi);
            flowButtons.Controls.Add(btnInBienBan);
            flowButtons.Controls.Add(btnKetNoiCSDL);
            flowButtons.Controls.Add(btnTaiLai);
            flowButtons.Controls.Add(chkHutLuoi);
            flowButtons.Dock = DockStyle.Left;
            flowButtons.Location = new Point(11, 11);
            flowButtons.Margin = new Padding(3, 4, 3, 4);
            flowButtons.Name = "flowButtons";
            flowButtons.Size = new Size(1731, 47);
            flowButtons.TabIndex = 0;
            flowButtons.WrapContents = false;
            // 
            // lblChonPhong
            // 
            lblChonPhong.AutoSize = true;
            lblChonPhong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblChonPhong.ForeColor = Color.FromArgb(30, 41, 59);
            lblChonPhong.Location = new Point(3, 11);
            lblChonPhong.Margin = new Padding(3, 11, 3, 0);
            lblChonPhong.Name = "lblChonPhong";
            lblChonPhong.Size = new Size(91, 21);
            lblChonPhong.TabIndex = 0;
            lblChonPhong.Text = "🏢 Phòng:";
            // 
            // cboPhongMay
            // 
            cboPhongMay.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhongMay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cboPhongMay.FormattingEnabled = true;
            cboPhongMay.Location = new Point(100, 5);
            cboPhongMay.Margin = new Padding(3, 5, 11, 0);
            cboPhongMay.Name = "cboPhongMay";
            cboPhongMay.Size = new Size(228, 28);
            cboPhongMay.TabIndex = 1;
            cboPhongMay.SelectedIndexChanged += CboPhongMay_SelectedIndexChanged;
            // 
            // btnThemMay
            // 
            btnThemMay.AutoSize = true;
            btnThemMay.BackColor = Color.FromArgb(16, 185, 129);
            btnThemMay.Cursor = Cursors.Hand;
            btnThemMay.FlatAppearance.BorderSize = 0;
            btnThemMay.FlatStyle = FlatStyle.Flat;
            btnThemMay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThemMay.ForeColor = Color.White;
            btnThemMay.Location = new Point(342, 0);
            btnThemMay.Margin = new Padding(3, 0, 3, 0);
            btnThemMay.Name = "btnThemMay";
            btnThemMay.Padding = new Padding(9, 0, 9, 0);
            btnThemMay.Size = new Size(154, 45);
            btnThemMay.TabIndex = 2;
            btnThemMay.Text = "➕ Thêm máy";
            btnThemMay.UseVisualStyleBackColor = false;
            btnThemMay.Click += BtnThemMay_Click;
            // 
            // btnSuaMay
            // 
            btnSuaMay.AutoSize = true;
            btnSuaMay.BackColor = Color.FromArgb(37, 99, 235);
            btnSuaMay.Cursor = Cursors.Hand;
            btnSuaMay.FlatAppearance.BorderSize = 0;
            btnSuaMay.FlatStyle = FlatStyle.Flat;
            btnSuaMay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSuaMay.ForeColor = Color.White;
            btnSuaMay.Location = new Point(502, 0);
            btnSuaMay.Margin = new Padding(3, 0, 3, 0);
            btnSuaMay.Name = "btnSuaMay";
            btnSuaMay.Padding = new Padding(9, 0, 9, 0);
            btnSuaMay.Size = new Size(138, 45);
            btnSuaMay.TabIndex = 3;
            btnSuaMay.Text = "✏️ Sửa máy";
            btnSuaMay.UseVisualStyleBackColor = false;
            btnSuaMay.Click += BtnSuaMay_Click;
            // 
            // btnXoaMay
            // 
            btnXoaMay.AutoSize = true;
            btnXoaMay.BackColor = Color.FromArgb(239, 68, 68);
            btnXoaMay.Cursor = Cursors.Hand;
            btnXoaMay.FlatAppearance.BorderSize = 0;
            btnXoaMay.FlatStyle = FlatStyle.Flat;
            btnXoaMay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnXoaMay.ForeColor = Color.White;
            btnXoaMay.Location = new Point(646, 0);
            btnXoaMay.Margin = new Padding(3, 0, 3, 0);
            btnXoaMay.Name = "btnXoaMay";
            btnXoaMay.Padding = new Padding(9, 0, 9, 0);
            btnXoaMay.Size = new Size(139, 45);
            btnXoaMay.TabIndex = 4;
            btnXoaMay.Text = "🗑️ Xóa máy";
            btnXoaMay.UseVisualStyleBackColor = false;
            btnXoaMay.Click += BtnXoaMay_Click;
            // 
            // btnKhoaViTri
            // 
            btnKhoaViTri.AutoSize = true;
            btnKhoaViTri.BackColor = Color.FromArgb(241, 245, 249);
            btnKhoaViTri.Cursor = Cursors.Hand;
            btnKhoaViTri.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnKhoaViTri.FlatStyle = FlatStyle.Flat;
            btnKhoaViTri.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnKhoaViTri.ForeColor = Color.FromArgb(30, 41, 59);
            btnKhoaViTri.Location = new Point(791, 0);
            btnKhoaViTri.Margin = new Padding(3, 0, 3, 0);
            btnKhoaViTri.Name = "btnKhoaViTri";
            btnKhoaViTri.Padding = new Padding(9, 0, 9, 0);
            btnKhoaViTri.Size = new Size(154, 45);
            btnKhoaViTri.TabIndex = 5;
            btnKhoaViTri.Text = "🔓 Khóa vị trí";
            btnKhoaViTri.UseVisualStyleBackColor = false;
            btnKhoaViTri.Click += BtnKhoaViTri_Click;
            // 
            // btnSapXepLuoi
            // 
            btnSapXepLuoi.AutoSize = true;
            btnSapXepLuoi.BackColor = Color.FromArgb(241, 245, 249);
            btnSapXepLuoi.Cursor = Cursors.Hand;
            btnSapXepLuoi.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnSapXepLuoi.FlatStyle = FlatStyle.Flat;
            btnSapXepLuoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSapXepLuoi.ForeColor = Color.FromArgb(30, 41, 59);
            btnSapXepLuoi.Location = new Point(951, 0);
            btnSapXepLuoi.Margin = new Padding(3, 0, 3, 0);
            btnSapXepLuoi.Name = "btnSapXepLuoi";
            btnSapXepLuoi.Padding = new Padding(9, 0, 9, 0);
            btnSapXepLuoi.Size = new Size(137, 45);
            btnSapXepLuoi.TabIndex = 6;
            btnSapXepLuoi.Text = "📐 Căn lưới";
            btnSapXepLuoi.UseVisualStyleBackColor = false;
            btnSapXepLuoi.Click += BtnSapXepLuoi_Click;
            // 
            // btnInBienBan
            // 
            btnInBienBan.AutoSize = true;
            btnInBienBan.BackColor = Color.FromArgb(124, 58, 237);
            btnInBienBan.Cursor = Cursors.Hand;
            btnInBienBan.FlatAppearance.BorderSize = 0;
            btnInBienBan.FlatStyle = FlatStyle.Flat;
            btnInBienBan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnInBienBan.ForeColor = Color.White;
            btnInBienBan.Location = new Point(1094, 0);
            btnInBienBan.Margin = new Padding(3, 0, 3, 0);
            btnInBienBan.Name = "btnInBienBan";
            btnInBienBan.Padding = new Padding(9, 0, 9, 0);
            btnInBienBan.Size = new Size(159, 45);
            btnInBienBan.TabIndex = 7;
            btnInBienBan.Text = "🖨️ In biên bản";
            btnInBienBan.UseVisualStyleBackColor = false;
            btnInBienBan.Click += BtnInBienBan_Click;
            // 
            // btnKetNoiCSDL
            // 
            btnKetNoiCSDL.AutoSize = true;
            btnKetNoiCSDL.BackColor = Color.FromArgb(236, 253, 245);
            btnKetNoiCSDL.Cursor = Cursors.Hand;
            btnKetNoiCSDL.FlatAppearance.BorderColor = Color.FromArgb(110, 231, 183);
            btnKetNoiCSDL.FlatStyle = FlatStyle.Flat;
            btnKetNoiCSDL.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnKetNoiCSDL.ForeColor = Color.FromArgb(6, 95, 70);
            btnKetNoiCSDL.Location = new Point(1259, 0);
            btnKetNoiCSDL.Margin = new Padding(3, 0, 3, 0);
            btnKetNoiCSDL.Name = "btnKetNoiCSDL";
            btnKetNoiCSDL.Padding = new Padding(9, 0, 9, 0);
            btnKetNoiCSDL.Size = new Size(174, 45);
            btnKetNoiCSDL.TabIndex = 8;
            btnKetNoiCSDL.Text = "\U0001f7e2 CSDL: Online";
            btnKetNoiCSDL.UseVisualStyleBackColor = false;
            btnKetNoiCSDL.Click += BtnKetNoiCSDL_Click;
            // 
            // btnTaiLai
            // 
            btnTaiLai.AutoSize = true;
            btnTaiLai.BackColor = Color.FromArgb(241, 245, 249);
            btnTaiLai.Cursor = Cursors.Hand;
            btnTaiLai.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnTaiLai.FlatStyle = FlatStyle.Flat;
            btnTaiLai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTaiLai.ForeColor = Color.FromArgb(30, 41, 59);
            btnTaiLai.Location = new Point(1439, 0);
            btnTaiLai.Margin = new Padding(3, 0, 3, 0);
            btnTaiLai.Name = "btnTaiLai";
            btnTaiLai.Padding = new Padding(9, 0, 9, 0);
            btnTaiLai.Size = new Size(119, 45);
            btnTaiLai.TabIndex = 9;
            btnTaiLai.Text = "🔄 Tải lại";
            btnTaiLai.UseVisualStyleBackColor = false;
            btnTaiLai.Click += BtnTaiLai_Click;
            // 
            // chkHutLuoi
            // 
            chkHutLuoi.AutoSize = true;
            chkHutLuoi.Checked = true;
            chkHutLuoi.CheckState = CheckState.Checked;
            chkHutLuoi.Cursor = Cursors.Hand;
            chkHutLuoi.Font = new Font("Segoe UI", 9F);
            chkHutLuoi.ForeColor = Color.FromArgb(71, 85, 105);
            chkHutLuoi.Location = new Point(1570, 11);
            chkHutLuoi.Margin = new Padding(9, 11, 5, 0);
            chkHutLuoi.Name = "chkHutLuoi";
            chkHutLuoi.Size = new Size(156, 24);
            chkHutLuoi.TabIndex = 10;
            chkHutLuoi.Text = "\U0001f9f2 Hút lưới (20px)";
            chkHutLuoi.UseVisualStyleBackColor = true;
            chkHutLuoi.CheckedChanged += ChkHutLuoi_CheckedChanged;
            // 
            // splitMain
            // 
            splitMain.BackColor = Color.FromArgb(226, 232, 240);
            splitMain.Dock = DockStyle.Fill;
            splitMain.FixedPanel = FixedPanel.Panel2;
            splitMain.Location = new Point(0, 162);
            splitMain.Margin = new Padding(3, 4, 3, 4);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(panelSoDo);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(pnlSidebar);
            splitMain.Size = new Size(1554, 945);
            splitMain.SplitterDistance = 1072;
            splitMain.SplitterWidth = 7;
            splitMain.TabIndex = 2;
            // 
            // panelSoDo
            // 
            panelSoDo.AutoScroll = true;
            panelSoDo.BackColor = Color.FromArgb(248, 250, 252);
            panelSoDo.ContextMenuStrip = mapContextMenu;
            panelSoDo.Dock = DockStyle.Fill;
            panelSoDo.Location = new Point(0, 0);
            panelSoDo.Margin = new Padding(3, 4, 3, 4);
            panelSoDo.Name = "panelSoDo";
            panelSoDo.Size = new Size(1072, 945);
            panelSoDo.TabIndex = 0;
            panelSoDo.MouseDown += PanelSoDo_MouseDown;
            // 
            // mapContextMenu
            // 
            mapContextMenu.Font = new Font("Segoe UI", 9.5F);
            mapContextMenu.ImageScalingSize = new Size(20, 20);
            mapContextMenu.Items.AddRange(new ToolStripItem[] { itemThemTaiCho, itemKhoaTatCa });
            mapContextMenu.Name = "mapContextMenu";
            mapContextMenu.Size = new Size(310, 56);
            // 
            // itemThemTaiCho
            // 
            itemThemTaiCho.Name = "itemThemTaiCho";
            itemThemTaiCho.Size = new Size(309, 26);
            itemThemTaiCho.Text = "➕ Thêm máy tại vị trí này";
            itemThemTaiCho.Click += ItemThemTaiCho_Click;
            // 
            // itemKhoaTatCa
            // 
            itemKhoaTatCa.Name = "itemKhoaTatCa";
            itemKhoaTatCa.Size = new Size(309, 26);
            itemKhoaTatCa.Text = "🔒 Khóa / Mở khóa toàn bộ vị trí";
            itemKhoaTatCa.Click += ItemKhoaTatCa_Click;
            // 
            // pnlSidebar
            // 
            pnlSidebar.AutoScroll = true;
            pnlSidebar.BackColor = Color.White;
            pnlSidebar.Controls.Add(grpThem);
            pnlSidebar.Controls.Add(grpChiTiet);
            pnlSidebar.Dock = DockStyle.Fill;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Margin = new Padding(3, 4, 3, 4);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Padding = new Padding(11, 13, 11, 13);
            pnlSidebar.Size = new Size(475, 945);
            pnlSidebar.TabIndex = 0;
            // 
            // grpThem
            // 
            grpThem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpThem.Controls.Add(btnThemNhanh);
            grpThem.Controls.Add(chkThemKhoa);
            grpThem.Controls.Add(cboThemTrangThai);
            grpThem.Controls.Add(lblTTT);
            grpThem.Controls.Add(txtThemTen);
            grpThem.Controls.Add(lblTTen);
            grpThem.Controls.Add(txtThemMa);
            grpThem.Controls.Add(lblTMa);
            grpThem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grpThem.ForeColor = Color.FromArgb(30, 41, 59);
            grpThem.Location = new Point(11, 677);
            grpThem.Margin = new Padding(3, 4, 3, 4);
            grpThem.Name = "grpThem";
            grpThem.Padding = new Padding(3, 4, 3, 4);
            grpThem.Size = new Size(453, 247);
            grpThem.TabIndex = 1;
            grpThem.TabStop = false;
            grpThem.Text = "➕ Thêm máy tính vào phòng";
            // 
            // btnThemNhanh
            // 
            btnThemNhanh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnThemNhanh.BackColor = Color.FromArgb(16, 185, 129);
            btnThemNhanh.Cursor = Cursors.Hand;
            btnThemNhanh.FlatAppearance.BorderSize = 0;
            btnThemNhanh.FlatStyle = FlatStyle.Flat;
            btnThemNhanh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThemNhanh.ForeColor = Color.White;
            btnThemNhanh.Location = new Point(109, 195);
            btnThemNhanh.Margin = new Padding(3, 4, 3, 4);
            btnThemNhanh.Name = "btnThemNhanh";
            btnThemNhanh.Size = new Size(327, 43);
            btnThemNhanh.TabIndex = 7;
            btnThemNhanh.Text = "➕ Thêm vào sơ đồ phòng máy";
            btnThemNhanh.UseVisualStyleBackColor = false;
            btnThemNhanh.Click += BtnThemNhanh_Click;
            // 
            // chkThemKhoa
            // 
            chkThemKhoa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            chkThemKhoa.AutoSize = true;
            chkThemKhoa.Cursor = Cursors.Hand;
            chkThemKhoa.Font = new Font("Segoe UI", 8.5F);
            chkThemKhoa.ForeColor = Color.FromArgb(100, 116, 139);
            chkThemKhoa.Location = new Point(109, 157);
            chkThemKhoa.Margin = new Padding(3, 4, 3, 4);
            chkThemKhoa.Name = "chkThemKhoa";
            chkThemKhoa.Size = new Size(234, 24);
            chkThemKhoa.TabIndex = 6;
            chkThemKhoa.Text = "🔒 Khóa vị trí ngay sau khi tạo";
            chkThemKhoa.UseVisualStyleBackColor = true;
            // 
            // cboThemTrangThai
            // 
            cboThemTrangThai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboThemTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboThemTrangThai.Font = new Font("Segoe UI", 9F);
            cboThemTrangThai.FormattingEnabled = true;
            cboThemTrangThai.Items.AddRange(new object[] { "Hoạt động", "Hư hỏng", "Bảo trì" });
            cboThemTrangThai.Location = new Point(109, 117);
            cboThemTrangThai.Margin = new Padding(3, 4, 3, 4);
            cboThemTrangThai.Name = "cboThemTrangThai";
            cboThemTrangThai.Size = new Size(326, 28);
            cboThemTrangThai.TabIndex = 5;
            // 
            // lblTTT
            // 
            lblTTT.AutoSize = true;
            lblTTT.Font = new Font("Segoe UI", 9F);
            lblTTT.Location = new Point(17, 120);
            lblTTT.Name = "lblTTT";
            lblTTT.Size = new Size(78, 20);
            lblTTT.TabIndex = 4;
            lblTTT.Text = "Trạng thái:";
            // 
            // txtThemTen
            // 
            txtThemTen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtThemTen.Font = new Font("Segoe UI", 9F);
            txtThemTen.Location = new Point(109, 75);
            txtThemTen.Margin = new Padding(3, 4, 3, 4);
            txtThemTen.Name = "txtThemTen";
            txtThemTen.Size = new Size(326, 27);
            txtThemTen.TabIndex = 3;
            // 
            // lblTTen
            // 
            lblTTen.AutoSize = true;
            lblTTen.Font = new Font("Segoe UI", 9F);
            lblTTen.Location = new Point(17, 77);
            lblTTen.Name = "lblTTen";
            lblTTen.Size = new Size(67, 20);
            lblTTen.TabIndex = 2;
            lblTTen.Text = "Tên máy:";
            // 
            // txtThemMa
            // 
            txtThemMa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtThemMa.Font = new Font("Segoe UI", 9F);
            txtThemMa.Location = new Point(109, 32);
            txtThemMa.Margin = new Padding(3, 4, 3, 4);
            txtThemMa.Name = "txtThemMa";
            txtThemMa.Size = new Size(326, 27);
            txtThemMa.TabIndex = 1;
            // 
            // lblTMa
            // 
            lblTMa.AutoSize = true;
            lblTMa.Font = new Font("Segoe UI", 9F);
            lblTMa.Location = new Point(17, 35);
            lblTMa.Name = "lblTMa";
            lblTMa.Size = new Size(65, 20);
            lblTMa.TabIndex = 0;
            lblTMa.Text = "Mã máy:";
            // 
            // grpChiTiet
            // 
            grpChiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpChiTiet.Controls.Add(tblNutHanhDong);
            grpChiTiet.Controls.Add(tblNutThietBi);
            grpChiTiet.Controls.Add(dgvLinhKienMay);
            grpChiTiet.Controls.Add(lblListTB);
            grpChiTiet.Controls.Add(chkChiTietKhoa);
            grpChiTiet.Controls.Add(lblKhoaMay);
            grpChiTiet.Controls.Add(tblToaDo);
            grpChiTiet.Controls.Add(lblToaDo);
            grpChiTiet.Controls.Add(cboChiTietTrangThai);
            grpChiTiet.Controls.Add(lblTT);
            grpChiTiet.Controls.Add(txtChiTietTen);
            grpChiTiet.Controls.Add(lblTen);
            grpChiTiet.Controls.Add(txtChiTietMa);
            grpChiTiet.Controls.Add(lblMa);
            grpChiTiet.Controls.Add(lblChiTietBanner);
            grpChiTiet.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grpChiTiet.ForeColor = Color.FromArgb(30, 41, 59);
            grpChiTiet.Location = new Point(11, 13);
            grpChiTiet.Margin = new Padding(3, 4, 3, 4);
            grpChiTiet.Name = "grpChiTiet";
            grpChiTiet.Padding = new Padding(3, 4, 3, 4);
            grpChiTiet.Size = new Size(453, 653);
            grpChiTiet.TabIndex = 0;
            grpChiTiet.TabStop = false;
            grpChiTiet.Text = "📌 Thông tin máy && Cấu hình thiết bị";
            // 
            // tblNutHanhDong
            // 
            tblNutHanhDong.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tblNutHanhDong.ColumnCount = 3;
            tblNutHanhDong.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblNutHanhDong.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tblNutHanhDong.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblNutHanhDong.Controls.Add(btnChiTietCapNhat, 0, 0);
            tblNutHanhDong.Controls.Add(btnChiTietInPhieu, 1, 0);
            tblNutHanhDong.Controls.Add(btnChiTietXoa, 2, 0);
            tblNutHanhDong.Location = new Point(17, 589);
            tblNutHanhDong.Margin = new Padding(3, 4, 3, 4);
            tblNutHanhDong.Name = "tblNutHanhDong";
            tblNutHanhDong.RowCount = 1;
            tblNutHanhDong.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblNutHanhDong.Size = new Size(418, 45);
            tblNutHanhDong.TabIndex = 15;
            // 
            // btnChiTietCapNhat
            // 
            btnChiTietCapNhat.BackColor = Color.FromArgb(37, 99, 235);
            btnChiTietCapNhat.Cursor = Cursors.Hand;
            btnChiTietCapNhat.Dock = DockStyle.Fill;
            btnChiTietCapNhat.FlatAppearance.BorderSize = 0;
            btnChiTietCapNhat.FlatStyle = FlatStyle.Flat;
            btnChiTietCapNhat.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnChiTietCapNhat.ForeColor = Color.White;
            btnChiTietCapNhat.Location = new Point(0, 0);
            btnChiTietCapNhat.Margin = new Padding(0, 0, 3, 0);
            btnChiTietCapNhat.Name = "btnChiTietCapNhat";
            btnChiTietCapNhat.Size = new Size(136, 45);
            btnChiTietCapNhat.TabIndex = 0;
            btnChiTietCapNhat.Text = "💾 Lưu máy";
            btnChiTietCapNhat.UseVisualStyleBackColor = false;
            btnChiTietCapNhat.Click += BtnChiTietCapNhat_Click;
            // 
            // btnChiTietInPhieu
            // 
            btnChiTietInPhieu.BackColor = Color.FromArgb(124, 58, 237);
            btnChiTietInPhieu.Cursor = Cursors.Hand;
            btnChiTietInPhieu.Dock = DockStyle.Fill;
            btnChiTietInPhieu.FlatAppearance.BorderSize = 0;
            btnChiTietInPhieu.FlatStyle = FlatStyle.Flat;
            btnChiTietInPhieu.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnChiTietInPhieu.ForeColor = Color.White;
            btnChiTietInPhieu.Location = new Point(142, 0);
            btnChiTietInPhieu.Margin = new Padding(3, 0, 3, 0);
            btnChiTietInPhieu.Name = "btnChiTietInPhieu";
            btnChiTietInPhieu.Size = new Size(133, 45);
            btnChiTietInPhieu.TabIndex = 1;
            btnChiTietInPhieu.Text = "🖨️ In phiếu";
            btnChiTietInPhieu.UseVisualStyleBackColor = false;
            btnChiTietInPhieu.Click += BtnChiTietInPhieu_Click;
            // 
            // btnChiTietXoa
            // 
            btnChiTietXoa.BackColor = Color.FromArgb(239, 68, 68);
            btnChiTietXoa.Cursor = Cursors.Hand;
            btnChiTietXoa.Dock = DockStyle.Fill;
            btnChiTietXoa.FlatAppearance.BorderSize = 0;
            btnChiTietXoa.FlatStyle = FlatStyle.Flat;
            btnChiTietXoa.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnChiTietXoa.ForeColor = Color.White;
            btnChiTietXoa.Location = new Point(281, 0);
            btnChiTietXoa.Margin = new Padding(3, 0, 0, 0);
            btnChiTietXoa.Name = "btnChiTietXoa";
            btnChiTietXoa.Size = new Size(137, 45);
            btnChiTietXoa.TabIndex = 2;
            btnChiTietXoa.Text = "🗑️ Xóa máy";
            btnChiTietXoa.UseVisualStyleBackColor = false;
            btnChiTietXoa.Click += BtnChiTietXoa_Click;
            // 
            // tblNutThietBi
            // 
            tblNutThietBi.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tblNutThietBi.ColumnCount = 3;
            tblNutThietBi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblNutThietBi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tblNutThietBi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblNutThietBi.Controls.Add(btnSidebarGanLinhKien, 0, 0);
            tblNutThietBi.Controls.Add(btnSidebarDoiStatusTB, 1, 0);
            tblNutThietBi.Controls.Add(btnSidebarQuanLyTB, 2, 0);
            tblNutThietBi.Location = new Point(17, 539);
            tblNutThietBi.Margin = new Padding(3, 4, 3, 4);
            tblNutThietBi.Name = "tblNutThietBi";
            tblNutThietBi.RowCount = 1;
            tblNutThietBi.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblNutThietBi.Size = new Size(418, 43);
            tblNutThietBi.TabIndex = 14;
            // 
            // btnSidebarGanLinhKien
            // 
            btnSidebarGanLinhKien.BackColor = Color.FromArgb(16, 185, 129);
            btnSidebarGanLinhKien.Cursor = Cursors.Hand;
            btnSidebarGanLinhKien.Dock = DockStyle.Fill;
            btnSidebarGanLinhKien.FlatAppearance.BorderSize = 0;
            btnSidebarGanLinhKien.FlatStyle = FlatStyle.Flat;
            btnSidebarGanLinhKien.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnSidebarGanLinhKien.ForeColor = Color.White;
            btnSidebarGanLinhKien.Location = new Point(0, 0);
            btnSidebarGanLinhKien.Margin = new Padding(0, 0, 3, 0);
            btnSidebarGanLinhKien.Name = "btnSidebarGanLinhKien";
            btnSidebarGanLinhKien.Size = new Size(136, 43);
            btnSidebarGanLinhKien.TabIndex = 0;
            btnSidebarGanLinhKien.Text = "➕ Gắn TB";
            btnSidebarGanLinhKien.UseVisualStyleBackColor = false;
            btnSidebarGanLinhKien.Click += BtnSidebarGanLinhKien_Click;
            // 
            // btnSidebarDoiStatusTB
            // 
            btnSidebarDoiStatusTB.BackColor = Color.FromArgb(245, 158, 11);
            btnSidebarDoiStatusTB.Cursor = Cursors.Hand;
            btnSidebarDoiStatusTB.Dock = DockStyle.Fill;
            btnSidebarDoiStatusTB.FlatAppearance.BorderSize = 0;
            btnSidebarDoiStatusTB.FlatStyle = FlatStyle.Flat;
            btnSidebarDoiStatusTB.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnSidebarDoiStatusTB.ForeColor = Color.White;
            btnSidebarDoiStatusTB.Location = new Point(142, 0);
            btnSidebarDoiStatusTB.Margin = new Padding(3, 0, 3, 0);
            btnSidebarDoiStatusTB.Name = "btnSidebarDoiStatusTB";
            btnSidebarDoiStatusTB.Size = new Size(133, 43);
            btnSidebarDoiStatusTB.TabIndex = 1;
            btnSidebarDoiStatusTB.Text = "🔄 Đổi TT";
            btnSidebarDoiStatusTB.UseVisualStyleBackColor = false;
            btnSidebarDoiStatusTB.Click += BtnSidebarDoiStatusTB_Click;
            // 
            // btnSidebarQuanLyTB
            // 
            btnSidebarQuanLyTB.BackColor = Color.FromArgb(37, 99, 235);
            btnSidebarQuanLyTB.Cursor = Cursors.Hand;
            btnSidebarQuanLyTB.Dock = DockStyle.Fill;
            btnSidebarQuanLyTB.FlatAppearance.BorderSize = 0;
            btnSidebarQuanLyTB.FlatStyle = FlatStyle.Flat;
            btnSidebarQuanLyTB.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnSidebarQuanLyTB.ForeColor = Color.White;
            btnSidebarQuanLyTB.Location = new Point(281, 0);
            btnSidebarQuanLyTB.Margin = new Padding(3, 0, 0, 0);
            btnSidebarQuanLyTB.Name = "btnSidebarQuanLyTB";
            btnSidebarQuanLyTB.Size = new Size(137, 43);
            btnSidebarQuanLyTB.TabIndex = 2;
            btnSidebarQuanLyTB.Text = "⚙️ Quản lý";
            btnSidebarQuanLyTB.UseVisualStyleBackColor = false;
            btnSidebarQuanLyTB.Click += BtnSidebarQuanLyTB_Click;
            // 
            // dgvLinhKienMay
            // 
            dgvLinhKienMay.AllowUserToAddRows = false;
            dgvLinhKienMay.AllowUserToDeleteRows = false;
            dgvLinhKienMay.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvLinhKienMay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLinhKienMay.BackgroundColor = Color.White;
            dgvLinhKienMay.BorderStyle = BorderStyle.Fixed3D;
            dgvLinhKienMay.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLinhKienMay.Columns.AddRange(new DataGridViewColumn[] { colMaTB, colTenTB, colTrangThaiTB });
            dgvLinhKienMay.Location = new Point(17, 340);
            dgvLinhKienMay.Margin = new Padding(3, 4, 3, 4);
            dgvLinhKienMay.MultiSelect = false;
            dgvLinhKienMay.Name = "dgvLinhKienMay";
            dgvLinhKienMay.ReadOnly = true;
            dgvLinhKienMay.RowHeadersVisible = false;
            dgvLinhKienMay.RowHeadersWidth = 51;
            dgvLinhKienMay.RowTemplate.Height = 28;
            dgvLinhKienMay.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLinhKienMay.Size = new Size(418, 189);
            dgvLinhKienMay.TabIndex = 13;
            dgvLinhKienMay.CellFormatting += DgvLinhKienMay_CellFormatting;
            // 
            // colMaTB
            // 
            colMaTB.FillWeight = 45F;
            colMaTB.HeaderText = "Mã";
            colMaTB.MinimumWidth = 6;
            colMaTB.Name = "colMaTB";
            colMaTB.ReadOnly = true;
            // 
            // colTenTB
            // 
            colTenTB.FillWeight = 115F;
            colTenTB.HeaderText = "Tên Linh Kiện";
            colTenTB.MinimumWidth = 6;
            colTenTB.Name = "colTenTB";
            colTenTB.ReadOnly = true;
            // 
            // colTrangThaiTB
            // 
            colTrangThaiTB.FillWeight = 60F;
            colTrangThaiTB.HeaderText = "Tình Trạng";
            colTrangThaiTB.MinimumWidth = 6;
            colTrangThaiTB.Name = "colTrangThaiTB";
            colTrangThaiTB.ReadOnly = true;
            // 
            // lblListTB
            // 
            lblListTB.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblListTB.AutoSize = true;
            lblListTB.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblListTB.ForeColor = Color.FromArgb(30, 41, 59);
            lblListTB.Location = new Point(17, 312);
            lblListTB.Name = "lblListTB";
            lblListTB.Size = new Size(325, 20);
            lblListTB.TabIndex = 12;
            lblListTB.Text = "⚙️ Danh sách linh kiện thiết bị gắn trên máy:";
            // 
            // chkChiTietKhoa
            // 
            chkChiTietKhoa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            chkChiTietKhoa.AutoSize = true;
            chkChiTietKhoa.Cursor = Cursors.Hand;
            chkChiTietKhoa.Font = new Font("Segoe UI", 9F);
            chkChiTietKhoa.ForeColor = Color.FromArgb(220, 38, 38);
            chkChiTietKhoa.Location = new Point(109, 269);
            chkChiTietKhoa.Margin = new Padding(3, 4, 3, 4);
            chkChiTietKhoa.Name = "chkChiTietKhoa";
            chkChiTietKhoa.Size = new Size(235, 24);
            chkChiTietKhoa.TabIndex = 11;
            chkChiTietKhoa.Text = "🔒 Khóa cố định vị trí máy này";
            chkChiTietKhoa.UseVisualStyleBackColor = true;
            // 
            // lblKhoaMay
            // 
            lblKhoaMay.AutoSize = true;
            lblKhoaMay.Font = new Font("Segoe UI", 9F);
            lblKhoaMay.Location = new Point(17, 272);
            lblKhoaMay.Name = "lblKhoaMay";
            lblKhoaMay.Size = new Size(79, 20);
            lblKhoaMay.TabIndex = 10;
            lblKhoaMay.Text = "Khóa vị trí:";
            // 
            // tblToaDo
            // 
            tblToaDo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tblToaDo.ColumnCount = 2;
            tblToaDo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblToaDo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblToaDo.Controls.Add(numChiTietX, 0, 0);
            tblToaDo.Controls.Add(numChiTietY, 1, 0);
            tblToaDo.Location = new Point(109, 224);
            tblToaDo.Margin = new Padding(3, 4, 3, 4);
            tblToaDo.Name = "tblToaDo";
            tblToaDo.RowCount = 1;
            tblToaDo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblToaDo.Size = new Size(327, 36);
            tblToaDo.TabIndex = 8;
            // 
            // numChiTietX
            // 
            numChiTietX.Dock = DockStyle.Fill;
            numChiTietX.Font = new Font("Segoe UI", 9F);
            numChiTietX.Location = new Point(0, 0);
            numChiTietX.Margin = new Padding(0, 0, 5, 0);
            numChiTietX.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            numChiTietX.Name = "numChiTietX";
            numChiTietX.Size = new Size(158, 27);
            numChiTietX.TabIndex = 0;
            // 
            // numChiTietY
            // 
            numChiTietY.Dock = DockStyle.Fill;
            numChiTietY.Font = new Font("Segoe UI", 9F);
            numChiTietY.Location = new Point(168, 0);
            numChiTietY.Margin = new Padding(5, 0, 0, 0);
            numChiTietY.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            numChiTietY.Name = "numChiTietY";
            numChiTietY.Size = new Size(159, 27);
            numChiTietY.TabIndex = 1;
            // 
            // lblToaDo
            // 
            lblToaDo.AutoSize = true;
            lblToaDo.Font = new Font("Segoe UI", 9F);
            lblToaDo.Location = new Point(17, 229);
            lblToaDo.Name = "lblToaDo";
            lblToaDo.Size = new Size(93, 20);
            lblToaDo.TabIndex = 7;
            lblToaDo.Text = "Tọa độ (X,Y):";
            // 
            // cboChiTietTrangThai
            // 
            cboChiTietTrangThai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboChiTietTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboChiTietTrangThai.Font = new Font("Segoe UI", 9F);
            cboChiTietTrangThai.FormattingEnabled = true;
            cboChiTietTrangThai.Location = new Point(109, 184);
            cboChiTietTrangThai.Margin = new Padding(3, 4, 3, 4);
            cboChiTietTrangThai.Name = "cboChiTietTrangThai";
            cboChiTietTrangThai.Size = new Size(326, 28);
            cboChiTietTrangThai.TabIndex = 6;
            // 
            // lblTT
            // 
            lblTT.AutoSize = true;
            lblTT.Font = new Font("Segoe UI", 9F);
            lblTT.Location = new Point(17, 187);
            lblTT.Name = "lblTT";
            lblTT.Size = new Size(78, 20);
            lblTT.TabIndex = 5;
            lblTT.Text = "Trạng thái:";
            // 
            // txtChiTietTen
            // 
            txtChiTietTen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtChiTietTen.Font = new Font("Segoe UI", 9F);
            txtChiTietTen.Location = new Point(109, 141);
            txtChiTietTen.Margin = new Padding(3, 4, 3, 4);
            txtChiTietTen.Name = "txtChiTietTen";
            txtChiTietTen.Size = new Size(326, 27);
            txtChiTietTen.TabIndex = 4;
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Segoe UI", 9F);
            lblTen.Location = new Point(17, 144);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(67, 20);
            lblTen.TabIndex = 3;
            lblTen.Text = "Tên máy:";
            // 
            // txtChiTietMa
            // 
            txtChiTietMa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtChiTietMa.BackColor = Color.FromArgb(241, 245, 249);
            txtChiTietMa.Font = new Font("Segoe UI", 9F);
            txtChiTietMa.Location = new Point(109, 99);
            txtChiTietMa.Margin = new Padding(3, 4, 3, 4);
            txtChiTietMa.Name = "txtChiTietMa";
            txtChiTietMa.ReadOnly = true;
            txtChiTietMa.Size = new Size(326, 27);
            txtChiTietMa.TabIndex = 2;
            // 
            // lblMa
            // 
            lblMa.AutoSize = true;
            lblMa.Font = new Font("Segoe UI", 9F);
            lblMa.Location = new Point(17, 101);
            lblMa.Name = "lblMa";
            lblMa.Size = new Size(65, 20);
            lblMa.TabIndex = 1;
            lblMa.Text = "Mã máy:";
            // 
            // lblChiTietBanner
            // 
            lblChiTietBanner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblChiTietBanner.BackColor = Color.FromArgb(241, 245, 249);
            lblChiTietBanner.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblChiTietBanner.ForeColor = Color.FromArgb(100, 116, 139);
            lblChiTietBanner.Location = new Point(17, 32);
            lblChiTietBanner.Name = "lblChiTietBanner";
            lblChiTietBanner.Size = new Size(418, 59);
            lblChiTietBanner.TabIndex = 0;
            lblChiTietBanner.Text = "Chưa chọn máy nào\r\n(Nhấp chuột vào máy trên sơ đồ)";
            lblChiTietBanner.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.FromArgb(241, 245, 249);
            statusStrip.Font = new Font("Segoe UI", 9F);
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblTrangThaiCSDL, lblTrangThaiDuLieu, lblThongTinMayChon, lblToaDoConTro });
            statusStrip.Location = new Point(0, 1107);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1554, 26);
            statusStrip.TabIndex = 3;
            // 
            // lblTrangThaiCSDL
            // 
            lblTrangThaiCSDL.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTrangThaiCSDL.Name = "lblTrangThaiCSDL";
            lblTrangThaiCSDL.Size = new Size(138, 20);
            lblTrangThaiCSDL.Text = "\U0001f7e2 CSDL: Khởi tạo";
            lblTrangThaiCSDL.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTrangThaiDuLieu
            // 
            lblTrangThaiDuLieu.Name = "lblTrangThaiDuLieu";
            lblTrangThaiDuLieu.Size = new Size(68, 20);
            lblTrangThaiDuLieu.Text = "Sẵn sàng";
            lblTrangThaiDuLieu.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblThongTinMayChon
            // 
            lblThongTinMayChon.Name = "lblThongTinMayChon";
            lblThongTinMayChon.Size = new Size(1262, 20);
            lblThongTinMayChon.Spring = true;
            lblThongTinMayChon.Text = "Chưa chọn máy nào";
            lblThongTinMayChon.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblToaDoConTro
            // 
            lblToaDoConTro.Name = "lblToaDoConTro";
            lblToaDoConTro.Size = new Size(69, 20);
            lblToaDoConTro.Text = "Tọa độ: -";
            lblToaDoConTro.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FormSoDo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1554, 1133);
            Controls.Add(splitMain);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Controls.Add(statusStrip);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1140, 851);
            Name = "FormSoDo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý Sơ đồ Vị trí & Cấu hình Phòng máy";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlStats.ResumeLayout(false);
            pnlStats.PerformLayout();
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            flowButtons.ResumeLayout(false);
            flowButtons.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            mapContextMenu.ResumeLayout(false);
            pnlSidebar.ResumeLayout(false);
            grpThem.ResumeLayout(false);
            grpThem.PerformLayout();
            grpChiTiet.ResumeLayout(false);
            grpChiTiet.PerformLayout();
            tblNutHanhDong.ResumeLayout(false);
            tblNutThietBi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLinhKienMay).EndInit();
            tblToaDo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numChiTietX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numChiTietY).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // Khai báo các Controls cho Form Designer
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.FlowLayoutPanel pnlStats;
        private System.Windows.Forms.Label lblBaoTri;
        private System.Windows.Forms.Label lblHuHong;
        private System.Windows.Forms.Label lblHoatDong;
        private System.Windows.Forms.Label lblTongMay;

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.FlowLayoutPanel flowButtons;
        private System.Windows.Forms.Label lblChonPhong;
        private System.Windows.Forms.ComboBox cboPhongMay;
        private System.Windows.Forms.Button btnThemMay;
        private System.Windows.Forms.Button btnSuaMay;
        private System.Windows.Forms.Button btnXoaMay;
        private System.Windows.Forms.Button btnKhoaViTri;
        private System.Windows.Forms.Button btnSapXepLuoi;
        private System.Windows.Forms.Button btnInBienBan;
        private System.Windows.Forms.Button btnKetNoiCSDL;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.CheckBox chkHutLuoi;

        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtTimKiem;

        private System.Windows.Forms.SplitContainer splitMain;
        private QuanLyPhongMay.Views.MapCanvasPanel panelSoDo;

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.GroupBox grpChiTiet;
        private System.Windows.Forms.Label lblChiTietBanner;
        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.TextBox txtChiTietMa;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtChiTietTen;
        private System.Windows.Forms.Label lblTT;
        private System.Windows.Forms.ComboBox cboChiTietTrangThai;
        private System.Windows.Forms.Label lblToaDo;
        private System.Windows.Forms.TableLayoutPanel tblToaDo;
        private System.Windows.Forms.NumericUpDown numChiTietX;
        private System.Windows.Forms.NumericUpDown numChiTietY;
        private System.Windows.Forms.Label lblKhoaMay;
        private System.Windows.Forms.CheckBox chkChiTietKhoa;
        private System.Windows.Forms.Label lblListTB;
        private System.Windows.Forms.DataGridView dgvLinhKienMay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThaiTB;
        private System.Windows.Forms.TableLayoutPanel tblNutThietBi;
        private System.Windows.Forms.Button btnSidebarGanLinhKien;
        private System.Windows.Forms.Button btnSidebarDoiStatusTB;
        private System.Windows.Forms.Button btnSidebarQuanLyTB;
        private System.Windows.Forms.TableLayoutPanel tblNutHanhDong;
        private System.Windows.Forms.Button btnChiTietCapNhat;
        private System.Windows.Forms.Button btnChiTietInPhieu;
        private System.Windows.Forms.Button btnChiTietXoa;

        private System.Windows.Forms.GroupBox grpThem;
        private System.Windows.Forms.Label lblTMa;
        private System.Windows.Forms.TextBox txtThemMa;
        private System.Windows.Forms.Label lblTTen;
        private System.Windows.Forms.TextBox txtThemTen;
        private System.Windows.Forms.Label lblTTT;
        private System.Windows.Forms.ComboBox cboThemTrangThai;
        private System.Windows.Forms.CheckBox chkThemKhoa;
        private System.Windows.Forms.Button btnThemNhanh;

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblTrangThaiCSDL;
        private System.Windows.Forms.ToolStripStatusLabel lblTrangThaiDuLieu;
        private System.Windows.Forms.ToolStripStatusLabel lblThongTinMayChon;
        private System.Windows.Forms.ToolStripStatusLabel lblToaDoConTro;

        private System.Windows.Forms.ContextMenuStrip mapContextMenu;
        private System.Windows.Forms.ToolStripMenuItem itemThemTaiCho;
        private System.Windows.Forms.ToolStripMenuItem itemKhoaTatCa;
    }
}
