using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmBaoCaoVaThongKe : Form
    {
        private AccessData dataAccess;

        public fmBaoCaoVaThongKe()
        {
            InitializeComponent();
            dataAccess = new AccessData();
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            // Thêm các mục cho báo cáo
            cmbBaoCao.Items.Add("Thống kê sách được mượn nhiều nhất");
            cmbBaoCao.Items.Add("Thống kê số lượng sách còn lại");

            // Thêm các mục cho thống kê
            cmbThongKe.Items.Add("Thống kê số lượng sách theo thể loại");
            cmbThongKe.Items.Add("Thống kê số lượng độc giả đã mượn sách");
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (cmbBaoCao.SelectedItem != null)
            {
                string reportType = cmbBaoCao.SelectedItem.ToString();
                ShowReport(reportType);
            }
            else
            {
                lblThongBao.Text = "Vui lòng chọn loại báo cáo.";
            }
        }

        private void btnXemThongKe_Click(object sender, EventArgs e)
        {
            if (cmbThongKe.SelectedItem != null)
            {
                string statisticType = cmbThongKe.SelectedItem.ToString();
                ShowStatistic(statisticType);
            }
            else
            {
                lblThongBao.Text = "Vui lòng chọn loại thống kê.";
            }
        }

        private void ShowReport(string reportType)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(dataAccess.ConnectionString))
                {
                    conn.Open();
                    string query = "";
                    switch (reportType)
                    {
                        case "Thống kê sách được mượn nhiều nhất":
                            query = "SELECT TenSach, COUNT(*) AS SoLanMuon " +
                                    "FROM CHITIETPHIEUMUON " +
                                    "INNER JOIN SACH ON CHITIETPHIEUMUON.MaSach = SACH.MaSach " +
                                    "GROUP BY TenSach " +
                                    "ORDER BY SoLanMuon DESC";
                            break;

                        case "Thống kê số lượng sách còn lại":
                            query = "SELECT TenSach, SoLuong - ISNULL(SUM(C.SoLuongMuon), 0) AS SoLuongConLai " +
                                    "FROM SACH S " +
                                    "LEFT JOIN (SELECT MaSach, SUM(SoLuongMuon) AS SoLuongMuon " +
                                    "            FROM CHITIETPHIEUMUON " +
                                    "            GROUP BY MaSach) C " +
                                    "ON S.MaSach = C.MaSach " +
                                    "GROUP BY TenSach, SoLuong";
                            break;
                    }

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }

                dgvKetQua.DataSource = dt;
                lblThongBao.Text = "Hiển thị báo cáo thành công.";
            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Đã xảy ra lỗi khi hiển thị báo cáo: " + ex.Message;
            }
        }

        private void ShowStatistic(string statisticType)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(dataAccess.ConnectionString))
                {
                    conn.Open();
                    string query = "";

                    switch (statisticType)
                    {
                        case "Thống kê số lượng sách theo thể loại":
                            query = "SELECT THELOAI.TenTheLoai, COUNT(SACH.MaSach) AS SoLuongSach " +
                                    "FROM SACH " +
                                    "INNER JOIN THELOAI ON SACH.MaTheLoai = THELOAI.MaTheLoai " +
                                    "GROUP BY THELOAI.TenTheLoai";
                            break;

                        case "Thống kê số lượng độc giả đã mượn sách":
                            query = "SELECT DOCGIA.HoTen, COUNT(PHIEUMUON.MaPhieuMuon) AS SoPhieuMuon " +
                                    "FROM DOCGIA " +
                                    "LEFT JOIN PHIEUMUON ON DOCGIA.MaDG = PHIEUMUON.MaDG " +
                                    "GROUP BY DOCGIA.HoTen";
                            break;
                    }

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }

                dgvKetQua.DataSource = dt;
                lblThongBao.Text = "Hiển thị thống kê thành công.";
            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Đã xảy ra lỗi khi hiển thị thống kê: " + ex.Message;
            }
        }
    }
}
