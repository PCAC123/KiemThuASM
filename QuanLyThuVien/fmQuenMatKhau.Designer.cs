namespace QLTV
{
    partial class fmQuenMatKhau
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
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.linkLabelQuayLaiDangNhap = new System.Windows.Forms.LinkLabel();
            this.btnGuiYeuCau = new System.Windows.Forms.Button();
            this.labelTenDangNhap = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDangNhap.Location = new System.Drawing.Point(23, 42);
            this.txtTenDangNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTenDangNhap.Multiline = true;
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(363, 30);
            this.txtTenDangNhap.TabIndex = 27;
            // 
            // linkLabelQuayLaiDangNhap
            // 
            this.linkLabelQuayLaiDangNhap.AutoSize = true;
            this.linkLabelQuayLaiDangNhap.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelQuayLaiDangNhap.LinkColor = System.Drawing.Color.Blue;
            this.linkLabelQuayLaiDangNhap.Location = new System.Drawing.Point(20, 126);
            this.linkLabelQuayLaiDangNhap.Name = "linkLabelQuayLaiDangNhap";
            this.linkLabelQuayLaiDangNhap.Size = new System.Drawing.Size(111, 15);
            this.linkLabelQuayLaiDangNhap.TabIndex = 26;
            this.linkLabelQuayLaiDangNhap.TabStop = true;
            this.linkLabelQuayLaiDangNhap.Text = "Quay lại đăng nhập";
            this.linkLabelQuayLaiDangNhap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelQuayLaiDangNhap_LinkClicked);
            // 
            // btnGuiYeuCau
            // 
            this.btnGuiYeuCau.BackColor = System.Drawing.Color.Teal;
            this.btnGuiYeuCau.FlatAppearance.BorderSize = 0;
            this.btnGuiYeuCau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuiYeuCau.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuiYeuCau.ForeColor = System.Drawing.Color.White;
            this.btnGuiYeuCau.Location = new System.Drawing.Point(231, 102);
            this.btnGuiYeuCau.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGuiYeuCau.Name = "btnGuiYeuCau";
            this.btnGuiYeuCau.Size = new System.Drawing.Size(155, 39);
            this.btnGuiYeuCau.TabIndex = 25;
            this.btnGuiYeuCau.Text = "GỬI YÊU CẦU";
            this.btnGuiYeuCau.UseVisualStyleBackColor = false;
            this.btnGuiYeuCau.Click += new System.EventHandler(this.btnGuiYeuCau_Click);
            // 
            // labelTenDangNhap
            // 
            this.labelTenDangNhap.AutoSize = true;
            this.labelTenDangNhap.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.labelTenDangNhap.Location = new System.Drawing.Point(18, 15);
            this.labelTenDangNhap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTenDangNhap.Name = "labelTenDangNhap";
            this.labelTenDangNhap.Size = new System.Drawing.Size(158, 20);
            this.labelTenDangNhap.TabIndex = 24;
            this.labelTenDangNhap.Text = "Nhập tên đăng nhập:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.btnGuiYeuCau);
            this.panel1.Controls.Add(this.txtTenDangNhap);
            this.panel1.Controls.Add(this.labelTenDangNhap);
            this.panel1.Controls.Add(this.linkLabelQuayLaiDangNhap);
            this.panel1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(22, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(407, 169);
            this.panel1.TabIndex = 28;
            // 
            // fmQuenMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(452, 216);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fmQuenMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.LinkLabel linkLabelQuayLaiDangNhap;
        private System.Windows.Forms.Button btnGuiYeuCau;
        private System.Windows.Forms.Label labelTenDangNhap;
        private System.Windows.Forms.Panel panel1;
    }
}