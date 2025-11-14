using QuanLyCuaHangBanQuanAo.DAL;
using DTO_SanPham = QuanLyCuaHangBanQuanAo.DTO.SanPham; // Alias for DTO
using System.Collections.Generic;
using System.Linq;

namespace QuanLyCuaHangBanQuanAo.BLL
{
    public class SanPhamBLL
    {
        private readonly QuanLyCuaHangDataContext db = new QuanLyCuaHangDataContext("yourConnectionString");

        /// <summary>
        /// Lấy tất cả sản phẩm
                            /// </summary>
        /// <returns>Danh sách DTO Sản Phẩm</returns>
        public List<DTO_SanPham> GetAll()
        {
            var query = from sp in db.SanPhams
                        join lsp in db.LoaiSanPhams on sp.MaLoaiSP equals lsp.MaLoaiSP
                        select new DTO_SanPham
                        {
                            MaSP = sp.MaSP,
                            TenSP = sp.TenSP,
                            DonGia = sp.DonGia,
                            SoLuongTon = sp.SoLuongTon,
                            MoTa = sp.MoTa,
                            MaLoaiSP = sp.MaLoaiSP,
                            TenLoaiSP = lsp.TenLoaiSP
                        };

            return query.ToList();
        }

        /// <summary>
        /// Thêm một sản phẩm mới
        /// </summary>
        public void Insert(DTO_SanPham spDTO)
        {
            DAL.SanPham spDAL = new DAL.SanPham();
            spDAL.TenSP = spDTO.TenSP;
            spDAL.DonGia = spDTO.DonGia;
            spDAL.SoLuongTon = spDTO.SoLuongTon;
            spDAL.MoTa = spDTO.MoTa;
            spDAL.MaLoaiSP = spDTO.MaLoaiSP;

            db.SanPhams.InsertOnSubmit(spDAL);
            db.SubmitChanges();
        }

        /// <summary>
        /// Cập nhật sản phẩm
        /// </summary>
        public void Update(DTO_SanPham spDTO)
        {
            DAL.SanPham spDAL = db.SanPhams.Single(sp => sp.MaSP == spDTO.MaSP);

            spDAL.TenSP = spDTO.TenSP;
            spDAL.DonGia = spDTO.DonGia;
            spDAL.SoLuongTon = spDTO.SoLuongTon;
            spDAL.MoTa = spDTO.MoTa;
            spDAL.MaLoaiSP = spDTO.MaLoaiSP;

            db.SubmitChanges();
        }

        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        public void Delete(int maSP)
        {
            DAL.SanPham spDAL = db.SanPhams.Single(sp => sp.MaSP == maSP);
            db.SanPhams.DeleteOnSubmit(spDAL);
            db.SubmitChanges();
        }

        /// <summary>
        /// Lấy một sản phẩm theo ID
        /// </summary>
        /// <returns>DTO Sản Phẩm hoặc null nếu không tìm thấy</returns>
        public DTO_SanPham GetByID(int maSP)
        {
            var query = from sp in db.SanPhams
                        join lsp in db.LoaiSanPhams on sp.MaLoaiSP equals lsp.MaLoaiSP
                        where sp.MaSP == maSP
                        select new DTO_SanPham
                        {
                            MaSP = sp.MaSP,
                            TenSP = sp.TenSP,
                            DonGia = sp.DonGia,
                            SoLuongTon = sp.SoLuongTon,
                            MoTa = sp.MoTa,
                            MaLoaiSP = sp.MaLoaiSP,
                            TenLoaiSP = lsp.TenLoaiSP
                        };

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Tìm kiếm sản phẩm (theo Tên SP)
        /// </summary>
        /// <returns>Danh sách DTO Sản Phẩm</returns>
        public List<DTO_SanPham> Search(string keyword)
        {
            string keywordLower = keyword.ToLower();

            var query = from sp in db.SanPhams
                        join lsp in db.LoaiSanPhams on sp.MaLoaiSP equals lsp.MaLoaiSP
                        where sp.TenSP.ToLower().Contains(keywordLower)
                        select new DTO_SanPham
                        {
                            MaSP = sp.MaSP,
                            TenSP = sp.TenSP,
                            DonGia = sp.DonGia,
                            SoLuongTon = sp.SoLuongTon,
                            MoTa = sp.MoTa,
                            MaLoaiSP = sp.MaLoaiSP,
                            TenLoaiSP = lsp.TenLoaiSP
                        };

            return query.ToList();
        }
    }
}