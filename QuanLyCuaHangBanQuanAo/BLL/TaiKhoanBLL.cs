using System;
using System.Linq;
using System.Collections.Generic;
using QuanLyCuaHangBanQuanAo.DAL;
using QuanLyCuaHangBanQuanAo.Common; // Đã thêm ở Bước 1
using DALTaiKhoan = QuanLyCuaHangBanQuanAo.DAL.TaiKhoan;
using DTOTaiKhoan = QuanLyCuaHangBanQuanAo.DTO.TaiKhoan;

namespace QuanLyCuaHangBanQuanAo.BLL
{
    public class TaiKhoanBLL
    {
        // Chuyển đổi Entity (DAL) sang DTO (khớp CSDL mới)
        private DTOTaiKhoan EntityToDTO(DALTaiKhoan entity)
        {
            if (entity == null) return null;
            return new DTOTaiKhoan
            {
                TenDangNhap = entity.TenDangNhap,
                MatKhau = entity.MatKhau, // Giữ hash
                MaNV = entity.MaNV,
                MaQuyen = entity.MaQuyen
            };
        }

        // KiemTraDangNhap (Sửa logic)
        public bool KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            try
            {
                using (QuanLyCuaHangDataContext db = new QuanLyCuaHangDataContext  ())
                {
                    var entity = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == tenDangNhap);
                    if (entity == null) return false;

                    // Xác thực mật khẩu đã hash
                    return PasswordHelper.VerifyPassword(matKhau, entity.MatKhau);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra đăng nhập: " + ex.Message);
            }
        }

        // LayThongTinTaiKhoan (Sửa logic)
        public DTOTaiKhoan LayThongTinTaiKhoan(string tenDangNhap)
        {
            try
            {
                using (QuanLyCuaHangDataContext db = new QuanLyCuaHangDataContext   ())
                {
                    var entity = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == tenDangNhap);
                    return EntityToDTO(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin tài khoản: " + ex.Message);
            }
        }

        // DoiMatKhau (Sửa logic)
        public bool DoiMatKhau(string tenDangNhap, string mkCu, string mkMoi)
        {
            try
            {
                using (QuanLyCuaHangDataContext db = new QuanLyCuaHangDataContext ())
                {
                    var entity = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == tenDangNhap);
                    if (entity == null)
                    {
                        throw new Exception("Tài khoản không tồn tại!");
                    }

                    if (!PasswordHelper.VerifyPassword(mkCu, entity.MatKhau))
                    {
                        throw new Exception("Mật khẩu cũ không đúng!");
                    }

                    entity.MatKhau = PasswordHelper.HashPassword(mkMoi);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đổi mật khẩu: " + ex.Message);
            }
        }
    }
}