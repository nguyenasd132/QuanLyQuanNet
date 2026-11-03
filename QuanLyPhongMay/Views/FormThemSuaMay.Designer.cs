namespace QuanLyPhongMay.Views
{
    partial class FormThemSuaMay
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
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderSub = new System.Windows.Forms.Label();
            this.lblMaMay = new System.Windows.Forms.Label();
            this.txtMaMay = new System.Windows.Forms.TextBox();
            this.lblTenMay = new System.Windows.Forms.Label();
            this.txtTenMay = new System.Windows.Forms.TextBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblToaDo = new System.Windows.Forms.Label();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.lblKhoa = new System.Windows.Forms.Label();
            this.chkDaKhoa = new System.Windows.Forms.CheckBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.pnlHeader.Controls.Add(this.lblHeaderSub);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlHeader.Size = new System.Drawing.Size(460, 65);
            this.pnlHeader.TabIndex = 0;

            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(18, 12);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(221, 25);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "➕ THÊM MÁY TÍNH MỚI";

            // 
            // lblHeaderSub
            // 
            this.lblHeaderSub.AutoSize = true;
            this.lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.lblHeaderSub.Location = new System.Drawing.Point(20, 38);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.Size = new System.Drawing.Size(294, 15);
            this.lblHeaderSub.TabIndex = 1;
            this.lblHeaderSub.Text = "Nhập thông tin máy để bố trí vào sơ đồ phòng máy";

            // 
            // lblMaMay
            // 
            this.lblMaMay.AutoSize = true;
            this.lblMaMay.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMaMay.Location = new System.Drawing.Point(25, 80);
            this.lblMaMay.Name = "lblMaMay";
            this.lblMaMay.Size = new System.Drawing.Size(81, 17);
            this.lblMaMay.TabIndex = 1;
            this.lblMaMay.Text = "Mã máy (*):";

            // 
            // txtMaMay
            // 
            this.txtMaMay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaMay.Location = new System.Drawing.Point(140, 77);
            this.txtMaMay.Name = "txtMaMay";
            this.txtMaMay.Size = new System.Drawing.Size(270, 25);
            this.txtMaMay.TabIndex = 2;

            // 
            // lblTenMay
            // 
            this.lblTenMay.AutoSize = true;
            this.lblTenMay.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTenMay.Location = new System.Drawing.Point(25, 126);
            this.lblTenMay.Name = "lblTenMay";
            this.lblTenMay.Size = new System.Drawing.Size(83, 17);
            this.lblTenMay.TabIndex = 3;
            this.lblTenMay.Text = "Tên máy (*):";

            // 
            // txtTenMay
            // 
            this.txtTenMay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenMay.Location = new System.Drawing.Point(140, 123);
            this.txtTenMay.Name = "txtTenMay";
            this.txtTenMay.Size = new System.Drawing.Size(270, 25);
            this.txtTenMay.TabIndex = 4;

            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.Location = new System.Drawing.Point(25, 172);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(75, 17);
            this.lblTrangThai.TabIndex = 5;
            this.lblTrangThai.Text = "Trạng thái:";

            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Hoạt động",
            "Hư hỏng",
            "Bảo trì"});
            this.cboTrangThai.Location = new System.Drawing.Point(140, 169);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(270, 25);
            this.cboTrangThai.TabIndex = 6;

            // 
            // lblToaDo
            // 
            this.lblToaDo.AutoSize = true;
            this.lblToaDo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblToaDo.Location = new System.Drawing.Point(25, 218);
            this.lblToaDo.Name = "lblToaDo";
            this.lblToaDo.Size = new System.Drawing.Size(95, 17);
            this.lblToaDo.TabIndex = 7;
            this.lblToaDo.Text = "Tọa độ (X, Y):";

            // 
            // numX
            // 
            this.numX.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numX.Location = new System.Drawing.Point(140, 215);
            this.numX.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(125, 25);
            this.numX.TabIndex = 8;

            // 
            // numY
            // 
            this.numY.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numY.Location = new System.Drawing.Point(285, 215);
            this.numY.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(125, 25);
            this.numY.TabIndex = 9;

            // 
            // lblKhoa
            // 
            this.lblKhoa.AutoSize = true;
            this.lblKhoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKhoa.Location = new System.Drawing.Point(25, 264);
            this.lblKhoa.Name = "lblKhoa";
            this.lblKhoa.Size = new System.Drawing.Size(63, 17);
            this.lblKhoa.TabIndex = 10;
            this.lblKhoa.Text = "Khóa vị trí:";

            // 
            // chkDaKhoa
            // 
            this.chkDaKhoa.AutoSize = true;
            this.chkDaKhoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkDaKhoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkDaKhoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.chkDaKhoa.Location = new System.Drawing.Point(140, 263);
            this.chkDaKhoa.Name = "chkDaKhoa";
            this.chkDaKhoa.Size = new System.Drawing.Size(326, 19);
            this.chkDaKhoa.TabIndex = 11;
            this.chkDaKhoa.Text = "🔒 Khóa cố định vị trí (ngăn không cho kéo thả máy này)";
            this.chkDaKhoa.UseVisualStyleBackColor = true;

            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblGhiChu.Location = new System.Drawing.Point(25, 304);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(59, 17);
            this.lblGhiChu.TabIndex = 12;
            this.lblGhiChu.Text = "Ghi chú:";

            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtGhiChu.Location = new System.Drawing.Point(140, 301);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(270, 65);
            this.txtGhiChu.TabIndex = 13;

            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlBottom.Controls.Add(this.btnHuy);
            this.pnlBottom.Controls.Add(this.btnLuu);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 440);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBottom.Size = new System.Drawing.Size(460, 60);
            this.pnlBottom.TabIndex = 14;

            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(175, 12);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(125, 36);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "➕ Thêm máy";
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
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnHuy.Location = new System.Drawing.Point(310, 12);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 36);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy bỏ";
            this.btnHuy.UseVisualStyleBackColor = false;

            // 
            // FormThemSuaMay
            // 
            this.AcceptButton = this.btnLuu;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(460, 500);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.chkDaKhoa);
            this.Controls.Add(this.lblKhoa);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.numX);
            this.Controls.Add(this.lblToaDo);
            this.Controls.Add(this.cboTrangThai);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.txtTenMay);
            this.Controls.Add(this.lblTenMay);
            this.Controls.Add(this.txtMaMay);
            this.Controls.Add(this.lblMaMay);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormThemSuaMay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm máy tính mới";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSub;
        private System.Windows.Forms.Label lblMaMay;
        private System.Windows.Forms.TextBox txtMaMay;
        private System.Windows.Forms.Label lblTenMay;
        private System.Windows.Forms.TextBox txtTenMay;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblToaDo;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.Label lblKhoa;
        private System.Windows.Forms.CheckBox chkDaKhoa;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}
