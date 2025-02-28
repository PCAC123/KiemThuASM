using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmDoiMatKhauDG : Form
    {
        private AccessData dataAccess;

        public fmDoiMatKhauDG()
        {
            InitializeComponent();
            dataAccess = new AccessData();

            SetPasswordChar();
        }

        private void chkMKCu_CheckedChanged(object sender, EventArgs e)
        {
            SetPasswordChar();
        }

        private void chkMKMoi_CheckedChanged(object sender, EventArgs e)
        {
            SetPasswordChar();
        }

        private void chkNhapLaiMKMoi_CheckedChanged(object sender, EventArgs e)
        {
            SetPasswordChar();
        }

        private void SetPasswordChar()
        {
            // Cập nhật thuộc tính PasswordChar cho các TextBox dựa trên trạng thái của các Checkbox
            txtMatKhauCu.PasswordChar = chkMKCu.Checked ? '\0' : '*';
            txtMatKhauMoi.PasswordChar = chkMKMoi.Checked ? '\0' : '*';
            txtXacNhanMatKhau.PasswordChar = chkNhapLaiMKMoi.Checked ? '\0' : '*';
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
