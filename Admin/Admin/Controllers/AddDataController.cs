using Admin.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;
namespace Admin.Controllers
{
    public class AddDataController : Controller
    {
        private readonly ProjectContext _context;

        public AddDataController(ProjectContext context)
        {
            _context = context;
        }
        public String addMonHoc()
        {
            for(int i=1; i<10;i++)
            {
                MonHoc monHoc = new MonHoc();
                monHoc.SoTiet = 50;
                monHoc.SoTinChi = 4;
                monHoc.TrangThai = true;
                monHoc.TenMonHoc = "Công nghệ phần mềm " + i;
                _context.MonHocs.Add(monHoc);
                _context.SaveChanges();
            }
            return "1";
        }
        public String addGiangVien()
        {
            for(int i=0; i<20;i++)
            {
                TaiKhoan taikhoan = new TaiKhoan();
                taikhoan.UserName = "GiangVien"+i;
                taikhoan.Password = BCrypt.Net.BCrypt.HashPassword("123456");
                taikhoan.LoaiTaiKhoan = 1;
                taikhoan.TrangThai = true;
                //end tạo tai khoan bang model
                //tao tai khoan use entity
                _context.TaiKhoans.Add(taikhoan);
                _context.SaveChanges();

                int id = taikhoan.Id;
                GiangVien giangVien = new GiangVien();
                giangVien.TenGiangVien = "GiangVien"+i;
                giangVien.DiaChi = "Củ Chi";
                giangVien.SoDienThoai = "123456789";
                giangVien.IsAdmin = false;
                giangVien.MaTaiKhoan = id;
                giangVien.TrangThai = true;
                giangVien.Email = "minhhai"+i+"@gmail.com";
                _context.GiangViens.Add(giangVien);
                _context.SaveChanges();
            }
            return "1";        
        }

        public String addSinhVien()
        {
            for (int i = 0; i < 10; i++)
            {
                TaiKhoan taikhoan = new TaiKhoan();
                taikhoan.UserName = "SinhVien" + i;
                taikhoan.Password = BCrypt.Net.BCrypt.HashPassword("123456");
                taikhoan.LoaiTaiKhoan = 2;
                taikhoan.TrangThai = true;
                //end tạo tai khoan bang model
                //tao tai khoan use entity
                _context.TaiKhoans.Add(taikhoan);
                _context.SaveChanges();

                int id = taikhoan.Id;
                SinhVien sinhVien = new SinhVien();
                sinhVien.TenSV = "SinhVien" + i;
                sinhVien.DiaChi = "Củ Chi";
                sinhVien.SoDienThoai = "123456789";
                sinhVien.Lop = 1;
                sinhVien.MaTaiKhoan = id;
                sinhVien.TrangThai = true;
                sinhVien.Email = "minhanh" + i + "@gmail.com";
                _context.SinhViens.Add(sinhVien);
                _context.SaveChanges();
            }
            return "1";
        }
        public String addChuDe()
        {
            for (int i = 0; i < 10; i++)
            {
                ChuDe chude = new ChuDe();
                chude.TenChuDe = "Chu đề " + i;
                chude.TrangThai = true;
                chude.MonHoc = i+1;
                _context.ChuDes.Add(chude);
                _context.SaveChanges();
            }
            return "1";
        }
        public string addCauHoi()
        {
            for (int i =1; i < 100; i++)
            {
                CauHoi cauHoi = new CauHoi();
                cauHoi.TenCauHoi = "Câu hỏi sô : " + i;
                cauHoi.A = "Câu hỏi sô 1 của : "+ i;
                cauHoi.B = "Câu hỏi số 2 của : " + i;
                cauHoi.C = "Câu hỏi số 3 của : " + i;
                cauHoi.D = "Câu hỏi số 4 của : " + i;
                cauHoi.DapAn = "A";
                cauHoi.MaChuDe = i % 10;
                cauHoi.TrangThai = true;
                _context.CauHois.Add(cauHoi);
                _context.SaveChanges();
            }
            return "1";
        }    
        public IActionResult Index()
        {
            return View();
        }
        public String addLop()
        {
            for(int i=1;i<=20;i++)
            {
                Lop lop = new Lop();
                lop.SoLuongSV = 40;
                lop.TenLop = "CDTH" + i;
                lop.TrangThai = true;
                this._context.Lops.Add(lop);
                this._context.SaveChanges();
            }
            return "1";
        }
        public String addLopHP()
        {
            for (int i = 1; i < 20; i++)
            {
                LopHocPhan lhp = new LopHocPhan();
                lhp.MaGiangVien = i;
                lhp.MaLop = i;
                lhp.TenLopHP = "Lớp Học phần thứ " + i;
                lhp.MaMonHoc = i / 2;
                lhp.TrangThai = 1;
                this._context.LopHocPhans.Add(lhp);
                this._context.SaveChanges();
            }
            return "1";
        }
        public String addCTLopHp()
        {
            for(int i= 1;i<=10;i++)
            {
                CTLopHP ct = new CTLopHP();
                ct.MaLopHocPhan = 2;
                ct.MaSinhVien = i;
                ct.LopHocPhanMaLopHP = 2;
                ct.SinhVienMaSV = i;
                ct.Status = true;
                this._context.CTLopHPs.Add(ct);
                this._context.SaveChanges();
            }
            return "1";
        }
    }
}
