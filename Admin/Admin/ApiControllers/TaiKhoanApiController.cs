using Admin.Data;
using Admin.ModelJoin;
using Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ApiControllers
{
    [Route("api/taikhoan")]
    [ApiController]
    public class TaiKhoanApiController : ControllerBase
    {
        private readonly ProjectContext _context;
        public TaiKhoanApiController(ProjectContext context)
        {
            _context = context;
        }

        [Route("DoiMatKhau")]
        public ActionResult<TaiKhoan> DoiMatKhau(dynamic value)
        {
            try
            {
                string oldPassword = RevertPassword((string)value.oldPassword);
                string newPassword = RevertPassword((string)value.newPassword);
                string role = (string)value.role;
                int id = (int)value.id;

                //var hashOldPassword = BCrypt.Net.BCrypt.HashPassword(oldPassword);
                var hashNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

                if (role == "gv")
                {
                    var data = _context.TaiKhoans
                        .Join(_context.GiangViens.Where(u => u.MaGiangVien == id),
                        tk => tk.Id,
                        gv => gv.MaTaiKhoan,
                        (tk, gv) => new
                        {
                            tkId = tk.Id,
                            tkPass = tk.Password
                        }).FirstOrDefault();
                    if (data != null)
                    {
                        TaiKhoan taiKhoan = _context.TaiKhoans.Where(u => u.Id == data.tkId).FirstOrDefault();
                        bool verified = BCrypt.Net.BCrypt.Verify(oldPassword, taiKhoan.Password);
                        if (verified)
                        {
                            taiKhoan.Password = hashNewPassword;
                            _context.TaiKhoans.Update(taiKhoan);
                            _context.SaveChanges();
                            return Ok("OK");
                        }
                    }
                }
                else if (role == "sv")
                {
                    var data = _context.TaiKhoans
                        .Join(_context.SinhViens.Where(u => u.MaSV == id),
                        tk => tk.Id,
                        sv => sv.MaTaiKhoan,
                        (tk, sv) => new
                        {
                            tkId = tk.Id,
                            tkPass = tk.Password
                        }).FirstOrDefault();

                    if (data != null)
                    {
                        TaiKhoan taiKhoan = _context.TaiKhoans.Where(u => u.Id == data.tkId).FirstOrDefault();
                        bool verified = BCrypt.Net.BCrypt.Verify(oldPassword, taiKhoan.Password);
                        if (verified)
                        {
                            taiKhoan.Password = hashNewPassword;
                            _context.TaiKhoans.Update(taiKhoan);
                            _context.SaveChanges();
                            return Ok("OK");
                        }
                    }
                }
                return BadRequest("The old password is not correct!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private string RevertPassword(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return null;
            }
            string newPassword = String.Empty;
            for (int i = password.Length - 1; i >= 0; i--)
            {
                newPassword += password[i];
            }

            return newPassword;
        }
    }
}
