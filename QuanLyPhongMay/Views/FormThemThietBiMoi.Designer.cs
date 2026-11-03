namespace QuanLyPhongMay.Views
{
    partial class FormThemThietBiMoi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null!;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMa = new System.Windows.Forms.Label();
            this.txtMaTB = new System.Windows.Forms.TextBox();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTenTB = new System.Windows.Forms.TextBox();
            this.lblTS = new System.Windows.Forms.Label();
            this.txtThongSo = new System.Windows.Forms.TextBox();
            this.lblTT = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // lblMa
            // 
            this.lblMa.AutoSize = true;
            this.lblMa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMa.Location = new System.Drawing.Point(20, 20);
            this.lblMa.Name = "lblMa";
            this.lblMa.Size = new System.Drawing.Size(102, 17);
            this.lblMa.TabIndex = 0;
            this.lblMa.Text = "Mã thiết bị (*):";

            // 
            // txtMaTB
            // 
            this.txtMaTB.Location = new System.Drawing.Point(140, 18);
            this.txtMaTB.Name = "txtMaTB";
            this.txtMaTB.Size = new System.Drawing.Size(260, 24);
            this.txtMaTB.TabIndex = 1;

            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblLoai.Location = new System.Drawing.Point(20, 62);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(87, 17);
            this.lblLoai.TabIndex = 2;
            this.lblLoai.Text = "Loại thiết bị:";

            // 
            // cboLoai
            // 
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Items.AddRange(new object[] {
            "L01 - Màn hình",
            "L02 - Bàn phím",
            "L03 - Chuột",
            "L04 - RAM",
            "L05 - CPU",
            "L06 - VGA",
            "L07 - Nguồn",
            "L08 - Tai nghe"});
            this.cboLoai.Location = new System.Drawing.Point(140, 60);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(260, 23);
            this.cboLoai.TabIndex = 3;

            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTen.Location = new System.Drawing.Point(20, 104);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(104, 17);
            this.lblTen.TabIndex = 4;
            this.lblTen.Text = "Tên thiết bị (*):";

            // 
            // txtTenTB
            // 
            this.txtTenTB.Location = new System.Drawing.Point(140, 102);
            this.txtTenTB.Name = "txtTenTB";
            this.txtTenTB.PlaceholderText = "Ví dụ: Màn hình Dell 24 inch";
            this.txtTenTB.Size = new System.Drawing.Size(260, 24);
            this.txtTenTB.TabIndex = 5;

            // 
            // lblTS
            // 
            this.lblTS.AutoSize = true;
            this.lblTS.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTS.Location = new System.Drawing.Point(20, 146);
            this.lblTS.Name = "lblTS";
            this.lblTS.Size = new System.Drawing.Size(71, 17);
            this.lblTS.TabIndex = 6;
            this.lblTS.Text = "Thông số:";

            // 
            // txtThongSo
            // 
            this.txtThongSo.Location = new System.Drawing.Point(140, 144);
            this.txtThongSo.Name = "txtThongSo";
            this.txtThongSo.PlaceholderText = "Ví dụ: 1920x1080 75Hz";
            this.txtThongSo.Size = new System.Drawing.Size(260, 24);
            this.txtThongSo.TabIndex = 7;

            // 
            // lblTT
            // 
            this.lblTT.AutoSize = true;
            this.lblTT.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTT.Location = new System.Drawing.Point(20, 188);
            this.lblTT.Name = "lblTT";
            this.lblTT.Size = new System.Drawing.Size(75, 17);
            this.lblTT.TabIndex = 8;
            this.lblTT.Text = "Trạng thái:";

            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Hoạt động",
            "Hư hỏng"});
            this.cboTrangThai.Location = new System.Drawing.Point(140, 186);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(260, 23);
            this.cboTrangThai.TabIndex = 9;

            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(140, 238);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(130, 34);
            this.btnLuu.TabIndex = 10;
            this.btnLuu.Text = "💾 Gắn vào máy";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.BtnLuu_Click);

            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.White;
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Location = new System.Drawing.Point(285, 238);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(115, 34);
            this.btnHuy.TabIndex = 11;
            this.btnHuy.Text = "Hủy bỏ";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);

            // 
            // FormThemThietBiMoi
            // 
            this.AcceptButton = this.btnLuu;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(440, 300);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.cboTrangThai);
            this.Controls.Add(this.lblTT);
            this.Controls.Add(this.txtThongSo);
            this.Controls.Add(this.lblTS);
            this.Controls.Add(this.txtTenTB);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.cboLoai);
            this.Controls.Add(this.lblLoai);
            this.Controls.Add(this.txtMaTB);
            this.Controls.Add(this.lblMa);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormThemThietBiMoi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gắn thêm Linh kiện / Thiết bị mới";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.TextBox txtMaTB;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTenTB;
        private System.Windows.Forms.Label lblTS;
        private System.Windows.Forms.TextBox txtThongSo;
        private System.Windows.Forms.Label lblTT;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}
