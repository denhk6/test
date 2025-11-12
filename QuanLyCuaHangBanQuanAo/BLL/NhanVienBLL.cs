using QuanLyCuaHangBanQuanAo.DAL;
using QuanLyCuaHangBanQuanAo.DTO;
using QuanLyCuaHangBanQuanAo.Common;
using System;
using System.Collections.Generic;
using System.Linq;


namespace QuanLyCuaHangBanQuanAo.BLL
{
    public class NhanVienBLL
    {
        QuanLyCuaHangDataContext db = new QuanLyCuaHangDataContext();

        /// <summary>
        /// Lấy tất cả nhân viên (có thể join với Tài Khoản để lấy Tên quyền)
        /// </summary>
        public List<DTO.NhanVien> GetAll()
        {
            // Giả sử DTO.NhanVien có thêm thuộc tính TenQuyen
            var query = from nv in db.NhanViens
                        join tk in db.TaiKhoans on nv.MaNV equals tk.MaNhanVien
                        join q in db.PhanQuyens on tk.MaQuyen equals q.MaQuyen
                        select new DTO.NhanVien
                        {
                            MaNV = nv.MaNV,
                            TenNV = nv.TenNV,
                            ChucVu = nv.ChucVu,
                            DiaChi = nv.DiaChi,
                            SoDienThoai = nv.SoDienThoai,
                            NgayVaoLam = nv.NgayVaoLam,
                            MaTaiKhoan = tk.TenDangNhap, // Hiển thị tên đăng nhập
                            TenQuyen = q.TenQuyen // Hiển thị tên quyền
                        };
            return query.ToList();
        }

        /// <summary>
        /// Thêm nhân viên mới (Cần thêm cả Tài Khoản)
        /// </summary>
        /// <param name="nvDTO">Thông tin nhân viên</param>
        /// <param name="tkDTO">Thông tin tài khoản (Tên, Mật khẩu, Quyền)</param>
        public void Insert(DTO.NhanVien nvDTO, DTO.TaiKhoan tkDTO)
        {
            // Logic này phức tạp, cần dùng Transaction
            // 1. Thêm NhanVien
            // 2. Lấy MaNV vừa thêm
            // 3. Thêm TaiKhoan với MaNV đó

            // Bắt đầu 1 transaction
            using (var transaction = db.Connection.BeginTransaction())
            {
                db.Transaction = transaction;
                try
                {
                    // 1. Thêm NhanVien
                    DAL.NhanVien nvDAL = new DAL.NhanVien
                    {
                        TenNV = nvDTO.TenNV,
                        ChucVu = nvDTO.ChucVu,
                        DiaChi = nvDTO.DiaChi,
                        SoDienThoai = nvDTO.SoDienThoai,
                        NgayVaoLam = nvDTO.NgayVaoLam
                    };
                    db.NhanViens.InsertOnSubmit(nvDAL);
                    // Cần SubmitChanges để lấy MaNV mới
                    

                    // 2. Thêm TaiKhoan
                    DAL.TaiKhoan tkDAL = new DAL.TaiKhoan
                    {
                        TenDangNhap = tkDTO.TenDangNhap,
                        MatKhau = PasswordHelper.HashPassword(tkDTO.MatKhau), // Đã mã hóa mât khẩu
                        MaQuyen = (int)tkDTO.MaQuyen
                    };

                    // Liên kết tài khoản với nhân viên
                    tkDAL.NhanVien = nvDAL;

                    db.TaiKhoans.InsertOnSubmit(tkDAL);

                    // Submit 1 lần duy nhất
                    db.SubmitChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; // Ném lỗi ra ngoài
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên (và cả tài khoản)
        /// </summary>
        public void Update(DTO.NhanVien nvDTO, DTO.TaiKhoan tkDTO)
        {
            using (var transaction = db.Connection.BeginTransaction())
            {
                db.Transaction = transaction;
                try
                {
                    // 1. Cập nhật NhanVien
                    DAL.NhanVien nvDAL = db.NhanViens.Single(nv => nv.MaNV == nvDTO.MaNV);
                    nvDAL.TenNV = nvDTO.TenNV;
                    nvDAL.ChucVu = nvDTO.ChucVu;
                    nvDAL.DiaChi = nvDTO.DiaChi;
                    nvDAL.SoDienThoai = nvDTO.SoDienThoai;
                    nvDAL.NgayVaoLam = nvDTO.NgayVaoLam;

                    // 2. Cập nhật TaiKhoan
                    DAL.TaiKhoan tkDAL = db.TaiKhoans.Single(tk => tk.MaNhanVien == nvDTO.MaNV);
                    tkDAL.TenDangNhap = tkDTO.TenDangNhap;
                    tkDAL.MaQuyen = tkDTO.MaQuyen;
                    // Chỉ cập nhật mật khẩu nếu người dùng nhập mật khẩu mới
                    if (!string.IsNullOrEmpty(tkDTO.MatKhau))
                    {
                        tkDAL.MatKhau = PasswordHelper.HashPassword(tkDTO.MatKhau); // Đã mã hóa mật khẩu
                    }

                    db.SubmitChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Xóa nhân viên (Sẽ xóa cả Tài Khoản)
        /// </summary>
        public void Delete(int maNV)
        {
            // Cần xóa Tài Khoản trước, rồi xóa Nhân Viên
            using (var transaction = db.Connection.BeginTransaction())
            {
                db.Transaction = transaction;
                try
                {
                    // Xóa tài khoản
                    DAL.TaiKhoan tkDAL = db.TaiKhoans.Single(tk => tk.MaNhanVien == maNV);
                    db.TaiKhoans.DeleteOnSubmit(tkDAL);

                    // Xóa nhân viên
                    DAL.NhanVien nvDAL = db.NhanViens.Single(nv => nv.MaNV == maNV);
                    db.NhanViens.DeleteOnSubmit(nvDAL);

                    db.SubmitChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Không thể xóa nhân viên. Lỗi: " + ex.Message);
                }
            }
        }
    }
}