using QuanLyCuaHangBanQuanAo.BLL;
using QuanLyCuaHangBanQuanAo.DTO;
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
        private DataGridView dgvTimKhachHang;
        private TextBox txtSearch;
        private Button btnSearch;
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

        private void InitializeComponent()
        {
            this.dgvTimKhachHang = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimKhachHang)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTimKhachHang
            // 
            this.dgvTimKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimKhachHang.Location = new System.Drawing.Point(32, 28);
            this.dgvTimKhachHang.Name = "dgvTimKhachHang";
            this.dgvTimKhachHang.Size = new System.Drawing.Size(240, 150);
            this.dgvTimKhachHang.TabIndex = 0;
            this.dgvTimKhachHang.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTimKhachHang_CellDoubleClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(135, 195);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(73, 216);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "button1";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmTimKhachHang
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvTimKhachHang);
            this.Name = "frmTimKhachHang";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimKhachHang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}