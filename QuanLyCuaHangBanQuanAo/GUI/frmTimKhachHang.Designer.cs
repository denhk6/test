namespace QuanLyCuaHangBanQuanAo.GUI
{
    partial class frmTimKhachHang
    {
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvTimKhachHang = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimKhachHang)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 22);
            this.txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(218, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvTimKhachHang
            // 
            this.dgvTimKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTimKhachHang.Location = new System.Drawing.Point(0, 50);
            this.dgvTimKhachHang.Name = "dgvTimKhachHang";
            this.dgvTimKhachHang.RowTemplate.Height = 24;
            this.dgvTimKhachHang.Size = new System.Drawing.Size(800, 400);
            this.dgvTimKhachHang.TabIndex = 2;
            this.dgvTimKhachHang.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTimKhachHang_CellDoubleClick);
            // 
            // frmTimKhachHang
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvTimKhachHang);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Name = "frmTimKhachHang";
            this.Load += new System.EventHandler(this.frmTimKhachHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimKhachHang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvTimKhachHang;
    }
}