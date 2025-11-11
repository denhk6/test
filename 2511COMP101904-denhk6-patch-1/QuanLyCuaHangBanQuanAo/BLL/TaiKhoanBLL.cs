using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using QuanLyCuaHangBanQuanAo.DAL;
using DALTaiKhoan = QuanLyCuaHangBanQuanAo.DAL.TaiKhoan;
using DTOTaiKhoan = QuanLyCuaHangBanQuanAo.DTO.TaiKhoan;

namespace QuanLyCuaHangBanQuanAo.BLL
{
    public class TaiKhoanBLL
    {
        private DTOTaiKhoan EntityToDTO(DALTaiKhoan entity)
        {
            if (entity == null) return null;

            return new DTOTaiKhoan
            {
                MaTK = entity.MaTK,
                TK = entity.TK,
                MK = entity.MK,
                Role = entity.Role
            };
        }

        private DALTaiKhoan DTOToEntity(DTOTaiKhoan dto)
        {
            if (dto == null) return null;

            return new DALTaiKhoan
            {
                MaTK = dto.MaTK,
                TK = dto.TK,
                MK = dto.MK,
                Role = dto.Role
            };
        }

        private List<DTOTaiKhoan> EntityListToDTOList(List<DALTaiKhoan> entities)
        {
            return entities.Select(e => EntityToDTO(e)).ToList();
        }

        public bool KiemTraDangNhap(string tk, string mk)
        {
            try
            {
                using (QuanLyCuaHangDataContextDataContext db = new QuanLyCuaHangDataContextDataContext())
                {
                    var entity = db.TaiKhoans.FirstOrDefault(x => x.TK == tk && x.MK == mk);
                    return entity != null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra đăng nhập: " + ex.Message);
            }
        }

        public DTOTaiKhoan LayThongTinTaiKhoan(string tk)
        {
            try
            {
                using (QuanLyCuaHangDataContextDataContext db = new QuanLyCuaHangDataContextDataContext())
                {
                    var entity = db.TaiKhoans.FirstOrDefault(x => x.TK == tk);
                    return EntityToDTO(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin tài khoản: " + ex.Message);
            }
        }

        public bool CapNhatTaiKhoan(DTOTaiKhoan taiKhoan)
        {
            try
            {
                using (QuanLyCuaHangDataContextDataContext db = new QuanLyCuaHangDataContextDataContext())
                {
                    var entity = db.TaiKhoans.FirstOrDefault(x => x.MaTK == taiKhoan.MaTK);
                    if (entity == null)
                    {
                        throw new Exception("Không tìm thấy tài khoản!");
                    }

                    entity.TK = taiKhoan.TK;
                    entity.MK = taiKhoan.MK;
                    entity.Role = taiKhoan.Role;

                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật tài khoản: " + ex.Message);
            }
        }

        public bool DoiMatKhau(string tk, string mkCu, string mkMoi)
        {
            try
            {
                using (QuanLyCuaHangDataContextDataContext db = new QuanLyCuaHangDataContextDataContext())
                {
                    var entity = db.TaiKhoans.FirstOrDefault(x => x.TK == tk && x.MK == mkCu);
                    if (entity == null)
                    {
                        throw new Exception("Mật khẩu cũ không đúng!");
                    }

                    entity.MK = mkMoi;
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