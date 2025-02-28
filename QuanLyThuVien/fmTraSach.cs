using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System;

namespace QuanLyThuVien
{
    public partial class fmTraSach : Form
    {
        private AccessData dataAccess;
        private GroupBox groupBox2;
        private ComboBox cbMasach;
        private DateTimePicker dtpNgayTra;
        private Button btnTraSach;
        private Label label10;
        private Label label9;
        private TextBox txtSoLuong;
        private DateTimePicker dtpNgayMuon;
        private TextBox txtTenSach;
        private Label label6;
        private Label label1;
        private Label label2;
        private bool isProcessing = false; // Biến cờ để kiểm tra trạng thái xử lý

        public fmTraSach()
        {
            InitializeComponent();
            dataAccess = new AccessData();
            LoadSachComboBox();
            cbMasach.SelectedIndexChanged += CbMasach_SelectedIndexChanged;
            btnTraSach.Click += btnTraSach_Click;
        }

        private void CbMasach_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSachInfo();
        }

        private void LoadSachComboBox()
        {
            try
            {
                string query = "SELECT MaPhieuMuon FROM CHITIETPHIEUMUON";
                DataTable data = dataAccess.ExecuteQuery(query);
                cbMasach.DataSource = data;
                cbMasach.DisplayMember = "MaPhieuMuon";
                cbMasach.ValueMember = "MaPhieuMuon";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu sách: " + ex.Message);
            }
        }

