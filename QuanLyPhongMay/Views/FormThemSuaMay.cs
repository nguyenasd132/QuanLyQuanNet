using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyPhongMay.Models;

namespace QuanLyPhongMay.Views
{
    public partial class FormThemSuaMay : Form
    {
        public MayTinh MayTinhData { get; private set; }
        public bool IsEditMode { get; private set; }

        public FormThemSuaMay(MayTinh? may = null, int defaultX = 20, int defaultY = 20)
        {
            IsEditMode = may != null;
            MayTinhData = may != null 
                ? new MayTinh(may.MaMay, may.TenMay, may.TrangThai, may.ViTriX, may.ViTriY, may.MaPhong, may.GhiChu, may.DaKhoa)
                : new MayTinh("", "", "Hoạt động", defaultX, defaultY, "PM01", "", false);

            InitializeComponent();
            ApplyModeUI();
            LoadDataToUI();
        }

        private void ApplyModeUI()
        {
            if (IsEditMode)
            {
                this.Text = "Cập nhật thông tin máy tính";
                pnlHeader.BackColor = Color.FromArgb(37, 99, 235);
                lblHeaderTitle.Text = "✏️ CẬP NHẬT MÁY TÍNH";
                lblHeaderSub.Text = "Chỉnh sửa thông số, trạng thái và vị trí trên sơ đồ";
                txtMaMay.ReadOnly = true;
                txtMaMay.BackColor = Color.FromArgb(241, 245, 249);
                btnLuu.Text = "💾 Cập nhật";
                btnLuu.BackColor = Color.FromArgb(37, 99, 235);
            }
            else
            {
                this.Text = "Thêm máy tính mới";
                pnlHeader.BackColor = Color.FromArgb(16, 185, 129);
                lblHeaderTitle.Text = "➕ THÊM MÁY TÍNH MỚI";
                lblHeaderSub.Text = "Nhập thông tin máy để bố trí vào sơ đồ phòng máy";
                txtMaMay.ReadOnly = false;
                txtMaMay.BackColor = Color.White;
                btnLuu.Text = "➕ Thêm máy";
                btnLuu.BackColor = Color.FromArgb(16, 185, 129);
            }
        }

        private void LoadDataToUI()
        {
            txtMaMay.Text = MayTinhData.MaMay;
            txtTenMay.Text = MayTinhData.TenMay;
            cboTrangThai.SelectedItem = string.IsNullOrEmpty(MayTinhData.TrangThai) ? "Hoạt động" : MayTinhData.TrangThai;
            numX.Value = Math.Max(0, MayTinhData.ViTriX);
            numY.Value = Math.Max(0, MayTinhData.ViTriY);
            chkDaKhoa.Checked = MayTinhData.DaKhoa;
            txtGhiChu.Text = MayTinhData.GhiChu;
        }

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            string ma = txtMaMay.Text.Trim();
            string ten = txtTenMay.Text.Trim();

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng nhập Mã máy!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaMay.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập Tên máy!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMay.Focus();
                return;
            }

            MayTinhData.MaMay = ma;
            MayTinhData.TenMay = ten;
            MayTinhData.TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Hoạt động";
            MayTinhData.ViTriX = (int)numX.Value;
            MayTinhData.ViTriY = (int)numY.Value;
            MayTinhData.DaKhoa = chkDaKhoa.Checked;
            MayTinhData.GhiChu = txtGhiChu.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
