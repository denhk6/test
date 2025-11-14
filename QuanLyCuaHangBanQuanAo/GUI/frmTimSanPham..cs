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

        // Add this declaration for txtSearch and btnSearch
        private TextBox txtSearch;
        private Button btnSearch;
        private DataGridView dgvTimSanPham;

        public frmTimSanPham()
        {
            // Initialize controls before using them
            this.dgvTimSanPham = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            InitializeComponent();
        }

        private void frmTimSanPham_Load(object sender, EventArgs e)
        {
            // Ensure dgvTimSanPham is initialized before use
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
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimSanPham)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTimSanPham
            // 
            this.dgvTimSanPham.ColumnHeadersHeight = 34;
            this.dgvTimSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTimSanPham.Location = new System.Drawing.Point(0, 0);
            this.dgvTimSanPham.Name = "dgvTimSanPham";
            this.dgvTimSanPham.RowHeadersWidth = 62;
            this.dgvTimSanPham.Size = new System.Drawing.Size(511, 526);
            this.dgvTimSanPham.TabIndex = 0;
            this.dgvTimSanPham.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTimSanPham_CellDoubleClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(10, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 26);
            this.txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(320, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmTimSanPham
            // 
            this.ClientSize = new System.Drawing.Size(511, 526);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvTimSanPham);
            this.Name = "frmTimSanPham";
            this.Load += new System.EventHandler(this.frmTimSanPham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimSanPham)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}