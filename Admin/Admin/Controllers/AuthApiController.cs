using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Controllers
{
   
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly ProjectContext _context;
        public AuthApiController(ProjectContext context)
        {
            _context = context;
        }

        [HttpPost, Route("login")]
        public async Task<ActionResult<TaiKhoan>> Login(TaiKhoan taiKhoan)
        {
           // var TaiKhoan = _context.TaiKhoans.Where(u => u.UserName == "hai").FirstOrDefault();
            var user = _context.TaiKhoans.Where(u => u.UserName == taiKhoan.UserName).FirstOrDefault();
            if (user != null)
            {
                var gv = _context.GiangViens.Where(u => u.MaTaiKhoan == user.Id).FirstOrDefault();
                SinhVien sv = new SinhVien();

                bool verified = BCrypt.Net.BCrypt.Verify(taiKhoan.Password, user.Password);
                if (verified)
                {
                    
                    var claim = new List<Claim> {
                        new Claim(ClaimTypes.Name,taiKhoan.UserName),
                        new Claim(ClaimTypes.Role,"lecturer"),
                    };

                    
                    var serectKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supperSecretKey@345"));
                    var singingCreate = new SigningCredentials(serectKey, SecurityAlgorithms.HmacSha256);
                    var tokenOption = new JwtSecurityToken(
                        issuer: "https://localhost:5001",
                        audience: "https://localhost:5001",
                        claims: claim,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: singingCreate
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

                    if(gv != null)
                    {
                        return Ok(new { Token = tokenString, id = gv.MaGiangVien, role = "gv", ten = gv.TenGiangVien });
                    }
                    else
                    {
                        sv = _context.SinhViens.Where(u => u.MaTaiKhoan == user.Id).FirstOrDefault();

                        return Ok(new { Token = tokenString, id = sv.MaSV, role = "sv", ten= sv.TenSV });
                    }
                }
                else
                {
                    return Unauthorized();
                }    
            }
            else
            {
                return Unauthorized();
            }  
        }
    }
}
