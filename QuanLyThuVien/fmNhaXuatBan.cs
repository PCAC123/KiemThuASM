using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmNhaXuatBan : Form
    {
        private AccessData accessData;

        public fmNhaXuatBan()
        {
            InitializeComponent();
            accessData = new AccessData();
            LoadData();
            dgvNhaXuatBan.CellClick += dgvNhaXuatBan_CellClick; // Đăng ký sự kiện CellClick
        }

        private void LoadData()
        {
            DataTable nhaXuatBanTable = accessData.GetNhaXuatBan();
            dgvNhaXuatBan.DataSource = nhaXuatBanTable;

            // Đặt tên cột cho DataGridView
            dgvNhaXuatBan.Columns["MaNXB"].HeaderText = "Mã NXB";
            dgvNhaXuatBan.Columns["TenNXB"].HeaderText = "Tên NXB";
            dgvNhaXuatBan.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvNhaXuatBan.Columns["NgayThanhLap"].HeaderText = "Ngày thành lập";

            // Tải dữ liệu cho ComboBox
            cbMaNXB.DataSource = nhaXuatBanTable;
            cbMaNXB.DisplayMember = "MaNXB";
            cbMaNXB.ValueMember = "MaNXB";
            cbMaNXB.SelectedIndex = -1; // Đảm bảo ComboBox không chọn mục nào mặc định
        }

        private void dgvNhaXuatBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhaXuatBan.Rows[e.RowIndex];

                // Cập nhật các ô nhập liệu từ dữ liệu của dòng đã chọn
                if (row.Cells["MaNXB"].Value != null)
                {
                    cbMaNXB.SelectedValue = row.Cells["MaNXB"].Value;
                }
                if (row.Cells["TenNXB"].Value != null && row.Cells["TenNXB"].Value != DBNull.Value)
                {
                    txtTenNXB.Text = row.Cells["TenNXB"].Value.ToString();
                }
                else
                {
                    txtTenNXB.Clear();
                }

                if (row.Cells["DiaChi"].Value != null && row.Cells["DiaChi"].Value != DBNull.Value)
                {
                    txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                }
                else
                {
                    txtDiaChi.Clear();
                }

                if (row.Cells["NgayThanhLap"].Value != null && row.Cells["NgayThanhLap"].Value != DBNull.Value)
                {
                    dtpNgayThanhLap.Value = Convert.ToDateTime(row.Cells["NgayThanhLap"].Value);
                }
                else
                {
                    dtpNgayThanhLap.Value = DateTime.Today; // Giá trị mặc định nếu không có ngày
                }
            }
        }

        private void cbMaNXB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaNXB.SelectedValue != null && int.TryParse(cbMaNXB.SelectedValue.ToString(), out int maNXB))
            {
                DataRow row = accessData.GetNhaXuatBanById(maNXB);
                if (row != null)
                {
                    txtTenNXB.Text = row["TenNXB"] != DBNull.Value ? row["TenNXB"].ToString() : string.Empty;
                    txtDiaChi.Text = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : string.Empty;
                    dtpNgayThanhLap.Value = row["NgayThanhLap"] != DBNull.Value ? Convert.ToDateTime(row["NgayThanhLap"]) : DateTime.Today;
                }
                else
                {
                    ClearInputFields();
                }
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenNXB = txtTenNXB.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            DateTime ngayThanhLap = dtpNgayThanhLap.Value;

            if (string.IsNullOrWhiteSpace(tenNXB))
            {
                MessageBox.Show("Tên nhà xuất bản không được để trống.");
                return;
            }

            if (IsDuplicate(tenNXB))
            {
                MessageBox.Show("Tên nhà xuất bản này đã tồn tại.");
                return;
            }

            string query = "INSERT INTO NHAXUATBAN (TenNXB, DiaChi, NgayThanhLap) VALUES (@TenNXB, @DiaChi, @NgayThanhLap)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenNXB", tenNXB),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@NgayThanhLap", ngayThanhLap)
            };

            accessData.ExecuteNonQuery(query, parameters);
            MessageBox.Show("Thêm nhà xuất bản thành công!");
            LoadData();
            ClearInputFields();
        }

        private bool IsDuplicate(string tenNXB, int? excludedMaNXB = null)
        {
            string query = "SELECT COUNT(*) FROM NHAXUATBAN WHERE TenNXB = @TenNXB";
            if (excludedMaNXB.HasValue)
            {
                query += " AND MaNXB != @ExcludedMaNXB";
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenNXB", tenNXB)
            };
            if (excludedMaNXB.HasValue)
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenNXB", tenNXB),
                    new SqlParameter("@ExcludedMaNXB", excludedMaNXB.Value)
                };
            }

            int count = (int)accessData.ExecuteScalar(query, parameters);
            return count > 0;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (cbMaNXB.SelectedValue != null && int.TryParse(cbMaNXB.SelectedValue.ToString(), out int maNXB))
            {
                string tenNXB = txtTenNXB.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                DateTime ngayThanhLap = dtpNgayThanhLap.Value;

                if (string.IsNullOrWhiteSpace(tenNXB))
                {
                    MessageBox.Show("Tên nhà xuất bản không được để trống.");
                    return;
                }

                // Kiểm tra nếu tên nhà xuất bản mới đã tồn tại và không phải là nhà xuất bản hiện tại
                if (IsDuplicate(tenNXB, maNXB))
                {
                    MessageBox.Show("Tên nhà xuất bản này đã tồn tại.");
                    return;
                }

                string query = "UPDATE NHAXUATBAN SET TenNXB = @TenNXB, DiaChi = @DiaChi, NgayThanhLap = @NgayThanhLap WHERE MaNXB = @MaNXB";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenNXB", tenNXB),
                    new SqlParameter("@DiaChi", diaChi),
                    new SqlParameter("@NgayThanhLap", ngayThanhLap),
                    new SqlParameter("@MaNXB", maNXB)
                };

                accessData.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Cập nhật nhà xuất bản thành công!");
                LoadData();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã nhà xuất bản để cập nhật.");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (cbMaNXB.SelectedValue != null && int.TryParse(cbMaNXB.SelectedValue.ToString(), out int maNXB))
            {
                // Kiểm tra xem nhà xuất bản có sách liên quan hay không
                if (HasRelatedBooks(maNXB))
                {
                    MessageBox.Show("Không thể xóa nhà xuất bản này vì có các bản ghi sách liên quan.");
                    return;
                }

                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà xuất bản này không?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string deleteNXBQuery = "DELETE FROM NHAXUATBAN WHERE MaNXB = @MaNXB";
                        SqlParameter[] nxbParameters = new SqlParameter[]
                        {
                    new SqlParameter("@MaNXB", maNXB)
                        };
                        accessData.ExecuteNonQuery(deleteNXBQuery, nxbParameters);

                        MessageBox.Show("Xóa nhà xuất bản thành công!");
                        LoadData();
                        ClearInputFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa nhà xuất bản: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã nhà xuất bản để xóa.");
            }
        }

        private bool HasRelatedBooks(int maNXB)
        {
            // Kiểm tra xem nhà xuất bản có sách liên quan không
            string query = "SELECT COUNT(*) FROM SACH WHERE MaNXB = @MaNXB";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaNXB", maNXB)
            };

            int count = (int)accessData.ExecuteScalar(query, parameters);

            // Trả về true nếu có sách liên quan, false nếu không
            return count > 0;
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cbMaNXB.SelectedIndex = -1; // Đặt lại ComboBox
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            txtTenNXB.Clear();
            txtDiaChi.Clear();
            dtpNgayThanhLap.Value = DateTime.Today;
        }
    }
}
