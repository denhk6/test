using QuanLyCuaHangBanQuanAo.DAL;
using QuanLyCuaHangBanQuanAo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
namespace QuanLyCuaHangBanQuanAo.BLL
{
public class KhachHangBLL
{
QuanLyCuaHangDataContext db = new QuanLyCuaHangDataContext();
/// <summary>
/// Lấy tất cả khách hàng
/// </summary>
public List<DTO.KhachHang> GetAll()
{
var query = from kh in db.KhachHangs
select new DTO.KhachHang
{
MaKH = kh.MaKH,
TenKH = kh.TenKH,
DiaChi = kh.DiaChi,
SoDienThoai = kh.SoDienThoai,
Email = kh.Email,
DiemTichLuy = kh.DiemTichLuy.GetValueOrDefault(0)
};
return query.ToList();
}
/// <summary>
/// Lấy một khách hàng theo ID
/// </summary>
public DTO.KhachHang GetByID(int maKH)
{
var kh = db.KhachHangs.FirstOrDefault(k => k.MaKH == maKH);
if (kh == null) return null;
return new DTO.KhachHang
{
MaKH = kh.MaKH,
TenKH = kh.TenKH,
DiaChi = kh.DiaChi,
SoDienThoai = kh.SoDienThoai,
Email = kh.Email,
DiemTichLuy = kh.DiemTichLuy.GetValueOrDefault(0)
};
}
/// <summary>
/// Thêm khách hàng mới
/// </summary>
public void Insert(DTO.KhachHang khDTO)
{
DAL.KhachHang khDAL = new DAL.KhachHang
{
TenKH = khDTO.TenKH,
DiaChi = khDTO.DiaChi,
SoDienThoai = khDTO.SoDienThoai,
Email = khDTO.Email,
DiemTichLuy = khDTO.DiemTichLuy
};
// MaKH sẽ tự động tăng
db.KhachHangs.InsertOnSubmit(khDAL);
db.SubmitChanges();
}
/// <summary>
/// Cập nhật thông tin khách hàng
/// </summary>
public void Update(DTO.KhachHang khDTO)
{
DAL.KhachHang khDAL = db.KhachHangs.Single(kh => kh.MaKH == khDTO.MaKH);
khDAL.TenKH = khDTO.TenKH;
khDAL.DiaChi = khDTO.DiaChi;
khDAL.SoDienThoai = khDTO.SoDienThoai;
khDAL.Email = khDTO.Email;
khDAL.DiemTichLuy = khDTO.DiemTichLuy;
db.SubmitChanges();
}
/// <summary>
/// Xóa khách hàng
/// </summary>
public void Delete(int maKH)
{
// Cần xử lý ràng buộc khóa ngoại (ví dụ: khách hàng đã có hóa đơn)
// Trong ví dụ này, chúng ta giả định là xóa được.
try
{
DAL.KhachHang khDAL = db.KhachHangs.Single(kh => kh.MaKH == maKH);
db.KhachHangs.DeleteOnSubmit(khDAL);
db.SubmitChanges();
}
catch (Exception ex)
{
// Ném ra lỗi để GUI bắt
throw new Exception("Không thể xóa khách hàng. Khách hàng có thể đã có
hóa đơn. Lỗi: " + ex.Message);
}
}
/// <summary>
/// Tìm kiếm khách hàng (theo Tên hoặc SĐT)
/// </summary>
public List<DTO.KhachHang> Search(string keyword)
{
string keywordLower = keyword.ToLower();
var query = from kh in db.KhachHangs
where kh.TenKH.ToLower().Contains(keywordLower) ||
kh.SoDienThoai.Contains(keyword)
select new DTO.KhachHang
{
MaKH = kh.MaKH,
TenKH = kh.TenKH,
DiaChi = kh.DiaChi,
SoDienThoai = kh.SoDienThoai,
Email = kh.Email,
DiemTichLuy = kh.DiemTichLuy.GetValueOrDefault(0)
};
return query.ToList();
}
}
}