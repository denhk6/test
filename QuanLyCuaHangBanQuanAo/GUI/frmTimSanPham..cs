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
        private DataGridView dgvTimSanPham;
        private TextBox txtSearch;
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

        private void InitializeComponent()
        {
            this.dgvTimSanPham = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimSanPham)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTimSanPham
            // 
            this.dgvTimSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimSanPham.Location = new System.Drawing.Point(21, 21);
            this.dgvTimSanPham.Name = "dgvTimSanPham";
            this.dgvTimSanPham.Size = new System.Drawing.Size(240, 150);
            this.dgvTimSanPham.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(189, 206);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // frmTimSanPham
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvTimSanPham);
            this.Name = "frmTimSanPham";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimSanPham)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}