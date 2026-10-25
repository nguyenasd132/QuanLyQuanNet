using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace QuanLyPhongMay.Utils
{
    public class InBienBan
    {
        private PrintDocument printDoc = new PrintDocument();
        private PrintPreviewDialog previewDlg = new PrintPreviewDialog();
        private string thongTinLoi;

        public InBienBan(string thongTinLoi)
        {
            this.thongTinLoi = thongTinLoi;
            printDoc.PrintPage += PrintDoc_PrintPage;

            // Cấu hình giao diện cửa sổ xem trước
            previewDlg.Document = printDoc;
            previewDlg.Width = 800;
            previewDlg.Height = 600;
        }

        public void HienThiBanIn()
        {
            previewDlg.ShowDialog();
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fontTieuDe = new Font("Arial", 18, FontStyle.Bold);
            Font fontThuong = new Font("Arial", 12, FontStyle.Regular);

            // Vẽ tiêu đề và các trường thông tin bằng tọa độ (X, Y)
            g.DrawString("CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM", fontThuong, Brushes.Black, new Point(220, 40));
            g.DrawString("BIÊN BẢN BÀN GIAO & BẢO TRÌ THIẾT BỊ", fontTieuDe, Brushes.Black, new Point(200, 100));

            g.DrawString($"Ngày lập phiếu: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", fontThuong, Brushes.Black, new Point(50, 160));
            g.DrawString("Thông tin thiết bị gặp sự cố:", fontThuong, Brushes.Black, new Point(50, 190));
            g.DrawString(thongTinLoi, new Font("Arial", 12, FontStyle.Bold), Brushes.DarkRed, new Point(50, 220));

            g.DrawString("Người lập phiếu", fontThuong, Brushes.Black, new Point(100, 320));
            g.DrawString("Kỹ thuật viên tiếp nhận", fontThuong, Brushes.Black, new Point(500, 320));
        }
    }
}