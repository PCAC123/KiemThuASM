using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmThongTinNhanVien : Form
    {

        private AccessData accessData = new AccessData();       
        public void SetAccessData(AccessData accessData)
        {
            if (accessData == null)
            {
                throw new ArgumentNullException(nameof(accessData), "AccessData cannot be null.");
            }
            this.accessData = accessData;
        }

        public fmThongTinNhanVien()
        {
            InitializeComponent();
            LoadNhanVien();

            // Đăng ký sự kiện CellClick
            dgvNV.CellClick += dgvNV_CellClick;

         
        }
        // Các phương thức khác trong form, bao gồm LoadNhanVien, btnThem_Click, btnSua_Click, btnXoa_Click, btnLamMoi_Click, ValidateAge, ValidateInput...
        // Phương thức để inject đối tượng AccessData vào lớp (để kiểm thử)
        
        // Phương thức để chuyển đổi các trường form thành model NhanVienModel
        // Getter và Setter cho tất cả các trường nhập liệu
        public void Test_ThemNhanVien()
        {
            btnThem_Click(null, EventArgs.Empty);
        }
        public void Test_SuaNhanVien()
        {
            btnSua_Click(null, EventArgs.Empty);
        }
        // Wrapper cho MessageBox.Show() để dễ kiểm thử
        public virtual DialogResult ShowMessage(string message)
        {
            return MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //public virtual DialogResult ShowDialoga()
        //{
        //    return MessageBox.Show( MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        // Thêm getter cho dgvNV
        public DataGridView GetDgvNV()
        {
            return dgvNV;
        }

        public string GetHoTen()
        {
            return txtHoTen.Text;
        }

        public void SetHoTen(string hoTen)
        {
            txtHoTen.Text = hoTen;
        }

        public string GetDiaChi()
        {
            return txtDiaChi.Text;
        }

        public void SetDiaChi(string diaChi)
        {
            txtDiaChi.Text = diaChi;
        }

        public string GetSDT()
        {
            return txtSDT.Text;
        }

        public void SetSDT(string sdt)
        {
            txtSDT.Text = sdt;
        }

        public string GetTenDangNhap()
        {
            return txtTenDangNhap.Text;
        }

        public void SetTenDangNhap(string tenDangNhap)
        {
            txtTenDangNhap.Text = tenDangNhap;
        }

        public string GetMatKhau()
        {
            return txtMatKhau.Text;
        }

        public void SetMatKhau(string matKhau)
        {
            txtMatKhau.Text = matKhau;
        }

        public DateTime GetNgaySinh()
        {
            return dtNgaySinh.Value;
        }

        public void SetNgaySinh(DateTime ngaySinh)
        {
            dtNgaySinh.Value = ngaySinh;
        }

        public string GetMaNV()
        {
            return txtMaNV.Text;
        }

        public void SetMaNV(string maNV)
        {
            txtMaNV.Text = maNV;
        }

        public void SelectRowInDgv(int rowIndex)
        {
            dgvNV_CellClick(null, new DataGridViewCellEventArgs(0, rowIndex));
        }
        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNV.Rows[e.RowIndex];

                // Kiểm tra nếu cột MaNV có giá trị, nếu không thì gán giá trị mặc định
                txtMaNV.Text = row.Cells["MaNV"].Value != DBNull.Value ? row.Cells["MaNV"].Value.ToString() : string.Empty;

                // Kiểm tra nếu cột HoTen có giá trị
                txtHoTen.Text = row.Cells["HoTen"].Value != DBNull.Value ? row.Cells["HoTen"].Value.ToString() : string.Empty;

                // Kiểm tra nếu cột NgaySinh có giá trị và chuyển đổi
                dtNgaySinh.Value = row.Cells["NgaySinh"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["NgaySinh"].Value) : DateTime.Now;

                // Kiểm tra nếu cột DiaChi có giá trị
                txtDiaChi.Text = row.Cells["DiaChi"].Value != DBNull.Value ? row.Cells["DiaChi"].Value.ToString() : string.Empty;

                // Kiểm tra nếu cột DienThoai có giá trị
                txtSDT.Text = row.Cells["DienThoai"].Value != DBNull.Value ? row.Cells["DienThoai"].Value.ToString() : string.Empty;

                // Kiểm tra nếu cột username có giá trị
                txtTenDangNhap.Text = row.Cells["username"].Value != DBNull.Value ? row.Cells["username"].Value.ToString() : string.Empty;

                // Kiểm tra nếu cột password có giá trị
                txtMatKhau.Text = row.Cells["password"].Value != DBNull.Value ? row.Cells["password"].Value.ToString() : string.Empty;
            }
        }



        private void LoadNhanVien()
        {
            // Nạp dữ liệu nhân viên và thông tin tài khoản vào DataGridView
            string query = @"
        SELECT N.MaNV, N.HoTen, N.NgaySinh, N.DiaChi, N.DienThoai, UR.username, UR.password
        FROM NhanVien N
        LEFT JOIN user_roles UR ON N.MaNV = UR.MaNV";
            DataTable dt = accessData.ExecuteQuery(query);
            dgvNV.DataSource = dt;
        }


        

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            if (!ValidateAge(dtNgaySinh.Value))
            {
                ShowMessage("Nhân viên phải đủ 18 tuổi!");
                return;
            }

            // Check if the username already exists
            string checkUserQuery = "SELECT COUNT(*) FROM user_roles WHERE username = @Username";
            SqlParameter[] checkParams = new SqlParameter[]
            {
        new SqlParameter("@Username", txtTenDangNhap.Text)
            };

            int userCount = (int)accessData.ExecuteScalar(checkUserQuery, checkParams);

            if (userCount > 0)
            {
                ShowMessage("Tên đăng nhập đã tồn tại!");
                return;
            }

            string query = @"
        DECLARE @MaNV INT;
        
        INSERT INTO NhanVien (HoTen, NgaySinh, DiaChi, DienThoai) 
        VALUES (@HoTen, @NgaySinh, @DiaChi, @DienThoai);
        
        SET @MaNV = SCOPE_IDENTITY();
        
        INSERT INTO user_roles (username, password, role_id, MaNV) 
        VALUES (@Username, @Password, @RoleId, @MaNV);";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@HoTen", txtHoTen.Text),
        new SqlParameter("@NgaySinh", dtNgaySinh.Value),
        new SqlParameter("@DiaChi", txtDiaChi.Text),
        new SqlParameter("@DienThoai", txtSDT.Text),
        new SqlParameter("@Username", txtTenDangNhap.Text),
        new SqlParameter("@Password", txtMatKhau.Text),
        new SqlParameter("@RoleId", 1) // Set role_id as per your requirement
            };

            int result = accessData.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                ShowMessage("Thêm nhân viên thành công!");
                btnLamMoi_Click(null, null); // Clear form after success
                LoadNhanVien(); // Refresh data
            }
            else
            {
                ShowMessage("Thêm nhân viên thất bại!");
            }
        }



        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            if (!ValidateAge(dtNgaySinh.Value))
            {
                ShowMessage("Nhân viên phải đủ 18 tuổi!");
                return;
            }

            string query = @"
        UPDATE NhanVien 
        SET HoTen = @HoTen, NgaySinh = @NgaySinh, DiaChi = @DiaChi, DienThoai = @DienThoai 
        WHERE MaNV = @MaNV;
        
        UPDATE user_roles 
        SET username = @Username, password = @Password
        WHERE MaNV = @MaNV";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@HoTen", txtHoTen.Text),
        new SqlParameter("@NgaySinh", dtNgaySinh.Value),
        new SqlParameter("@DiaChi", txtDiaChi.Text),
        new SqlParameter("@DienThoai", txtSDT.Text),
        new SqlParameter("@Username", txtTenDangNhap.Text),
        new SqlParameter("@Password", txtMatKhau.Text),
        new SqlParameter("@MaNV", txtMaNV.Text)
            };

            int result = accessData.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                ShowMessage("Sửa thông tin nhân viên thành công!");
                btnLamMoi_Click(null, null); // Clear form after success
                LoadNhanVien(); // Refresh data
            }
            else
            {
                ShowMessage("Sửa thông tin nhân viên thất bại!");
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Xóa nhân viên và tài khoản
            string query = @"
        DELETE FROM user_roles WHERE MaNV = @MaNV;
        DELETE FROM NhanVien WHERE MaNV = @MaNV";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaNV", txtMaNV.Text)
            };

            int result = accessData.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                ShowMessage("Xóa nhân viên thành công!");
                LoadNhanVien(); // Làm mới dữ liệu
            }
            else
            {
                ShowMessage("Xóa nhân viên thất bại!");
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Clear form fields
            txtMaNV.Text = "";
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtTenDangNhap.Text = "";
            txtMatKhau.Text = "";
            dtNgaySinh.Value = DateTime.Now;
        }

        private bool ValidateAge(DateTime birthDate)
        {
            // Calculate age
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now < birthDate.AddYears(age)) age--;

            // Return whether age is 18 or older
            return age >= 18;
        }
        private bool ValidateInput()
        {
            // Kiểm tra nếu trường Họ tên bị bỏ trống
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                ShowMessage("Họ tên không được để trống!");
                return false;
            }

            // Kiểm tra nếu trường Địa chỉ bị bỏ trống
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                ShowMessage("Địa chỉ không được để trống!");
                return false;
            }

            // Kiểm tra nếu độ dài của Địa chỉ nhỏ hơn 20 ký tự
            if (txtDiaChi.Text.Length < 20)
            {
                ShowMessage("Địa chỉ phải có ít nhất 20 ký tự, bao gồm cả chữ và số!");
                return false;
            }

            // Kiểm tra nếu trường Số điện thoại bị bỏ trống
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                ShowMessage("Số điện thoại không được để trống!");
                return false;
            }

            // Kiểm tra nếu trường Tên đăng nhập bị bỏ trống
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                ShowMessage("Tên đăng nhập không được để trống!");
                return false;
            }

            // Kiểm tra nếu trường Mật khẩu bị bỏ trống
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                ShowMessage("Mật khẩu không được để trống!");
                return false;
            }

            // Bạn có thể thêm các kiểm tra bổ sung khác tại đây, như kiểm tra định dạng số điện thoại, email, v.v.

            return true;
        }


    }
}
