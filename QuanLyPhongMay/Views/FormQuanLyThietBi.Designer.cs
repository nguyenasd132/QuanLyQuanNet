namespace QuanLyPhongMay.Views
{
    partial class FormQuanLyThietBi
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.dgvThietBi = new System.Windows.Forms.DataGridView();
            this.colMaThietBi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenThietBi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThongSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnThemThietBiMoi = new System.Windows.Forms.Button();
            this.btnDoiTrangThai = new System.Windows.Forms.Button();
            this.btnThaoThietBi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlHeader.Size = new System.Drawing.Size(720, 65);
            this.pnlHeader.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "⚙️ CẤU HÌNH & LINH KIỆN MÁY";

            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSub.Location = new System.Drawing.Point(17, 36);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(360, 15);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "Quản lý các linh kiện phần cứng gắn trong máy tính • Thay thế • Báo hỏng";

            // 
            // pnlCenter
            // 
            this.pnlCenter.Controls.Add(this.dgvThietBi);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 65);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Padding = new System.Windows.Forms.Padding(15);
            this.pnlCenter.Size = new System.Drawing.Size(720, 380);
            this.pnlCenter.TabIndex = 1;

            // 
            // dgvThietBi
            // 
            this.dgvThietBi.AllowUserToAddRows = false;
            this.dgvThietBi.AllowUserToDeleteRows = false;
            this.dgvThietBi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThietBi.BackgroundColor = System.Drawing.Color.White;
            this.dgvThietBi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvThietBi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThietBi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaThietBi,
            this.colTenLoai,
            this.colTenThietBi,
            this.colThongSo,
            this.colTrangThai});
            this.dgvThietBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThietBi.Location = new System.Drawing.Point(15, 15);
            this.dgvThietBi.MultiSelect = false;
            this.dgvThietBi.Name = "dgvThietBi";
            this.dgvThietBi.ReadOnly = true;
            this.dgvThietBi.RowHeadersVisible = false;
            this.dgvThietBi.RowTemplate.Height = 32;
            this.dgvThietBi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThietBi.Size = new System.Drawing.Size(690, 350);
            this.dgvThietBi.TabIndex = 0;
            this.dgvThietBi.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvThietBi_CellFormatting);

            // 
            // colMaThietBi
            // 
            this.colMaThietBi.FillWeight = 50F;
            this.colMaThietBi.HeaderText = "Mã TB";
            this.colMaThietBi.Name = "colMaThietBi";
            this.colMaThietBi.ReadOnly = true;

            // 
            // colTenLoai
            // 
            this.colTenLoai.FillWeight = 60F;
            this.colTenLoai.HeaderText = "Loại";
            this.colTenLoai.Name = "colTenLoai";
            this.colTenLoai.ReadOnly = true;

            // 
            // colTenThietBi
            // 
            this.colTenThietBi.FillWeight = 110F;
            this.colTenThietBi.HeaderText = "Tên Linh Kiện / Thiết Bị";
            this.colTenThietBi.Name = "colTenThietBi";
            this.colTenThietBi.ReadOnly = true;

            // 
            // colThongSo
            // 
            this.colThongSo.FillWeight = 100F;
            this.colThongSo.HeaderText = "Thông Số Kỹ Thuật";
            this.colThongSo.Name = "colThongSo";
            this.colThongSo.ReadOnly = true;

            // 
            // colTrangThai
            // 
            this.colTrangThai.FillWeight = 60F;
            this.colTrangThai.HeaderText = "Tình Trạng";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;

            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlBottom.Controls.Add(this.btnDong);
            this.pnlBottom.Controls.Add(this.btnThaoThietBi);
            this.pnlBottom.Controls.Add(this.btnDoiTrangThai);
            this.pnlBottom.Controls.Add(this.btnThemThietBiMoi);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 445);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlBottom.Size = new System.Drawing.Size(720, 55);
            this.pnlBottom.TabIndex = 2;

            // 
            // btnThemThietBiMoi
            // 
            this.btnThemThietBiMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnThemThietBiMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemThietBiMoi.FlatAppearance.BorderSize = 0;
            this.btnThemThietBiMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemThietBiMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemThietBiMoi.ForeColor = System.Drawing.Color.White;
            this.btnThemThietBiMoi.Location = new System.Drawing.Point(15, 10);
            this.btnThemThietBiMoi.Name = "btnThemThietBiMoi";
            this.btnThemThietBiMoi.Size = new System.Drawing.Size(160, 34);
            this.btnThemThietBiMoi.TabIndex = 0;
            this.btnThemThietBiMoi.Text = "➕ Gắn linh kiện mới";
            this.btnThemThietBiMoi.UseVisualStyleBackColor = false;
            this.btnThemThietBiMoi.Click += new System.EventHandler(this.BtnThemThietBiMoi_Click);

            // 
            // btnDoiTrangThai
            // 
            this.btnDoiTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnDoiTrangThai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDoiTrangThai.FlatAppearance.BorderSize = 0;
            this.btnDoiTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoiTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDoiTrangThai.ForeColor = System.Drawing.Color.White;
            this.btnDoiTrangThai.Location = new System.Drawing.Point(185, 10);
            this.btnDoiTrangThai.Name = "btnDoiTrangThai";
            this.btnDoiTrangThai.Size = new System.Drawing.Size(160, 34);
            this.btnDoiTrangThai.TabIndex = 1;
            this.btnDoiTrangThai.Text = "🔄 Đổi trạng thái TB";
            this.btnDoiTrangThai.UseVisualStyleBackColor = false;
            this.btnDoiTrangThai.Click += new System.EventHandler(this.BtnDoiTrangThai_Click);

            // 
            // btnThaoThietBi
            // 
            this.btnThaoThietBi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnThaoThietBi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThaoThietBi.FlatAppearance.BorderSize = 0;
            this.btnThaoThietBi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThaoThietBi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThaoThietBi.ForeColor = System.Drawing.Color.White;
            this.btnThaoThietBi.Location = new System.Drawing.Point(355, 10);
            this.btnThaoThietBi.Name = "btnThaoThietBi";
            this.btnThaoThietBi.Size = new System.Drawing.Size(140, 34);
            this.btnThaoThietBi.TabIndex = 2;
            this.btnThaoThietBi.Text = "➖ Tháo linh kiện";
            this.btnThaoThietBi.UseVisualStyleBackColor = false;
            this.btnThaoThietBi.Click += new System.EventHandler(this.BtnThaoThietBi_Click);

            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.White;
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDong.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnDong.Location = new System.Drawing.Point(590, 10);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 34);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;

            // 
            // FormQuanLyThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(720, 500);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormQuanLyThietBi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý Cấu hình & Linh kiện";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.DataGridView dgvThietBi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaThietBi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenThietBi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThongSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnThemThietBiMoi;
        private System.Windows.Forms.Button btnDoiTrangThai;
        private System.Windows.Forms.Button btnThaoThietBi;
        private System.Windows.Forms.Button btnDong;
    }
}
