using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmQLSach : Form
    {
        private AccessData accessData;

        public fmQLSach()
        {
            InitializeComponent();
            accessData = new AccessData();
            LoadData();
            LoadTheLoai();
            LoadNXB();
        }

        private void LoadData()
        {
            DataTable dtSach = accessData.GetSachData();
            dgvSach.DataSource = dtSach;

            // Đặt tên cột cho DataGridView
            dgvSach.Columns["MaSach"].HeaderText = "Mã Sách";
            dgvSach.Columns["TenSach"].HeaderText = "Tên Sách";
            dgvSach.Columns["TenTheLoai"].HeaderText = "Thể Loại";
            dgvSach.Columns["TenNXB"].HeaderText = "Nhà Xuất Bản";
            dgvSach.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvSach.Columns["NamXB"].HeaderText = "Năm Xuất Bản";
        }

        private void LoadTheLoai()
        {
            DataTable theLoaiTable = accessData.GetTheLoai();
            cboTheLoai.DataSource = theLoaiTable;
            cboTheLoai.DisplayMember = "TenTheLoai";
            cboTheLoai.ValueMember = "MaTheLoai";
        }

        private void LoadNXB()
        {
            DataTable nxbTable = accessData.GetNhaXuatBan();
            cboNXB.DataSource = nxbTable;
            cboNXB.DisplayMember = "TenNXB";
            cboNXB.ValueMember = "MaNXB";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenSach = txtTenSach.Text.Trim(); // Loại bỏ khoảng trắng thừa
            string maTheLoai = cboTheLoai.SelectedValue.ToString();
            string maNXB = cboNXB.SelectedValue.ToString();
            string namXuatBan = dtpNamXuatBan.Value.Year.ToString();
            int soLuong;

            // Kiểm tra tên sách
            if (string.IsNullOrEmpty(tenSach))
            {
                MessageBox.Show("Tên sách không được để trống.");
                return;
            }

            // Kiểm tra xem tên sách đã tồn tại trong cơ sở dữ liệu chưa
            string checkQuery = $"SELECT COUNT(*) FROM Sach WHERE TenSach = N'{tenSach}'";
            int count = Convert.ToInt32(accessData.ExecuteScalar(checkQuery));

            if (count > 0)
            {
                MessageBox.Show("Sách với tên này đã tồn tại.");
                return;
            }

            if (int.TryParse(txtSoLuong.Text, out soLuong))
            {
                if (soLuong >= 0)
                {
                    string query = $"INSERT INTO Sach (TenSach, MaTheLoai, MaNXB, NamXB, SoLuong) " +
                                   $"VALUES (N'{tenSach}', '{maTheLoai}', '{maNXB}', '{namXuatBan}', {soLuong})";
                    accessData.ExecuteNonQuery(query);
                    MessageBox.Show("Thêm sách thành công!");
                    LoadData();
                    btnLamMoi_Click(null, null); // Gọi phương thức làm mới
                }
                else
                {
                    MessageBox.Show("Số lượng không thể nhỏ hơn 0.");
                }
            }
            else
            {
                MessageBox.Show("Số lượng phải là một số nguyên dương.");
            }
        }





        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSach.SelectedRows.Count > 0)
            {
                string maSach = dgvSach.SelectedRows[0].Cells["MaSach"].Value.ToString();
                string tenSach = txtTenSach.Text.Trim(); // Loại bỏ khoảng trắng thừa
                string maTheLoai = cboTheLoai.SelectedValue.ToString();
                string maNXB = cboNXB.SelectedValue.ToString();
                string namXuatBan = dtpNamXuatBan.Value.Year.ToString();
                int soLuong;

                // Kiểm tra tên sách
                if (string.IsNullOrEmpty(tenSach))
                {
                    MessageBox.Show("Tên sách không được để trống.");
                    return;
                }

                if (int.TryParse(txtSoLuong.Text, out soLuong))
                {
                    if (soLuong >= 0)
                    {
                        string query = $"UPDATE Sach SET TenSach = N'{tenSach}', MaTheLoai = '{maTheLoai}', MaNXB = '{maNXB}', " +
                                       $"NamXB = '{namXuatBan}', SoLuong = {soLuong} WHERE MaSach = {maSach}";
                        accessData.ExecuteNonQuery(query);
                        MessageBox.Show("Cập nhật sách thành công!");
                        LoadData();
                        btnLamMoi_Click(null, null); // Gọi phương thức làm mới
                    }
                    else
                    {
                        MessageBox.Show("Số lượng không thể nhỏ hơn 0.");
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng phải là một số nguyên dương.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách cần cập nhật.");
            }
        }




        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSach.SelectedRows.Count > 0)
            {
                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa sách này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string maSach = dgvSach.SelectedRows[0].Cells["MaSach"].Value.ToString();
                    string query = $"DELETE FROM Sach WHERE MaSach = {maSach}";
                    accessData.ExecuteNonQuery(query);
                    MessageBox.Show("Xóa sách thành công!");
                    LoadData();
                    btnLamMoi_Click(null, null); // Gọi phương thức làm mới
                }
                // Nếu chọn No hoặc đóng hộp thoại, không thực hiện hành động xóa
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa.");
            }
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenSach.Clear();
            cboTheLoai.SelectedIndex = -1;
            cboNXB.SelectedIndex = -1;
            dtpNamXuatBan.Value = DateTime.Now;
            txtSoLuong.Clear();
            LoadData();
        }

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSach.Rows[e.RowIndex];

                // Kiểm tra và thiết lập các giá trị cho điều khiển
                if (row.Cells["TenSach"].Value != null)
                {
                    txtTenSach.Text = row.Cells["TenSach"].Value.ToString();
                }

                if (row.Cells["TenTheLoai"].Value != null) // Thay đổi từ "MaTheLoai" thành "TenTheLoai"
                {
                    string tenTheLoai = row.Cells["TenTheLoai"].Value.ToString();
                    cboTheLoai.SelectedIndex = cboTheLoai.FindStringExact(tenTheLoai); // Chọn giá trị từ ComboBox dựa trên tên
                }
                else
                {
                    cboTheLoai.SelectedIndex = -1;
                }

                if (row.Cells["TenNXB"].Value != null) // Thay đổi từ "MaNXB" thành "TenNXB"
                {
                    string tenNXB = row.Cells["TenNXB"].Value.ToString();
                    cboNXB.SelectedIndex = cboNXB.FindStringExact(tenNXB); // Chọn giá trị từ ComboBox dựa trên tên
                }
                else
                {
                    cboNXB.SelectedIndex = -1;
                }

                if (row.Cells["NamXB"].Value != null)
                {
                    int namXuatBan;
                    if (int.TryParse(row.Cells["NamXB"].Value.ToString(), out namXuatBan))
                    {
                        dtpNamXuatBan.Value = new DateTime(namXuatBan, 1, 1);
                    }
                    else
                    {
                        dtpNamXuatBan.Value = DateTime.Now; // Hoặc giá trị mặc định khác
                    }
                }

                if (row.Cells["SoLuong"].Value != null)
                {
                    txtSoLuong.Text = row.Cells["SoLuong"].Value.ToString();
                }

            }

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            int soLuong;
            if (int.TryParse(txtSoLuong.Text, out soLuong))
            {
                if (soLuong < 0)
                {
                    MessageBox.Show("Số lượng không thể nhỏ hơn 0.");
                    txtSoLuong.Text = ""; // Xóa giá trị không hợp lệ
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtSoLuong.Text))
                {
                    MessageBox.Show("Số lượng phải là một số nguyên dương.");
                    txtSoLuong.Text = ""; // Xóa giá trị không hợp lệ
                }
            }
        }

    }

}


