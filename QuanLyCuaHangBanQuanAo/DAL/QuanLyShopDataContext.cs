using System.Collections.Generic;

namespace QuanLyCuaHangBanQuanAo.DAL
{
    public class QuanLyShopDataContext
    {
        public List<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
        // Add other collections as needed, e.g.:
        // public List<SanPham> SanPhams { get; set; } = new List<SanPham>();
    }
}