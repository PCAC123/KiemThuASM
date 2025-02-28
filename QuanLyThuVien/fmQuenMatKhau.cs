using QuanLyThuVien;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLTV
{
    public partial class fmQuenMatKhau : Form
    {
        private AccessData dataAccess;

        public fmQuenMatKhau()
        {
            InitializeComponent();
            dataAccess = new AccessData();
        }

        private void btnGuiYeuCau_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTenDangNhap.Text.Trim();
            if (string.IsNullOrEmpty(tenTaiKhoan))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.");
                return;
            }

            string query = "SELECT password FROM user_roles WHERE username = @username";
            SqlParameter[] parameters =
            {
                new SqlParameter("@username", SqlDbType.NVarChar) { Value = tenTaiKhoan }
            };

            object result = dataAccess.ExecuteScalar(query, parameters);

            if (result != null && result != DBNull.Value)
            {
                string matKhau = result.ToString();
                MessageBox.Show($"Mật khẩu của bạn là: {matKhau}. Vui lòng ghi nhớ mật khẩu của mình.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập không tồn tại trong hệ thống. Vui lòng kiểm tra lại.");
            }
        }

        private void linkLabelQuayLaiDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
