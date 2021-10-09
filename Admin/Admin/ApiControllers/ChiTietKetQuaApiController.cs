using Admin.Data;
using Admin.ModelJoin;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ApiControllers
{
    [Route("api/chi_tiet_kq")]
    [ApiController]
    public class ChiTietKetQuaApiController : ControllerBase
    {
        private readonly ProjectContext _context;

        public ChiTietKetQuaApiController(ProjectContext context)
        {
            _context = context;
        }
        [HttpPost, Route("index")]
        public IActionResult index(dynamic val)
        {
            int svId = (int)val.MaSV;
            var data = this._context.CTKetQuas.Where(u => u.SinhVienMaSV == svId).Select(x => x.BaiKiemTra).Distinct();

            var query = this._context.LopHocPhans.Join(data,
                b => b.MaLopHP, c => c.MaLopHocPhan, (b, c) => new { b, c }).Select(m =>
                 new BaiKiemTraJoinLopHocPhan
                 {
                     BaiKiemTra = m.c,
                     LopHocPhan = m.b
                 }).Distinct();

            var result = query.Where(u => u.BaiKiemTra.TrangThai).ToList();
            
            return Ok(result);
        }
        [HttpPost, Route("CreateKetQua")]
        public IActionResult CreateKetQua(dynamic val)
        {
            int maBKT = (int)val.MaBaiKT;
            int maSV = (int)val.MaSV;
            int soCauDung = 0;
            int tongCH = 0;
            float soDiem = 0;

            var listCTKQ = _context.CTKetQuas.Where(u => u.SinhVienMaSV == maSV && u.BaiKiemTraMaBaiKT == maBKT).ToList();

            tongCH = listCTKQ.Count;

            foreach(var item in listCTKQ)
            {
                int maCH = item.CauHoiMaCauHoi;
                var cauhoi = _context.CauHois.SingleOrDefault(u => u.MaCauHoi == maCH);
                if(item.DapAnSVChon == cauhoi.DapAn)
                {
                    soCauDung++;
                }
            }

            soDiem = (float)Math.Round((double)100 / tongCH * soCauDung , 2);

            KetQua ketQua = new KetQua
            {
                MaSinhVien = maSV,
                MaBaiKiemTra = maBKT,
                Diem = soDiem,
                SoCauDung = soCauDung,
                TrangThai = true,
            };

            _context.KetQuas.Add(ketQua);
            _context.SaveChanges();

            var queryKQ = _context.KetQuas.FirstOrDefault(u => u.MaBaiKiemTra == maBKT && u.MaSinhVien == maSV && u.TrangThai);
            
            return Ok(queryKQ);
        }

        [HttpPost, Route("GetKQ")]
        public IActionResult GetKQ(dynamic val)
        {
            int maBKT = (int)val.MaBaiKT;
            int maSV = (int)val.MaSV;

            var listCTKQ = _context.CTKetQuas.Where(u => u.SinhVienMaSV == maSV && u.BaiKiemTraMaBaiKT == maBKT).ToList();
            int tongCH = listCTKQ.Count;

            var queryKQ = _context.KetQuas.FirstOrDefault(u => u.MaBaiKiemTra == maBKT && u.MaSinhVien == maSV && u.TrangThai);

            if(queryKQ == null)
            {
                KetQua ketQua = new KetQua
                {
                    MaSinhVien = maSV,
                    MaBaiKiemTra = maBKT,
                    Diem = 0,
                    SoCauDung = 0,
                    TrangThai = true,
                };

                _context.KetQuas.Add(ketQua);
                _context.SaveChanges();

                return Ok(ketQua);
            }

            return Ok(new { Ketqua = queryKQ, TongCauHoi = tongCH} );
        }
        [HttpGet, Route("getlistdiem/{id?}")]
        public IActionResult XemDiem(int id)
        {
            int id_BKT = (int)id;
            BaiKiemTra baiKiemTra = this._context.BaiKiemTras.Find(id_BKT);
            LopHocPhan lopHocPhan = this._context.LopHocPhans.Find(baiKiemTra.MaLopHocPhan);
            Lop lop = this._context.Lops.Find(lopHocPhan.MaLop);
            MonHoc monHoc = this._context.MonHocs.Find(lopHocPhan.MaMonHoc);
            var list = this._context.KetQuas.Where(u => u.MaBaiKiemTra == id_BKT && u.TrangThai == true).Join(
                this._context.SinhViens, k => k.MaSinhVien, s => s.MaSV, (k, s) => new { k, s }).Select(m =>
                   new SinhVienJoinBaiKiemTra
                   {
                       KetQua = m.k,
                       SinhVien = m.s
                   });
            if(list != null && list.Count() != 0)
            {
                var obj = new
                {
                    list = list,
                    baiKiemTra = baiKiemTra,
                    lop = lop,
                    lopHocPhan = lopHocPhan,
                    monHoc = monHoc,
                };


                return Ok(obj);
            }    
            return BadRequest();
        }

        [HttpPost, Route("GetChiTietKQ")]
        public IActionResult GetChiTietKQ(dynamic val)
        {
            int maBaiKt = (int)val.MaBaiKT;
            int maSV = (int)val.MaSV;
            
            var query = this._context.CTBaiKTs.Where(u => u.MaBaiKT == maBaiKt).Join(this._context.CauHois,
                b => b.CauHoi, c => c.MaCauHoi, (b, c) => new { b, c }).Select(m =>
                 new ChiTietJoinCauHoi
                 {
                     CauHoi = m.c,
                     CTBaiKT = m.b
                 });
            var dsCauTraLoi = _context.CTKetQuas.Where(u => u.BaiKiemTraMaBaiKT == maBaiKt && u.SinhVienMaSV == maSV).Select(u => u.DapAnSVChon);
            return Ok(new { ChiTietBKT = query, DSCauTraLoi = dsCauTraLoi });
            //return Ok(dsCauTraLoi);
        }
    }
}
