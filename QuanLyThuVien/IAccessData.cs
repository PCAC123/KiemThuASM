using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    public interface IAccessData
    {
        object ExecuteScalar(string query, SqlParameter[] parameters = null);
        int ExecuteNonQuery(string query, SqlParameter[] parameters = null);
    }

}
