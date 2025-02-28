using NUnit.Framework;
using QuanLyThuVien;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NUnit_Test
{
    [TestFixture]
    public class NUnitTest_FmQLMuonSach 
    {
        private fmQuanLyMuonSach form;
        // Override phương thức ShowMessagea trong lớp kiểm thử
        private class TestableFmQuanLyMuonSach : fmQuanLyMuonSach
        {
            private string _message;

            public string GetLastMessage() => _message;

            public override DialogResult ShowMessagea(string message)
            {
                _message = message; // Lưu lại nội dung MessageBox để test
                return DialogResult.OK; // Giả lập luôn nhấn "OK"
            }
        }

        // Các phương thức kiểm thử khác...
       
        [SetUp]
        public void Setup()
        {
            try
            {
                // Khởi tạo form
                form = new TestableFmQuanLyMuonSach();
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
        public void Test_MuonSach()
        {
            var formMS = new TestableFmQuanLyMuonSach();
            //formMS.Show();
            // Chạy form
            
            //Thread.Sleep(10000);

            formMS.SetMaSach("1");
            formMS.SetMaDocGia("1");
            formMS.SetNgayMuon(DateTime.Now);
            formMS.SetNgayTra(DateTime.Now.AddDays(1));
            formMS.SetSoLuongMuon("");

            formMS.Test_btnMuonSach();

            string actualMessage = ((TestableFmQuanLyMuonSach)formMS).GetLastMessage();
            Thread.Sleep(5000);
            string checkQuery = "SELECT COUNT(*) FROM PHIEUMUON WHERE MaPhieuMuon = @MaPhieuMuon";
            SqlParameter[] checkParams = { new SqlParameter("@MaPhieuMuon", 11) };

            int userCount = (int)new AccessData().ExecuteScalar(checkQuery, checkParams);

            //formMS.Close();
            
            Assert.AreEqual(1, userCount, $"Test FAIL: {actualMessage}");
            Console.WriteLine();
        }
        [Test]
        public void Test_MuonSach2()
        {
            //var form = new fmQuanLyMuonSach();
            //form.Show();
            // Chạy form

            //Thread.Sleep(10000);

            form.SetMaSach("");
            form.SetMaDocGia("1");
            form.SetNgayMuon(DateTime.Now);
            form.SetNgayTra(DateTime.Now.AddDays(1));
            form.SetSoLuongMuon("1");

            form.Test_btnMuonSach();
            string actualMessage = ((TestableFmQuanLyMuonSach)form).GetLastMessage();
            Thread.Sleep(5000);
            string checkQuery = "SELECT COUNT(*) FROM PHIEUMUON WHERE MaPhieuMuon = @MaPhieuMuon";
            SqlParameter[] checkParams = { new SqlParameter("@MaPhieuMuon", 11) };

            int userCount = (int)new AccessData().ExecuteScalar(checkQuery, checkParams);

            //form.Close();

            Assert.AreEqual(1, userCount, $"Test FAIL: {actualMessage}");
            Console.WriteLine();
        }
        [Test]
        public void Test_MuonSach3_Pass()
        {
            //var form = new fmQuanLyMuonSach();
            //form.Show();
            // Chạy form

            //Thread.Sleep(10000);

            form.SetMaSach("1");
            form.SetMaDocGia("1");
            form.SetNgayMuon(DateTime.Now);
            form.SetNgayTra(DateTime.Now.AddDays(1));
            form.SetSoLuongMuon("1");

            form.Test_btnMuonSach();
            string actualMessage = ((TestableFmQuanLyMuonSach)form).GetLastMessage();
            Thread.Sleep(5000);
            string checkQuery = "SELECT COUNT(*) FROM PHIEUMUON WHERE MaPhieuMuon = @MaPhieuMuon";
            SqlParameter[] checkParams = { new SqlParameter("@MaPhieuMuon", 14) };

            int userCount = (int)new AccessData().ExecuteScalar(checkQuery, checkParams);

            //form.Close();

            Assert.AreEqual(1, userCount, $"Test FAIL: {actualMessage}");
            Console.WriteLine();
        }
        [TearDown]
        public void TearDown()
        {
            form.Dispose();
            form.Close();
        }
    }
}
