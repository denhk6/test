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
        private Label lblMaHD;
        private Label lblNgayLap;
        private Label lblTenKH;
        private Label lblTenNV;
        private Label lblTongTien;
        private DataGridView dgvChiTiet;
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
            this.lblMaHD = new System.Windows.Forms.Label();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.lblTenNV = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMaHD
            // 
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Location = new System.Drawing.Point(40, 36);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(35, 13);
            this.lblMaHD.TabIndex = 0;
            this.lblMaHD.Text = "label1";
            this.lblMaHD.Click += new System.EventHandler(this.lblMaHD_Click);
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(194, 121);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(35, 13);
            this.lblNgayLap.TabIndex = 1;
            this.lblNgayLap.Text = "label1";
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Location = new System.Drawing.Point(89, 189);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(35, 13);
            this.lblTenKH.TabIndex = 2;
            this.lblTenKH.Text = "label1";
            // 
            // lblTenNV
            // 
            this.lblTenNV.AutoSize = true;
            this.lblTenNV.Location = new System.Drawing.Point(165, 55);
            this.lblTenNV.Name = "lblTenNV";
            this.lblTenNV.Size = new System.Drawing.Size(35, 13);
            this.lblTenNV.TabIndex = 3;
            this.lblTenNV.Text = "label1";
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new System.Drawing.Point(197, 188);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(35, 13);
            this.lblTongTien.TabIndex = 4;
            this.lblTongTien.Text = "label1";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Location = new System.Drawing.Point(32, 285);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.Size = new System.Drawing.Size(240, 150);
            this.dgvChiTiet.TabIndex = 5;
            // 
            // frmChiTietHoaDon
            // 
            this.ClientSize = new System.Drawing.Size(284, 559);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.lblTenNV);
            this.Controls.Add(this.lblTenKH);
            this.Controls.Add(this.lblNgayLap);
            this.Controls.Add(this.lblMaHD);
            this.Name = "frmChiTietHoaDon";
            this.Load += new System.EventHandler(this.frmChiTietHoaDon_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void frmChiTietHoaDon_Load_1(object sender, EventArgs e)
        {
            
        }

        private void lblMaHD_Click(object sender, EventArgs e)
        {

        }
    }
}