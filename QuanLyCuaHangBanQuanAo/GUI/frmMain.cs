using System;
using System.Linq;
using System.Windows.Forms;
using QuanLyCuaHangBanQuanAo.DAL; // <-- Thêm dòng này
using DTOTaiKhoan = QuanLyCuaHangBanQuanAo.DTO.TaiKhoan; // Sửa lỗi "ambiguous"

namespace QuanLyCuaHangBanQuanAo.GUI
{
    public partial class frmMain : Form
    {
        private DTOTaiKhoan currentUser; // Sửa: Dùng DTO.TaiKhoan

        public frmMain()
        {
            InitializeComponent();
        }

        // Sửa: Dùng DTO.TaiKhoan
        public frmMain(DTOTaiKhoan user) : this()
        {
            currentUser = user;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyUserPermissions();
        }

        // Sửa: Logic quyền và tên hiển thị
        private void ApplyUserPermissions()
        {
            if (currentUser == null)
            {
                buttonPhanQuyen.Visible = false;
                return;
            }

            // Sửa: Dùng TenDangNhap
            labelUsername.Text = currentUser.TenDangNhap;
            labelStatus.Text = $"Xin chào: {currentUser.TenDangNhap}";

            string userRole = "user"; // Mặc định
            try
            {
                // Sửa: Dùng tên DataContext đúng
                using (var db = new QuanLyCuaHangDataContext())
                {
                    var quyen = db.PhanQuyens.FirstOrDefault(q => q.MaQuyen == currentUser.MaQuyen);
                    if (quyen != null) { userRole = quyen.TenQuyen; }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi tải quyền: " + ex.Message); }

            // Sửa: Thêm "using System;" ở đầu tệp (nếu báo lỗi StringComparison)
            bool isAdmin = string.Equals(userRole, "admin", StringComparison.OrdinalIgnoreCase);
            buttonPhanQuyen.Visible = isAdmin;
        }

        // (Các hàm sự kiện trống khác)
        private void labelUsername_Click(object sender, EventArgs e) { }
        private void buttonTongQuan_Click(object sender, EventArgs e) { }
        private void labelDateTime_Click(object sender, EventArgs e) { }
        private void panelMain_Paint(object sender, PaintEventArgs e) { }
        private void labelTitle2_Click(object sender, EventArgs e) { }
        private void panelTop_Paint(object sender, PaintEventArgs e) { }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            labelDateTime.Text = "Hôm nay: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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

        private void buttonGioiThieu_Click(object sender, EventArgs e)
        {
            frmGioiThieu gioiThieu = new frmGioiThieu();
            gioiThieu.ShowDialog();
        }

        // Sửa: Dùng TenDangNhap
        private void buttonDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("Lỗi: Không tìm thấy thông tin người dùng.");
                return;
            }

            frmDoiMatKhau doiMatKhau = new frmDoiMatKhau(currentUser.TenDangNhap);
            doiMatKhau.ShowDialog();
        }

        private void buttonBanHang_Click(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("Lỗi: Không thể xác định người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Sửa: Truyền currentUser (đã đúng kiểu DTO)
            frmHoaDon frmHD = new frmHoaDon(currentUser);
            frmHD.ShowDialog();
        }
    }
}
