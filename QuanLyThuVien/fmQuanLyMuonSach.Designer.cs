namespace QuanLyThuVien
{
    partial class fmQuanLyMuonSach
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbbMaDG = new System.Windows.Forms.ComboBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.txtSoLuonCon = new System.Windows.Forms.TextBox();
            this.cbMasach = new System.Windows.Forms.ComboBox();
            this.dtpNgayTra = new System.Windows.Forms.DateTimePicker();
            this.btnMuonSach = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.dtpNgayMuon = new System.Windows.Forms.DateTimePicker();
            this.txtHoTenDG = new System.Windows.Forms.TextBox();
            this.txtTenSach = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGiaHan = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvPhieuMuon = new System.Windows.Forms.DataGridView();
            this.cmbPhieuMuon = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpNgayGiaHan = new System.Windows.Forms.DateTimePicker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuon)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox2.Controls.Add(this.cbbMaDG);
            this.groupBox2.Controls.Add(this.btnShow);
            this.groupBox2.Controls.Add(this.txtSoLuonCon);
            this.groupBox2.Controls.Add(this.cbMasach);
            this.groupBox2.Controls.Add(this.dtpNgayTra);
            this.groupBox2.Controls.Add(this.btnMuonSach);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtSoLuong);
            this.groupBox2.Controls.Add(this.dtpNgayMuon);
            this.groupBox2.Controls.Add(this.txtHoTenDG);
            this.groupBox2.Controls.Add(this.txtTenSach);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.groupBox2.Location = new System.Drawing.Point(12, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(433, 324);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin Mượn Sách";
            // 
            // cbbMaDG
            // 
            this.cbbMaDG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMaDG.FormattingEnabled = true;
            this.cbbMaDG.Location = new System.Drawing.Point(38, 100);
            this.cbbMaDG.Name = "cbbMaDG";
            this.cbbMaDG.Size = new System.Drawing.Size(146, 27);
            this.cbbMaDG.TabIndex = 37;
            // 
            // btnShow
            // 
            this.btnShow.BackColor = System.Drawing.Color.Cyan;
            this.btnShow.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnShow.Location = new System.Drawing.Point(275, 199);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(123, 41);
            this.btnShow.TabIndex = 35;
            this.btnShow.Text = "Gia Hạn";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // txtSoLuonCon
            // 
            this.txtSoLuonCon.Location = new System.Drawing.Point(233, 157);
            this.txtSoLuonCon.Name = "txtSoLuonCon";
            this.txtSoLuonCon.Size = new System.Drawing.Size(180, 27);
            this.txtSoLuonCon.TabIndex = 36;
            // 
            // cbMasach
            // 
            this.cbMasach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMasach.FormattingEnabled = true;
            this.cbMasach.Location = new System.Drawing.Point(37, 46);
            this.cbMasach.Name = "cbMasach";
            this.cbMasach.Size = new System.Drawing.Size(146, 27);
            this.cbMasach.TabIndex = 35;
            this.cbMasach.SelectedIndexChanged += new System.EventHandler(this.cbMasach_SelectedIndexChanged_1);
            // 
            // dtpNgayTra
            // 
            this.dtpNgayTra.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayTra.Location = new System.Drawing.Point(37, 229);
            this.dtpNgayTra.Name = "dtpNgayTra";
            this.dtpNgayTra.Size = new System.Drawing.Size(147, 27);
            this.dtpNgayTra.TabIndex = 33;
            // 
            // btnMuonSach
            // 
            this.btnMuonSach.BackColor = System.Drawing.Color.Aqua;
            this.btnMuonSach.Location = new System.Drawing.Point(275, 255);
            this.btnMuonSach.Name = "btnMuonSach";
            this.btnMuonSach.Size = new System.Drawing.Size(123, 42);
            this.btnMuonSach.TabIndex = 10;
            this.btnMuonSach.Text = "Mượn Sách";
            this.btnMuonSach.UseVisualStyleBackColor = false;
            this.btnMuonSach.Click += new System.EventHandler(this.btnMuonSach_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 199);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 19);
            this.label10.TabIndex = 20;
            this.label10.Text = "Ngày Trả:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 19);
            this.label9.TabIndex = 22;
            this.label9.Text = "Ngày Mượn:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(37, 281);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(147, 27);
            this.txtSoLuong.TabIndex = 30;
            // 
            // dtpNgayMuon
            // 
            this.dtpNgayMuon.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayMuon.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayMuon.Location = new System.Drawing.Point(36, 163);
            this.dtpNgayMuon.Name = "dtpNgayMuon";
            this.dtpNgayMuon.Size = new System.Drawing.Size(147, 27);
            this.dtpNgayMuon.TabIndex = 33;
            // 
            // txtHoTenDG
            // 
            this.txtHoTenDG.ForeColor = System.Drawing.Color.Black;
            this.txtHoTenDG.Location = new System.Drawing.Point(233, 100);
            this.txtHoTenDG.Name = "txtHoTenDG";
            this.txtHoTenDG.Size = new System.Drawing.Size(180, 27);
            this.txtHoTenDG.TabIndex = 29;
            this.txtHoTenDG.Text = "Tên độc giả";
            // 
            // txtTenSach
            // 
            this.txtTenSach.ForeColor = System.Drawing.Color.Black;
            this.txtTenSach.Location = new System.Drawing.Point(233, 46);
            this.txtTenSach.Name = "txtTenSach";
            this.txtTenSach.Size = new System.Drawing.Size(180, 27);
            this.txtTenSach.TabIndex = 29;
            this.txtTenSach.Text = "Tên sách";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 19);
            this.label6.TabIndex = 21;
            this.label6.Text = "SL Mượn:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 19);
            this.label1.TabIndex = 18;
            this.label1.Text = "Nhập Mã Sách:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 19);
            this.label5.TabIndex = 18;
            this.label5.Text = "Nhập Mã ĐG:";
            // 
            // btnGiaHan
            // 
            this.btnGiaHan.BackColor = System.Drawing.Color.Aqua;
            this.btnGiaHan.Location = new System.Drawing.Point(77, 236);
            this.btnGiaHan.Name = "btnGiaHan";
            this.btnGiaHan.Size = new System.Drawing.Size(114, 42);
            this.btnGiaHan.TabIndex = 9;
            this.btnGiaHan.Text = "Gia Hạn";
            this.btnGiaHan.UseVisualStyleBackColor = false;
            this.btnGiaHan.Click += new System.EventHandler(this.btnGiaHan_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgvPhieuMuon);
            this.groupBox6.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.groupBox6.Location = new System.Drawing.Point(12, 356);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(705, 200);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Danh sách Mượn";
            // 
            // dgvPhieuMuon
            // 
            this.dgvPhieuMuon.AllowUserToResizeColumns = false;
            this.dgvPhieuMuon.AllowUserToResizeRows = false;
            this.dgvPhieuMuon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhieuMuon.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhieuMuon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhieuMuon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhieuMuon.Location = new System.Drawing.Point(3, 23);
            this.dgvPhieuMuon.Name = "dgvPhieuMuon";
            this.dgvPhieuMuon.ReadOnly = true;
            this.dgvPhieuMuon.RowHeadersWidth = 51;
            this.dgvPhieuMuon.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvPhieuMuon.RowTemplate.Height = 24;
            this.dgvPhieuMuon.Size = new System.Drawing.Size(699, 174);
            this.dgvPhieuMuon.TabIndex = 0;
            // 
            // cmbPhieuMuon
            // 
            this.cmbPhieuMuon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPhieuMuon.FormattingEnabled = true;
            this.cmbPhieuMuon.Location = new System.Drawing.Point(60, 73);
            this.cmbPhieuMuon.Name = "cmbPhieuMuon";
            this.cmbPhieuMuon.Size = new System.Drawing.Size(147, 27);
            this.cmbPhieuMuon.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 19);
            this.label2.TabIndex = 21;
            this.label2.Text = "Phiếu Mượn:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 19);
            this.label3.TabIndex = 20;
            this.label3.Text = "Ngày Gia Hạn:";
            // 
            // dtpNgayGiaHan
            // 
            this.dtpNgayGiaHan.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayGiaHan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayGiaHan.Location = new System.Drawing.Point(60, 155);
            this.dtpNgayGiaHan.Name = "dtpNgayGiaHan";
            this.dtpNgayGiaHan.Size = new System.Drawing.Size(147, 27);
            this.dtpNgayGiaHan.TabIndex = 33;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox5.Controls.Add(this.cmbPhieuMuon);
            this.groupBox5.Controls.Add(this.btnGiaHan);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.dtpNgayGiaHan);
            this.groupBox5.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.groupBox5.Location = new System.Drawing.Point(460, 26);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(257, 324);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Thông tin Gia Hạn";
            // 
            // fmQuanLyMuonSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(729, 558);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fmQuanLyMuonSach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ MƯỢN SÁCH";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuon)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TextBox txtTenSach;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpNgayTra;
        private System.Windows.Forms.DateTimePicker dtpNgayMuon;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnGiaHan;
        private System.Windows.Forms.Button btnMuonSach;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgvPhieuMuon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHoTenDG;
        private System.Windows.Forms.ComboBox cmbPhieuMuon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpNgayGiaHan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtSoLuonCon;
        private System.Windows.Forms.ComboBox cbMasach;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.ComboBox cbbMaDG;
    }
}