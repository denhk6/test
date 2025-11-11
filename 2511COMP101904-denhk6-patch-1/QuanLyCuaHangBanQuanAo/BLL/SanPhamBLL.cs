using QuanLyCuaHangBanQuanAo.DAL;
using QuanLyCuaHangBanQuanAo.DTO; // Sử dụng DTO
using System.Collections.Generic;
using System.Linq;
namespace QuanLyCuaHangBanQuanAo.BLL
{
public class SanPhamBLL
{
QuanLyCuaHangDataContext db = new QuanLyCuaHangDataContext();
/// <summary>
/// Lấy tất cả sản phẩm
/// </summary>
/// <returns>Danh sách DTO Sản Phẩm</returns>
public List<SanPham> GetAll()
{
// Dùng LINQ để query và chuyển đổi (Project) từ class DAL (SanPham)
// sang class DTO (DTO.SanPham)
var query = from sp in db.SanPhams
join lsp in db.LoaiSanPhams on sp.MaLoaiSP equals lsp.MaLoaiSP
select new DTO.SanPham // Tạo DTO mới
{
MaSP = sp.MaSP,
TenSP = sp.TenSP,
DonGia = sp.DonGia.GetValueOrDefault(0),
SoLuongTon = sp.SoLuongTon.GetValueOrDefault(0),
MoTa = sp.MoTa,
MaLoaiSP = sp.MaLoaiSP,
TenLoaiSP = lsp.TenLoaiSP
};
return query.ToList();
}
/// <summary>
/// Thêm một sản phẩm mới
/// </summary>
public void Insert(DTO.SanPham spDTO)
{
// Chuyển từ DTO sang class DAL
DAL.SanPham spDAL = new DAL.SanPham();
spDAL.TenSP = spDTO.TenSP;
spDAL.DonGia = spDTO.DonGia;
spDAL.SoLuongTon = spDTO.SoLuongTon;
spDAL.MoTa = spDTO.MoTa;
spDAL.MaLoaiSP = spDTO.MaLoaiSP;
// Giả sử MaSP là tự tăng (Identity)
db.SanPhams.InsertOnSubmit(spDAL);
db.SubmitChanges();
}
/// <summary>
/// Cập nhật sản phẩm
/// </summary>
public void Update(DTO.SanPham spDTO)
{
// Tìm đối tượng DAL trong CSDL
DAL.SanPham spDAL = db.SanPhams.Single(sp => sp.MaSP == spDTO.MaSP);
// Cập nhật từ DTO
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
// Lưu ý: Cần xử lý các ràng buộc khóa ngoại
// (ví dụ: không xóa SP đã có trong ChiTietHoaDon)
}
}
}