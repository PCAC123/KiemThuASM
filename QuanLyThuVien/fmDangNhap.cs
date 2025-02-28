using QLTV;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmDangNhap : Form
    {
        public fmDangNhap()
        {
            InitializeComponent();          
        }
        public class AuthenticationService
        {
            private AccessData accessData = new AccessData();

            public bool AuthenticateUser(string username, string password, out int roleId)
            {
                roleId = -1;
                string query = "SELECT password, role_id FROM user_roles WHERE username = @Username";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Username", username)
                };

                DataTable result = accessData.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    string storedPassword = result.Rows[0]["password"].ToString();
                    roleId = Convert.ToInt32(result.Rows[0]["role_id"]);

                    // So sánh mật khẩu (trong thực tế, bạn nên mã hóa mật khẩu)
                    return storedPassword == password;
                }

                return false;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTaikhoan.Text;
            string password = txtMatKhau.Text;
            AuthenticationService authService = new AuthenticationService();

            if (authService.AuthenticateUser(username, password, out int roleId))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Chuyển hướng tới form chính dựa trên role
                if (roleId == 1) // ThuThu
                {
                    fmMainThuThu mainForm = new fmMainThuThu();
                    mainForm.Show();
                }
                else if (roleId == 2) // DocGia
                {
                    fmMainDocGia mainForm = new fmMainDocGia();
                    mainForm.Show();
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap_Click(sender, e);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabelQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fmQuenMatKhau fmQuenMatKhau = new fmQuenMatKhau();
            fmQuenMatKhau.Show();
        }
    }
}
