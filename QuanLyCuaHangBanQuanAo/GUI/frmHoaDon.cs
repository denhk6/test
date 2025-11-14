using QuanLyCuaHangBanQuanAo.BLL;
using QuanLyCuaHangBanQuanAo.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyCuaHangBanQuanAo.GUI
{
    // Đây là file code-behind cho form Bán Hàng (Tạo Hóa Đơn)
    // Đây là form phức tạp, bạn cần thiết kế [Design] cẩn thận:
    // 
    // 1. GroupBox "Thông tin chung":
    //    - lblMaHD (Text: "HĐ-Tự động")
    //    - lblNgayLap (Text: "dd/MM/yyyy")
    //    - lblNhanVien (Text: "Tên NV đăng nhập") (load từ GlobalData)
    // 
    // 2. GroupBox "Thông tin khách hàng":
    //    - txtMaKH (ReadOnly)
    //    - txtTenKH
    //    - txtSoDienThoai
    //    - btnTimKhachHang (Mở 1 form popup tìm kiếm KH)
    //
    // 3. GroupBox "Chi tiết sản phẩm":
    //    - (Label) "Tìm sản phẩm":
    //    - txtTimMaSP
    //    - btnTimSanPham (Mở form popup tìm SP)
    //    - (Label) "Tên SP": lblTenSP
    //    - (Label) "Đơn giá": lblDonGia
    //    - (Label) "Số lượng": nudSoLuong (NumericUpDown)
    //    - btnThemSanPham (Button "Thêm vào giỏ")
    //
    // 4. DataGridView (Tên: dgvGioHang): Hiển thị các SP trong HĐ hiện tại
    //    - (Cột: MaSP, TenSP, DonGia, SoLuong, ThanhTien, btnXoa)
    //
    // 5. GroupBox "Thanh toán":
    //    - lblTongTien (Text: "0")
    //    - (Label) "Giảm giá": txtGiamGia
    //    - lblThanhTien (Text: "0", Font Lớn, Bold)
    //    - btnTaoHoaDon (Button "Thanh Toán")
    //    - btnHuyHoaDon

    public partial class frmHoaDon : Form
    {
        HoaDonBLL hdBLL = new HoaDonBLL();
        SanPhamBLL spBLL = new SanPhamBLL(); // Dùng để tìm SP
        KhachHangBLL khBLL = new KhachHangBLL(); // Dùng để tìm KH

        private DTO.TaiKhoan _nhanVienHienTai;

        // Dùng BindingList để DataGridView tự động cập nhật khi thêm/xóa
        BindingList<DTO.ChiTietHoaDon> gioHang = new BindingList<DTO.ChiTietHoaDon>();
        DTO.KhachHang khachHangHienTai = null;
        private BackgroundWorker backgroundWorker1;
        private Label lblNgayLap;
        private Label lblMaHD;
        private Label lblTenNV;
        private TextBox txtSoDienThoai;
        private TextBox txtMaKH;
        private TextBox txtTenKH;
        private Button btnTimKhachHang;
        private TextBox txtTimMaSP;
        private Button btnTimSanPham;
        private Label lblTenSP;
        private Label lblDonGia;
        private NumericUpDown nudSoLuong;
        private Button btnThemSanPham;
        private DataGridView dgvGioHang;
        private DataGridViewButtonColumn btnXoa;
        private Label lblTongTien;
        private TextBox txtGiamGia;
        private Label lblThanhTien;
        private Button btnTaoHoaDon;
        private Button btnHuyHoaDon;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        DTO.SanPham sanPhamHienTai = null;
        // DTO.NhanVien nhanVienHienTai = GlobalData.CurrentNhanVien; // Lấy NV đã đăng nhập

        public frmHoaDon()
        {
            InitializeComponent();
        }

        // THÊM HÀM KHỞI TẠO MỚI NÀY:
        public frmHoaDon(DTO.TaiKhoan nhanVien) : this()
        {
            _nhanVienHienTai = nhanVien;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            // Gán giỏ hàng cho DataGridView
            dgvGioHang.DataSource = gioHang;

            // Cài đặt ban đầu
            ResetFormHoaDon();

            // Sửa dòng bị chú thích:
            // lblNhanVien.Text = nhanVienHienTai.TenNV;

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

        private void btnTimSanPham_Click(object sender, EventArgs e)
        {
            // Mở 1 form (ví dụ: frmTimSanPham) để tìm và trả về 1 DTO.SanPham
            // Hoặc tìm trực tiếp bằng Mã SP

            if (string.IsNullOrWhiteSpace(txtTimMaSP.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Sản Phẩm để tìm.");
                return;
            }

            try
            {
                int maSP = int.Parse(txtTimMaSP.Text);
                sanPhamHienTai = spBLL.GetByID(maSP); // Bỏ chú thích và gọi BLL
            }
            catch
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ.");
                sanPhamHienTai = null;
            }

            // Giữ nguyên phần code "if (sanPhamHienTai != null)"

            // Giả sử sanPhamHienTai được tìm thấy
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
            // Gán MaNV (từ DTO/TaiKhoan) của nhân viên vào Hóa Đơn
            hdDTO.MaNV = _nhanVienHienTai.MaNV;
            hdDTO.MaKH = khachHangHienTai.MaKH;
            hdDTO.TongTien = decimal.Parse(lblThanhTien.Text, System.Globalization.NumberStyles.Any);
            // Các thông tin khác như Giảm giá...

            // 2. Tạo List<DTO.ChiTietHoaDon>
            List<DTO.ChiTietHoaDon> listCTHD = new List<DTO.ChiTietHoaDon>(gioHang);

            try
            {
                // 3. Gọi BLL để tạo
                int maHDMoi = hdBLL.CreateHoaDon(hdDTO, listCTHD);

                MessageBox.Show($"Tạo hóa đơn thành công! Mã HĐ: {maHDMoi}");

                // 4. (Tùy chọn) Mở form In Hóa Đơn
                // frmInHoaDon frmIn = new frmInHoaDon(maHDMoi);
                // frmIn.ShowDialog();

                // 5. Reset form
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHoaDon));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.lblTenNV = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.btnTimKhachHang = new System.Windows.Forms.Button();
            this.txtTimMaSP = new System.Windows.Forms.TextBox();
            this.btnTimSanPham = new System.Windows.Forms.Button();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnThemSanPham = new System.Windows.Forms.Button();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.btnXoa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.txtGiamGia = new System.Windows.Forms.TextBox();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.btnTaoHoaDon = new System.Windows.Forms.Button();
            this.btnHuyHoaDon = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // lblNgayLap
            // 
            resources.ApplyResources(this.lblNgayLap, "lblNgayLap");
            this.lblNgayLap.Name = "lblNgayLap";
            // 
            // lblMaHD
            // 
            resources.ApplyResources(this.lblMaHD, "lblMaHD");
            this.lblMaHD.Name = "lblMaHD";
            // 
            // lblTenNV
            // 
            resources.ApplyResources(this.lblTenNV, "lblTenNV");
            this.lblTenNV.Name = "lblTenNV";
            // 
            // txtSoDienThoai
            // 
            resources.ApplyResources(this.txtSoDienThoai, "txtSoDienThoai");
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            // 
            // txtMaKH
            // 
            resources.ApplyResources(this.txtMaKH, "txtMaKH");
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.ReadOnly = true;
            // 
            // txtTenKH
            // 
            resources.ApplyResources(this.txtTenKH, "txtTenKH");
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.ReadOnly = true;
            this.txtTenKH.TextChanged += new System.EventHandler(this.txtTenKH_TextChanged);
            // 
            // btnTimKhachHang
            // 
            resources.ApplyResources(this.btnTimKhachHang, "btnTimKhachHang");
            this.btnTimKhachHang.Name = "btnTimKhachHang";
            this.btnTimKhachHang.UseVisualStyleBackColor = true;
            this.btnTimKhachHang.Click += new System.EventHandler(this.btnTimKhachHang_Click);
            // 
            // txtTimMaSP
            // 
            resources.ApplyResources(this.txtTimMaSP, "txtTimMaSP");
            this.txtTimMaSP.Name = "txtTimMaSP";
            // 
            // btnTimSanPham
            // 
            resources.ApplyResources(this.btnTimSanPham, "btnTimSanPham");
            this.btnTimSanPham.Name = "btnTimSanPham";
            this.btnTimSanPham.UseVisualStyleBackColor = true;
            this.btnTimSanPham.Click += new System.EventHandler(this.btnTimSanPham_Click);
            // 
            // lblTenSP
            // 
            resources.ApplyResources(this.lblTenSP, "lblTenSP");
            this.lblTenSP.Name = "lblTenSP";
            // 
            // lblDonGia
            // 
            resources.ApplyResources(this.lblDonGia, "lblDonGia");
            this.lblDonGia.Name = "lblDonGia";
            // 
            // nudSoLuong
            // 
            resources.ApplyResources(this.nudSoLuong, "nudSoLuong");
            this.nudSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnThemSanPham
            // 
            resources.ApplyResources(this.btnThemSanPham, "btnThemSanPham");
            this.btnThemSanPham.Name = "btnThemSanPham";
            this.btnThemSanPham.UseVisualStyleBackColor = true;
            this.btnThemSanPham.Click += new System.EventHandler(this.btnThemSanPham_Click_1);
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnXoa});
            resources.ApplyResources(this.dgvGioHang, "dgvGioHang");
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.RowTemplate.Height = 28;
            this.dgvGioHang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGioHang_CellContentClick);
            // 
            // btnXoa
            // 
            resources.ApplyResources(this.btnXoa, "btnXoa");
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Text = "xóa";
            this.btnXoa.UseColumnTextForButtonValue = true;
            // 
            // lblTongTien
            // 
            resources.ApplyResources(this.lblTongTien, "lblTongTien");
            this.lblTongTien.Name = "lblTongTien";
            // 
            // txtGiamGia
            // 
            resources.ApplyResources(this.txtGiamGia, "txtGiamGia");
            this.txtGiamGia.Name = "txtGiamGia";
            this.txtGiamGia.TextChanged += new System.EventHandler(this.txtGiamGia_TextChanged);
            // 
            // lblThanhTien
            // 
            resources.ApplyResources(this.lblThanhTien, "lblThanhTien");
            this.lblThanhTien.Name = "lblThanhTien";
            // 
            // btnTaoHoaDon
            // 
            resources.ApplyResources(this.btnTaoHoaDon, "btnTaoHoaDon");
            this.btnTaoHoaDon.Name = "btnTaoHoaDon";
            this.btnTaoHoaDon.UseVisualStyleBackColor = true;
            this.btnTaoHoaDon.Click += new System.EventHandler(this.btnTaoHoaDon_Click);
            // 
            // btnHuyHoaDon
            // 
            resources.ApplyResources(this.btnHuyHoaDon, "btnHuyHoaDon");
            this.btnHuyHoaDon.Name = "btnHuyHoaDon";
            this.btnHuyHoaDon.UseVisualStyleBackColor = true;
            this.btnHuyHoaDon.Click += new System.EventHandler(this.btnHuyHoaDon_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // frmHoaDon
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHuyHoaDon);
            this.Controls.Add(this.btnTaoHoaDon);
            this.Controls.Add(this.lblThanhTien);
            this.Controls.Add(this.txtGiamGia);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.dgvGioHang);
            this.Controls.Add(this.btnThemSanPham);
            this.Controls.Add(this.nudSoLuong);
            this.Controls.Add(this.lblDonGia);
            this.Controls.Add(this.lblTenSP);
            this.Controls.Add(this.btnTimSanPham);
            this.Controls.Add(this.txtTimMaSP);
            this.Controls.Add(this.btnTimKhachHang);
            this.Controls.Add(this.txtTenKH);
            this.Controls.Add(this.txtMaKH);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.lblTenNV);
            this.Controls.Add(this.lblMaHD);
            this.Controls.Add(this.lblNgayLap);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "frmHoaDon";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.frmHoaDon_Load_1);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.frmHoaDon_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
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

        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void btnThemSanPham_Click_1(object sender, EventArgs e)
        {

        }

        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}