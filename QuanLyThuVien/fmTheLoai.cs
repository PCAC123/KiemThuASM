using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmTheLoai : Form
    {
        private AccessData accessData;

        public fmTheLoai()
        {
            InitializeComponent();
            accessData = new AccessData();
            LoadData(); // Load data when the form is initialized

            dgvTheLoai.CellClick += dgvTheLoai_CellClick; // Register the CellClick event
        }

        private void dgvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTheLoai.Rows[e.RowIndex];

                // Get the selected TheLoai ID and set it in the ComboBox
                if (row.Cells["MaTheLoai"].Value != null)
                {
                    cbMaTheLoai.SelectedValue = row.Cells["MaTheLoai"].Value;
                }

                // Get the selected TheLoai name and set it in the TextBox
                if (row.Cells["TenTheLoai"].Value != null)
                {
                    txtTenTheLoai.Text = row.Cells["TenTheLoai"].Value.ToString();
                }
            }
        }

        private void LoadData()
        {
            DataTable theLoaiTable = accessData.GetTheLoai();
            dgvTheLoai.DataSource = theLoaiTable;

            // Set the column names for DataGridView
            dgvTheLoai.Columns["MaTheLoai"].HeaderText = "Mã Thể Loại";
            dgvTheLoai.Columns["TenTheLoai"].HeaderText = "Tên Thể Loại";

            // Load data for ComboBox cbMaTheLoai
            cbMaTheLoai.DataSource = theLoaiTable;
            cbMaTheLoai.DisplayMember = "MaTheLoai";
            cbMaTheLoai.ValueMember = "MaTheLoai";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenTheLoai = txtTenTheLoai.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenTheLoai))
            {
                MessageBox.Show("Tên thể loại không được để trống.");
                return;
            }

            if (IsDuplicate(tenTheLoai))
            {
                MessageBox.Show("Tên thể loại này đã tồn tại.");
                return;
            }

            string query = "INSERT INTO TheLoai (TenTheLoai) VALUES (@TenTheLoai)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenTheLoai", tenTheLoai)
            };
            accessData.ExecuteNonQuery(query, parameters);
            MessageBox.Show("Thêm thể loại thành công!");
            LoadData();
            txtTenTheLoai.Clear();
        }

        private bool IsDuplicate(string tenTheLoai, int? excludedMaTheLoai = null)
        {
            string query = "SELECT COUNT(*) FROM TheLoai WHERE TenTheLoai = @TenTheLoai";
            if (excludedMaTheLoai.HasValue)
            {
                query += " AND MaTheLoai != @ExcludedMaTheLoai";
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenTheLoai", tenTheLoai)
            };
            if (excludedMaTheLoai.HasValue)
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenTheLoai", tenTheLoai),
                    new SqlParameter("@ExcludedMaTheLoai", excludedMaTheLoai.Value)
                };
            }

            int count = (int)accessData.ExecuteScalar(query, parameters);
            return count > 0;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (cbMaTheLoai.SelectedValue != null && int.TryParse(cbMaTheLoai.SelectedValue.ToString(), out int maTheLoai))
            {
                string tenTheLoai = txtTenTheLoai.Text.Trim();

                if (string.IsNullOrWhiteSpace(tenTheLoai))
                {
                    MessageBox.Show("Tên thể loại không được để trống.");
                    return;
                }

                // Kiểm tra nếu tên thể loại mới đã tồn tại và không phải là thể loại hiện tại
                if (IsDuplicate(tenTheLoai, maTheLoai))
                {
                    MessageBox.Show("Tên thể loại này đã tồn tại.");
                    return;
                }

                string query = "UPDATE TheLoai SET TenTheLoai = @TenTheLoai WHERE MaTheLoai = @MaTheLoai";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenTheLoai", tenTheLoai),
                    new SqlParameter("@MaTheLoai", maTheLoai)
                };
                accessData.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Cập nhật thể loại thành công!");
                LoadData();
                txtTenTheLoai.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã thể loại để cập nhật.");
            }
        }
        private bool HasDependencies(int maTheLoai)
        {
            // Kiểm tra xem thể loại có tồn tại trong bảng SACH không
            string query = "SELECT COUNT(*) FROM SACH WHERE MaTheLoai = @MaTheLoai";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaTheLoai", maTheLoai)
            };

            int count = (int)accessData.ExecuteScalar(query, parameters);
            return count > 0; // Trả về true nếu có sách liên quan
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (cbMaTheLoai.SelectedValue != null && int.TryParse(cbMaTheLoai.SelectedValue.ToString(), out int maTheLoai))
            {
                // Kiểm tra nếu có bản ghi liên quan trong bảng SACH
                if (HasDependencies(maTheLoai))
                {
                    MessageBox.Show("Không thể xóa thể loại này vì có sách liên quan.");
                    return;
                }

                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa thể loại này không?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM TheLoai WHERE MaTheLoai = @MaTheLoai";
                        SqlParameter[] parameters = new SqlParameter[]
                        {
                    new SqlParameter("@MaTheLoai", maTheLoai)
                        };
                        accessData.ExecuteNonQuery(query, parameters);
                        MessageBox.Show("Xóa thể loại thành công!");
                        LoadData();
                        txtTenTheLoai.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa thể loại: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã thể loại để xóa.");
            }
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cbMaTheLoai.SelectedIndex = -1;
            txtTenTheLoai.Clear();
        }

        private void cbMaTheLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaTheLoai.SelectedValue != null && int.TryParse(cbMaTheLoai.SelectedValue.ToString(), out int maTheLoai))
            {
                // Lấy thông tin thể loại theo MaTheLoai từ cơ sở dữ liệu
                DataRow row = accessData.GetTheLoaiById(maTheLoai);
                if (row != null)
                {
                    // Kiểm tra giá trị có phải là DBNull trước khi hiển thị
                    txtTenTheLoai.Text = row["TenTheLoai"] != DBNull.Value ? row["TenTheLoai"].ToString() : string.Empty;
                }
                else
                {
                    txtTenTheLoai.Clear();
                }
            }
        }

    }
}
