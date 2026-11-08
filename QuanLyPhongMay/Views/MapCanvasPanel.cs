using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace QuanLyPhongMay.Views
{
    public class MapCanvasPanel : Panel
    {
        private Image? _backgroundMapImage = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowGrid { get; set; } = true;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int GridSize { get; set; } = 20;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string? BackgroundMapPath { get; private set; } = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float MapOpacity { get; set; } = 0.88f;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool StretchMapToCanvas { get; set; } = true;

        public bool HasBackgroundMap => _backgroundMapImage != null;

        public MapCanvasPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);
            this.UpdateStyles();
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.AutoScroll = true;
        }

        /// <summary>
        /// Nạp ảnh bản đồ mặt bằng phòng máy từ đường dẫn tuyệt đối hoặc tương đối.
        /// Đọc qua MemoryStream để không khóa (lock) tệp ảnh trên ổ cứng.
        /// </summary>
        public bool LoadBackgroundMap(string? imagePath)
        {
            ClearBackgroundMap(false);

            if (string.IsNullOrWhiteSpace(imagePath))
            {
                Invalidate();
                return false;
            }

            try
            {
                string resolvedPath = imagePath;
                if (!Path.IsPathRooted(resolvedPath))
                {
                    string candidateBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, resolvedPath);
                    if (File.Exists(candidateBase))
                    {
                        resolvedPath = candidateBase;
                    }
                    else
                    {
                        string candidateWork = Path.GetFullPath(resolvedPath);
                        if (File.Exists(candidateWork))
                        {
                            resolvedPath = candidateWork;
                        }
                    }
                }

                if (!File.Exists(resolvedPath))
                {
                    Invalidate();
                    return false;
                }

                byte[] bytes = File.ReadAllBytes(resolvedPath);
                using (MemoryStream ms = new MemoryStream(bytes))
                using (Image tempImg = Image.FromStream(ms))
                {
                    _backgroundMapImage = new Bitmap(tempImg);
                }

                BackgroundMapPath = imagePath;
                Invalidate();
                return true;
            }
            catch
            {
                ClearBackgroundMap(false);
                Invalidate();
                return false;
            }
        }

        /// <summary>
        /// Xóa ảnh bản đồ nền hiện tại của phòng máy và giải phóng bộ nhớ GDI+.
        /// </summary>
        public void ClearBackgroundMap(bool redraw = true)
        {
            if (_backgroundMapImage != null)
            {
                _backgroundMapImage.Dispose();
                _backgroundMapImage = null;
            }
            BackgroundMapPath = null;
            if (redraw)
            {
                Invalidate();
            }
        }

        /// <summary>
        /// Tự động tạo một bản đồ mặt bằng kiến trúc mẫu (bàn giáo viên, cửa ra vào, lối đi, dãy bàn)
        /// lưu ra file PNG để người dùng có thể trải nghiệm ngay nếu chưa có sẵn file ảnh ngoài.
        /// </summary>
        public static string GenerateSampleFloorPlanImage(string maPhong, string tenPhong, string outputFilePath)
        {
            int width = 1280;
            int height = 860;

            Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath)!);

            using (Bitmap bmp = new Bitmap(width, height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                // Nền bản vẽ kỹ thuật sáng dịu
                g.Clear(Color.FromArgb(241, 245, 249));

                // Viền tường bao quanh phòng máy
                Rectangle wallRect = new Rectangle(12, 12, width - 24, height - 24);
                using (SolidBrush floorBrush = new SolidBrush(Color.FromArgb(248, 250, 252)))
                using (Pen wallPen = new Pen(Color.FromArgb(100, 116, 139), 4f))
                {
                    g.FillRectangle(floorBrush, wallRect);
                    g.DrawRectangle(wallPen, wallRect);
                }

                // Khu vực Bàn Giáo Viên / Máy Chủ Quản Lý + Bảng chiếu ở phía trên hoặc bên trái
                using (SolidBrush zoneBrush = new SolidBrush(Color.FromArgb(224, 242, 254)))
                using (Pen zonePen = new Pen(Color.FromArgb(56, 189, 248), 2f) { DashStyle = DashStyle.Dash })
                using (Font zoneFont = new Font("Segoe UI", 10f, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(3, 105, 161)))
                {
                    Rectangle teacherZone = new Rectangle(960, 28, 290, 130);
                    g.FillRectangle(zoneBrush, teacherZone);
                    g.DrawRectangle(zonePen, teacherZone);
                    g.DrawString("KHU VỰC BÀN GIẢNG VIÊN\n& TỦ RACK SWITCH / SERVER", zoneFont, textBrush, new PointF(975, 65));
                }

                // Cửa chính ra vào phòng máy
                using (SolidBrush doorBrush = new SolidBrush(Color.FromArgb(254, 243, 199)))
                using (Pen doorPen = new Pen(Color.FromArgb(217, 119, 6), 2f))
                using (Font doorFont = new Font("Segoe UI", 9.5f, FontStyle.Bold))
                using (SolidBrush doorText = new SolidBrush(Color.FromArgb(180, 83, 9)))
                {
                    Rectangle doorRect = new Rectangle(960, height - 68, 290, 44);
                    g.FillRectangle(doorBrush, doorRect);
                    g.DrawRectangle(doorPen, doorRect);
                    g.DrawString("🚪 CỬA RA VÀO CHÍNH - HÀNH LANG", doorFont, doorText, new PointF(990, height - 56));
                }

                // Vẽ các dãy bàn thực hành gợi ý (3 hàng dãy bàn đôi)
                using (SolidBrush deskBrush = new SolidBrush(Color.FromArgb(226, 232, 240)))
                using (Pen deskPen = new Pen(Color.FromArgb(148, 163, 184), 1.5f))
                using (Font rowFont = new Font("Segoe UI", 9f, FontStyle.Italic))
                using (SolidBrush rowText = new SolidBrush(Color.FromArgb(100, 116, 139)))
                {
                    int[] rowYs = { 16, 236, 456, 660 };
                    for (int r = 0; r < rowYs.Length; r++)
                    {
                        Rectangle rowZone = new Rectangle(16, rowYs[r], 920, 188);
                        g.FillRectangle(deskBrush, rowZone);
                        g.DrawRectangle(deskPen, rowZone);
                        g.DrawString($"DÃY BÀN THỰC HÀNH SỐ {r + 1} (Kéo thả máy vào các ô vị trí địa lý trên dãy bàn)", rowFont, rowText, new PointF(26, rowYs[r] + 165));
                    }
                }

                // Lối đi kỹ thuật dọc phòng
                using (Font aisleFont = new Font("Segoe UI", 9.5f, FontStyle.Bold))
                using (SolidBrush aisleBrush = new SolidBrush(Color.FromArgb(148, 163, 184)))
                {
                    g.DrawString("⬆ LỐI ĐI CHÍNH & HỆ THỐNG MÁNG CÁP MẠNG LAN / ĐIỆN ÂM SÀN ⬇", aisleFont, aisleBrush, new PointF(220, 211));
                    g.DrawString("⬆ LỐI ĐI PHỤ GIỮA CÁC DÃY BÀN THỰC HÀNH ⬇", aisleFont, aisleBrush, new PointF(290, 431));
                }

                // Nhãn tên bản đồ góc phải
                using (Font titleFont = new Font("Segoe UI", 11f, FontStyle.Bold))
                using (SolidBrush titleBrush = new SolidBrush(Color.FromArgb(51, 65, 85)))
                {
                    g.DrawString($"SƠ ĐỒ MẶT BẰNG ĐỊA LÝ:\n{maPhong} - {tenPhong}", titleFont, titleBrush, new PointF(965, 190));
                }

                bmp.Save(outputFilePath, ImageFormat.Png);
            }

            return outputFilePath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            int scrollX = this.AutoScrollPosition.X;
            int scrollY = this.AutoScrollPosition.Y;

            // Lấy kích thước vùng làm việc bao gồm cả vùng cuộn (AutoScroll)
            int canvasWidth = Math.Max(this.ClientSize.Width, this.DisplayRectangle.Width);
            int canvasHeight = Math.Max(this.ClientSize.Height, this.DisplayRectangle.Height);

            // 1. Vẽ ảnh bản đồ nền của phòng máy (nếu có)
            if (_backgroundMapImage != null)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Rectangle destRect = StretchMapToCanvas
                    ? new Rectangle(scrollX, scrollY, canvasWidth, canvasHeight)
                    : new Rectangle(scrollX, scrollY, _backgroundMapImage.Width, _backgroundMapImage.Height);

                if (MapOpacity >= 0.99f)
                {
                    g.DrawImage(_backgroundMapImage, destRect);
                }
                else
                {
                    ColorMatrix matrix = new ColorMatrix
                    {
                        Matrix33 = Math.Clamp(MapOpacity, 0.1f, 1.0f)
                    };
                    using (ImageAttributes attributes = new ImageAttributes())
                    {
                        attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                        g.DrawImage(
                            _backgroundMapImage,
                            destRect,
                            0,
                            0,
                            _backgroundMapImage.Width,
                            _backgroundMapImage.Height,
                            GraphicsUnit.Pixel,
                            attributes);
                    }
                }

                // Vẽ nhãn watermark nhỏ góc dưới bên phải báo hiệu đang bật bản đồ nền
                string mapTag = $"🗺️ Bản đồ nền: {Path.GetFileName(BackgroundMapPath)}";
                using (Font tagFont = new Font("Segoe UI", 8f, FontStyle.Bold))
                using (SolidBrush tagBg = new SolidBrush(Color.FromArgb(210, 15, 23, 42)))
                using (SolidBrush tagFg = new SolidBrush(Color.White))
                {
                    SizeF tagSize = g.MeasureString(mapTag, tagFont);
                    RectangleF pillRect = new RectangleF(
                        this.ClientSize.Width - tagSize.Width - 24,
                        this.ClientSize.Height - tagSize.Height - 16,
                        tagSize.Width + 14,
                        tagSize.Height + 6);
                    g.FillRectangle(tagBg, pillRect);
                    g.DrawString(mapTag, tagFont, tagFg, pillRect.X + 7, pillRect.Y + 3);
                }
            }

            // 2. Vẽ các chấm điểm lưới (Grid dots) hỗ trợ căn chỉnh vị trí Snap-to-Grid
            if (ShowGrid && GridSize > 5)
            {
                Color dotColor = _backgroundMapImage != null
                    ? Color.FromArgb(150, 148, 163, 184)
                    : Color.FromArgb(203, 213, 225);

                using (SolidBrush dotBrush = new SolidBrush(dotColor))
                {
                    int startX = scrollX % GridSize;
                    int startY = scrollY % GridSize;

                    for (int x = startX; x < canvasWidth; x += GridSize)
                    {
                        for (int y = startY; y < canvasHeight; y += GridSize)
                        {
                            g.FillRectangle(dotBrush, x, y, 1.5f, 1.5f);
                        }
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClearBackgroundMap(false);
            }
            base.Dispose(disposing);
        }
    }
}
