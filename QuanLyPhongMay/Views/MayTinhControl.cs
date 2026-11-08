using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QuanLyPhongMay.Models;

namespace QuanLyPhongMay.Views
{
    public class MayTinhControl : Control
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MayTinh Data { get; private set; }

        private bool isSelected = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowDrag { get; set; } = true;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsGlobalLocked { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool SnapToGrid { get; set; } = true;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int GridSize { get; set; } = 20;

        // Sự kiện
        public event EventHandler<MayTinh>? MaySelected;
        public event EventHandler<MayTinh>? PositionChanged;
        public event EventHandler<MayTinh>? DragFinished;
        public event EventHandler<MayTinh>? RequestEdit;
        public event EventHandler<MayTinh>? RequestDelete;
        public event EventHandler<(MayTinh May, string NewStatus)>? RequestChangeStatus;
        public event EventHandler<MayTinh>? RequestPrint;
        public event EventHandler<MayTinh>? RequestToggleLock;
        public event EventHandler<MayTinh>? AttemptDragWhileLocked;
        public event EventHandler<MayTinh>? RequestManageComponents;

        // Biến phục vụ kéo thả & tỷ lệ co giãn theo màn hình
        private bool isDragging = false;
        private Point dragStartPoint;
        private bool isHovered = false;
        private ToolTip toolTipInfo;
        private float _scaleX = 1.0f;
        private float _scaleY = 1.0f;

        public MayTinhControl(MayTinh data)
        {
            Data = data;
            // Kích thước chuẩn tham chiếu để hiển thị trực tiếp danh sách linh kiện & tình trạng thiết bị
            this.Size = new Size(165, 195);
            this.Location = new Point(data.ViTriX, data.ViTriY);
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Hand;

            toolTipInfo = new ToolTip
            {
                AutoPopDelay = 8000,
                InitialDelay = 500,
                ReshowDelay = 200,
                IsBalloon = true,
                ToolTipTitle = $"Chi tiết cấu hình: {Data.TenMay}"
            };

            CapNhatToolTip();
            SetupContextMenu();

            // Cấu hình sự kiện chuột
            this.MouseEnter += (s, e) => { isHovered = true; Invalidate(); };
            this.MouseLeave += (s, e) => { isHovered = false; Invalidate(); };
            this.MouseDown += OnMouseDown;
            this.MouseMove += OnMouseMove;
            this.MouseUp += OnMouseUp;
            this.MouseDoubleClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    RequestEdit?.Invoke(this, Data);
                }
            };
        }

        public void ApplyCanvasScale(float scaleX, float scaleY)
        {
            _scaleX = Math.Clamp(scaleX, 0.65f, 1.50f);
            _scaleY = Math.Clamp(scaleY, 0.65f, 1.50f);

            int w = Math.Clamp((int)Math.Round(165 * _scaleX), 112, 240);
            int h = Math.Clamp((int)Math.Round(195 * _scaleY), 135, 280);
            int x = Math.Max(6, (int)Math.Round(Data.ViTriX * _scaleX));
            int y = Math.Max(6, (int)Math.Round(Data.ViTriY * _scaleY));

            if (!isDragging)
            {
                this.SetBounds(x, y, w, h);
            }
            else
            {
                this.Size = new Size(w, h);
            }
            this.Invalidate();
        }

        public void CapNhatThongTin(MayTinh data)
        {
            Data = data;
            ApplyCanvasScale(_scaleX, _scaleY);
            CapNhatToolTip();
            SetupContextMenu();
            Invalidate();
        }

        private void CapNhatToolTip()
        {
            string tt = $"Mã máy: {Data.MaMay} | Phòng: {Data.MaPhong}\n" +
                        $"Trạng thái máy: {Data.TrangThai}\n" +
                        $"Tọa độ: ({Data.ViTriX}, {Data.ViTriY})\n" +
                        $"----------------------------------\n" +
                        $"CẤU HÌNH & LINH KIỆN ({Data.DanhSachThietBi.Count}):\n";

            if (Data.DanhSachThietBi.Count == 0)
            {
                tt += "(Chưa gắn thiết bị nào)";
            }
            else
            {
                foreach (var tb in Data.DanhSachThietBi)
                {
                    string statusIcon = tb.TrangThai == "Hoạt động" ? "🟢" : "🔴 [HỎNG]";
                    string thongSo = string.IsNullOrEmpty(tb.ThongSo) ? "" : $" ({tb.ThongSo})";
                    tt += $"• {tb.GetIcon()} {tb.TenThietBi}{thongSo} => {statusIcon}\n";
                }
            }

            toolTipInfo.SetToolTip(this, tt);
        }

        private void SetupContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Font = new Font("Segoe UI", 9.5F);

            ToolStripMenuItem itemHeader = new ToolStripMenuItem($"💻 {Data.TenMay} ({Data.MaMay})")
            {
                Enabled = false,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            menu.Items.Add(itemHeader);
            menu.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem itemSua = new ToolStripMenuItem("✏️ Sửa thông tin máy", null, (s, e) =>
            {
                RequestEdit?.Invoke(this, Data);
            });
            menu.Items.Add(itemSua);

            ToolStripMenuItem itemLinhKien = new ToolStripMenuItem("⚙️ Quản lý linh kiện thiết bị...", null, (s, e) =>
            {
                RequestManageComponents?.Invoke(this, Data);
            });
            menu.Items.Add(itemLinhKien);

            // Khóa / Mở khóa vị trí máy này
            ToolStripMenuItem itemKhoa = new ToolStripMenuItem(
                Data.DaKhoa ? "🔓 Mở khóa vị trí máy này" : "🔒 Khóa vị trí máy này", 
                null, 
                (s, e) =>
                {
                    Data.DaKhoa = !Data.DaKhoa;
                    SetupContextMenu();
                    Invalidate();
                    RequestToggleLock?.Invoke(this, Data);
                });
            menu.Items.Add(itemKhoa);

            // Sub-menu đổi trạng thái máy
            ToolStripMenuItem itemDoiTrangThai = new ToolStripMenuItem("🔄 Đổi trạng thái máy");
            
            ToolStripMenuItem stHoatDong = new ToolStripMenuItem("🟢 Hoạt động", null, (s, e) =>
            {
                RequestChangeStatus?.Invoke(this, (Data, "Hoạt động"));
            });
            ToolStripMenuItem stHuHong = new ToolStripMenuItem("🔴 Hư hỏng", null, (s, e) =>
            {
                RequestChangeStatus?.Invoke(this, (Data, "Hư hỏng"));
            });
            ToolStripMenuItem stBaoTri = new ToolStripMenuItem("🟡 Bảo trì", null, (s, e) =>
            {
                RequestChangeStatus?.Invoke(this, (Data, "Bảo trì"));
            });

            itemDoiTrangThai.DropDownItems.Add(stHoatDong);
            itemDoiTrangThai.DropDownItems.Add(stHuHong);
            itemDoiTrangThai.DropDownItems.Add(stBaoTri);
            menu.Items.Add(itemDoiTrangThai);

            ToolStripMenuItem itemInPhieu = new ToolStripMenuItem("🖨️ In biên bản sự cố", null, (s, e) =>
            {
                RequestPrint?.Invoke(this, Data);
            });
            menu.Items.Add(itemInPhieu);

            menu.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem itemXoa = new ToolStripMenuItem("🗑️ Xóa máy này", null, (s, e) =>
            {
                RequestDelete?.Invoke(this, Data);
            })
            {
                ForeColor = Color.DarkRed
            };
            menu.Items.Add(itemXoa);

            this.ContextMenuStrip = menu;
        }

        private void OnMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MaySelected?.Invoke(this, Data);
                this.BringToFront();

                bool isLocked = IsGlobalLocked || Data.DaKhoa || !AllowDrag;

                if (!isLocked)
                {
                    isDragging = true;
                    dragStartPoint = e.Location;
                    this.Cursor = Cursors.SizeAll;
                }
                else
                {
                    if (IsGlobalLocked || Data.DaKhoa)
                    {
                        AttemptDragWhileLocked?.Invoke(this, Data);
                    }
                }
            }
        }

        private void OnMouseMove(object? sender, MouseEventArgs e)
        {
            bool isLocked = IsGlobalLocked || Data.DaKhoa || !AllowDrag;

            if (isDragging && !isLocked && Parent != null)
            {
                int newX = this.Left + (e.X - dragStartPoint.X);
                int newY = this.Top + (e.Y - dragStartPoint.Y);

                newX = Math.Max(10, Math.Min(newX, Parent.DisplayRectangle.Width - this.Width - 10));
                newY = Math.Max(10, Math.Min(newY, Parent.DisplayRectangle.Height - this.Height - 10));

                if (SnapToGrid && GridSize > 1)
                {
                    newX = (int)Math.Round((double)newX / GridSize) * GridSize;
                    newY = (int)Math.Round((double)newY / GridSize) * GridSize;
                }

                if (newX != this.Left || newY != this.Top)
                {
                    this.Location = new Point(newX, newY);
                    int logicalX = Math.Max(0, (int)Math.Round(newX / Math.Max(0.5f, _scaleX)));
                    int logicalY = Math.Max(0, (int)Math.Round(newY / Math.Max(0.5f, _scaleY)));
                    if (SnapToGrid && GridSize > 1)
                    {
                        logicalX = (int)Math.Round((double)logicalX / GridSize) * GridSize;
                        logicalY = (int)Math.Round((double)logicalY / GridSize) * GridSize;
                    }
                    Data.ViTriX = logicalX;
                    Data.ViTriY = logicalY;
                    PositionChanged?.Invoke(this, Data);
                }
            }
        }

        private void OnMouseUp(object? sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                this.Cursor = Cursors.Hand;
                DragFinished?.Invoke(this, Data);
            }
        }

        protected override void OnDpiChangedAfterParent(EventArgs e)
        {
            base.OnDpiChangedAfterParent(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Tỷ lệ co giãn động dựa trên kích thước thực của Control (hỗ trợ màn hình to/nhỏ và High-DPI)
            float sx = Width / 165f;
            float sy = Height / 195f;
            float sfFont = Math.Clamp(Math.Min(sx, sy), 0.72f, 1.32f);
            int Sx(int val) => (int)Math.Round(val * sx);
            int Sy(int val) => (int)Math.Round(val * sy);
            float Fpt(float pt) => Math.Max(5.8f, pt * sfFont);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            bool coThietBiLoi = Data.CoThietBiHuHong();

            // Màu sắc tổng thể theo trạng thái
            Color bgCard = Color.White;
            Color borderColor = Color.FromArgb(203, 213, 225);
            Color screenBg = Color.FromArgb(241, 245, 249);
            Color screenIconColor = Color.FromArgb(71, 85, 105);
            Color tagBg = Color.FromArgb(241, 245, 249);
            Color tagBorder = Color.FromArgb(203, 213, 225);
            Color tagText = Color.FromArgb(51, 65, 85);

            if (Data.TrangThai == "Hoạt động")
            {
                if (coThietBiLoi)
                {
                    // Máy hoạt động nhưng có linh kiện hỏng -> viền cảnh báo cam đỏ
                    borderColor = Color.FromArgb(239, 68, 68);
                    bgCard = Color.FromArgb(255, 250, 250);
                    screenBg = Color.FromArgb(254, 242, 242);
                    screenIconColor = Color.FromArgb(220, 38, 38);
                    tagBg = Color.FromArgb(254, 226, 226);
                    tagBorder = Color.FromArgb(252, 165, 165);
                    tagText = Color.FromArgb(185, 28, 28);
                }
                else
                {
                    borderColor = Color.FromArgb(16, 185, 129);      // Emerald green
                    screenBg = Color.FromArgb(236, 253, 245);
                    screenIconColor = Color.FromArgb(5, 150, 105);
                    tagBg = Color.FromArgb(209, 250, 229);
                    tagBorder = Color.FromArgb(110, 231, 183);
                    tagText = Color.FromArgb(6, 95, 70);
                }
            }
            else if (Data.TrangThai == "Hư hỏng")
            {
                borderColor = Color.FromArgb(239, 68, 68);        // Red
                bgCard = Color.FromArgb(254, 242, 242);
                screenBg = Color.FromArgb(254, 226, 226);
                screenIconColor = Color.FromArgb(220, 38, 38);
                tagBg = Color.FromArgb(254, 202, 202);
                tagBorder = Color.FromArgb(252, 165, 165);
                tagText = Color.FromArgb(153, 27, 27);
            }
            else if (Data.TrangThai == "Bảo trì")
            {
                borderColor = Color.FromArgb(245, 158, 11);       // Amber
                bgCard = Color.FromArgb(255, 251, 235);
                screenBg = Color.FromArgb(254, 243, 199);
                screenIconColor = Color.FromArgb(217, 119, 6);
                tagBg = Color.FromArgb(253, 230, 138);
                tagBorder = Color.FromArgb(252, 211, 77);
                tagText = Color.FromArgb(146, 64, 14);
            }

            // Vẽ nền Card
            using (GraphicsPath path = GetRoundedRectangle(rect, Sx(8)))
            {
                using (SolidBrush brushBg = new SolidBrush(bgCard))
                {
                    g.FillPath(brushBg, path);
                }

                if (isHovered && !isSelected)
                {
                    using (Pen penHover = new Pen(Color.FromArgb(96, 165, 250), 2f * sx))
                    {
                        g.DrawPath(penHover, path);
                    }
                }
                else if (isSelected)
                {
                    using (Pen penSelected = new Pen(Color.FromArgb(37, 99, 235), 3f * sx))
                    {
                        g.DrawPath(penSelected, path);
                    }
                }
                else
                {
                    using (Pen penBorder = new Pen(borderColor, (coThietBiLoi ? 2f : 1.5f) * sx))
                    {
                        g.DrawPath(penBorder, path);
                    }
                }
            }

            // 1. Icon màn hình máy tính
            int iconX = Sx(12);
            int iconY = Sy(8);
            int iconW = Sx(28);
            int iconH = Sy(20);

            using (SolidBrush brushScreen = new SolidBrush(screenBg))
            using (Pen penScreen = new Pen(screenIconColor, 1.5f * sx))
            {
                g.FillRectangle(brushScreen, iconX, iconY, iconW, iconH);
                g.DrawRectangle(penScreen, iconX, iconY, iconW, iconH);
                g.DrawLine(penScreen, iconX + iconW / 2, iconY + iconH, iconX + iconW / 2, iconY + iconH + Sy(3));
                g.DrawLine(penScreen, iconX + Sx(6), iconY + iconH + Sy(3), iconX + iconW - Sx(6), iconY + iconH + Sy(3));
            }

            // 2. Tên máy & Mã máy
            using (Font fontTen = new Font("Segoe UI", Fpt(9.5F), FontStyle.Bold))
            using (Font fontMa = new Font("Segoe UI", Fpt(7.5F), FontStyle.Regular))
            using (SolidBrush brushTen = new SolidBrush(Color.FromArgb(15, 23, 42)))
            using (SolidBrush brushMa = new SolidBrush(Color.FromArgb(100, 116, 139)))
            {
                g.DrawString(Data.TenMay, fontTen, brushTen, new PointF(Sx(46), Sy(7)));
                g.DrawString($"ID: {Data.MaMay}", fontMa, brushMa, new PointF(Sx(47), Sy(24)));
            }

            // 3. Badge trạng thái máy
            string txtStatus = Data.TrangThai;
            if (coThietBiLoi && Data.TrangThai == "Hoạt động")
            {
                txtStatus = $"Lỗi {Data.SoThietBiHuHong()} TB";
            }

            Rectangle tagRect = new Rectangle(Sx(10), Sy(42), Width - Sx(20), Sy(20));
            using (GraphicsPath tagPath = GetRoundedRectangle(tagRect, Sx(4)))
            using (SolidBrush brushTagBg = new SolidBrush(tagBg))
            using (Pen penTagBorder = new Pen(tagBorder, 1f))
            using (Font fontTag = new Font("Segoe UI", Fpt(8F), FontStyle.Bold))
            using (SolidBrush brushTagText = new SolidBrush(tagText))
            {
                g.FillPath(brushTagBg, tagPath);
                g.DrawPath(penTagBorder, tagPath);

                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(txtStatus, fontTag, brushTagText, tagRect, sf);
            }

            // Đường gạch ngăn cách
            using (Pen penDiv = new Pen(Color.FromArgb(226, 232, 240), 1f))
            {
                g.DrawLine(penDiv, Sx(8), Sy(68), Width - Sx(8), Sy(68));
            }

            // 4. HIỂN THỊ CẤU HÌNH & LINH KIỆN VÀ TÌNH TRẠNG THIẾT BỊ TRỰC TIẾP TRÊN Ô GIAO DIỆN
            int tbStartY = Sy(72);
            int tbLineH = Sy(20);

            using (Font fontHeaderTB = new Font("Segoe UI", Fpt(7F), FontStyle.Bold))
            using (SolidBrush brushHeaderTB = new SolidBrush(Color.FromArgb(71, 85, 105)))
            {
                g.DrawString($"CẤU HÌNH LINH KIỆN ({Data.DanhSachThietBi.Count}):", fontHeaderTB, brushHeaderTB, new PointF(Sx(8), tbStartY));
            }
            tbStartY += Sy(14);

            if (Data.DanhSachThietBi.Count == 0)
            {
                using (Font fontNone = new Font("Segoe UI", Fpt(7.5F), FontStyle.Italic))
                using (SolidBrush brushNone = new SolidBrush(Color.FromArgb(148, 163, 184)))
                {
                    g.DrawString("(Chưa gắn linh kiện)", fontNone, brushNone, new PointF(Sx(18), tbStartY + Sy(8)));
                }
            }
            else
            {
                int maxHienThi = 4;
                int count = Math.Min(Data.DanhSachThietBi.Count, maxHienThi);

                using (Font fontTenTB = new Font("Segoe UI", Fpt(7.5F), FontStyle.Regular))
                using (Font fontBadgetTB = new Font("Segoe UI", Fpt(6.5F), FontStyle.Bold))
                {
                    for (int i = 0; i < count; i++)
                    {
                        var tb = Data.DanhSachThietBi[i];
                        int itemY = tbStartY + i * tbLineH;

                        Rectangle itemRect = new Rectangle(Sx(8), itemY, Width - Sx(16), Sy(18));
                        bool isLoi = tb.TrangThai.Equals("Hư hỏng", StringComparison.OrdinalIgnoreCase);

                        // Nền cho linh kiện hỏng
                        if (isLoi)
                        {
                            using (SolidBrush brushLoiBg = new SolidBrush(Color.FromArgb(254, 226, 226)))
                            {
                                g.FillRectangle(brushLoiBg, itemRect);
                            }
                        }

                        // Icon + Tên thiết bị (cắt ngắn nếu dài)
                        string icon = tb.GetIcon();
                        string tenRutGon = tb.TenThietBi;
                        if (tenRutGon.Length > 13) tenRutGon = tenRutGon.Substring(0, 11) + "..";

                        string displayStr = $"{icon} {tenRutGon}";
                        Color textCol = isLoi ? Color.FromArgb(185, 28, 28) : Color.FromArgb(30, 41, 59);

                        using (SolidBrush brushText = new SolidBrush(textCol))
                        {
                            g.DrawString(displayStr, fontTenTB, brushText, new PointF(Sx(10), itemY + Sy(1)));
                        }

                        // Badge trạng thái linh kiện bên phải: [🟢 OK] hoặc [🔴 LỖI]
                        Rectangle statusBadgeRect = new Rectangle(Width - Sx(44), itemY + Sy(2), Sx(34), Sy(14));
                        Color badgeBg = isLoi ? Color.FromArgb(239, 68, 68) : Color.FromArgb(209, 250, 229);
                        Color badgeText = isLoi ? Color.White : Color.FromArgb(6, 95, 70);
                        string badgeStr = isLoi ? "HỎNG" : "OK";

                        using (SolidBrush bBg = new SolidBrush(badgeBg))
                        using (SolidBrush bText = new SolidBrush(badgeText))
                        {
                            g.FillRectangle(bBg, statusBadgeRect);
                            StringFormat sfBadge = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                            g.DrawString(badgeStr, fontBadgetTB, bText, statusBadgeRect, sfBadge);
                        }
                    }

                    // Nếu có thêm linh kiện
                    if (Data.DanhSachThietBi.Count > maxHienThi)
                    {
                        int extra = Data.DanhSachThietBi.Count - maxHienThi;
                        using (Font fontExtra = new Font("Segoe UI", Fpt(6.5F), FontStyle.Italic))
                        using (SolidBrush brushExtra = new SolidBrush(Color.FromArgb(100, 116, 139)))
                        {
                            g.DrawString($"+{extra} linh kiện khác...", fontExtra, brushExtra, new PointF(Sx(10), tbStartY + maxHienThi * tbLineH));
                        }
                    }
                }
            }

            // 5. Tọa độ nhỏ hiển thị ở đáy
            using (Font fontCoord = new Font("Segoe UI", Fpt(7F), FontStyle.Regular))
            using (SolidBrush brushCoord = new SolidBrush(Color.FromArgb(148, 163, 184)))
            {
                StringFormat sfCoord = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                Rectangle coordRect = new Rectangle(0, Height - Sy(18), Width, Sy(16));
                g.DrawString($"({Data.ViTriX}, {Data.ViTriY})", fontCoord, brushCoord, coordRect, sfCoord);
            }

            // 6. Biểu tượng khóa vị trí
            bool isLocked = IsGlobalLocked || Data.DaKhoa;
            if (isLocked)
            {
                using (Font fontLock = new Font("Segoe UI", Fpt(8F), FontStyle.Regular))
                using (SolidBrush brushLock = new SolidBrush(Color.FromArgb(220, 38, 38)))
                {
                    g.DrawString("🔒", fontLock, brushLock, new PointF(Sx(6), Height - Sy(18)));
                }
            }

            // Nếu Selected, vẽ dấu tick xanh góc trên bên phải
            if (isSelected)
            {
                using (SolidBrush brushCheck = new SolidBrush(Color.FromArgb(37, 99, 235)))
                {
                    g.FillEllipse(brushCheck, Width - Sx(14), Sy(4), Sx(8), Sy(8));
                }
            }
        }

        private GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
