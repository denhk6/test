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

        // Dùng BindingList để DataGridView tự động cập nhật khi thêm/xóa
        BindingList<DTO.ChiTietHoaDon> gioHang = new BindingList<DTO.ChiTietHoaDon>();
        DTO.KhachHang khachHangHienTai = null;
        DTO.SanPham sanPhamHienTai = null;
        // DTO.NhanVien nhanVienHienTai = GlobalData.CurrentNhanVien; // Lấy NV đã đăng nhập

        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            // Gán giỏ hàng cho DataGridView
            dgvGioHang.DataSource = gioHang;

            // Cài đặt ban đầu
            ResetFormHoaDon();

            // Giả sử bạn có 1 class static GlobalData để giữ NhanVien
            // lblNhanVien.Text = nhanVienHienTai.TenNV;
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
            // Mở 1 form (ví dụ: frmTimKhachHang) để tìm và trả về 1 DTO.KhachHang
            // Sau khi tìm xong:
            // khachHangHienTai = ... (kết quả từ form tìm)

            // Giả lập tìm thấy khách hàng
            khachHangHienTai = khBLL.GetByID(1); // Giả sử tìm KH có mã 1
            if (khachHangHienTai != null)
            {
                txtMaKH.Text = khachHangHienTai.MaKH.ToString();
                txtTenKH.Text = khachHangHienTai.TenKH;
                txtSoDienThoai.Text = khachHangHienTai.SoDienThoai;
            }
        }

        private void btnTimSanPham_Click(object sender, EventArgs e)
        {
            // Mở 1 form (ví dụ: frmTimSanPham) để tìm và trả về 1 DTO.SanPham
            // Hoặc tìm trực tiếp bằng Mã SP

            // Giả lập tìm SP
            int maSP = int.Parse(txtTimMaSP.Text);
            // sanPhamHienTai = spBLL.GetByID(maSP); // Cần thêm GetByID vào SanPhamBLL

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
            // hdDTO.MaNV = nhanVienHienTai.MaNV;
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
            this.SuspendLayout();
            // 
            // frmHoaDon
            // 
            resources.ApplyResources(this, "$this");
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "frmHoaDon";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.frmHoaDon_Load_1);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.frmHoaDon_Layout);
            this.ResumeLayout(false);

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
    }
}