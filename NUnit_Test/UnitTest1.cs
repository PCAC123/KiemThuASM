using Moq;
using NUnit.Framework;
using QuanLyThuVien;
using System.Windows.Forms;
using System;
using System.Data.SqlClient;
using System.Threading;

namespace NUnit_Test
{
    [TestFixture]
    public class Tests
    {
        private fmThongTinNhanVien form;
        // Kế thừa fmThongTinNhanVien để ghi đè ShowMessage
       
        [SetUp]
        public void Setup()
        {
            try
            {
                // Khởi tạo form
                form = new fmThongTinNhanVien();
                if (form == null)
                {
                    throw new Exception("form is null!");
                }
                Console.WriteLine("Setup completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Setup failed: " + ex.Message);
                throw;
            }
        }

        [Test]
        public void Test_ThemDtabase()
        {
            // Arrange: Gán dữ liệu vào form
            string tenDangNhap = "user12043";

            form.SetHoTen("Nguyễn Văn A");
            form.SetNgaySinh(DateTime.Now.AddYears(-25));
            form.SetDiaChi("Địa chỉ XYZ khang poly khang poly khang poly");
            form.SetSDT("0901234567");
            form.SetTenDangNhap(tenDangNhap);
            form.SetMatKhau("password");

            // Act: Gọi sự kiện thêm nhân viên (bằng cách gọi Test_ThemNhanVien)
            form.Test_ThemNhanVien();

            // Assert: Kiểm tra dữ liệu có trong CSDL không
            string checkQuery = "SELECT COUNT(*) FROM user_roles WHERE username = @Username";
            SqlParameter[] checkParams = { new SqlParameter("@Username", tenDangNhap) };

            int userCount = (int)new AccessData().ExecuteScalar(checkQuery, checkParams);

            Assert.AreEqual(1, userCount, "Nhân viên không được thêm vào CSDL thực sự");
        }
        [Test]
        public void Test_ThemDtabase2_Fail()
        {
            // Arrange: Gán dữ liệu vào form
            string tenDangNhap = "user12031";

            form.SetHoTen("");
            form.SetNgaySinh(DateTime.Now.AddYears(-25));
            form.SetDiaChi("Địa chỉ XYZ khang poly khang poly khang poly");
            form.SetSDT("0901234567");
            form.SetTenDangNhap(tenDangNhap);
            form.SetMatKhau("password");

            // Act: Gọi sự kiện thêm nhân viên (bằng cách gọi Test_ThemNhanVien)
            form.Test_ThemNhanVien();

            // Assert: Kiểm tra dữ liệu có trong CSDL không
            string checkQuery = "SELECT COUNT(*) FROM user_roles WHERE username = @Username";
            SqlParameter[] checkParams = { new SqlParameter("@Username", tenDangNhap) };

            int userCount = (int)new AccessData().ExecuteScalar(checkQuery, checkParams);

            Assert.AreEqual(1, userCount, "Nhân viên không được thêm vào CSDL thực sự");
        }

        [Test]
        public void Test_ThemDatabase3_Fail_OpenForm()
        {
            // Arrange: Mở form thực tế
            var formTT = new fmThongTinNhanVien();
            formTT.Show();
            Thread.Sleep(10000);
            string tenDangNhap = "user1210431";

            // Nhập dữ liệu vào form
            formTT.SetHoTen("aaaa");
            formTT.SetNgaySinh(DateTime.Now.AddYears(-25));
            formTT.SetDiaChi("Địa chỉ XYZ khang poly khang poly khang poly");
            formTT.SetSDT("");
            formTT.SetTenDangNhap(tenDangNhap);
            formTT.SetMatKhau("password");

            // Act: Nhấn nút thêm nhân viên (hoặc gọi hàm Test_ThemNhanVien)
            formTT.Test_ThemNhanVien();
            //string actualMessage = ((TestableFmThongTinNhanVien)formTT).GetLastMessage();
            // Assert: Kiểm tra dữ liệu có trong CSDL không

            Thread.Sleep(2000);
            string checkQuery = "SELECT COUNT(*) FROM user_roles WHERE username = @Username";
            SqlParameter[] checkParams = { new SqlParameter("@Username", tenDangNhap) };

            int userCount = (int)new AccessData().ExecuteScalar(checkQuery, checkParams);

            // Đóng form sau khi test
            formTT.Close();

            Assert.AreEqual(1, userCount, "❌ Nhân viên không được thêm vào CSDL thực sự!");
        }

    }
}