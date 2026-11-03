using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyPhongMay.Models;
using QuanLyPhongMay.Utils;

namespace QuanLyPhongMay.Views
{
    public partial class FormQuanLyThietBi : Form
    {
        private MayTinh mayTinh;
        public bool DataChanged { get; private set; } = false;

        public FormQuanLyThietBi(MayTinh may)
        {
            this.mayTinh = may;
            InitializeComponent();
            ApplyDataHeader();
            TaiDanhSachThietBi();
        }

        private void ApplyDataHeader()
        {
            this.Text = $"Quản lý Cấu hình & Linh kiện - {mayTinh.TenMay} ({mayTinh.MaMay})";
            lblTitle.Text = $"⚙️ CẤU HÌNH & LINH KIỆN: {mayTinh.TenMay} ({mayTinh.MaMay})";
            lblSub.Text = $"Phòng: {mayTinh.MaPhong} • Trạng thái máy: {mayTinh.TrangThai} • Quản lý các linh kiện phần cứng gắn trong máy";
        }

        private void DgvThietBi_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvThietBi.Columns[e.ColumnIndex].Name == "colTrangThai" && e.Value != null)
            {
                string status = e.Value.ToString() ?? "";
                if (status.Equals("Hư hỏng", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle!.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle!.ForeColor = Color.FromArgb(5, 150, 105);
                    e.CellStyle.BackColor = Color.FromArgb(236, 253, 245);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        private void TaiDanhSachThietBi()
        {
            dgvThietBi.Rows.Clear();
            foreach (var tb in mayTinh.DanhSachThietBi)
            {
                dgvThietBi.Rows.Add(
                    tb.MaThietBi,
                    $"{tb.GetIcon()} {tb.TenLoai}",
                    tb.TenThietBi,
                    tb.ThongSo,
                    tb.TrangThai
                );
            }
        }

        private void BtnDoiTrangThai_Click(object? sender, EventArgs e)
        {
            if (dgvThietBi.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một linh kiện để đổi trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maTB = dgvThietBi.CurrentRow.Cells["colMaThietBi"].Value?.ToString() ?? "";
            var tb = mayTinh.DanhSachThietBi.Find(t => t.MaThietBi == maTB);
            if (tb == null) return;

            string trangThaiMoi = tb.TrangThai.Equals("Hoạt động", StringComparison.OrdinalIgnoreCase) ? "Hư hỏng" : "Hoạt động";
            tb.TrangThai = trangThaiMoi;

            DatabaseHelper.CapNhatTrangThaiThietBi(tb.MaThietBi, trangThaiMoi);

            if (trangThaiMoi == "Hư hỏng")
            {
                string maSC = "SC" + DateTime.Now.ToString("ddHHmmss");
                PhieuSuCo phieu = new PhieuSuCo(maSC, mayTinh.MaMay, tb.MaThietBi, 1, $"Sự cố linh kiện {tb.TenThietBi}: Báo hỏng tại máy {mayTinh.TenMay}");
                DatabaseHelper.TaoPhieuSuCo(phieu);

                mayTinh.TrangThai = "Hư hỏng";
                DatabaseHelper.CapNhatThongTinMay(mayTinh);
            }

            DataChanged = true;
            TaiDanhSachThietBi();
            MessageBox.Show($"Đã cập nhật trạng thái thiết bị '{tb.TenThietBi}' thành '{trangThaiMoi}'!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnThaoThietBi_Click(object? sender, EventArgs e)
        {
            if (dgvThietBi.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một linh kiện để tháo ra khỏi máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maTB = dgvThietBi.CurrentRow.Cells["colMaThietBi"].Value?.ToString() ?? "";
            var tb = mayTinh.DanhSachThietBi.Find(t => t.MaThietBi == maTB);
            if (tb == null) return;

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn tháo linh kiện '{tb.TenThietBi}' ra khỏi {mayTinh.TenMay} để chuyển về kho không?",
                "Xác nhận tháo linh kiện",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                mayTinh.DanhSachThietBi.Remove(tb);
                tb.MaMay = null;
                DatabaseHelper.GanThietBiVaoMay(tb.MaThietBi, null);

                DataChanged = true;
                TaiDanhSachThietBi();
                MessageBox.Show($"Đã tháo linh kiện '{tb.TenThietBi}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnThemThietBiMoi_Click(object? sender, EventArgs e)
        {
            using (FormThemThietBiMoi frm = new FormThemThietBiMoi(mayTinh.MaMay))
            {
                if (frm.ShowDialog(this) == DialogResult.OK && frm.ThietBiMoi != null)
                {
                    mayTinh.DanhSachThietBi.Add(frm.ThietBiMoi);
                    DataChanged = true;
                    TaiDanhSachThietBi();
                    MessageBox.Show($"Đã gắn thêm linh kiện '{frm.ThietBiMoi.TenThietBi}' vào {mayTinh.TenMay}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
