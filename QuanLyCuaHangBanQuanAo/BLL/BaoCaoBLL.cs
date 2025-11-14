using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyCuaHangBanQuanAo.DAL;
using QuanLyCuaHangBanQuanAo.DTO;

namespace QuanLyCuaHangBanQuanAo.BLL
{
    public class BaoCaoBLL
    {
        private readonly QuanLyShopDataContext db = new QuanLyShopDataContext();

        public List<DoanhThuDTO> GetDoanhThu(int year)
        {
            var query = db.HoaDons
                          .Where(h => h.NgayLap.HasValue && h.NgayLap.Value.Year == year)
                          .GroupBy(h => h.NgayLap.Value.Month)
                          .Select(g => new DoanhThuDTO
                          {
                              Thang = g.Key,
                              TongTien = g.Sum(h => (decimal?)h.TongTien) ?? 0
                          }).ToList();

            var result = new List<DoanhThuDTO>();
            for (int m = 1; m <= 12; m++)
            {
                var item = query.FirstOrDefault(x => x.Thang == m);
                result.Add(new DoanhThuDTO
                {
                    Thang = m,
                    TongTien = item?.TongTien ?? 0
                });
            }
            return result;
        }

        public List<TonKhoDTO> GetTonKho()
        {
            // Fix: Use db.HoaDons or another available collection, or ask for clarification if SanPhams is missing.
            // If you have a SanPhams property in your data context, ensure it's defined.
            // Otherwise, you need to add it to QuanLyShopDataContext or use the correct collection.
            throw new NotImplementedException("SanPhams collection is missing in QuanLyShopDataContext. Please add it or provide the correct collection.");
        }

        public List<TopKhachHangDTO> GetTopKhachHang(int year)
        {
            return db.HoaDons
                     .Where(h => h.NgayLap.HasValue && h.NgayLap.Value.Year == year)
                     .GroupBy(h => h.MaKH)
                     .Select(g => new TopKhachHangDTO
                     {
                         TenKH = g.FirstOrDefault().KhachHang.TenKH,
                         TongTien = g.Sum(h => (decimal?)h.TongTien) ?? 0
                     })
                     .OrderByDescending(x => x.TongTien)
                     .ToList();
        }
    }
}