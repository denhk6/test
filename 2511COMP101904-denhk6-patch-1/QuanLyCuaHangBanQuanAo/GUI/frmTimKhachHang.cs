using QuanLyCuaHangBanQuanAo.BLL;
using System;
using System.Windows.Forms;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    // Thiết kế [Design] cho form này:
    // 1. Panel Top:
    //    - TextBox (txtSearch)
    //    - Button (btnSearch)
    // 2. DataGridView (dgvTimKhachHang), Dock = Fill
    //
    // Form này sẽ có DialogResult, trả về Khách Hàng được chọn.

    public partial class frmTimKhachHang : Form
    {
        KhachHangBLL khBLL = new KhachHangBLL();

        // Thuộc tính này sẽ giữ KH được chọn
        public DTO.KhachHang SelectedKhachHang { get; private set; }

        public frmTimKhachHang()
        {
            InitializeComponent();
        }

        private void frmTimKhachHang_Load(object sender, EventArgs e)
        {
            dgvTimKhachHang.DataSource = khBLL.GetAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvTimKhachHang.DataSource = khBLL.Search(txtSearch.Text);
        }

        private void dgvTimKhachHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maKH = Convert.ToInt32(dgvTimKhachHang.Rows[e.RowIndex].Cells["MaKH"].Value);
                // Lấy thông tin chi tiết KH
                this.SelectedKhachHang = khBLL.GetByID(maKH);

                // Đặt kết quả và đóng form
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}