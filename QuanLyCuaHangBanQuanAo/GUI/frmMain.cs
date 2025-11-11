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
using QuanLyCuaHangBanQuanAo.DAL;

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

        // ĐÃ SỬA LẠI HÀM NÀY
        private void ApplyUserPermissions()
        {
            if (currentUser == null)
            {
                buttonPhanQuyen.Visible = false;
                return;
            }

            // Sửa: Dùng TenDangNhap (từ DTO/TaiKhoan.cs đã sửa)
            labelUsername.Text = currentUser.TenDangNhap;
            labelStatus.Text = $"Xin chào: {currentUser.TenDangNhap}";

            // Sửa: Lấy quyền (role) từ CSDL dựa trên MaQuyen
            string userRole = "user"; // Mặc định là user
            try
            {
                using (var db = new QuanLyCuaHangBanQuanAo.DAL.QuanLyCuaHangDataContextDataContext())
                {
                    // Tìm tên quyền dựa trên MaQuyen của người dùng
                    var quyen = db.PhanQuyens.FirstOrDefault(q => q.MaQuyen == currentUser.MaQuyen);
                    if (quyen != null)
                    {
                        userRole = quyen.TenQuyen; // Ví dụ: "admin", "user"
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải quyền: " + ex.Message);
            }

            bool isAdmin = string.Equals(userRole ?? string.Empty, "admin", StringComparison.OrdinalIgnoreCase);

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

        // ĐÃ SỬA LẠI HÀM NÀY
        private void buttonDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("Lỗi: Không tìm thấy thông tin người dùng.");
                return;
            }

            // Sửa: Dùng TenDangNhap
            frmDoiMatKhau doiMatKhau = new frmDoiMatKhau(currentUser.TenDangNhap);
            doiMatKhau.ShowDialog();
        }

        private void buttonBanHang_Click(object sender, EventArgs e)
        {
            // Sửa: Truyền currentUser vào frmHoaDon
            if (currentUser == null)
            {
                MessageBox.Show("Lỗi: Không thể xác định người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmHoaDon frmHD = new frmHoaDon(currentUser); // Truyền thông tin người dùng
            frmHD.ShowDialog();
        }
    }
}