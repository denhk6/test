using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangBanQuanAo.DTO
{
    public class HoaDon // <-- Phải là public
    {
        public int MaHD { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public int? MaKH { get; set; }
        public string TenKH { get; set; } // Lấy từ BLL
        public int? MaNV { get; set; }
        public string TenNV { get; set; } // Lấy từ BLL
    }
}