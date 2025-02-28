using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmTimKiemSach : Form
    {
        private AccessData accessData;

        public fmTimKiemSach()
        {
            InitializeComponent();
            accessData = new AccessData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            string criteria = GetSelectedCriteria();
            if (string.IsNullOrEmpty(criteria))
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm.");
                return;
            }
            string query = BuildQuery(keyword, criteria);

            DataTable searchResults = accessData.ExecuteQuery(query);
            dgvSach.DataSource = searchResults;
        }

        private string GetSelectedCriteria()
        {
            if (rdoTenSach.Checked) return "TenSach";
            if (rdoTheLoai.Checked) return "TenTheLoai"; // Sử dụng "TenTheLoai" để tìm kiếm trong bảng THELOAI

            return ""; // Trả về một giá trị mặc định nếu không có lựa chọn nào được chọn
        }

        private string BuildQuery(string keyword, string criteria)
        {
            string query = "SELECT S.MaSach, S.TenSach, S.MaTheLoai, S.SoLuong, S.MaNXB, S.NamXB, T.TenTheLoai " +
                           "FROM SACH S " +
                           "INNER JOIN THELOAI T ON S.MaTheLoai = T.MaTheLoai " +
                           "WHERE 1=1";

            if (!string.IsNullOrEmpty(keyword))
            {
                switch (criteria)
                {
                    case "TenSach":
                        query += $" AND S.TenSach LIKE N'%{keyword}%'";
                        break;
                    case "TenTheLoai":
                        query += $" AND T.TenTheLoai LIKE N'%{keyword}%'";
                        break;
                    default:
                        MessageBox.Show("Tiêu chí tìm kiếm không hợp lệ.");
                        return "";
                }
            }

            return query;
        }
    }
}
