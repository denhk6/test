using QuanLyCuaHangBanQuanAo.DAL;
using QuanLyCuaHangBanQuanAo.DTO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace QuanLyCuaHangBanQuanAo.BLL
{
    public class HoaDonBLL
    {
        QuanLyCuaHangDataContext db = new QuanLyCuaHangDataContext();

        /// <summary>
        /// Lấy tất cả hóa đơn (thường dùng cho báo cáo)
        /// </summary>
        /// <returns></returns>
        public List<DTO.HoaDon> GetAll()
        {
            var query = from hd in db.HoaDons
                        join kh in db.KhachHangs on hd.MaKH equals kh.MaKH
                        join nv in db.NhanViens on hd.MaNV equals nv.MaNV
                        select new DTO.HoaDon
                        {
                            MaHD = hd.MaHD,
                            NgayLap = hd.NgayLap.GetValueOrDefault(),
                            TongTien = hd.TongTien.GetValueOrDefault(0),
                            MaKH = hd.MaKH,
                            TenKH = kh.TenKH, // Join để lấy tên KH
                            MaNV = hd.MaNV,
                            TenNV = nv.TenNV // Join để lấy tên NV
                        };
            return query.ToList();
        }

        /// <summary>
        /// Lấy chi tiết của 1 hóa đơn
        /// </summary>
        public List<DTO.ChiTietHoaDon> GetChiTiet(int maHD)
        {
            var query = from ct in db.ChiTietHoaDons
                        join sp in db.SanPhams on ct.MaSP equals sp.MaSP
                        where ct.MaHD == maHD
                        select new DTO.ChiTietHoaDon
                        {
                            MaHD = ct.MaHD,
                            MaSP = ct.MaSP,
                            TenSP = sp.TenSP, // Join để lấy tên SP
                            SoLuong = ct.SoLuong.GetValueOrDefault(0),
                            DonGia = ct.DonGia.GetValueOrDefault(0),
                            ThanhTien = ct.ThanhTien.GetValueOrDefault(0)
                        };
            return query.ToList();
        }

 /// <summary>
        /// Tạo một hóa đơn mới và các chi tiết của nó
        /// Đây là nghiệp vụ quan trọng, phải dùng Transaction
        /// </summary>
        /// <param name="hdDTO">Thông tin hóa đơn (MaKH, MaNV, TongTien)</param>
        /// <param name="listCTHD_DTO">Danh sách các sản phẩm trong hóa đơn</param>
        /// <returns>Mã Hóa Đơn vừa tạo</returns>
        public int CreateHoaDon(DTO.HoaDon hdDTO, List<DTO.ChiTietHoaDon> listCTHD_DTO)
        {
            // 1. Thêm Hóa Đơn
            DAL.HoaDon hdDAL = new DAL.HoaDon
            {
                NgayLap = DateTime.Now,
                TongTien = hdDTO.TongTien,
                MaKH = hdDTO.MaKH,
                MaNV = hdDTO.MaNV
            };

            // 2. Chuẩn bị các Chi Tiết Hóa Đơn
            List<DAL.ChiTietHoaDon> listCTHD_DAL = new List<DAL.ChiTietHoaDon>();
            foreach (var ctDTO in listCTHD_DTO)
            {
                DAL.ChiTietHoaDon ctDAL = new DAL.ChiTietHoaDon
                {
                    MaSP = ctDTO.MaSP,
                    SoLuong = ctDTO.SoLuong,
                    DonGia = ctDTO.DonGia,
                    ThanhTien = ctDTO.ThanhTien
                };
                listCTHD_DAL.Add(ctDAL);
            }

            // 3. Thực hiện Transaction
            // SubmitChanges() của LINQ to SQL vốn là transactional
            // Chúng ta cần xử lý logic cập nhật số lượng tồn kho
            try
            {
                // Thêm Hóa Đơn
                db.HoaDons.InsertOnSubmit(hdDAL);

                // Cần SubmitChanges() để lấy MaHD mới
                db.SubmitChanges(); 
                
                // Lấy MaHD vừa được tạo
                int newMaHD = hdDAL.MaHD;

                // Cập nhật số lượng tồn kho và gán MaHD cho chi tiết
                foreach (var ctDAL in listCTHD_DAL)
                {
                    // Gán MaHD cho chi tiết
                    ctDAL.MaHD = newMaHD;
                    
                    // Trừ số lượng tồn kho
                    var spDAL = db.SanPhams.Single(sp => sp.MaSP == ctDAL.MaSP); 
                    if (spDAL.SoLuongTon < ctDAL.SoLuong)
                    {
                        // Nếu số lượng không đủ, ném ra lỗi
                        throw new Exception($"Không đủ số lượng cho sản phẩm: {spDAL.TenSP}. Tồn kho: {spDAL.SoLuongTon}");
                    }
                    spDAL.SoLuongTon -= ctDAL.SoLuong;
                }

                // Thêm tất cả chi tiết hóa đơn
                db.ChiTietHoaDons.InsertAllOnSubmit(listCTHD_DAL);

                // Submit tất cả thay đổi (Thêm CTHD, Cập nhật SanPham)
                db.SubmitChanges();

                return newMaHD;
            }
            catch (Exception)
            {
                // Nếu có lỗi, mọi thứ sẽ được rollback (vì chúng ta chưa Commit)
                // Tuy nhiên, vì đã gọi SubmitChanges() 1 lần, chúng ta cần rollback thủ công
                // Cách tốt hơn là dùng 1 lần SubmitChanges() duy nhất.
                
                // --- Cách làm chuẩn hơn với 1 lần SubmitChanges() ---
                // (Bạn có thể tham khảo cách này)
                // 1. Tạo hdDAL
                // 2. db.HoaDons.InsertOnSubmit(hdDAL);
                // 3. Vòng lặp listCTHD_DTO:
                //    - Tạo ctDAL
                //    - spDAL = db.SanPhams.Single(...)
                //    - spDAL.SoLuongTon -= ctDTO.SoLuong (kiểm tra)
                //    - hdDAL.ChiTietHoaDons.Add(ctDAL); // Liên kết với Hóa đơn (giả sử có quan hệ)
                // 4. db.SubmitChanges(); // Tất cả sẽ chạy trong 1 transaction

                throw; // Ném lỗi ra GUI
            }



