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

        // Biến phục vụ kéo thả
        private bool isDragging = false;
        private Point dragStartPoint;
        private bool isHovered = false;
        private ToolTip toolTipInfo;

        public MayTinhControl(MayTinh data)
        {
            Data = data;
            // Kích thước mở rộng để hiển thị trực tiếp danh sách linh kiện & tình trạng thiết bị
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

        public void CapNhatThongTin(MayTinh data)
        {
            Data = data;
            this.Location = new Point(data.ViTriX, data.ViTriY);
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
                    Data.ViTriX = newX;
                    Data.ViTriY = newY;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

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
            using (GraphicsPath path = GetRoundedRectangle(rect, 8))
            {
                using (SolidBrush brushBg = new SolidBrush(bgCard))
                {
                    g.FillPath(brushBg, path);
                }

                if (isHovered && !isSelected)
                {
                    using (Pen penHover = new Pen(Color.FromArgb(96, 165, 250), 2))
                    {
                        g.DrawPath(penHover, path);
                    }
                }
                else if (isSelected)
                {
                    using (Pen penSelected = new Pen(Color.FromArgb(37, 99, 235), 3))
                    {
                        g.DrawPath(penSelected, path);
                    }
                }
                else
                {
                    using (Pen penBorder = new Pen(borderColor, coThietBiLoi ? 2f : 1.5f))
                    {
                        g.DrawPath(penBorder, path);
                    }
                }
            }

            // 1. Icon màn hình máy tính
            int iconX = 12;
            int iconY = 8;
            int iconW = 28;
            int iconH = 20;

            using (SolidBrush brushScreen = new SolidBrush(screenBg))
            using (Pen penScreen = new Pen(screenIconColor, 1.5f))
            {
                g.FillRectangle(brushScreen, iconX, iconY, iconW, iconH);
                g.DrawRectangle(penScreen, iconX, iconY, iconW, iconH);
                g.DrawLine(penScreen, iconX + iconW / 2, iconY + iconH, iconX + iconW / 2, iconY + iconH + 3);
                g.DrawLine(penScreen, iconX + 6, iconY + iconH + 3, iconX + iconW - 6, iconY + iconH + 3);
            }

            // 2. Tên máy & Mã máy
            using (Font fontTen = new Font("Segoe UI", 9.5F, FontStyle.Bold))
            using (Font fontMa = new Font("Segoe UI", 7.5F, FontStyle.Regular))
            using (SolidBrush brushTen = new SolidBrush(Color.FromArgb(15, 23, 42)))
            using (SolidBrush brushMa = new SolidBrush(Color.FromArgb(100, 116, 139)))
            {
                g.DrawString(Data.TenMay, fontTen, brushTen, new PointF(46, 7));
                g.DrawString($"ID: {Data.MaMay}", fontMa, brushMa, new PointF(47, 24));
            }

            // 3. Badge trạng thái máy
            string txtStatus = Data.TrangThai;
            if (coThietBiLoi && Data.TrangThai == "Hoạt động")
            {
                txtStatus = $"Lỗi {Data.SoThietBiHuHong()} TB";
            }

            Rectangle tagRect = new Rectangle(10, 42, Width - 20, 20);
            using (GraphicsPath tagPath = GetRoundedRectangle(tagRect, 4))
            using (SolidBrush brushTagBg = new SolidBrush(tagBg))
            using (Pen penTagBorder = new Pen(tagBorder, 1))
            using (Font fontTag = new Font("Segoe UI", 8F, FontStyle.Bold))
            using (SolidBrush brushTagText = new SolidBrush(tagText))
            {
                g.FillPath(brushTagBg, tagPath);
                g.DrawPath(penTagBorder, tagPath);

                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(txtStatus, fontTag, brushTagText, tagRect, sf);
            }

            // Đường gạch ngăn cách
            using (Pen penDiv = new Pen(Color.FromArgb(226, 232, 240), 1))
            {
                g.DrawLine(penDiv, 8, 68, Width - 8, 68);
            }

            // 4. HIỂN THỊ CẤU HÌNH & LINH KIỆN VÀ TÌNH TRẠNG THIẾT BỊ TRỰC TIẾP TRÊN Ô GIAO DIỆN
            int tbStartY = 72;
            int tbLineH = 20;

            using (Font fontHeaderTB = new Font("Segoe UI", 7F, FontStyle.Bold))
            using (SolidBrush brushHeaderTB = new SolidBrush(Color.FromArgb(71, 85, 105)))
            {
                g.DrawString($"CẤU HÌNH LINH KIỆN ({Data.DanhSachThietBi.Count}):", fontHeaderTB, brushHeaderTB, new PointF(8, tbStartY));
            }
            tbStartY += 14;

            if (Data.DanhSachThietBi.Count == 0)
            {
                using (Font fontNone = new Font("Segoe UI", 7.5F, FontStyle.Italic))
                using (SolidBrush brushNone = new SolidBrush(Color.FromArgb(148, 163, 184)))
                {
                    g.DrawString("(Chưa gắn linh kiện)", fontNone, brushNone, new PointF(18, tbStartY + 8));
                }
            }
            else
            {
                int maxHienThi = 4;
                int count = Math.Min(Data.DanhSachThietBi.Count, maxHienThi);

                using (Font fontTenTB = new Font("Segoe UI", 7.5F, FontStyle.Regular))
                using (Font fontBadgetTB = new Font("Segoe UI", 6.5F, FontStyle.Bold))
                {
                    for (int i = 0; i < count; i++)
                    {
                        var tb = Data.DanhSachThietBi[i];
                        int itemY = tbStartY + i * tbLineH;

                        Rectangle itemRect = new Rectangle(8, itemY, Width - 16, 18);
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
                            g.DrawString(displayStr, fontTenTB, brushText, new PointF(10, itemY + 1));
                        }

                        // Badge trạng thái linh kiện bên phải: [🟢 OK] hoặc [🔴 LỖI]
                        Rectangle statusBadgeRect = new Rectangle(Width - 44, itemY + 2, 34, 14);
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
                        using (Font fontExtra = new Font("Segoe UI", 6.5F, FontStyle.Italic))
                        using (SolidBrush brushExtra = new SolidBrush(Color.FromArgb(100, 116, 139)))
                        {
                            g.DrawString($"+{extra} linh kiện khác...", fontExtra, brushExtra, new PointF(10, tbStartY + maxHienThi * tbLineH));
                        }
                    }
                }
            }

            // 5. Tọa độ nhỏ hiển thị ở đáy
            using (Font fontCoord = new Font("Segoe UI", 7F, FontStyle.Regular))
            using (SolidBrush brushCoord = new SolidBrush(Color.FromArgb(148, 163, 184)))
            {
                StringFormat sfCoord = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                Rectangle coordRect = new Rectangle(0, Height - 18, Width, 16);
                g.DrawString($"({Data.ViTriX}, {Data.ViTriY})", fontCoord, brushCoord, coordRect, sfCoord);
            }

            // 6. Biểu tượng khóa vị trí
            bool isLocked = IsGlobalLocked || Data.DaKhoa;
            if (isLocked)
            {
                using (Font fontLock = new Font("Segoe UI", 8F, FontStyle.Regular))
                using (SolidBrush brushLock = new SolidBrush(Color.FromArgb(220, 38, 38)))
                {
                    g.DrawString("🔒", fontLock, brushLock, new PointF(6, Height - 18));
                }
            }

            // Nếu Selected, vẽ dấu tick xanh góc trên bên phải
            if (isSelected)
            {
                using (SolidBrush brushCheck = new SolidBrush(Color.FromArgb(37, 99, 235)))
                {
                    g.FillEllipse(brushCheck, Width - 14, 4, 8, 8);
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
