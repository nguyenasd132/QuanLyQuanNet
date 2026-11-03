using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyPhongMay.Views
{
    public class MapCanvasPanel : Panel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowGrid { get; set; } = true;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int GridSize { get; set; } = 20;

        public MapCanvasPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.AutoScroll = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!ShowGrid || GridSize <= 5) return;

            Graphics g = e.Graphics;

            // Lấy kích thước hiển thị bao gồm cả thanh cuộn nếu có
            int width = Math.Max(this.Width, this.DisplayRectangle.Width);
            int height = Math.Max(this.Height, this.DisplayRectangle.Height);

            // Vẽ các chấm điểm lưới (Grid dots) màu xám nhạt tinh tế
            using (SolidBrush dotBrush = new SolidBrush(Color.FromArgb(203, 213, 225)))
            {
                int startX = this.AutoScrollPosition.X % GridSize;
                int startY = this.AutoScrollPosition.Y % GridSize;

                for (int x = startX; x < width; x += GridSize)
                {
                    for (int y = startY; y < height; y += GridSize)
                    {
                        g.FillRectangle(dotBrush, x, y, 1.5f, 1.5f);
                    }
                }
            }
        }
    }
}
