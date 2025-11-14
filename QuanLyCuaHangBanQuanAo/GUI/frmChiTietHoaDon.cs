using QuanLyCuaHangBanQuanAo.BLL;
using QuanLyCuaHangBanQuanAo.DTO; // Add this if DTO namespace exists and contains HoaDon
using System;
using System.Windows.Forms;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    public partial class frmChiTietHoaDon : Form
    {
        private int _maHD;
        private Label lblMaHD;
        private Label lblNgayLap;
        private Label lblTenKH;
        private Label lblTenNV;
        private Label lblTongTien;
        private DataGridView dgvChiTiet;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
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
        {            // FIX: Use HoaDon from BLL or correct namespace, and use GetAll() to get header info
            var hdList = hdBLL.GetAll();
            var hdHeader = hdList?.Find(hd => hd.MaHD == _maHD);
            if (hdHeader != null)
            {
                lblMaHD.Text = hdHeader.MaHD.ToString();
                lblNgayLap.Text = hdHeader.NgayLap.ToShortDateString();
                lblTenKH.Text = hdHeader.TenKH;
                lblTenNV.Text = hdHeader.TenNV;
                lblTongTien.Text = hdHeader.TongTien.ToString("N0") + " VNĐ";
            }

            dgvChiTiet.DataSource = hdBLL.GetChiTiet(_maHD);
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMaHD
            // 
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Location = new System.Drawing.Point(85, 611);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(0, 20);
            this.lblMaHD.TabIndex = 0;
            this.lblMaHD.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(440, 611);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(75, 20);
            this.lblNgayLap.TabIndex = 1;
            this.lblNgayLap.Text = "dd/mm/yy";
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Location = new System.Drawing.Point(123, 659);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(0, 20);
            this.lblTenKH.TabIndex = 2;
            this.lblTenKH.Click += new System.EventHandler(this.lblTenKH_Click);
            // 
            // lblTenNV
            // 
            this.lblTenNV.AutoSize = true;
            this.lblTenNV.Location = new System.Drawing.Point(440, 659);
            this.lblTenNV.Name = "lblTenNV";
            this.lblTenNV.Size = new System.Drawing.Size(0, 20);
            this.lblTenNV.TabIndex = 3;
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblTongTien.Location = new System.Drawing.Point(182, 712);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(32, 36);
            this.lblTongTien.TabIndex = 4;
            this.lblTongTien.Text = "0";
            this.lblTongTien.Click += new System.EventHandler(this.lblTongTien_Click);
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Location = new System.Drawing.Point(16, 46);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersWidth = 62;
            this.dgvChiTiet.RowTemplate.Height = 28;
            this.dgvChiTiet.Size = new System.Drawing.Size(607, 531);
            this.dgvChiTiet.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 611);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mã HĐ :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 659);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Khách Hàng :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.Location = new System.Drawing.Point(10, 712);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 36);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tổng Tiền :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(350, 611);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ngày Lập :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(350, 659);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Nhân Viên :";
            // 
            // frmChiTietHoaDon
            // 
            this.ClientSize = new System.Drawing.Size(657, 769);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.lblTenNV);
            this.Controls.Add(this.lblTenKH);
            this.Controls.Add(this.lblNgayLap);
            this.Controls.Add(this.lblMaHD);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "frmChiTietHoaDon";
            this.Text = "Chi Tiết Hóa Đơn";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.frmChiTietHoaDon_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void frmChiTietHoaDon_Load_1(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblTenKH_Click(object sender, EventArgs e)
        {

        }

        private void lblTongTien_Click(object sender, EventArgs e)
        {

        }
    }
}