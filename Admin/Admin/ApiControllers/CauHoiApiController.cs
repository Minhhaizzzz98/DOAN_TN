using Admin.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;
using Microsoft.EntityFrameworkCore;
using Admin.Helpers;
using Admin.Services;

namespace Admin.ApiControllers
{
    [Route("api/cauhoi")]
    [ApiController]
    public class CauHoiApiController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IUriService _uriService;

        public CauHoiApiController(ProjectContext context, IUriService uriService)
        {
            this._context = context;
            _uriService = uriService;
        }

        [HttpPost, Route("index")]
        public async Task<IActionResult> Index(dynamic val)
        {
            int MaBaiKT = (int)val.MaBaiKT;
            var listCT = _context.CTBaiKTs.Where(u => u.MaBaiKT == MaBaiKT).ToList();

           var m = _context.CauHois.ToList().Where(p => !( listCT.Any(item2 => item2.CauHoi == p.MaCauHoi)));
            return Ok(m);
        }

        [HttpGet, Route("GetAllCauHoi")]
        public async Task<IActionResult> GetAllCauHoi([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.CauHois
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.CauHois.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<CauHoi>(pagedData, validFilter, totalRecords, _uriService, route);
            
            return Ok(pagedReponse);
        }

        [HttpPost, Route("GetCauHoiTheoId")]
        public async Task<IActionResult> GetCauHoiTheoId(dynamic val)
        {
            int maCH = (int)val.MaCH;
            var cauHoi = await _context.CauHois.Where(a => a.MaCauHoi == maCH).FirstOrDefaultAsync();
            return Ok(new Response<CauHoi>(cauHoi));
        }
        [HttpPost, Route("getChuDe")]
        public  IActionResult GetCauHoiTheoChudeCuaMinhHai(dynamic val)
        {
            int maCD = (int)val.MaChuDe;
            int maGV = (int)val.MaGV;
            if (maCD > 0)
            {
                var list_cauhoi = _context.CauHois.Where(u => u.MaChuDe == maCD && u.TrangThai == true && u.MaGV == maGV).ToList();
                return Ok(list_cauhoi);
            }
            return BadRequest();
        }
        [HttpPost,Route("themcauhoi")]
        public IActionResult ThemCauHoi(CauHoi cauHoi)
        {
            CauHoi item = cauHoi;
            item.TrangThai = true;
            if(item != null)
            {
                if(item.MaCauHoi>0)
                {
                    this._context.CauHois.Update(item);
                }    
                else
                {
                    this._context.CauHois.Add(item);
                }    
                
                this._context.SaveChanges();
                return Ok(item);
            }
            return BadRequest();
        }

        [HttpPost, Route("GetCauHoiTheoChuDe")]
        public IActionResult GetCauHoiTheoChuDe(dynamic val)
        {
            int maCD = (int)val.MaChuDe;
            int MaBaiKT = (int)val.MaBaiKT;
            int maGV = (int)val.MaGV;

            var listCT = _context.CTBaiKTs.Where(u => u.MaBaiKT == MaBaiKT).ToList();
            var m = _context.CauHois.OrderByDescending(u => u.MaCauHoi).ToList().Where(p => !(listCT.Any(item2 => item2.CauHoi == p.MaCauHoi)) && p.MaChuDe == maCD && p.MaGV == maGV && p.TrangThai);
            
            return Ok(m);
        }
        [HttpPost, Route("GetCauHoiAllChuDe")]
        public IActionResult GetCauHoiAllChuDe(dynamic val)
        {
            int MaBaiKT = (int)val.MaBaiKT;
            int maGV = (int)val.MaGV;

            var bKT = _context.BaiKiemTras.FirstOrDefault(u => u.MaBaiKT == MaBaiKT);
            var lHP = _context.LopHocPhans.FirstOrDefault(u => u.MaLopHP == bKT.MaLopHocPhan);

            var listCT = _context.CTBaiKTs.Where(u => u.MaBaiKT == MaBaiKT).ToList();
            var listCD = _context.ChuDes.Where(u => u.MonHoc == lHP.MaMonHoc && u.TrangThai && u.MaGV == maGV).ToList();
            var m = _context.CauHois.ToList().Where(p => !(listCT.Any(item2 => item2.CauHoi == p.MaCauHoi)) && listCD.Any(u => u.MaChuDe == p.MaChuDe) && p.MaGV == maGV && p.TrangThai);
            
            return Ok(m);
        }
        [HttpGet, Route("XoaCauHoi/{id}")]
        public IActionResult XoaCauHoi(int id)
        {
            //int maCH = (int)val.MaCauHoi;

            var cauhoi = _context.CauHois.SingleOrDefault(u => u.MaCauHoi == id && u.TrangThai);

            cauhoi.TrangThai = false;

            _context.CauHois.Update(cauhoi);
            _context.SaveChanges();

            return Ok(cauhoi);
        }
    }
}
