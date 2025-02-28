using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyThuVien
{
    public class AccessData
    {
        private string connectionString = @"Data Source=DESKTOP-EMTN9BJ\AC_PC;Initial Catalog=demoQLTV12;Integrated Security=True";

        public string ConnectionString
        {
            get { return connectionString; }
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        connection.Open();
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        public virtual object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        public virtual int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetNhaXuatBan()
        {
            string query = "SELECT * FROM NHAXUATBAN";
            return ExecuteQuery(query);
        }

        public DataTable GetTheLoai()
        {
            string query = "SELECT MaTheLoai, TenTheLoai FROM THELOAI";
            return ExecuteQuery(query);
        }

        public DataTable GetSachData()
        {
            string query = @"
            SELECT s.MaSach, s.TenSach, tl.TenTheLoai, nxb.TenNXB, s.SoLuong, s.NamXB
            FROM SACH s
            INNER JOIN THELOAI tl ON s.MaTheLoai = tl.MaTheLoai
            INNER JOIN NHAXUATBAN nxb ON s.MaNXB = nxb.MaNXB";
            return ExecuteQuery(query);
        }

        public DataTable GetDocGiaData()
        {
            string query = @"
            SELECT dg.MaDG, dg.HoTen, dg.NgaySinh, dg.GioiTinh, dg.DiaChi, dg.MaLop, dg.MaKhoa, 
                   l.TenLop, k.TenKhoa
            FROM DOCGIA dg
            INNER JOIN Lop l ON dg.MaLop = l.MaLop
            INNER JOIN Khoa k ON dg.MaKhoa = k.MaKhoa";
            return ExecuteQuery(query);
        }

        public DataTable GetKhoa()
        {
            string query = "SELECT MaKhoa, TenKhoa FROM Khoa";
            return ExecuteQuery(query);
        }

        public DataRow GetTheLoaiById(int maTheLoai)
        {
            string query = "SELECT MaTheLoai, TenTheLoai FROM THELOAI WHERE MaTheLoai = @MaTheLoai";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTheLoai", maTheLoai)
            };
            DataTable table = ExecuteQuery(query, parameters);
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }

        public DataRow GetNhaXuatBanById(int maNXB)
        {
            string query = "SELECT * FROM NHAXUATBAN WHERE MaNXB = @MaNXB";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNXB", maNXB)
            };
            DataTable table = ExecuteQuery(query, parameters);
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }

        public DataRow GetKhoaById(int maKhoa)
        {
            string query = "SELECT MaKhoa, TenKhoa FROM KHOA WHERE MaKhoa = @MaKhoa";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKhoa", maKhoa)
            };
            DataTable table = ExecuteQuery(query, parameters);
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }

        public DataTable GetLop()
        {
            string query = "SELECT MaLop, TenLop FROM Lop";
            return ExecuteQuery(query);
        }


        public DataRow GetLopById(int maLop)
        {
            string query = "SELECT MaLop, TenLop FROM Lop WHERE MaLop = @MaLop";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLop", maLop)
            };
            DataTable table = ExecuteQuery(query, parameters);
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }
    }
}
