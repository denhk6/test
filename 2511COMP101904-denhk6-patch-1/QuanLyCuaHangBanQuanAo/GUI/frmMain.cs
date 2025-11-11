using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangBanQuanAo.DTO;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    public partial class frmMain : Form
    {
        private TaiKhoan currentUser;

        public frmMain()
        {
            InitializeComponent();
        }
        public frmMain(TaiKhoan user) : this()
        {
            currentUser = user;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyUserPermissions();
        }

        private void ApplyUserPermissions()
        {
            if (currentUser == null)
            {
                buttonPhanQuyen.Visible = false;
                return;
            }

            labelUsername.Text = currentUser.TK;
            labelStatus.Text = $"Xin chào: {currentUser.TK}";

            bool isAdmin = string.Equals(currentUser.Role ?? string.Empty, "admin", StringComparison.OrdinalIgnoreCase);

            if (!isAdmin)
            {
                buttonPhanQuyen.Visible = false;
            }
            else
            {
                buttonPhanQuyen.Visible = true;
            }
        }

        private void labelUsername_Click(object sender, EventArgs e)
        {

        }

        private void buttonTongQuan_Click(object sender, EventArgs e)
        {

        }

        private void labelDateTime_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            labelDateTime.Text = "Hôm nay: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void labelTitle2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonGioiThieu_Click(object sender, EventArgs e)
        {
            frmGioiThieu gioiThieu = new frmGioiThieu();
            gioiThieu.ShowDialog();
        }

        private void buttonDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau doiMatKhau = new frmDoiMatKhau();
            doiMatKhau.ShowDialog();
        }

        private void buttonBanHang_Click(object sender, EventArgs e)
        {

        }
    }
}
