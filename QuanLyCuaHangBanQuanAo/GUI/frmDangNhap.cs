using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangBanQuanAo.BLL;
using QuanLyCuaHangBanQuanAo.DTO;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    public partial class frmDangNhap : Form
    {
        TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string taiKhoan = txtUsername.Text.Trim();
            string matKhau = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(taiKhoan))
            {
                MessageBox.Show("Vui lòng nhập tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                if (taiKhoanBLL.KiemTraDangNhap(taiKhoan, matKhau))
                {
                    TaiKhoan tk = taiKhoanBLL.LayThongTinTaiKhoan(taiKhoan);

                    MessageBox.Show($"Đăng nhập thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    frmMain main = new frmMain(tk);
                    main.FormClosed += (s, args) => {
                        if (main.DialogResult == DialogResult.Retry)
                        {
                            txtUsername.Clear();
                            txtPassword.Clear();
                            txtUsername.Focus();
                            this.Show();
                        }
                        else
                        {
                            this.Close();
                        }
                    };
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
