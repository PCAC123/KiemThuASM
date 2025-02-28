using QLTV;
using System;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class fmMainThuThu : Form
    {
        public fmMainThuThu()
        {
            InitializeComponent();
        }


        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmDoiMatKhauTT frmDoiMatKhau = new fmDoiMatKhauTT();
            frmDoiMatKhau.ShowDialog();
        }

        private void quảnLýSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmQLSach frmQuanLySach = new fmQLSach();
            frmQuanLySach.ShowDialog();
        }

        private void quảnLýĐộcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmQuanLyDG fmQuanLyDG = new fmQuanLyDG();
            fmQuanLyDG.ShowDialog();
        }

        private void phiếuMượnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmQuanLyMuonSach frmQuanLyMuonSach = new fmQuanLyMuonSach();
            frmQuanLyMuonSach.ShowDialog();
        }       

        private void báoCáoThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmBaoCaoVaThongKe frmBaoCaoThongKe = new fmBaoCaoVaThongKe();
            frmBaoCaoThongKe.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lịchSửMượnSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmLichSuMuonSach frmLichSuMuonSach = new fmLichSuMuonSach();
            frmLichSuMuonSach.ShowDialog();
        }

        private void lậpPhiếuTrảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmTraSach fm = new fmTraSach();
            fm.ShowDialog();
        }

        private void quảnLýLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmLop fm = new fmLop();
            fm.ShowDialog();
        }

        private void quảnLýKhoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmKhoa fm = new fmKhoa();
            fm.ShowDialog();
        }

        private void quảnLýThểLoạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmTheLoai fm = new fmTheLoai();
            fm.ShowDialog();
        }

        private void quảnLýNhàXuấtBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmNhaXuatBan fm = new fmNhaXuatBan();
            fm.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fmTimKiemSach fm = new fmTimKiemSach();
            fm.ShowDialog();
        }
    }
}
