using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmLichSuMuonSach : Form
    {
        public fmLichSuMuonSach()
        {
            InitializeComponent();
        }

        public void LoadLichSuMuonSach()
        {
            try
            {
                string connectionString = @"Data Source=DESKTOP-C7PM3MB;Initial Catalog=demoQLTV12;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        dg.HoTen AS [Tên Độc Giả],
                                        s.TenSach AS [Tên Sách],
                                        pm.NgayMuon AS [Ngày Mượn],
                                        pm.NgayTra AS [Ngày Trả]
                                    FROM 
                                        PHIEUMUON pm
                                    INNER JOIN 
                                        DOCGIA dg ON pm.MaDG = dg.MaDG
                                    INNER JOIN 
                                        CHITIETPHIEUMUON ctpm ON pm.MaPhieuMuon = ctpm.MaPhieuMuon
                                    INNER JOIN 
                                        SACH s ON ctpm.MaSach = s.MaSach
                                    ORDER BY 
                                        pm.NgayMuon DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvLichSuMuonSach.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử mượn sách: " + ex.Message);
            }
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadLichSuMuonSach();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int GetMaDG()
        {
            // Giả sử mã độc giả lấy từ một nguồn thực tế như TextBox hoặc biến toàn cục
            return 1; // Thay thế bằng mã độc giả thực tế
        }
    }
}
