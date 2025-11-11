using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangBanQuanAo.DTO
{
    public class ChiTietHoaDon // <-- Phải là public
    {
        public int MaHD { get; set; }
        public int MaSP { get; set; }
        public string TenSP { get; set; } // Lấy từ BLL/HoaDonBLL.cs
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}