using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyPhongMay.Models;

namespace QuanLyPhongMay.Views
{
    public partial class FormSoDo : Form
    {
        private Panel panelSoDo;

        public FormSoDo()
        {
            InitializeComponent();
            ThietLapGiaoDien();
            TaoDuLieuGiaVaVeSoDo();
        }

        private void ThietLapGiaoDien()
        {
            this.Size = new Size(700, 500);
            this.Text = "Sơ đồ Vị trí Máy tính";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Tạo một Panel động để chứa sơ đồ, cho phép cuộn chuột nếu sơ đồ lớn
            panelSoDo = new Panel();
            panelSoDo.Dock = DockStyle.Fill;
            panelSoDo.AutoScroll = true;
            panelSoDo.BackColor = Color.White;
            this.Controls.Add(panelSoDo);
        }

        private void TaoDuLieuGiaVaVeSoDo()
        {
            // 1. Tạo 20 máy tính giả
            List<MayTinh> danhSachMay = new List<MayTinh>();
            for (int i = 1; i <= 20; i++)
            {
                string trangThai = "Hoạt động";
                if (i == 5 || i == 12) trangThai = "Hư hỏng";
                if (i == 8) trangThai = "Bảo trì";

                danhSachMay.Add(new MayTinh { MaMay = "M" + i, TenMay = "Máy " + i, TrangThai = trangThai });
            }

            // 2. Thuật toán chia tọa độ X, Y để vẽ sơ đồ động
            int chieuRongO = 100;
            int chieuCaoO = 80;
            int khoangCach = 20;
            int soCot = 5; // Hiển thị 5 máy mỗi hàng

            for (int i = 0; i < danhSachMay.Count; i++)
            {
                // Phép chia lấy nguyên và lấy dư để tìm ra tọa độ
                int hang = i / soCot;
                int cot = i % soCot;

                int x = khoangCach + cot * (chieuRongO + khoangCach);
                int y = khoangCach + hang * (chieuCaoO + khoangCach);

                Button btnMay = new Button();
                btnMay.Size = new Size(chieuRongO, chieuCaoO);
                btnMay.Location = new Point(x, y);
                btnMay.Text = $"{danhSachMay[i].TenMay}\n({danhSachMay[i].TrangThai})";
                btnMay.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                btnMay.FlatStyle = FlatStyle.Flat;

                // Tự động đổi màu dựa trên trạng thái
                if (danhSachMay[i].TrangThai == "Hư hỏng")
                    btnMay.BackColor = Color.LightCoral;
                else if (danhSachMay[i].TrangThai == "Bảo trì")
                    btnMay.BackColor = Color.Khaki;
                else
                    btnMay.BackColor = Color.LightGreen;
                // Bắt sự kiện click để in biên bản
                MayTinh mayHienTai = danhSachMay[i];
                btnMay.Click += (s, e) =>
                {
                    if (mayHienTai.TrangThai != "Hoạt động")
                    {
                        Utils.InBienBan inPhieu = new Utils.InBienBan($"- Mã thiết bị: {mayHienTai.MaMay}\n- Tên máy: {mayHienTai.TenMay}\n- Tình trạng: {mayHienTai.TrangThai}");
                        inPhieu.HienThiBanIn();
                    }
                    else
                    {
                        MessageBox.Show("Thiết bị đang hoạt động bình thường, không cần bảo trì!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };
                panelSoDo.Controls.Add(btnMay);
            }
        }
    }
}