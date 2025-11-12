using QuanLyCuaHangBanQuanAo.BLL;
using QuanLyCuaHangBanQuanAo.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    public partial class frmHoaDon : Form
    {
        // Khai báo BLL
        HoaDonBLL hdBLL = new HoaDonBLL();
        SanPhamBLL spBLL = new SanPhamBLL();
        KhachHangBLL khBLL = new KhachHangBLL();

        // Biến lưu trữ nhân viên đang đăng nhập
        private DTO.TaiKhoan _nhanVienHienTai;

        // Các biến tạm
        BindingList<DTO.ChiTietHoaDon> gioHang = new BindingList<DTO.ChiTietHoaDon>();
        DTO.KhachHang khachHangHienTai = null;
        private DataGridView dgvGioHang;
        private TextBox txtSoDienThoai;
        private TextBox txtMaKH;
        private TextBox txtTenKH;
        private TextBox txtTimMaSP;
        private Label lblTenSP;
        private Label lblDonGia;
        private Label lblTongTien;
        private Label lblThanhTien;
        private Label lblNgayLap;
        private Label lblMaHD;
        private Label lblTenNV;
        private TextBox txtGiamGia;
        private NumericUpDown nudSoLuong;
        private Button btnTimKhachHang;
        private Button btnTimSanPham;
        private Button btnThemSanPham;
        private Button btnTaoHoaDon;
        private Button btnHuyHoaDon;
        DTO.SanPham sanPhamHienTai = null;

        // Constructor mặc định (cần cho Designer)
        public frmHoaDon()
        {
            InitializeComponent();
        }

        // Constructor mới (để nhận TaiKhoan từ frmMain)
        public frmHoaDon(DTO.TaiKhoan nhanVien) : this()
        {
            _nhanVienHienTai = nhanVien;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            dgvGioHang.DataSource = gioHang;
            ResetFormHoaDon();

            // Hiển thị tên nhân viên (Giả sử bạn có label tên lblNhanVien)
            if (_nhanVienHienTai != null)
            {
                // lblNhanVien.Text = _nhanVienHienTai.TenDangNhap;
            }
        }

        void ResetFormHoaDon()
        {
            khachHangHienTai = null;
            sanPhamHienTai = null;
            gioHang.Clear();

            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtSoDienThoai.Text = "";

            txtTimMaSP.Text = "";
            lblTenSP.Text = "...";
            lblDonGia.Text = "0";
            nudSoLuong.Value = 1;

            lblTongTien.Text = "0";
            txtGiamGia.Text = "0";
            lblThanhTien.Text = "0";

            lblNgayLap.Text = DateTime.Now.ToShortDateString();
        }

        private void btnTimKhachHang_Click(object sender, EventArgs e)
        {
            // Mở form tìm kiếm
            frmTimKhachHang frmTim = new frmTimKhachHang();
            if (frmTim.ShowDialog() == DialogResult.OK)
            {
                // Lấy kết quả trả về
                khachHangHienTai = frmTim.SelectedKhachHang;

                if (khachHangHienTai != null)
                {
                    txtMaKH.Text = khachHangHienTai.MaKH.ToString();
                    txtTenKH.Text = khachHangHienTai.TenKH;
                    txtSoDienThoai.Text = khachHangHienTai.SoDienThoai;
                }
            }
        }

        // Sửa lại logic tìm SP (đã bỏ code giả lập)
        private void btnTimSanPham_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimMaSP.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Sản Phẩm để tìm.");
                return;
            }

            try
            {
                int maSP = int.Parse(txtTimMaSP.Text);
                // Gọi BLL (đã sửa ở các bước trước)
                sanPhamHienTai = spBLL.GetByID(maSP);
            }
            catch
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ.");
                sanPhamHienTai = null;
            }

            if (sanPhamHienTai != null)
            {
                lblTenSP.Text = sanPhamHienTai.TenSP;
                lblDonGia.Text = sanPhamHienTai.DonGia.ToString("N0");
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm.");
            }
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            if (sanPhamHienTai == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm.");
                return;
            }

            int soLuong = (int)nudSoLuong.Value;
            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0.");
                return;
            }

            // (Thêm) Kiểm tra tồn kho
            if (soLuong > sanPhamHienTai.SoLuongTon)
            {
                MessageBox.Show($"Số lượng tồn kho không đủ. Chỉ còn {sanPhamHienTai.SoLuongTon} sản phẩm.");
                return;
            }

            // Kiểm tra xem SP đã có trong giỏ hàng chưa
            var spTrongGio = gioHang.FirstOrDefault(sp => sp.MaSP == sanPhamHienTai.MaSP);
            if (spTrongGio != null)
            {
                // Nếu đã có, cộng dồn số lượng
                spTrongGio.SoLuong += soLuong;
                spTrongGio.ThanhTien = spTrongGio.SoLuong * spTrongGio.DonGia;
            }
            else
            {
                // Nếu chưa có, thêm mới
                DTO.ChiTietHoaDon ct = new DTO.ChiTietHoaDon
                {
                    MaSP = sanPhamHienTai.MaSP,
                    TenSP = sanPhamHienTai.TenSP,
                    SoLuong = soLuong,
                    DonGia = sanPhamHienTai.DonGia,
                    ThanhTien = soLuong * sanPhamHienTai.DonGia
                };
                gioHang.Add(ct);
            }

            // Cập nhật lại tổng tiền
            CapNhatTongTien();

            // Reset phần chọn SP
            sanPhamHienTai = null;
            txtTimMaSP.Text = "";
            lblTenSP.Text = "...";
            lblDonGia.Text = "0";
            nudSoLuong.Value = 1;
        }

        void CapNhatTongTien()
        {
            decimal tongTien = 0;
            foreach (var ct in gioHang)
            {
                tongTien += ct.ThanhTien;
            }

            lblTongTien.Text = tongTien.ToString("N0");

            decimal giamGia = decimal.TryParse(txtGiamGia.Text, out decimal gg) ? gg : 0;
            decimal thanhTien = tongTien - giamGia;

            lblThanhTien.Text = thanhTien.ToString("N0");
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            CapNhatTongTien();
        }

        private void dgvGioHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý khi click vào nút "Xóa" (btnXoa) trong DataGridView
            if (dgvGioHang.Columns[e.ColumnIndex].Name == "btnXoa" && e.RowIndex >= 0)
            {
                gioHang.RemoveAt(e.RowIndex);
                CapNhatTongTien();
            }
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            if (_nhanVienHienTai == null)
            {
                MessageBox.Show("Lỗi: Không thể xác định nhân viên. Vui lòng đăng nhập lại.");
                return;
            }
            if (khachHangHienTai == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng.");
                return;
            }
            if (gioHang.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có sản phẩm.");
                return;
            }

            // 1. Tạo DTO Hóa Đơn (Header)
            DTO.HoaDon hdDTO = new DTO.HoaDon();

            // Sửa: Lấy MaNV từ _nhanVienHienTai
            hdDTO.MaNV = _nhanVienHienTai.MaNV;

            hdDTO.MaKH = khachHangHienTai.MaKH;
            hdDTO.TongTien = decimal.Parse(lblThanhTien.Text, System.Globalization.NumberStyles.Any);

            // 2. Tạo List<DTO.ChiTietHoaDon>
            List<DTO.ChiTietHoaDon> listCTHD = new List<DTO.ChiTietHoaDon>(gioHang);

            try
            {
                // 3. Gọi BLL để tạo (BLL/HoaDonBLL.cs đã sửa logic transaction)
                int maHDMoi = hdBLL.CreateHoaDon(hdDTO, listCTHD);

                MessageBox.Show($"Tạo hóa đơn thành công! Mã HĐ: {maHDMoi}");

                // 4. Reset form
                ResetFormHoaDon();
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi từ BLL (ví dụ: hết hàng)
                MessageBox.Show("Lỗi khi tạo hóa đơn: " + ex.Message);
            }
        }

        private void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hủy hóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetFormHoaDon();
            }
        }

        // (Hàm InitializeComponent() và các hàm sự kiện trống giữ nguyên)
        // ...
        private void InitializeComponent()
        {
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.txtTimMaSP = new System.Windows.Forms.TextBox();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.lblTenNV = new System.Windows.Forms.Label();
            this.txtGiamGia = new System.Windows.Forms.TextBox();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnTimKhachHang = new System.Windows.Forms.Button();
            this.btnTimSanPham = new System.Windows.Forms.Button();
            this.btnThemSanPham = new System.Windows.Forms.Button();
            this.btnTaoHoaDon = new System.Windows.Forms.Button();
            this.btnHuyHoaDon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioHang.Location = new System.Drawing.Point(28, 140);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.Size = new System.Drawing.Size(240, 150);
            this.dgvGioHang.TabIndex = 1;
            this.dgvGioHang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGioHang_CellContentClick);
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Location = new System.Drawing.Point(28, 72);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(100, 20);
            this.txtSoDienThoai.TabIndex = 2;
            // 
            // txtMaKH
            // 
            this.txtMaKH.Location = new System.Drawing.Point(288, 270);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(100, 20);
            this.txtMaKH.TabIndex = 3;
            // 
            // txtTenKH
            // 
            this.txtTenKH.Location = new System.Drawing.Point(288, 383);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(100, 20);
            this.txtTenKH.TabIndex = 4;
            // 
            // txtTimMaSP
            // 
            this.txtTimMaSP.Location = new System.Drawing.Point(288, 338);
            this.txtTimMaSP.Name = "txtTimMaSP";
            this.txtTimMaSP.Size = new System.Drawing.Size(100, 20);
            this.txtTimMaSP.TabIndex = 5;
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Location = new System.Drawing.Point(372, 41);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(16, 13);
            this.lblTenSP.TabIndex = 6;
            this.lblTenSP.Text = "...";
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(447, 41);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(35, 13);
            this.lblDonGia.TabIndex = 7;
            this.lblDonGia.Text = "label1";
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new System.Drawing.Point(285, 41);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(35, 13);
            this.lblTongTien.TabIndex = 8;
            this.lblTongTien.Text = "label1";
            // 
            // lblThanhTien
            // 
            this.lblThanhTien.AutoSize = true;
            this.lblThanhTien.Location = new System.Drawing.Point(560, 78);
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Size = new System.Drawing.Size(35, 13);
            this.lblThanhTien.TabIndex = 9;
            this.lblThanhTien.Text = "label1";
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(610, 156);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(35, 13);
            this.lblNgayLap.TabIndex = 10;
            this.lblNgayLap.Text = "label1";
            // 
            // lblMaHD
            // 
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Location = new System.Drawing.Point(739, 113);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(35, 13);
            this.lblMaHD.TabIndex = 11;
            this.lblMaHD.Text = "label1";
            // 
            // lblTenNV
            // 
            this.lblTenNV.AutoSize = true;
            this.lblTenNV.Location = new System.Drawing.Point(711, 214);
            this.lblTenNV.Name = "lblTenNV";
            this.lblTenNV.Size = new System.Drawing.Size(35, 13);
            this.lblTenNV.TabIndex = 12;
            this.lblTenNV.Text = "label1";
            // 
            // txtGiamGia
            // 
            this.txtGiamGia.Location = new System.Drawing.Point(613, 324);
            this.txtGiamGia.Name = "txtGiamGia";
            this.txtGiamGia.Size = new System.Drawing.Size(100, 20);
            this.txtGiamGia.TabIndex = 13;
            this.txtGiamGia.TextChanged += new System.EventHandler(this.txtGiamGia_TextChanged);
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Location = new System.Drawing.Point(485, 174);
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(120, 20);
            this.nudSoLuong.TabIndex = 14;
            // 
            // btnTimKhachHang
            // 
            this.btnTimKhachHang.Location = new System.Drawing.Point(450, 298);
            this.btnTimKhachHang.Name = "btnTimKhachHang";
            this.btnTimKhachHang.Size = new System.Drawing.Size(75, 23);
            this.btnTimKhachHang.TabIndex = 15;
            this.btnTimKhachHang.Text = "button1";
            this.btnTimKhachHang.UseVisualStyleBackColor = true;
            this.btnTimKhachHang.Click += new System.EventHandler(this.btnTimKhachHang_Click);
            // 
            // btnTimSanPham
            // 
            this.btnTimSanPham.Location = new System.Drawing.Point(397, 204);
            this.btnTimSanPham.Name = "btnTimSanPham";
            this.btnTimSanPham.Size = new System.Drawing.Size(75, 23);
            this.btnTimSanPham.TabIndex = 16;
            this.btnTimSanPham.Text = "button1";
            this.btnTimSanPham.UseVisualStyleBackColor = true;
            this.btnTimSanPham.Click += new System.EventHandler(this.btnTimSanPham_Click);
            // 
            // btnThemSanPham
            // 
            this.btnThemSanPham.Location = new System.Drawing.Point(244, 145);
            this.btnThemSanPham.Name = "btnThemSanPham";
            this.btnThemSanPham.Size = new System.Drawing.Size(75, 23);
            this.btnThemSanPham.TabIndex = 17;
            this.btnThemSanPham.Text = "button1";
            this.btnThemSanPham.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnThemSanPham.UseVisualStyleBackColor = true;
            this.btnThemSanPham.Click += new System.EventHandler(this.btnThemSanPham_Click);
            // 
            // btnTaoHoaDon
            // 
            this.btnTaoHoaDon.Location = new System.Drawing.Point(420, 131);
            this.btnTaoHoaDon.Name = "btnTaoHoaDon";
            this.btnTaoHoaDon.Size = new System.Drawing.Size(75, 23);
            this.btnTaoHoaDon.TabIndex = 18;
            this.btnTaoHoaDon.Text = "button1";
            this.btnTaoHoaDon.UseVisualStyleBackColor = true;
            this.btnTaoHoaDon.Click += new System.EventHandler(this.btnTaoHoaDon_Click);
            // 
            // btnHuyHoaDon
            // 
            this.btnHuyHoaDon.Location = new System.Drawing.Point(679, 255);
            this.btnHuyHoaDon.Name = "btnHuyHoaDon";
            this.btnHuyHoaDon.Size = new System.Drawing.Size(75, 23);
            this.btnHuyHoaDon.TabIndex = 19;
            this.btnHuyHoaDon.Text = "button1";
            this.btnHuyHoaDon.UseVisualStyleBackColor = true;
            this.btnHuyHoaDon.Click += new System.EventHandler(this.btnHuyHoaDon_Click);
            // 
            // frmHoaDon
            // 
            this.ClientSize = new System.Drawing.Size(1101, 508);
            this.Controls.Add(this.btnHuyHoaDon);
            this.Controls.Add(this.btnTaoHoaDon);
            this.Controls.Add(this.btnThemSanPham);
            this.Controls.Add(this.btnTimSanPham);
            this.Controls.Add(this.btnTimKhachHang);
            this.Controls.Add(this.nudSoLuong);
            this.Controls.Add(this.txtGiamGia);
            this.Controls.Add(this.lblTenNV);
            this.Controls.Add(this.lblMaHD);
            this.Controls.Add(this.lblNgayLap);
            this.Controls.Add(this.lblThanhTien);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.lblDonGia);
            this.Controls.Add(this.lblTenSP);
            this.Controls.Add(this.txtTimMaSP);
            this.Controls.Add(this.txtTenKH);
            this.Controls.Add(this.txtMaKH);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.dgvGioHang);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "frmHoaDon";
            this.Text = "HoaDon";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.frmHoaDon_Load_1);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.frmHoaDon_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void frmHoaDon_Load_1(object sender, EventArgs e)
        {

        }

        private void frmHoaDon_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtSoDienThoai_TextChanged(object sender, EventArgs e)
        {

        }
    }
}