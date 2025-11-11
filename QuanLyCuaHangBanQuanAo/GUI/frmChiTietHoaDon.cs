using QuanLyCuaHangBanQuanAo.BLL;
using System;
using System.Windows.Forms;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    // Đây là file code-behind cho form XEM Chi Tiết Hóa Đơn.
    // Form này chỉ dùng để XEM, không SỬA.
    // Form này được gọi từ một form khác (ví dụ: Báo cáo) và truyền vào MaHD.
    //
    // Thiết kế [Design]:
    // 1. GroupBox "Thông tin hóa đơn":
    //    - lblMaHD
    //    - lblNgayLap
    //    - lblTenKH
    //    - lblTenNV
    //    - lblTongTien
    // 2. DataGridView (Tên: dgvChiTiet)
    //    - (Cột: MaSP, TenSP, SoLuong, DonGia, ThanhTien)
    // 3. Button (Tên: btnDong)

    public partial class frmChiTietHoaDon : Form
    {
        private int _maHD; // Mã HĐ được truyền vào
        HoaDonBLL hdBLL = new HoaDonBLL();

        public frmChiTietHoaDon(int maHD)
        {
            InitializeComponent();
            _maHD = maHD;
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            // 1. Tải thông tin Header
            DTO.HoaDon hdHeader = hdBLL.GetHeaderByID(_maHD);
            if (hdHeader != null)
            {
                lblMaHD.Text = hdHeader.MaHD.ToString();
                lblNgayLap.Text = hdHeader.NgayLap.ToShortDateString();
                lblTenKH.Text = hdHeader.TenKH;
                lblTenNV.Text = hdHeader.TenNV;
                lblTongTien.Text = hdHeader.TongTien.ToString("N0") + " VNĐ";
            }

            // 2. Tải chi tiết sản phẩm
            dgvChiTiet.DataSource = hdBLL.GetChiTiet(_maHD);

            // Ẩn các cột không cần thiết nếu DTO có nhiều thuộc tính
            dgvChiTiet.Columns["MaHD"].Visible = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmChiTietHoaDon
            // 
            this.ClientSize = new System.Drawing.Size(284, 323);
            this.Name = "frmChiTietHoaDon";
            this.Load += new System.EventHandler(this.frmChiTietHoaDon_Load_1);
            this.ResumeLayout(false);

        }

        private void frmChiTietHoaDon_Load_1(object sender, EventArgs e)
        {
            
        }
    }
}