using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmQuanLyDG : Form
    {
        private AccessData accessData;

        public fmQuanLyDG()
        {
            InitializeComponent();
            accessData = new AccessData();
            LoadData();
            LoadKhoa();
            LoadLop(); // Thêm phương thức để tải dữ liệu cho ComboBox cbLop
        }

        private void LoadData()
        {
            DataTable dtDocGia = accessData.GetDocGiaData();
            dgvDocGia.DataSource = dtDocGia;

            // Đặt tên column cho DataGridView
            dgvDocGia.Columns["MaDG"].HeaderText = "Mã Độc Giả";
            dgvDocGia.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvDocGia.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvDocGia.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvDocGia.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvDocGia.Columns["TenLop"].HeaderText = "Tên Lớp";
            dgvDocGia.Columns["TenKhoa"].HeaderText = "Tên Khoa";
        }

        private void LoadKhoa()
        {
            DataTable khoaTable = accessData.GetKhoa();
            cboKhoa.DataSource = khoaTable;
            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "MaKhoa";
        }

        private void LoadLop()
        {
            DataTable lopTable = accessData.GetLop(); // Giả sử phương thức GetLop() tồn tại
            cbLop.DataSource = lopTable;
            cbLop.DisplayMember = "TenLop";
            cbLop.ValueMember = "MaLop";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                cboGioiTinh.SelectedIndex == -1 ||
                cboKhoa.SelectedIndex == -1 ||
                cbLop.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            string hoTen = txtHoTen.Text;
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = cboGioiTinh.SelectedItem.ToString();
            string diaChi = txtDiaChi.Text;
            int maLop = (int)cbLop.SelectedValue;
            string maKhoa = cboKhoa.SelectedValue.ToString();

            // Kiểm tra tuổi của đọc giả
            int tuoi = DateTime.Now.Year - ngaySinh.Year;
            if (tuoi < 16)
            {
                MessageBox.Show("Độc giả phải từ 16 tuổi trở lên.");
                return;
            }

            // Kiểm tra trùng lặp thông tin
            string checkQuery = $"SELECT COUNT(*) FROM DOCGIA WHERE HoTen = N'{hoTen}' AND NgaySinh = '{ngaySinh:yyyy-MM-dd}' " +
                                $"AND DiaChi = N'{diaChi}' AND MaLop = {maLop} AND MaKhoa = {maKhoa}";
            int count = (int)accessData.ExecuteScalar(checkQuery);

            if (count > 0)
            {
                MessageBox.Show("Thông tin độc giả bị trùng lặp. Vui lòng kiểm tra lại.");
                return;
            }

            // Nếu không trùng lặp, tiến hành thêm mới
            string query = $"INSERT INTO DOCGIA (HoTen, NgaySinh, GioiTinh, DiaChi, MaLop, MaKhoa) " +
                           $"VALUES (N'{hoTen}', '{ngaySinh:yyyy-MM-dd}', N'{gioiTinh}', N'{diaChi}', {maLop}, {maKhoa})";
            accessData.ExecuteNonQuery(query);

            MessageBox.Show("Thêm độc giả thành công!");
            LoadData(); // Refresh the DataGridView
            btnLamMoi_Click(null, null); // Clear the form fields
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.SelectedRows.Count > 0)
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                    cboGioiTinh.SelectedIndex == -1 ||
                    cboKhoa.SelectedIndex == -1 ||
                    cbLop.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                    return;
                }

                string maDG = dgvDocGia.SelectedRows[0].Cells["MaDG"].Value.ToString();
                string hoTen = txtHoTen.Text;
                DateTime ngaySinh = dtpNgaySinh.Value;
                string gioiTinh = cboGioiTinh.SelectedItem.ToString();
                string diaChi = txtDiaChi.Text;
                int maLop = (int)cbLop.SelectedValue;
                string maKhoa = cboKhoa.SelectedValue.ToString();

                // Kiểm tra tuổi của đọc giả
                int tuoi = DateTime.Now.Year - ngaySinh.Year;
                if (tuoi < 16)
                {
                    MessageBox.Show("Độc giả phải từ 16 tuổi trở lên.");
                    return;
                }

                // Kiểm tra trùng lặp thông tin
                string checkQuery = $"SELECT COUNT(*) FROM DOCGIA WHERE HoTen = N'{hoTen}' AND NgaySinh = '{ngaySinh:yyyy-MM-dd}' " +
                                    $"AND DiaChi = N'{diaChi}' AND MaLop = {maLop} AND MaKhoa = {maKhoa} AND MaDG != {maDG}";
                int count = (int)accessData.ExecuteScalar(checkQuery);

                if (count > 0)
                {
                    MessageBox.Show("Thông tin độc giả bị trùng lặp. Vui lòng kiểm tra lại.");
                    return;
                }

                // Nếu không trùng lặp, tiến hành cập nhật
                string query = $"UPDATE DOCGIA SET HoTen = N'{hoTen}', NgaySinh = '{ngaySinh:yyyy-MM-dd}', GioiTinh = N'{gioiTinh}', " +
                               $"DiaChi = N'{diaChi}', MaLop = {maLop}, MaKhoa = {maKhoa} WHERE MaDG = {maDG}";
                accessData.ExecuteNonQuery(query);

                MessageBox.Show("Cập nhật độc giả thành công!");
                LoadData(); // Refresh the DataGridView
                btnLamMoi_Click(null, null); // Clear the form fields
            }
            else
            {
                MessageBox.Show("Vui lòng chọn độc giả cần cập nhật.");
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.SelectedRows.Count > 0)
            {
                string maDG = dgvDocGia.SelectedRows[0].Cells["MaDG"].Value.ToString();
                string query = $"DELETE FROM DOCGIA WHERE MaDG = {maDG}";
                accessData.ExecuteNonQuery(query);

                MessageBox.Show("Xóa độc giả thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn độc giả cần xóa.");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = -1;
            txtDiaChi.Clear();
            cbLop.SelectedIndex = -1; // Đặt lại ComboBox cbLop
            cboKhoa.SelectedIndex = -1; // Đặt lại ComboBox cboKhoa
            LoadData();
        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];

                // Kiểm tra giá trị 'HoTen'
                txtHoTen.Text = row.Cells["HoTen"].Value != DBNull.Value ? row.Cells["HoTen"].Value.ToString() : string.Empty;

                // Kiểm tra và chuyển đổi giá trị 'NgaySinh'
                if (row.Cells["NgaySinh"].Value != DBNull.Value)
                {
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Now; // Nếu giá trị là DBNull, thiết lập về ngày hiện tại
                }

                // Kiểm tra giá trị 'DiaChi'
                txtDiaChi.Text = row.Cells["DiaChi"].Value != DBNull.Value ? row.Cells["DiaChi"].Value.ToString() : string.Empty;

                // Đặt giới tính
                if (row.Cells["GioiTinh"].Value != DBNull.Value)
                {
                    string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                    cboGioiTinh.SelectedIndex = cboGioiTinh.Items.IndexOf(gioiTinh);
                }
                else
                {
                    cboGioiTinh.SelectedIndex = -1; // Thiết lập về giá trị mặc định nếu không có giá trị
                }

                // Đặt lớp học
                if (row.Cells["TenLop"].Value != DBNull.Value)
                {
                    string tenLop = row.Cells["TenLop"].Value.ToString();
                    cbLop.SelectedIndex = cbLop.FindStringExact(tenLop);
                }
                else
                {
                    cbLop.SelectedIndex = -1; // Thiết lập về giá trị mặc định nếu không có giá trị
                }

                // Đặt khoa
                if (row.Cells["TenKhoa"].Value != DBNull.Value)
                {
                    string tenKhoa = row.Cells["TenKhoa"].Value.ToString();
                    cboKhoa.SelectedIndex = cboKhoa.FindStringExact(tenKhoa);
                }
                else
                {
                    cboKhoa.SelectedIndex = -1; // Thiết lập về giá trị mặc định nếu không có giá trị
                }
            }
        }


        private void dgvDocGia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Có thể bỏ qua hoặc xử lý nếu cần
        }
    }
}
