using Admin.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace Admin.ApiControllers
{
    [Route("api/ctkq")]
    [ApiController]
    public class CTKetQuaApiController : ControllerBase
    {
        private readonly ProjectContext _context;
        public CTKetQuaApiController(ProjectContext context)
        {
            this._context = context;
        }
        [HttpPost, Route("lambaikt")]
        public async Task<IActionResult> LamBaiKiemTra(dynamic val)
        {
            string key = (string)val.KeyBaiKT;
            var baiKTra = this._context.BaiKiemTras.Where(u => u.KeyBaiKT == key).FirstOrDefault();
            if(baiKTra != null)
            {
                var MaSV = (int)val.MaSV;
                var ketQua = this._context.KetQuas.Where(u => u.MaSinhVien == MaSV && u.MaBaiKiemTra == baiKTra.MaBaiKT).FirstOrDefault();
                if (ketQua == null)
                {
                    var query = this._context.CTBaiKTs.Where(u => u.MaBaiKT == baiKTra.MaBaiKT);
                    var list = query.OrderBy(n => Guid.NewGuid()).ToArray();
                    if(list == null || list.Count()==0)
                    {
                        return BadRequest("Bài kiểm tra chưa có câu hỏi!");
                    }
                    foreach (var item in list)
                    {
                        var temp = this._context.CTKetQuas.Where(u => u.CauHoiMaCauHoi == item.CauHoi
                          && u.SinhVienMaSV == MaSV && u.BaiKiemTraMaBaiKT == baiKTra.MaBaiKT).FirstOrDefault();
                        if (temp == null)
                        {
                            var tempCT = new CTKetQua();
                            tempCT.CauHoiMaCauHoi = item.CauHoi;
                            tempCT.SinhVienMaSV = MaSV;
                            tempCT.BaiKiemTraMaBaiKT = baiKTra.MaBaiKT;
                            tempCT.DapAnSVChon = null;
                            this._context.CTKetQuas.Add(tempCT);
                            this._context.SaveChanges();
                        }
                    }
                        var listCT = this._context.CTKetQuas.Where(u => u.BaiKiemTraMaBaiKT == baiKTra.MaBaiKT && u.SinhVienMaSV == MaSV).Include(u => u.CauHoi)
                        .ToArray();
                        return Ok(listCT);
                    
                }
                return BadRequest("Bạn đã làm bài kiểm tra!");
            }
            return BadRequest("Key bài kiểm tra không hợp lệ!");
        }
        [HttpPost, Route("updatelambaikt")]
        public async Task<IActionResult> UpdateCauHoi(dynamic val)
        {
            int MaCT = (int)val.MaCTKetQua;
            string CauHoi = val.DapAn;
            CTKetQua cTKetQua = this._context.CTKetQuas.Where(u => u.MaCTKetQua == MaCT).FirstOrDefault();
            cTKetQua.DapAnSVChon = CauHoi;
            if (cTKetQua != null)
            {
                this._context.CTKetQuas.Update(cTKetQua);
                this._context.SaveChanges();
                return Ok(cTKetQua);
            }    
            return BadRequest();
        }
    }
}
