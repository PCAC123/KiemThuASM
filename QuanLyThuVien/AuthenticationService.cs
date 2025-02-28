using System;

namespace QuanLyThuVien
{
    public class AuthenticationService
    {
        public bool AuthenticateUser(string username, string password, out int roleId)
        {
            AccessData accessData = new AccessData();
            string query = $"SELECT role_id FROM user_roles WHERE username = '{username}' AND password = '{password}'";
            object result = accessData.ExecuteScalar(query);

            if (result != null)
            {
                roleId = Convert.ToInt32(result);
                return true;
            }

            roleId = -1;
            return false;
        }
    }
}
