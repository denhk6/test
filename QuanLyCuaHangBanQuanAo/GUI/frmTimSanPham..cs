using QuanLyCuaHangBanQuanAo.BLL;
using QuanLyCuaHangBanQuanAo.DTO;
using System;
using System.Windows.Forms;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    // Thiết kế [Design] cho form này:
    // Tương tự frmTimKhachHang
    // 1. Panel Top:
    //    - TextBox (txtSearch)
    //    - Button (btnSearch)
    // 2. DataGridView (dgvTimSanPham), Dock = Fill

    public partial class frmTimSanPham : Form
    {
        SanPhamBLL spBLL = new SanPhamBLL();

        // Thuộc tính này sẽ giữ SP được chọn
        public DTO.SanPham SelectedSanPham { get; private set; }

        public frmTimSanPham()
        {
            InitializeComponent();
        }

        private void frmTimSanPham_Load(object sender, EventArgs e)
        {
            dgvTimSanPham.DataSource = spBLL.GetAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvTimSanPham.DataSource = spBLL.Search(txtSearch.Text);
        }

        private void dgvTimSanPham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maSP = Convert.ToInt32(dgvTimSanPham.Rows[e.RowIndex].Cells["MaSP"].Value);
                // Lấy thông tin chi tiết SP
                this.SelectedSanPham = spBLL.GetByID(maSP);

                // Đặt kết quả và đóng form
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}