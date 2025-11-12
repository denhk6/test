using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangBanQuanAo.DTO
{
    public class TaiKhoan
    {
        // Các thuộc tính này PHẢI KHỚP với CSDL mới
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int MaNV { get; set; }
        public int MaQuyen { get; set; }

        // Các thuộc tính cũ (MaTK, TK, MK, Role) không còn dùng nữa
    }
}