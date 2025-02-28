using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmDoiMatKhauTT : Form
    {
        private AccessData dataAccess;

        public fmDoiMatKhauTT()
        {
            InitializeComponent();
            dataAccess = new AccessData();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                DoiMatKhau();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhauCu.Text) ||
                string.IsNullOrEmpty(txtMatKhauMoi.Text) || string.IsNullOrEmpty(txtXacNhanMatKhau.Text))
            {
                lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin.";
                return false;
            }

            if (txtMatKhauMoi.Text != txtXacNhanMatKhau.Text)
            {
                lblThongBao.Text = "Mật khẩu mới và xác nhận mật khẩu không khớp.";
                return false;
            }

            return true;
        }

        private void DoiMatKhau()
        {
            try
            {
                string tenDangNhap = txtTenDangNhap.Text;
                string matKhauCu = txtMatKhauCu.Text; // Mã hóa mật khẩu cũ trước khi so sánh
                string matKhauMoi = txtMatKhauMoi.Text; // Mã hóa mật khẩu mới trước khi lưu

                using (SqlConnection conn = new SqlConnection(dataAccess.ConnectionString))
                {
                    conn.Open();

                    // Kiểm tra mật khẩu cũ
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM user_roles WHERE username = @Username AND password = @Password", conn);
                    cmd.Parameters.AddWithValue("@Username", tenDangNhap);
                    cmd.Parameters.AddWithValue("@Password", matKhauCu); // Mã hóa mật khẩu cũ ở đây

                    int count = (int)cmd.ExecuteScalar();

                    if (count == 0)
                    {
                        lblThongBao.Text = "Mật khẩu cũ không chính xác.";
                        return;
                    }

                    // Cập nhật mật khẩu mới
                    cmd = new SqlCommand("UPDATE user_roles SET password = @NewPassword WHERE username = @Username", conn);
                    cmd.Parameters.AddWithValue("@NewPassword", matKhauMoi); // Mã hóa mật khẩu mới ở đây
                    cmd.Parameters.AddWithValue("@Username", tenDangNhap);

                    cmd.ExecuteNonQuery();

                    lblThongBao.Text = "Đổi mật khẩu thành công.";
                }
            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Đã xảy ra lỗi khi đổi mật khẩu: " + ex.Message;
            }
        }
    }
}
