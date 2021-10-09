using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    // Route register : /Login/UserRegister
    // Route login : /Login/UserLogin (minhanh 123456)
    public class LoginController : Controller
    {
        private readonly ProjectContext _dbContext;
        public LoginController(ProjectContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult UserLogin()
        {
            return View();
        }
        public IActionResult UserRegister()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserRegister(IFormCollection collection)
        {
            //lay ra gia trị post lên từ view
            var userName = collection["username"];
            var pass = collection["pass"];
            var mail = collection["email"];
            var loaitaikhoan = collection["type_taikhoan"];
            var name = collection["name"];
            var diachi = collection["diachi"];
            var sdt = collection["sdt"];
            var email = collection["email"];

            //tao ra tài khoan
            TaiKhoan taikhoan = new TaiKhoan();
            taikhoan.UserName = userName;
            taikhoan.Password= BCrypt.Net.BCrypt.HashPassword(pass);
            taikhoan.LoaiTaiKhoan = Int32.Parse(loaitaikhoan);
            taikhoan.TrangThai = true;
            //end tạo tai khoan bang model
            //tao tai khoan use entity
            _dbContext.TaiKhoans.Add(taikhoan);
            _dbContext.SaveChanges();
            int id = taikhoan.Id;
            if(taikhoan.LoaiTaiKhoan==0)
            {
                // them sinh vien ở đây
            }  
            else
            {
                //them giao vien ở đây
                GiangVien giangVien = new GiangVien();
                giangVien.TenGiangVien = name;
                giangVien.DiaChi = diachi;
                giangVien.SoDienThoai = sdt;
                giangVien.IsAdmin = false;
                giangVien.MaTaiKhoan = id;
                giangVien.TrangThai = true;
                giangVien.Email = mail;
                _dbContext.GiangViens.Add(giangVien);
            }
            _dbContext.SaveChanges();
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(IFormCollection collection)
        {
            //var cookie = HttpContext.Request.Cookies.FirstOrDefault(u => u.Key == "login");
            //if (cookie.Value != null)
            //{
            //    return Redirect("/Home/Index");
            //}
            var userName = collection["username"].ToString();
            var pass = collection["pass"].ToString();
            var TaiKhoan = _dbContext.TaiKhoans.Where(u => u.UserName == userName).FirstOrDefault();
            if(TaiKhoan!=null)
            {
                //check password
                //pass = BCrypt.Net.BCrypt.HashPassword(pass);
                bool verified = BCrypt.Net.BCrypt.Verify(pass,TaiKhoan.Password);
                if (verified)
                {
                    if (TaiKhoan.LoaiTaiKhoan == 1)
                    {
                        GiangVien gv = _dbContext.GiangViens.FirstOrDefault(u => u.MaTaiKhoan == TaiKhoan.Id && u.IsAdmin);
                        
                        if(gv != null)
                        {
                            var str = JsonConvert.SerializeObject(TaiKhoan);
                            HttpContext.Response.Cookies.Append("login", TaiKhoan.UserName);
                            return Redirect("~/Home/Index");
                        }
                        ViewData["Message"] = "This account should have admin rights!";
                        return View();
                    }
                    else
                    {
                        ViewData["Message"] = "This account should have admin rights!";
                        return View();
                    }
                }
                else
                {
                    ViewData["Message"] = "Username and password should be matched!";
                    return View();
                }
            }
            ViewData["Message"] = "Please enter username and password correctly!";
            return View(); 
        }

        public void Logout() {
            HttpContext.Response.Cookies.Delete("login");
            HttpContext.Response.Redirect("/Login/UserLogin");
        }
    }
}
