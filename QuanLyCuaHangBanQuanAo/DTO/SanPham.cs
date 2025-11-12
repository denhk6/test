using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangBanQuanAo.DTO
{
    public class SanPham // <-- Phải là public
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuongTon { get; set; }
        public string MoTa { get; set; }
        public int? MaLoaiSP { get; set; }
        public string TenLoaiSP { get; set; } // Thuộc tính này lấy từ BLL/SanPhamBLL.cs
    }
}