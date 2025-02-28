using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmLop : Form
    {
        private AccessData accessData;

        public fmLop()
        {
            InitializeComponent();
            accessData = new AccessData();
            LoadData();

            dgvLop.CellClick += dgvLop_CellClick;
        }

        private void LoadData()
        {
            DataTable lopTable = accessData.GetLop();
            dgvLop.DataSource = lopTable;

            // Đặt lại tiêu đề cho cột
            dgvLop.Columns["MaLop"].HeaderText = "Mã Lớp";
            dgvLop.Columns["TenLop"].HeaderText = "Tên Lớp";

            // Gán dữ liệu cho ComboBox
            cbMaLop.DataSource = lopTable;
            cbMaLop.DisplayMember = "MaLop";
            cbMaLop.ValueMember = "MaLop";
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            string tenLop = txtTenLop.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenLop))
            {
                MessageBox.Show("Tên lớp không được để trống.");
                return;
            }

            if (IsDuplicate(tenLop))
            {
                MessageBox.Show("Lớp này đã tồn tại.");
                return;
            }

            string query = "INSERT INTO Lop (TenLop) VALUES (@TenLop)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenLop", tenLop)
            };
            accessData.ExecuteNonQuery(query, parameters);
            MessageBox.Show("Thêm lớp thành công!");
            LoadData();
            txtTenLop.Clear();
        }

        private bool IsDuplicate(string tenLop)
        {
            string query = "SELECT COUNT(*) FROM Lop WHERE TenLop = @TenLop";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenLop", tenLop)
            };

            int count = (int)accessData.ExecuteScalar(query, parameters);
            return count > 0;
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra mã lớp và chuyển đổi kiểu hợp lệ
            if (cbMaLop.SelectedValue != null && int.TryParse(cbMaLop.SelectedValue.ToString(), out int maLop))
            {
                string tenLop = txtTenLop.Text.Trim();

                if (string.IsNullOrWhiteSpace(tenLop))
                {
                    MessageBox.Show("Tên lớp không được để trống.");
                    return;
                }

                string query = "UPDATE Lop SET TenLop = @TenLop WHERE MaLop = @MaLop";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenLop", tenLop),
                    new SqlParameter("@MaLop", maLop)
                };
                accessData.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Cập nhật lớp thành công!");
                LoadData();
                txtTenLop.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã lớp để cập nhật.");
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra mã lớp và chuyển đổi kiểu hợp lệ
            if (cbMaLop.SelectedValue != null && int.TryParse(cbMaLop.SelectedValue.ToString(), out int maLop))
            {
                if (HasDependencies(maLop))
                {
                    MessageBox.Show("Không thể xóa lớp này vì có các bản ghi liên quan.");
                    return;
                }

                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp này không?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Lop WHERE MaLop = @MaLop";
                        SqlParameter[] parameters = new SqlParameter[]
                        {
                            new SqlParameter("@MaLop", maLop)
                        };
                        accessData.ExecuteNonQuery(query, parameters);
                        MessageBox.Show("Xóa lớp thành công!");
                        LoadData();
                        txtTenLop.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa lớp: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã lớp để xóa.");
            }
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            cbMaLop.SelectedIndex = -1;
            txtTenLop.Clear();
        }

        private bool HasDependencies(int maLop)
        {
            string query = "SELECT COUNT(*) FROM DOCGIA WHERE MaLop = @MaLop";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLop", maLop)
            };

            int count = (int)accessData.ExecuteScalar(query, parameters);
            return count > 0;
        }

        private void cbMaLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có giá trị hợp lệ không
            if (cbMaLop.SelectedValue != null && int.TryParse(cbMaLop.SelectedValue.ToString(), out int maLop))
            {
                DataRow row = accessData.GetLopById(maLop);
                if (row != null)
                {
                    txtTenLop.Text = row["TenLop"].ToString();
                }
                else
                {
                    txtTenLop.Clear();
                }
            }
        }

        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLop.Rows[e.RowIndex];

                if (row.Cells["MaLop"].Value != null)
                {
                    cbMaLop.SelectedValue = row.Cells["MaLop"].Value;
                }

                if (row.Cells["TenLop"].Value != null)
                {
                    txtTenLop.Text = row.Cells["TenLop"].Value.ToString();
                }
            }
        }

        private void dgvLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Có thể thêm chức năng bổ sung ở đây nếu cần
        }
    }
}
