using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmKhoa : Form
    {
        private AccessData accessData;

        public fmKhoa()
        {
            InitializeComponent();
            accessData = new AccessData();
            LoadData(); // Load data when the form is initialized

            dgvKhoa.CellClick += dgvKhoa_CellClick; // Register the CellClick event
        }

        // Load data for DataGridView and ComboBox
        private void LoadData()
        {
            DataTable khoaTable = accessData.GetKhoa();
            dgvKhoa.DataSource = khoaTable;

            // Set the column names for DataGridView
            dgvKhoa.Columns["MaKhoa"].HeaderText = "Mã Khoa";
            dgvKhoa.Columns["TenKhoa"].HeaderText = "Tên Khoa";

            // Load data for ComboBox cbMaKhoa
            cbMaKhoa.DataSource = khoaTable;
            cbMaKhoa.DisplayMember = "MaKhoa";
            cbMaKhoa.ValueMember = "MaKhoa";
        }

        // Handle the CellClick event for DataGridView
        private void dgvKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhoa.Rows[e.RowIndex];

                // Get the selected Khoa ID and set it in the ComboBox
                if (row.Cells["MaKhoa"].Value != null)
                {
                    cbMaKhoa.SelectedValue = row.Cells["MaKhoa"].Value;
                }

                // Get the selected Khoa name and set it in the TextBox
                if (row.Cells["TenKhoa"].Value != null)
                {
                    txtTenKhoa.Text = row.Cells["TenKhoa"].Value.ToString();
                }
            }
        }

        // Add a new Khoa
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenKhoa = txtTenKhoa.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenKhoa))
            {
                MessageBox.Show("Tên khoa không được để trống.");
                return;
            }

            // Check if the Khoa already exists
            if (IsDuplicate(tenKhoa))
            {
                MessageBox.Show("Tên khoa này đã tồn tại.");
                return;
            }

            string query = "INSERT INTO Khoa (TenKhoa) VALUES (@TenKhoa)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenKhoa", tenKhoa)
            };
            accessData.ExecuteNonQuery(query, parameters);
            MessageBox.Show("Thêm khoa thành công!");
            LoadData();
            txtTenKhoa.Clear();
        }

        // Check if the Khoa is a duplicate
        private bool IsDuplicate(string tenKhoa)
        {
            string query = "SELECT COUNT(*) FROM Khoa WHERE TenKhoa = @TenKhoa";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenKhoa", tenKhoa)
            };

            int count = (int)accessData.ExecuteScalar(query, parameters);
            return count > 0;
        }

        // Update the selected Khoa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (cbMaKhoa.SelectedValue != null && int.TryParse(cbMaKhoa.SelectedValue.ToString(), out int maKhoa))
            {
                string tenKhoa = txtTenKhoa.Text.Trim();

                if (string.IsNullOrWhiteSpace(tenKhoa))
                {
                    MessageBox.Show("Tên khoa không được để trống.");
                    return;
                }

                // Kiểm tra nếu tên khoa mới đã tồn tại và không phải là khoa hiện tại
                if (IsDuplicateForUpdate(maKhoa, tenKhoa))
                {
                    MessageBox.Show("Tên khoa này đã tồn tại.");
                    return;
                }

                string query = "UPDATE Khoa SET TenKhoa = @TenKhoa WHERE MaKhoa = @MaKhoa";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenKhoa", tenKhoa),
                    new SqlParameter("@MaKhoa", maKhoa)
                };
                accessData.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Cập nhật khoa thành công!");
                LoadData();
                txtTenKhoa.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã khoa để cập nhật.");
            }
        }

        // Check if Khoa already exists for update
        private bool IsDuplicateForUpdate(int maKhoa, string tenKhoa)
        {
            string query = "SELECT COUNT(*) FROM Khoa WHERE TenKhoa = @TenKhoa AND MaKhoa != @MaKhoa";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenKhoa", tenKhoa),
                new SqlParameter("@MaKhoa", maKhoa)
            };

            int count = (int)accessData.ExecuteScalar(query, parameters);
            return count > 0;
        }
        // Kiểm tra xem Khoa có bản ghi liên quan trong các bảng khác (ví dụ như Lớp hoặc Giảng Viên)
        private bool HasDependencies(int maKhoa)
        {
            string query = "SELECT COUNT(*) FROM DOCGIA WHERE MaKhoa = @MaKhoa";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaKhoa", maKhoa)
            };

            int count = (int)accessData.ExecuteScalar(query, parameters);
            return count > 0; // Trả về true nếu có bản ghi liên quan trong bảng DOCGIA
        }


        // Delete the selected Khoa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (cbMaKhoa.SelectedValue != null && int.TryParse(cbMaKhoa.SelectedValue.ToString(), out int maKhoa))
            {
                if (HasDependencies(maKhoa))
                {
                    MessageBox.Show("Không thể xóa lớp này vì có các bản ghi liên quan.");
                    return;
                }
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa khoa này không?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Khoa WHERE MaKhoa = @MaKhoa";
                        SqlParameter[] parameters = new SqlParameter[]
                        {
                            new SqlParameter("@MaKhoa", maKhoa)
                        };
                        accessData.ExecuteNonQuery(query, parameters);
                        MessageBox.Show("Xóa khoa thành công!");
                        LoadData();
                        txtTenKhoa.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa khoa: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã khoa để xóa.");
            }
        }

        // Refresh the form inputs
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cbMaKhoa.SelectedIndex = -1;
            txtTenKhoa.Clear();
        }

        // When ComboBox selection changes, update the TextBox with the corresponding Khoa name
        private void cbMaKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaKhoa.SelectedValue != null && int.TryParse(cbMaKhoa.SelectedValue.ToString(), out int maKhoa))
            {
                // Gọi hàm để lấy thông tin của khoa dựa vào MaKhoa
                DataRow row = accessData.GetKhoaById(maKhoa);

                if (row != null)
                {
                    // Hiển thị tên khoa trong TextBox
                    txtTenKhoa.Text = row["TenKhoa"].ToString();
                }
                else
                {
                    // Nếu không tìm thấy dữ liệu, xóa dữ liệu trong TextBox
                    txtTenKhoa.Clear();
                }
            }
            else
            {
                // Nếu không chọn mã khoa, xóa dữ liệu trong TextBox
                txtTenKhoa.Clear();
            }
        }

    }
}
