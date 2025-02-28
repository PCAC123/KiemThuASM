using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmQuanLyMuonSach : Form
    {
        private AccessData dataAccess;
        private bool giaHanVisible = false;
        // Wrapper cho MessageBox.Show() để dễ kiểm thử
      
        public fmQuanLyMuonSach()
        {
            InitializeComponent();
            dataAccess = new AccessData();
            LoadDocGiaComboBox(); // Thay đổi tên phương thức để tải mã độc giả
            LoadSachComboBox();
            LoadPhieuMuonData();
            // Đăng ký sự kiện Leave cho txtMaDG
            cbbMaDG.Leave += TxtMaDG_Leave;
            // Đăng ký sự kiện TextChanged cho txtMaDG
            cbbMaDG.TextChanged += TxtMaDG_TextChanged;

            // Đăng ký sự kiện SelectedIndexChanged cho cbMasach
            cbMasach.SelectedIndexChanged += cbMasach_SelectedIndexChanged;

            // Đăng ký sự kiện TextChanged cho txtSoLuong
            txtSoLuong.TextChanged += txtSoLuong_TextChanged;

            // Ẩn các điều khiển gia hạn ban đầu
            btnGiaHan.Visible = false;
            cmbPhieuMuon.Visible = false;
            dtpNgayGiaHan.Visible = false;

            // Ẩn groupBox5 ban đầu
            groupBox5.Visible = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public virtual DialogResult ShowMessagea(string message)
        {
            if (!string.IsNullOrEmpty(message))  // Kiểm tra nếu message có dữ liệu
            {
                return DialogResult.OK;  // Nếu có message, trả về DialogResult.OK mà không hiển thị MessageBox
            }
            else
            {
                return MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);  // Nếu không có message, hiển thị MessageBox
            }
        }

        
        public void Test_btnMuonSach()
        {
            btnMuonSach_Click(null, EventArgs.Empty);
        }

        public void SetMaSach(string maSach)
        {
            cbbMaDG.Text = maSach;
        }


        public void SetMaDocGia(string maDocGia)
        {
            cbbMaDG.SelectedValue = maDocGia;
        }

        public void SetNgayMuon(DateTime ngayMuon)
        {
            dtpNgayGiaHan.Value = ngayMuon;
        }

        public void SetNgayTra(DateTime ngayTra)
        {
            dtpNgayTra.Value = ngayTra;
        }

        public void SetSoLuongMuon(string soLuong)
        {
            txtSoLuong.Text = soLuong;
        }

        public void SetTenSach(string tenSach)
        {
            txtTenSach.Text = tenSach;
        }

        public void SetTenDocGia(string tenDocGia)
        {
            txtHoTenDG.Text = tenDocGia;
        }

        public void SetSoLuongCon(string soLuongCon)
        {
            txtSoLuonCon.Text = soLuongCon;
        }


        /////
        private void LoadDocGiaComboBox()
        {
            try
            {
                string query = "SELECT MaDG FROM DOCGIA"; // Chỉ lấy MaDG
                DataTable data = dataAccess.ExecuteQuery(query);
                cbbMaDG.DataSource = data;
                cbbMaDG.DisplayMember = "MaDG"; // Hiển thị mã độc giả trong ComboBox
                cbbMaDG.ValueMember = "MaDG";   // Giá trị thực tế của mục trong ComboBox
            }
            catch (Exception ex)
            {
                ShowMessagea("Đã xảy ra lỗi khi tải dữ liệu độc giả: " + ex.Message);
            }
        }

        private void TxtMaDG_TextChanged(object sender, EventArgs e)
        {
            LoadDocGiaInfo(); // Tải thông tin độc giả khi văn bản thay đổi
        }

        private void LoadSachComboBox()
        {
            try
            {
                string query = "SELECT MaSach FROM SACH"; // Chỉ lấy MaSach
                DataTable data = dataAccess.ExecuteQuery(query);
                cbMasach.DataSource = data;
                cbMasach.DisplayMember = "MaSach"; // Hiển thị mã sách trong ComboBox
                cbMasach.ValueMember = "MaSach";   // Giá trị thực tế của mục trong ComboBox
            }
            catch (Exception ex)
            {
                ShowMessagea("Đã xảy ra lỗi khi tải dữ liệu sách: " + ex.Message);
            }
        }

        private void TxtMaDG_Leave(object sender, EventArgs e)
        {
            LoadDocGiaInfo(); // Tải thông tin độc giả khi rời khỏi ô nhập mã độc giả
        }

        private void TxtMaSach_Leave(object sender, EventArgs e)
        {
            LoadSachInfo(); // Tải thông tin sách khi rời khỏi ô nhập mã sách
        }

        private void LoadPhieuMuonData()
        {
            try
            {
                string query = @"
                SELECT pm.MaPhieuMuon, pm.MaDG, dg.HoTen, pm.NgayMuon, pm.NgayTra
                FROM PHIEUMUON pm
                INNER JOIN DOCGIA dg ON pm.MaDG = dg.MaDG";

                DataTable data = dataAccess.ExecuteQuery(query);

                // Hiển thị dữ liệu trong DataGridView
                dgvPhieuMuon.DataSource = data;

                // Tải dữ liệu vào ComboBox
                cmbPhieuMuon.DataSource = data;
                cmbPhieuMuon.DisplayMember = "MaPhieuMuon"; // Thuộc tính hiển thị trên ComboBox
                cmbPhieuMuon.ValueMember = "MaPhieuMuon";   // Giá trị thực tế của mục trong ComboBox
            }
            catch (Exception ex)
            {
                ShowMessagea("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadDocGiaInfo()
        {
            if (!string.IsNullOrEmpty(cbbMaDG.Text))
            {
                string query = "SELECT HoTen FROM DOCGIA WHERE MaDG = @MaDG";
                try
                {
                    using (SqlConnection conn = new SqlConnection(dataAccess.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MaDG", cbbMaDG.Text);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtHoTenDG.Text = reader["HoTen"].ToString();
                        }
                        else
                        {
                            ShowMessagea("Không tìm thấy độc giả với mã: " + cbbMaDG.Text);
                            txtHoTenDG.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowMessagea("Đã xảy ra lỗi khi tải thông tin độc giả: " + ex.Message);
                }
            }
        }

        private void LoadSachInfo()
        {
            if (cbMasach.SelectedValue != null)
            {
                int maSach;
                if (int.TryParse(cbMasach.SelectedValue.ToString(), out maSach))
                {
                    string query = "SELECT TenSach, SoLuong FROM SACH WHERE MaSach = @MaSach";
                    using (SqlConnection conn = new SqlConnection(dataAccess.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MaSach", maSach);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtTenSach.Text = reader["TenSach"].ToString();
                            int soLuongCon = Convert.ToInt32(reader["SoLuong"]);
                            txtSoLuonCon.Text = $"Số lượng còn lại: {soLuongCon}"; // Hiển thị số lượng còn lại
                            txtSoLuong.Text = "1"; // Mặc định số lượng mượn là 1
                        }
                        else
                        {
                            ShowMessagea("Không tìm thấy sách.");
                            txtTenSach.Clear();
                            txtSoLuonCon.Text = "Số lượng còn lại: 0";
                        }
                    }
                }
                else
                {
                    ShowMessagea("Mã sách không hợp lệ.");
                }
            }
        }

        private void btnMuonSach_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                MuonSach();
                LoadPhieuMuonData();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(cbbMaDG.Text) || cbMasach.SelectedValue == null)
            {
                ShowMessagea("Vui lòng nhập đầy đủ thông tin.");
                return false;
            }
            if (string.IsNullOrEmpty(txtHoTenDG.Text))
            {
                ShowMessagea("Thông tin độc giả không hợp lệ.");
                return false;
            }

            int soLuongMuon;
            if (!int.TryParse(txtSoLuong.Text, out soLuongMuon) || soLuongMuon <= 0)
            {
                ShowMessagea("Số lượng sách mượn không hợp lệ.");
                return false;
            }

            int maSach = Convert.ToInt32(cbMasach.SelectedValue);
            int soLuongSachCoSan = GetSoLuongSachCoSan(maSach);
            if (soLuongMuon > soLuongSachCoSan)
            {
                ShowMessagea("Số lượng sách mượn vượt quá số lượng sách có sẵn.");
                return false;
            }

            if (dtpNgayTra.Value <= dtpNgayMuon.Value)
            {
                ShowMessagea("Ngày trả phải lớn hơn ngày mượn.");
                return false;
            }
            return true;
        }

        private void MuonSach()
        {
            try
            {
                int maDocGia = int.Parse(cbbMaDG.Text);
                int maSach = Convert.ToInt32(cbMasach.SelectedValue);
                int soLuongMuon = int.Parse(txtSoLuong.Text);
                DateTime ngayMuon = DateTime.Now;
                DateTime ngayTra = dtpNgayTra.Value;

                using (SqlConnection conn = new SqlConnection(dataAccess.ConnectionString))
                {
                    conn.Open();

                    // Thực hiện chèn phiếu mượn
                    SqlCommand cmd = new SqlCommand("INSERT INTO PHIEUMUON (MaDG, MaNV, NgayMuon, NgayTra) OUTPUT INSERTED.MaPhieuMuon VALUES (@MaDG, @MaNV, @NgayMuon, @NgayTra)", conn);
                    cmd.Parameters.AddWithValue("@MaDG", maDocGia);
                    cmd.Parameters.AddWithValue("@MaNV", 1); // Thay thế bằng mã nhân viên hiện tại
                    cmd.Parameters.AddWithValue("@NgayMuon", ngayMuon);
                    cmd.Parameters.AddWithValue("@NgayTra", ngayTra);

                    int maPhieuMuon = (int)cmd.ExecuteScalar();

                    // Thực hiện chèn chi tiết phiếu mượn
                    cmd = new SqlCommand("INSERT INTO CHITIETPHIEUMUON (MaPhieuMuon, MaSach, NgayMuon, NgayTra, SoLuongMuon) VALUES (@MaPhieuMuon, @MaSach, @NgayMuon, @NgayTra, @SoLuongMuon)", conn);
                    cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                    cmd.Parameters.AddWithValue("@MaSach", maSach);
                    cmd.Parameters.AddWithValue("@NgayMuon", ngayMuon);
                    cmd.Parameters.AddWithValue("@NgayTra", ngayTra);
                    cmd.Parameters.AddWithValue("@SoLuongMuon", soLuongMuon);

                    cmd.ExecuteNonQuery();

                    // Cập nhật số lượng sách còn lại
                    cmd = new SqlCommand("UPDATE SACH SET SoLuong = SoLuong - @SoLuongMuon WHERE MaSach = @MaSach", conn);
                    cmd.Parameters.AddWithValue("@SoLuongMuon", soLuongMuon);
                    cmd.Parameters.AddWithValue("@MaSach", maSach);

                    cmd.ExecuteNonQuery();

                    ShowMessagea("Mượn sách thành công!");
                }
            }
            catch (Exception ex)
            {
                ShowMessagea("Đã xảy ra lỗi khi mượn sách: " + ex.Message);
            }
        }

        private int GetSoLuongSachCoSan(int maSach)
        {
            string query = "SELECT SoLuong FROM SACH WHERE MaSach = @MaSach";
            using (SqlConnection conn = new SqlConnection(dataAccess.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (!giaHanVisible)
            {
                btnGiaHan.Visible = true;
                cmbPhieuMuon.Visible = true;
                dtpNgayGiaHan.Visible = true;
                giaHanVisible = true;
            }
            else
            {
                btnGiaHan.Visible = false;
                cmbPhieuMuon.Visible = false;
                dtpNgayGiaHan.Visible = false;
                giaHanVisible = false;
            }

            // Hiển thị hoặc ẩn groupBox5 khi btnShow được nhấn
            groupBox5.Visible = !groupBox5.Visible;
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            if (cmbPhieuMuon.SelectedValue != null)
            {
                int maPhieuMuon = Convert.ToInt32(cmbPhieuMuon.SelectedValue);
                DateTime ngayGiaHan = dtpNgayGiaHan.Value;

                try
                {
                    // Lấy ngày trả hiện tại từ cơ sở dữ liệu
                    DateTime ngayTraHienTai = GetNgayTraHienTai(maPhieuMuon);

                    if (ngayGiaHan <= ngayTraHienTai)
                    {
                        ShowMessagea("Ngày gia hạn phải lớn hơn ngày trả hiện tại.");
                        return;
                    }

                    string query = "UPDATE PHIEUMUON SET NgayTra = @NgayTra WHERE MaPhieuMuon = @MaPhieuMuon";
                    using (SqlConnection conn = new SqlConnection(dataAccess.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@NgayTra", ngayGiaHan);
                        cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        ShowMessagea("Gia hạn sách thành công!");
                    }
                }
                catch (Exception ex)
                {
                    ShowMessagea("Đã xảy ra lỗi khi gia hạn sách: " + ex.Message);
                }
            }
            else
            {
                ShowMessagea("Vui lòng chọn mã phiếu mượn để gia hạn.");
            }
        }

        private DateTime GetNgayTraHienTai(int maPhieuMuon)
        {
            string query = "SELECT NgayTra FROM PHIEUMUON WHERE MaPhieuMuon = @MaPhieuMuon";
            using (SqlConnection conn = new SqlConnection(dataAccess.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                conn.Open();
                return (DateTime)cmd.ExecuteScalar();
            }
        }

        private void cbMasach_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSachInfo();
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            int soLuongMuon;
            if (int.TryParse(txtSoLuong.Text, out soLuongMuon) && cbMasach.SelectedValue != null)
            {
                int maSach = Convert.ToInt32(cbMasach.SelectedValue);
                int soLuongSachCoSan = GetSoLuongSachCoSan(maSach);
                if (soLuongMuon > soLuongSachCoSan)
                {
                    ShowMessagea("Số lượng sách mượn vượt quá số lượng sách có sẵn.");
                }
            }
        }

        private void cbMasach_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
