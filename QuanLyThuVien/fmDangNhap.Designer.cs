namespace QuanLyThuVien
{
    partial class fmDangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelHeThongDangNhap = new System.Windows.Forms.Label();
            this.panelThongTinDangNhap = new System.Windows.Forms.Panel();
            this.chkHienMatKhau = new System.Windows.Forms.CheckBox();
            this.linkLabelQuenMatKhau = new System.Windows.Forms.LinkLabel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.labelTaiKhoan = new System.Windows.Forms.Label();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.labelMatkhau = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.txtTaikhoan = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.panelThongTinDangNhap.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.labelHeThongDangNhap);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(364, 69);
            this.panel2.TabIndex = 17;
            // 
            // labelHeThongDangNhap
            // 
            this.labelHeThongDangNhap.AutoSize = true;
            this.labelHeThongDangNhap.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeThongDangNhap.Location = new System.Drawing.Point(22, 21);
            this.labelHeThongDangNhap.Name = "labelHeThongDangNhap";
            this.labelHeThongDangNhap.Size = new System.Drawing.Size(299, 29);
            this.labelHeThongDangNhap.TabIndex = 14;
            this.labelHeThongDangNhap.Text = "HỆ THỐNG ĐĂNG NHẬP";
            // 
            // panelThongTinDangNhap
            // 
            this.panelThongTinDangNhap.BackColor = System.Drawing.Color.AliceBlue;
            this.panelThongTinDangNhap.Controls.Add(this.chkHienMatKhau);
            this.panelThongTinDangNhap.Controls.Add(this.linkLabelQuenMatKhau);
            this.panelThongTinDangNhap.Controls.Add(this.btnThoat);
            this.panelThongTinDangNhap.Controls.Add(this.labelTaiKhoan);
            this.panelThongTinDangNhap.Controls.Add(this.btnDangNhap);
            this.panelThongTinDangNhap.Controls.Add(this.labelMatkhau);
            this.panelThongTinDangNhap.Controls.Add(this.txtMatKhau);
            this.panelThongTinDangNhap.Controls.Add(this.txtTaikhoan);
            this.panelThongTinDangNhap.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelThongTinDangNhap.Location = new System.Drawing.Point(12, 75);
            this.panelThongTinDangNhap.Name = "panelThongTinDangNhap";
            this.panelThongTinDangNhap.Size = new System.Drawing.Size(340, 293);
            this.panelThongTinDangNhap.TabIndex = 16;
            // 
            // chkHienMatKhau
            // 
            this.chkHienMatKhau.AutoSize = true;
            this.chkHienMatKhau.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHienMatKhau.Location = new System.Drawing.Point(204, 158);
            this.chkHienMatKhau.Name = "chkHienMatKhau";
            this.chkHienMatKhau.Size = new System.Drawing.Size(105, 19);
            this.chkHienMatKhau.TabIndex = 4;
            this.chkHienMatKhau.Text = "Hiện mật khẩu";
            this.chkHienMatKhau.UseVisualStyleBackColor = true;
            this.chkHienMatKhau.CheckedChanged += new System.EventHandler(this.chkHienMatKhau_CheckedChanged);
            // 
            // linkLabelQuenMatKhau
            // 
            this.linkLabelQuenMatKhau.AutoSize = true;
            this.linkLabelQuenMatKhau.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelQuenMatKhau.Location = new System.Drawing.Point(12, 158);
            this.linkLabelQuenMatKhau.Name = "linkLabelQuenMatKhau";
            this.linkLabelQuenMatKhau.Size = new System.Drawing.Size(87, 15);
            this.linkLabelQuenMatKhau.TabIndex = 5;
            this.linkLabelQuenMatKhau.TabStop = true;
            this.linkLabelQuenMatKhau.Text = "Quên mật khẩu";
            this.linkLabelQuenMatKhau.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelQuenMatKhau_LinkClicked);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(232, 223);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(92, 43);
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "THOÁT";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // labelTaiKhoan
            // 
            this.labelTaiKhoan.AutoSize = true;
            this.labelTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTaiKhoan.Location = new System.Drawing.Point(12, 21);
            this.labelTaiKhoan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTaiKhoan.Name = "labelTaiKhoan";
            this.labelTaiKhoan.Size = new System.Drawing.Size(74, 19);
            this.labelTaiKhoan.TabIndex = 7;
            this.labelTaiKhoan.Text = "Tài khoản";
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap.Location = new System.Drawing.Point(15, 223);
            this.btnDangNhap.Margin = new System.Windows.Forms.Padding(4);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(156, 43);
            this.btnDangNhap.TabIndex = 6;
            this.btnDangNhap.Text = "ĐĂNG NHẬP";
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // labelMatkhau
            // 
            this.labelMatkhau.AutoSize = true;
            this.labelMatkhau.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMatkhau.Location = new System.Drawing.Point(12, 101);
            this.labelMatkhau.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMatkhau.Name = "labelMatkhau";
            this.labelMatkhau.Size = new System.Drawing.Size(71, 19);
            this.labelMatkhau.TabIndex = 8;
            this.labelMatkhau.Text = "Mật khẩu";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(16, 124);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(4);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(308, 27);
            this.txtMatKhau.TabIndex = 3;
            this.txtMatKhau.UseSystemPasswordChar = true;
            this.txtMatKhau.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMatKhau_KeyDown);
            // 
            // txtTaikhoan
            // 
            this.txtTaikhoan.Location = new System.Drawing.Point(16, 44);
            this.txtTaikhoan.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaikhoan.Name = "txtTaikhoan";
            this.txtTaikhoan.Size = new System.Drawing.Size(308, 27);
            this.txtTaikhoan.TabIndex = 2;
            // 
            // fmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(364, 380);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelThongTinDangNhap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelThongTinDangNhap.ResumeLayout(false);
            this.panelThongTinDangNhap.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelHeThongDangNhap;
        private System.Windows.Forms.Panel panelThongTinDangNhap;
        private System.Windows.Forms.CheckBox chkHienMatKhau;
        private System.Windows.Forms.LinkLabel linkLabelQuenMatKhau;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label labelTaiKhoan;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Label labelMatkhau;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.TextBox txtTaikhoan;
    }
}

