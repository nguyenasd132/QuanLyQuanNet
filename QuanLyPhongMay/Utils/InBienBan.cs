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

        private void PrintDoc_PrintPage(object? sender, PrintPageEventArgs e)
        {
            Graphics? g = e.Graphics;
            if (g == null) return;

            using (Font fontTieuDe = new Font("Arial", 18, FontStyle.Bold))
            using (Font fontThuong = new Font("Arial", 12, FontStyle.Regular))
            using (Font fontLoi = new Font("Arial", 12, FontStyle.Bold))
            {
                // Vẽ tiêu đề và các trường thông tin bằng tọa độ (X, Y)
                g.DrawString("CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM", fontThuong, Brushes.Black, new Point(220, 40));
                g.DrawString("Độc lập - Tự do - Hạnh phúc", fontThuong, Brushes.Black, new Point(280, 65));
                g.DrawString("BIÊN BẢN BÀN GIAO & BẢO TRÌ THIẾT BỊ", fontTieuDe, Brushes.Black, new Point(160, 110));

                g.DrawString($"Ngày lập phiếu: {DateTime.Now:dd/MM/yyyy HH:mm}", fontThuong, Brushes.Black, new Point(50, 170));
                g.DrawString("Thông tin thiết bị gặp sự cố:", fontThuong, Brushes.Black, new Point(50, 200));
                g.DrawString(thongTinLoi, fontLoi, Brushes.DarkRed, new Point(50, 230));

                g.DrawString("Người lập phiếu\n  (Ký và ghi rõ họ tên)", fontThuong, Brushes.Black, new Point(100, 360));
                g.DrawString("Kỹ thuật viên tiếp nhận\n  (Ký và ghi rõ họ tên)", fontThuong, Brushes.Black, new Point(480, 360));
            }
        }
    }
}
