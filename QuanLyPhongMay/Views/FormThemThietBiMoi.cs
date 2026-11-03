using System;
using System.Windows.Forms;
using QuanLyPhongMay.Models;
using QuanLyPhongMay.Utils;

namespace QuanLyPhongMay.Views
{
    public partial class FormThemThietBiMoi : Form
    {
        public ThietBi? ThietBiMoi { get; private set; }
        private string maMay;

        public FormThemThietBiMoi(string maMay)
        {
            this.maMay = maMay;
            InitializeComponent();
            txtMaTB.Text = "TB" + DateTime.Now.ToString("HHmmss");
            cboLoai.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
        }

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            string ma = txtMaTB.Text.Trim();
            string ten = txtTenTB.Text.Trim();
            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên thiết bị!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string loaiStr = cboLoai.SelectedItem?.ToString() ?? "L01";
            string maLoai = loaiStr.Split('-')[0].Trim();
            string tenLoai = loaiStr.Split('-')[1].Trim();
            string tt = cboTrangThai.SelectedItem?.ToString() ?? "Hoạt động";

            ThietBiMoi = new ThietBi(ma, maLoai, maMay, ten, txtThongSo.Text.Trim(), null, tt, tenLoai);

            try
            {
                if (DatabaseHelper.IsConnectedToSql)
                {
                    using (var conn = new Microsoft.Data.SqlClient.SqlConnection(DatabaseHelper.CurrentConnectionString))
                    {
                        conn.Open();
                        string sql = @"INSERT INTO ThietBi (MaThietBi, MaLoai, MaMay, TenThietBi, ThongSo, TrangThai) 
                                       VALUES (@ma, @loai, @may, @ten, @ts, @tt)";
                        using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ma", ma);
                            cmd.Parameters.AddWithValue("@loai", maLoai);
                            cmd.Parameters.AddWithValue("@may", maMay);
                            cmd.Parameters.AddWithValue("@ten", ten);
                            cmd.Parameters.AddWithValue("@ts", txtThongSo.Text.Trim());
                            cmd.Parameters.AddWithValue("@tt", tt);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch { }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnHuy_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