        private void LoadSachInfo()
        {
            if (cbMasach.SelectedValue != null)
            {
                string maPhieuMuon = cbMasach.SelectedValue.ToString();
                string query = @"
            SELECT S.TenSach, S.SoLuong AS SoLuongCon, 
                   C.SoLuongMuon AS SoLuongMuon, 
                   P.NgayMuon, P.NgayTra
            FROM CHITIETPHIEUMUON C 
            JOIN SACH S ON C.MaSach = S.MaSach
            JOIN PHIEUMUON P ON C.MaPhieuMuon = P.MaPhieuMuon
            WHERE C.MaPhieuMuon = @MaPhieuMuon";

                using (SqlConnection conn = new SqlConnection(dataAccess.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtTenSach.Text = reader["TenSach"] != DBNull.Value ? reader["TenSach"].ToString() : string.Empty;
                        int soLuongCon = reader["SoLuongCon"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongCon"]) : 0;
                        int soLuongMuon = reader["SoLuongMuon"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongMuon"]) : 0;
                        txtSoLuong.Text = soLuongMuon.ToString();
                        DateTime ngayMuon = reader["NgayMuon"] != DBNull.Value ? Convert.ToDateTime(reader["NgayMuon"]) : DateTime.Now;
                        DateTime ngayTra = reader["NgayTra"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTra"]) : DateTime.Now;
                        dtpNgayMuon.Value = ngayMuon;
                        dtpNgayTra.Value = ngayTra;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin.");
                        txtTenSach.Clear();
                        txtSoLuong.Clear();
                    }
                }
            }
        }

        private void TraSachVaCapNhat()
        {
            if (isProcessing) // Kiểm tra trạng thái xử lý
            {
                MessageBox.Show("Đang xử lý, vui lòng chờ.");
                return;
            }

            try
            {
                isProcessing = true; // Đặt cờ trạng thái là đang xử lý

                if (cbMasach.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn mã phiếu mượn.");
                    return;
                }

                string maPhieuMuon = cbMasach.SelectedValue.ToString();
                int soLuongMuon = int.Parse(txtSoLuong.Text);
                DateTime ngayTra = dtpNgayTra.Value;

                using (SqlConnection conn = new SqlConnection(dataAccess.ConnectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string query = @"
                    SELECT C.MaSach, P.MaDG, C.SoLuongMuon
                    FROM CHITIETPHIEUMUON C 
                    JOIN PHIEUMUON P ON C.MaPhieuMuon = P.MaPhieuMuon
                    WHERE C.MaPhieuMuon = @MaPhieuMuon";

                            SqlCommand cmd = new SqlCommand(query, conn, transaction);
                            cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                            SqlDataReader reader = cmd.ExecuteReader();

                            if (reader.Read())
                            {
                                int maSach = reader["MaSach"] != DBNull.Value ? Convert.ToInt32(reader["MaSach"]) : 0;
                                int maDocGia = reader["MaDG"] != DBNull.Value ? Convert.ToInt32(reader["MaDG"]) : 0;
                                int soLuongMuonTruoc = reader["SoLuongMuon"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongMuon"]) : 0;

                                if (soLuongMuon > soLuongMuonTruoc)
                                {
                                    MessageBox.Show("Số lượng sách trả không hợp lệ.");
                                    transaction.Rollback();
                                    return;
                                }

                                reader.Close();

                                // Cập nhật số lượng sách trong bảng SACH
                                cmd = new SqlCommand("UPDATE SACH SET SoLuong = SoLuong + @SoLuongMuon WHERE MaSach = @MaSach", conn, transaction);
                                cmd.Parameters.AddWithValue("@SoLuongMuon", soLuongMuon);
                                cmd.Parameters.AddWithValue("@MaSach", maSach);
                                cmd.ExecuteNonQuery();

                                // Cập nhật số lượng mượn trong bảng CHITIETPHIEUMUON
                                cmd = new SqlCommand("UPDATE CHITIETPHIEUMUON SET SoLuongMuon = SoLuongMuon - @SoLuongMuon WHERE MaPhieuMuon = @MaPhieuMuon AND MaSach = @MaSach", conn, transaction);
                                cmd.Parameters.AddWithValue("@SoLuongMuon", soLuongMuon);
                                cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                                cmd.Parameters.AddWithValue("@MaSach", maSach);
                                cmd.ExecuteNonQuery();

                                // Xóa phiếu mượn nếu không còn sách nào trong phiếu mượn
                                cmd = new SqlCommand("IF NOT EXISTS (SELECT 1 FROM CHITIETPHIEUMUON WHERE MaPhieuMuon = @MaPhieuMuon AND SoLuongMuon > 0) DELETE FROM CHITIETPHIEUMUON WHERE MaPhieuMuon = @MaPhieuMuon", conn, transaction);
                                cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                                cmd.ExecuteNonQuery();

                                // Cập nhật ngày trả trong bảng PHIEUMUON
                                cmd = new SqlCommand("UPDATE PHIEUMUON SET NgayTra = @NgayTra WHERE MaPhieuMuon = @MaPhieuMuon", conn, transaction);
                                cmd.Parameters.AddWithValue("@NgayTra", ngayTra);
                                cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                                cmd.ExecuteNonQuery();

                                // Xóa phiếu mượn nếu không còn chi tiết phiếu mượn nào
                                cmd = new SqlCommand("IF NOT EXISTS (SELECT 1 FROM CHITIETPHIEUMUON WHERE MaPhieuMuon = @MaPhieuMuon) DELETE FROM PHIEUMUON WHERE MaPhieuMuon = @MaPhieuMuon", conn, transaction);
                                cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                                cmd.ExecuteNonQuery();

                                transaction.Commit();

                                MessageBox.Show("Trả sách thành công!");

                                // Làm sạch các điều khiển trên form
                                cbMasach.SelectedIndex = -1;
                                txtTenSach.Clear();
                                txtSoLuong.Clear();
                                dtpNgayMuon.Value = DateTime.Now;
                                dtpNgayTra.Value = DateTime.Now;

                                LoadSachComboBox(); // Tải lại dữ liệu cho ComboBox
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy phiếu mượn.");
                                transaction.Rollback();
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Đã xảy ra lỗi khi trả sách: " + ex.Message);
                        }
                        finally
                        {
                            isProcessing = false; // Đặt cờ trạng thái về chưa xử lý
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi trả sách: " + ex.Message);
                isProcessing = false; // Đặt cờ trạng thái về chưa xử lý
            }
        }



        private void btnTraSach_Click(object sender, EventArgs e)
        {
            TraSachVaCapNhat();
        }

        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMasach = new System.Windows.Forms.ComboBox();
            this.dtpNgayTra = new System.Windows.Forms.DateTimePicker();
            this.btnTraSach = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.dtpNgayMuon = new System.Windows.Forms.DateTimePicker();
            this.txtTenSach = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbMasach);
            this.groupBox2.Controls.Add(this.dtpNgayTra);
            this.groupBox2.Controls.Add(this.btnTraSach);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtSoLuong);
            this.groupBox2.Controls.Add(this.dtpNgayMuon);
            this.groupBox2.Controls.Add(this.txtTenSach);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(531, 353);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin trả sách";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.label2.Location = new System.Drawing.Point(91, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 19);
            this.label2.TabIndex = 36;
            this.label2.Text = "Tên sách";
            // 
            // cbMasach
            // 
            this.cbMasach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMasach.FormattingEnabled = true;
            this.cbMasach.Location = new System.Drawing.Point(217, 34);
            this.cbMasach.Name = "cbMasach";
            this.cbMasach.Size = new System.Drawing.Size(202, 27);
            this.cbMasach.TabIndex = 35;
            // 
            // dtpNgayTra
            // 
            this.dtpNgayTra.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayTra.Location = new System.Drawing.Point(216, 165);
            this.dtpNgayTra.Name = "dtpNgayTra";
            this.dtpNgayTra.Size = new System.Drawing.Size(203, 27);
            this.dtpNgayTra.TabIndex = 33;
            // 
            // btnTraSach
            // 
            this.btnTraSach.BackColor = System.Drawing.Color.Aqua;
            this.btnTraSach.Location = new System.Drawing.Point(235, 268);
            this.btnTraSach.Name = "btnTraSach";
            this.btnTraSach.Size = new System.Drawing.Size(106, 42);
            this.btnTraSach.TabIndex = 10;
            this.btnTraSach.Text = "Trả sách";
            this.btnTraSach.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.label10.Location = new System.Drawing.Point(91, 173);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 19);
            this.label10.TabIndex = 20;
            this.label10.Text = "Ngày Trả:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.label9.Location = new System.Drawing.Point(91, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 19);
            this.label9.TabIndex = 22;
            this.label9.Text = "Ngày Mượn:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(215, 214);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(204, 27);
            this.txtSoLuong.TabIndex = 30;
            // 
            // dtpNgayMuon
            // 
            this.dtpNgayMuon.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayMuon.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayMuon.Location = new System.Drawing.Point(216, 117);
            this.dtpNgayMuon.Name = "dtpNgayMuon";
            this.dtpNgayMuon.Size = new System.Drawing.Size(203, 27);
            this.dtpNgayMuon.TabIndex = 33;
            // 
            // txtTenSach
            // 
            this.txtTenSach.ForeColor = System.Drawing.Color.Black;
            this.txtTenSach.Location = new System.Drawing.Point(216, 72);
            this.txtTenSach.Name = "txtTenSach";
            this.txtTenSach.Size = new System.Drawing.Size(203, 27);
            this.txtTenSach.TabIndex = 29;
            this.txtTenSach.Text = "Tên sách";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.label6.Location = new System.Drawing.Point(91, 222);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 19);
            this.label6.TabIndex = 21;
            this.label6.Text = "SL Mượn:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.label1.Location = new System.Drawing.Point(91, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 19);
            this.label1.TabIndex = 18;
            this.label1.Text = "Nhập Mã Mượn:";
            // 
            // fmTraSach
            // 
            this.ClientSize = new System.Drawing.Size(555, 377);
            this.Controls.Add(this.groupBox2);
            this.Name = "fmTraSach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ TRẢ SÁCH";
            this.Load += new System.EventHandler(this.fmTraSach_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        private void fmTraSach_Load(object sender, EventArgs e)
        {

        }

    }
}
