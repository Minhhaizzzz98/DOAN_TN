using Admin.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;

namespace Admin.ApiControllers
{
    [Route("api/chude")]
    [ApiController]
    public class ChuDeApiController : ControllerBase
    {
        private readonly ProjectContext _context;
        public ChuDeApiController(ProjectContext context)
        {
            this._context = context;
        }
        [HttpPost, Route("getChude")]
        public IActionResult getChuDe(dynamic val)
        {
            int maGV = (int)val.MaGV;
            int maMH = (int)val.MaMonHoc;

            if(maMH > 0)
            {
                var list = _context.ChuDes.Where(u => u.MonHoc == maMH && u.MaGV == maGV && u.TrangThai == true).ToList();
                return Ok(list);
            }
            return BadRequest();
        }

        [HttpPost, Route("SuaChuDe")]
        public IActionResult SuaChuDe(dynamic val)
        {
            int maCD = (int)val.MaChuDe;

            var ChuDe = _context.ChuDes.SingleOrDefault(u => u.MaChuDe == maCD);
            ChuDe.TenChuDe = val.TenChuDe;

            _context.ChuDes.Update(ChuDe);
            _context.SaveChanges();

            return Ok(_context.ChuDes.SingleOrDefault(u => u.MaChuDe == maCD));
        }
        [HttpGet, Route("XoaChuDe/{id}")]
        public IActionResult XoaChuDe(int id)
        {
            var ChuDe = _context.ChuDes.SingleOrDefault(u => u.MaChuDe == id);
            ChuDe.TrangThai = false;

            _context.ChuDes.Update(ChuDe);
            _context.SaveChanges();

            return Ok(_context.ChuDes.SingleOrDefault(u => u.MaChuDe == id));
        }

        [HttpPost, Route("ThemChuDe")]
        public IActionResult ThemChuDe(dynamic val)
        {
            int maGV = (int)val.MaGV;
            string tenChuDe = val.TenChuDe;
            int maMH = (int)val.MaMH;

            ChuDe chuDe = new ChuDe()
            {
                TenChuDe = tenChuDe,
                MaGV = maGV,
                MonHoc = maMH,
                TrangThai = true,
            };

            _context.ChuDes.Add(chuDe);
            _context.SaveChanges();

            return Ok(chuDe);
        }

    }
}
