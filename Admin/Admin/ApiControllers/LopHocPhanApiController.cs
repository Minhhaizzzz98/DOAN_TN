using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;
using Admin.Data;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Admin.ModelJoin;

namespace Admin.Controllers
{
    [Route("api/lophocphan")]
    [ApiController]
    public class LopHocPhanApiController : ControllerBase
    {
        private readonly ProjectContext _context;
        public LopHocPhanApiController(ProjectContext context)
        {
            _context = context;
        }
        [HttpPost, Route("index")]
        public async Task<ActionResult<IEnumerable<LopHocPhan>>> getLopHocPhan(dynamic tk)
        {
            int id =(int)tk.id_user;

             return await _context.LopHocPhans.Where(u => u.MaGiangVien ==id).ToArrayAsync();

        }
        //[HttpGet,Route("indexsv/{id?}")]
        //public async Task<IActionResult> getLopHocPhanSV(int? id)
        //{
        //    return Ok(_context.CTLopHPs.Where(u => u.MaSinhVien == id && u.Status).Include(u => u.LopHocPhan).Include(u => u.SinhVien).ToArray());
        //}
        [HttpPost, Route("GetChuDeTheoLHP")]
        public IActionResult GetChuDeTheoLHP(dynamic val)
        {
            int maLHP = (int)val.MaLHP;
            int maGV = (int)val.MaGV;

            var lHP = _context.LopHocPhans.FirstOrDefault(u => u.MaLopHP == maLHP);

            var query = _context.ChuDes.Where(u => u.MonHoc == lHP.MaMonHoc && u.TrangThai && u.MaGV == maGV).ToList();

            return Ok(query);
        }
        [HttpPost, Route("GetLHPCuaSV")]
        public async Task<IActionResult> GetLHPCuaSV(dynamic val)
        {
            int maSV = (int)val.MaSV;

            var lhpTheoLop = _context.SinhViens.Where(u => u.MaSV == maSV && u.TrangThai)
                .Join(_context.Lops, sinhvien => sinhvien.Lop, lop => lop.MaLop,
                (sinhvien, lop) => new
                {
                    Lop = lop
                }).Where(u => u.Lop.TrangThai)
                .Join(_context.LopHocPhans, lop => lop.Lop.MaLop, lhp => lhp.MaLop,
                (lop, lhp) => new
                {
                    LopHocPhan = lhp
                }).Where(u => u.LopHocPhan.TrangThai ==1).Distinct();

            var lhpTheoCTLHP = _context.CTLopHPs.Where(u => u.MaSinhVien == maSV && u.Status)
                .Join(_context.LopHocPhans, ct => ct.MaLopHocPhan, lhp => lhp.MaLopHP,
                (ct, lhp) => new
                {
                    LopHocPhan = lhp
                }).Where(u => u.LopHocPhan.TrangThai ==1).Distinct();
          
            var query = lhpTheoLop.Union(lhpTheoCTLHP);

            return Ok(query.ToList());
        }
        [HttpGet, Route("GetLHPGV/{id?}")]
        public async Task<IActionResult> getLopHocPhanGV(int?  id)
        {
    
     
            var query = this._context.LopHocPhans.Where(u => u.MaGiangVien == id && u.TrangThai == 1).Join(
                _context.Lops, lhp => lhp.MaLop, lop => lop.MaLop, (lhp, lop) => new
                {
                    lhp,
                    lop
                }).Join(this._context.GiangViens, lop1 => lop1.lhp.MaGiangVien, gv => gv.MaGiangVien, (lop1, gv) => new
                {
                    lop1,
                    gv
                }).Select(m => new LopHocPhanJoinGiangVien
                {
                    GiangVien = m.gv,
                    LopHocPhan = m.lop1.lhp,
                    Lop = m.lop1.lop
                   
                });
            return Ok(query.ToArray());
        }
        [HttpGet, Route("Get-LHP-GV-HT/{id?}")]
        public async Task<IActionResult> getLopHocPhanGVHT(int? id)
        {


            var query = this._context.LopHocPhans.Where(u => u.MaGiangVien == id && u.TrangThai == 2).Join(
                _context.Lops, lhp => lhp.MaLop, lop => lop.MaLop, (lhp, lop) => new
                {
                    lhp,
                    lop
                }).Join(this._context.GiangViens, lop1 => lop1.lhp.MaGiangVien, gv => gv.MaGiangVien, (lop1, gv) => new
                {
                    lop1,
                    gv
                }).Select(m => new LopHocPhanJoinGiangVien
                {
                    GiangVien = m.gv,
                    LopHocPhan = m.lop1.lhp,
                    Lop = m.lop1.lop

                });
            return Ok(query.ToArray());
        }

        [HttpGet, Route("GetLHPSV/{id?}")]
        public async Task<IActionResult> getLopHocPhanSVLop(int? id)
        {
            var sinhVien = this._context.SinhViens.Find(id);

            var query = this._context.LopHocPhans.Where(u => u.MaLop == sinhVien.Lop && u.TrangThai == 1).Join(
                _context.Lops, lhp => lhp.MaLop, lop => lop.MaLop, (lhp, lop) => new
                {
                    lhp,
                    lop
                }).Join(this._context.GiangViens, lop1 => lop1.lhp.MaGiangVien, gv => gv.MaGiangVien, (lop1, gv) => new
                {
                    lop1,
                    gv
                }).Select(m => new LopHocPhanJoinGiangVien
                {
                    GiangVien = m.gv,
                    LopHocPhan = m.lop1.lhp,
                    Lop = m.lop1.lop

                });
            //var listCT = _context.CTBaiKTs.Where(u => u.MaBaiKT == MaBaiKT).ToList();

            //var m = _context.CauHois.ToList().Where(p => !(listCT.Any(item2 => item2.CauHoi == p.MaCauHoi)));
            var query2 = this._context.CTLopHPs.Where(u => u.MaSinhVien == sinhVien.MaSV).ToList();
            var query3 = this._context.LopHocPhans.Where(u => (query2.Any(item => item.MaLopHocPhan == u.MaLopHP))).Join(
                _context.Lops, lhp => lhp.MaLop, lop => lop.MaLop, (lhp, lop) => new
                {
                    lhp,
                    lop
                }).Join(this._context.GiangViens, lop1 => lop1.lhp.MaGiangVien, gv => gv.MaGiangVien, (lop1, gv) => new
                {
                    lop1,
                    gv
                }).Select(m => new LopHocPhanJoinGiangVien
                {
                    GiangVien = m.gv,
                    LopHocPhan = m.lop1.lhp,
                    Lop = m.lop1.lop

                });
            var qr4 = query.Union(query3);
            return Ok(query.ToArray());


        }
        [HttpPost, Route("edit-status/{id?}")]
        public async Task<IActionResult> EditStatus(int? id, dynamic val)
        {

            int status = (int)val.status;
            var lhp = this._context.LopHocPhans.Find(id);
            lhp.TrangThai = status;
            this._context.LopHocPhans.Update(lhp);
            this._context.SaveChanges();
            return Ok(lhp);
        }
    }
}
