using QLTV;
using System;
using System.Windows.Forms;


namespace QuanLyThuVien
{
    public partial class fmMainDocGia : Form
    {


        public fmMainDocGia()
        {
            InitializeComponent();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmThongTinNhanVien fm = new fmThongTinNhanVien();
            fm.ShowDialog();

        }

        private void tìmKiếmSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmTimKiemSach fm = new fmTimKiemSach();
            fm.ShowDialog();
        }

        private void lịchSửMượnSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmLichSuMuonSach fm = new fmLichSuMuonSach();
            fm.ShowDialog();
        }

        private void đổiMâtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmDoiMatKhauTT fm = new fmDoiMatKhauTT();
            fm.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void báoCáoVàThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmBaoCaoVaThongKe fm = new fmBaoCaoVaThongKe();
            fm.ShowDialog();
        }
    }
}
