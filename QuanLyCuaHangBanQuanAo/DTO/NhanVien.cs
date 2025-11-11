using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangBanQuanAo.DTO
{
    public class NhanVien
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public string ChucVu { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime? NgayVaoLam { get; set; }

        // Thêm các thuộc tính từ bảng TaiKhoan/PhanQuyen
        public string MaTaiKhoan { get; set; } // TenDangNhap
        public string TenQuyen { get; set; }
    }
}
