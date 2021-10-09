using Admin.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;
using Admin.ModelJoin;
using Microsoft.EntityFrameworkCore;

namespace Admin.ApiControllers
{
    [Route("api/chi_tiet_bai_kt")]
    [ApiController]
    public class ChiTietBaiKiemTraApiController : ControllerBase
    {
        private readonly ProjectContext _context;
        public ChiTietBaiKiemTraApiController(ProjectContext context)
        {
            this._context = context;
        }
        [HttpPost, Route("add")]
        public async Task<IActionResult> addCauHoi(dynamic tk)
        {
            int maBaiKt = (int)tk.MaBaiKT;
            int maCauHoi = (int)tk.CauHoi;
            CTBaiKT cTBaiKT = new CTBaiKT();
            cTBaiKT.MaBaiKT = maBaiKt;
            cTBaiKT.CauHoi = maCauHoi;
            cTBaiKT.STT = 1;
            this._context.CTBaiKTs.Add(cTBaiKT);
            this._context.SaveChanges();
            CauHoi cauHoi = this._context.CauHois.Where(u => u.MaCauHoi == maCauHoi).FirstOrDefault();
            ChiTietJoinCauHoi chiTietJoinCauHoi = new ChiTietJoinCauHoi
            {
                CTBaiKT = cTBaiKT,
                CauHoi = cauHoi

            };
            return Ok(chiTietJoinCauHoi);
        }
        [HttpPost, Route("index")]
        public async Task<IActionResult> index(dynamic val)
        {
            int maBaiKt = (int)val.MaBaiKT;
            var query = this._context.CTBaiKTs.Where(u => u.MaBaiKT == maBaiKt).Join(this._context.CauHois,
                b => b.CauHoi, c => c.MaCauHoi, (b, c) => new { b, c }).Select(m =>
                 new ChiTietJoinCauHoi
                 {
                     CauHoi = m.c,
                     CTBaiKT = m.b
                 });
            return Ok(query.ToArray());
        }
        [HttpPost, Route("remove")]
        public async Task<IActionResult> Remove(dynamic val)
        {
            int id = (int)val.MaCTBaiKT;

            var item = this._context.CTBaiKTs.Find(id);

            _context.CTBaiKTs.Remove(item);
            _context.SaveChanges();

            return Ok(item);

        }
    }

}
