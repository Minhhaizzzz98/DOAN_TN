using Admin.Data;
using Admin.Helpers;
using Admin.ModelJoin;
using Admin.Models;
using Admin.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Admin.ApiControllers
{
    [Route("api/baikiemtra")]
    [ApiController]
    public class BaiKiemTraApiController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IUriService _uriService;

        public BaiKiemTraApiController(ProjectContext context, IUriService uriService)
        {
            this._context = context;
            _uriService = uriService;
        }
        [HttpPost, Route("add")]
        public async Task<ActionResult<BaiKiemTra>> Add(dynamic baiKiemTra)
        {
            BaiKiemTra _baiKiemTra = new BaiKiemTra();
            _baiKiemTra.TenBaiKT = baiKiemTra.TenBaiKT;
            _baiKiemTra.KeyBaiKT = baiKiemTra.KeyBaiKT;
            _baiKiemTra.Ngay = baiKiemTra.Ngay;
            _baiKiemTra.ThoiGianLam = (int)baiKiemTra.ThoiGianLam;
            _baiKiemTra.MaGiangVien = baiKiemTra.MaGiangVien;
            _baiKiemTra.MaLopHocPhan = (int)baiKiemTra.MaLopHocPhan;
            _baiKiemTra.ThoiGianBatDau = baiKiemTra.ThoiGianBatDau;
            _baiKiemTra.TrangThai = true;
            _baiKiemTra.IsEnd = false;
            _baiKiemTra.TrangThaiBatDau = false;
            this._context.BaiKiemTras.Add(_baiKiemTra);
            this._context.SaveChanges();
            var lopHocPhan = this._context.LopHocPhans.Where(u => u.MaLopHP == _baiKiemTra.MaLopHocPhan).FirstOrDefault();
            return Ok(new BaiKiemTraJoinLopHocPhan
            {
                BaiKiemTra = _baiKiemTra,
                LopHocPhan = lopHocPhan
            });
        }
        [HttpPost, Route("index")]
        public async Task<IActionResult> Index(dynamic val)
        {
              int id = (int)val.MaGiangVien;
            int id_lop = (int)val.MaLopHocPhan;
            var qurey = this._context.BaiKiemTras.Where(u => u.MaGiangVien == id && u.TrangThai == true && u.MaLopHocPhan == id_lop)
                .Join(_context.LopHocPhans, b => b.MaLopHocPhan, l => l.MaLopHP, (b, l) => new
                {
                    b,
                    l
                }).Select(m =>
                new BaiKiemTraJoinLopHocPhan
                {
                    BaiKiemTra = m.b,
                    LopHocPhan = m.l
                }).OrderByDescending(u =>u.BaiKiemTra.MaBaiKT);
            var lopHocPhan = this._context.LopHocPhans.Find(id_lop);

            var item = this._context.Lops.Where(u => u.MaLop == lopHocPhan.MaLop).FirstOrDefault();
            var obj = new
            {
                list = qurey.ToArray(),
                lop = item,
                lopHocPhan = lopHocPhan

            };
            return Ok(obj);

        }
        [HttpGet, Route("remove/{id?}")]
        public async Task<IActionResult> Remove(int? id)
        {
            var baiKiemTra = this._context.BaiKiemTras.Find(id);
            baiKiemTra.TrangThai = false;
            _context.SaveChanges();
            return Ok(baiKiemTra);
        }

        [HttpPost, Route("GetTenLopTheoIdLop")]
        public async Task<IActionResult> GetTenLopTheoIdLop(dynamic val)
        {
            var route = Request.Path.Value;

            int maLHP = (int)val.MaLHP;
            int pageNumber = (int)val.PageNumber;
            int pageSize = (int)val.PageSize;

            var validFilter = new PaginationFilter(pageNumber, pageSize);

            var query = this._context.LopHocPhans.Where(u => u.MaLopHP == maLHP).Join(this._context.Lops,
                b => b.MaLop, c => c.MaLop, (b, c) => new { b, c }).Select(m =>
                 new LopHocPhanJoinGiangVien
                 {
                     Lop = m.c,
                     LopHocPhan = m.b
                 })
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize).ToList();

            var totalRecords = await _context.BaiKiemTras.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<LopHocPhanJoinGiangVien>(query, validFilter, totalRecords, _uriService, route);

            return Ok(pagedReponse);
            //return Ok(query.ToArray());
        }

        [HttpGet, Route("GetAllBKT")]
        public async Task<IActionResult> GetAllBKT([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.BaiKiemTras
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.BaiKiemTras.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<BaiKiemTra>(pagedData, validFilter, totalRecords, _uriService, route);

            return Ok(pagedReponse);
        }
        [HttpPost, Route("nhapkeybaikt")]
        public async Task<IActionResult> InputKeyBaiKiemTra(dynamic val)
        {

            string key = (string)val.KeyBaiKT;
            var baiKTra = this._context.BaiKiemTras.Where(u => u.KeyBaiKT == key).FirstOrDefault();
            if(baiKTra != null)
            {
                var MaSV = (int)val.MaSV;
                var ketQua = this._context.KetQuas.Where(u => u.MaSinhVien == MaSV && u.MaBaiKiemTra == baiKTra.MaBaiKT).FirstOrDefault();
                if (ketQua == null)
                {   
                    if(baiKTra.TrangThaiBatDau == false)
                    {
                        return BadRequest("Giáo viên chưa bắt đầu bài kiểm tra");
                    }    
                    if (baiKTra != null)
                    {
                        var query = this._context.CTBaiKTs.Where(u => u.MaBaiKT == baiKTra.MaBaiKT);
                        var list = query.OrderBy(n => Guid.NewGuid()).ToArray();
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
                        var listCT = this._context.CTKetQuas.Where(u => u.BaiKiemTraMaBaiKT == baiKTra.MaBaiKT).Include(u => u.CauHoi).Include(u => u.BaiKiemTra)
                            .Include(u => u.SinhVien).ToArray();
                        return Ok(listCT);
                    }
                }

                return BadRequest("Bạn đã làm bài kiểm tra!");
            }
            return BadRequest("Key bài kiểm tra không hợp lệ!");

        }

        [HttpPost, Route("editBKT")]
        public async Task<ActionResult<BaiKiemTra>> Edit(BaiKiemTra baiKiemTra)
        {
            //BaiKiemTra _baiKiemTra = new BaiKiemTra();

            //_baiKiemTra.MaBaiKT = id;
            //_baiKiemTra.TenBaiKT = baiKiemTra.TenBaiKT;
            //_baiKiemTra.KeyBaiKT = baiKiemTra.KeyBaiKT;
            //_baiKiemTra.Ngay = baiKiemTra.Ngay;
            //_baiKiemTra.ThoiGianLam = baiKiemTra.ThoiGianLam;
            //_baiKiemTra.MaGiangVien = baiKiemTra.MaGiangVien;
            //_baiKiemTra.MaLopHocPhan = baiKiemTra.MaLopHocPhan;
            //_baiKiemTra.ThoiGianBatDau = baiKiemTra.ThoiGianBatDau;
            //_baiKiemTra.TrangThai = true;
            baiKiemTra.TrangThai = true;
            this._context.BaiKiemTras.Update(baiKiemTra);
            this._context.SaveChanges();
            var lopHocPhan = this._context.LopHocPhans.Where(u => u.MaLopHP == baiKiemTra.MaLopHocPhan).FirstOrDefault();
            return Ok(new BaiKiemTraJoinLopHocPhan
            {
                BaiKiemTra = baiKiemTra,
                LopHocPhan = lopHocPhan
            });
        }

        [HttpGet, Route("GetKey")]
        public IActionResult GetKey()
        {
            var data = _context.BaiKiemTras.Select(u => u.KeyBaiKT).ToList();

            return Ok(data);
        }
        [HttpPost, Route("GetBKTSapToi")]
        public IActionResult GetBKTSapToi(dynamic val)
        {
            int maLHP = (int)val.MaLHP;
            int maSV = (int)val.MaSV;

            List<BaiKiemTra> baiKiemTras = _context.BaiKiemTras.ToList();

            var data = from ch in baiKiemTras
                       where ch.MaLopHocPhan == maLHP &&
                       ch.TrangThai == true &&
                       ch.ThoiGianBatDau != null &&
                       ch.TrangThaiBatDau == false &&
                       ch.IsEnd == false
                       //((ch.Ngay == DateTime.Now.Date &&
                       //TimeSpan.Parse(ch.ThoiGianBatDau) >= DateTime.Now.TimeOfDay ||
                       //ch.Ngay > DateTime.Now.Date))
                       select new BaiKiemTra
                       {
                           MaBaiKT = ch.MaBaiKT,
                           KeyBaiKT =ch.KeyBaiKT,
                           MaLopHocPhan = ch.MaLopHocPhan,
                           Ngay = ch.Ngay,
                           ThoiGianBatDau = ch.ThoiGianBatDau,
                           MaGiangVien = ch.MaGiangVien,
                           TenBaiKT = ch.TenBaiKT,
                           ThoiGianLam = ch.ThoiGianLam,
                           TrangThai = ch.TrangThai,
                           TrangThaiBatDau = ch.TrangThaiBatDau,
                           IsEnd = ch.IsEnd,
                       };

            var dataBKTChuaEnd = from ch in baiKiemTras
                                 where ch.MaLopHocPhan == maLHP &&
                                 ch.TrangThai == true &&
                                 ch.TrangThaiBatDau == true &&
                                 ch.ThoiGianBatDau != null &&
                                 ch.IsEnd == false
                                 //((ch.Ngay == DateTime.Now.Date &&
                                 //DateTime.Now.TimeOfDay - TimeSpan.Parse(ch.ThoiGianBatDau) < TimeSpan.FromMinutes(ch.ThoiGianLam)))
                                 //ch.Ngay < DateTime.Now.Date))
            select new BaiKiemTra
                                 {
                                     MaBaiKT = ch.MaBaiKT,
                                     KeyBaiKT = ch.KeyBaiKT,
                                     MaLopHocPhan = ch.MaLopHocPhan,
                                     Ngay = ch.Ngay,
                                     ThoiGianBatDau = ch.ThoiGianBatDau,
                                     MaGiangVien = ch.MaGiangVien,
                                     TenBaiKT = ch.TenBaiKT,
                                     ThoiGianLam = ch.ThoiGianLam,
                                     TrangThai = ch.TrangThai,
                                     TrangThaiBatDau = ch.TrangThaiBatDau,
                                     IsEnd = ch.IsEnd,
                                 };
            var query = dataBKTChuaEnd.ToList().Where(p => !(_context.KetQuas.Any(u => u.MaBaiKiemTra == p.MaBaiKT && u.MaSinhVien == maSV)));
            var result = query.Union(data).Distinct();

            return Ok(result.ToList());
        }
        [HttpPost, Route("GetBKTDaHoanThanh")]
        public IActionResult GetBKTDaHoanThanh(dynamic val)
        {
            int maLHP = (int)val.MaLHP;
            int maSV = (int)val.MaSV;

            List<BaiKiemTra> baiKiemTras = _context.BaiKiemTras.ToList();

            var data = from ch in baiKiemTras
                       where ch.MaLopHocPhan == maLHP &&
                       ch.TrangThai == true &&
                       //ch.TrangThaiBatDau == true &&
                       ch.ThoiGianBatDau != null &&
                       ch.IsEnd == true

                       // note
                       //((ch.Ngay == DateTime.Now.Date &&
                       //DateTime.Now.TimeOfDay - TimeSpan.Parse(ch.ThoiGianBatDau) >= TimeSpan.FromMinutes(ch.ThoiGianLam) ||
                       //ch.Ngay < DateTime.Now.Date))
                       select new BaiKiemTra
                       {
                           MaBaiKT = ch.MaBaiKT,
                           KeyBaiKT = ch.KeyBaiKT,
                           MaLopHocPhan = ch.MaLopHocPhan,
                           Ngay = ch.Ngay,
                           ThoiGianBatDau = ch.ThoiGianBatDau,
                           MaGiangVien = ch.MaGiangVien,
                           TenBaiKT = ch.TenBaiKT,
                           ThoiGianLam = ch.ThoiGianLam,
                           TrangThai = ch.TrangThai,
                           TrangThaiBatDau = ch.TrangThaiBatDau,
                           IsEnd = ch.IsEnd,
                       };

            var dataBKTChuaEnd = from ch in baiKiemTras
                                 where ch.MaLopHocPhan == maLHP &&
                                 ch.TrangThai == true &&
                                 ch.TrangThaiBatDau == true &&
                                 ch.ThoiGianBatDau != null &&
                                 ch.IsEnd == false
                                 //((ch.Ngay == DateTime.Now.Date &&
                                 //DateTime.Now.TimeOfDay - TimeSpan.Parse(ch.ThoiGianBatDau) < TimeSpan.FromMinutes(ch.ThoiGianLam)))
                                 //ch.Ngay < DateTime.Now.Date))
                                 select new BaiKiemTra
                                 {
                                     MaBaiKT = ch.MaBaiKT,
                                     KeyBaiKT = ch.KeyBaiKT,
                                     MaLopHocPhan = ch.MaLopHocPhan,
                                     Ngay = ch.Ngay,
                                     ThoiGianBatDau = ch.ThoiGianBatDau,
                                     MaGiangVien = ch.MaGiangVien,
                                     TenBaiKT = ch.TenBaiKT,
                                     ThoiGianLam = ch.ThoiGianLam,
                                     TrangThai = ch.TrangThai,
                                     TrangThaiBatDau = ch.TrangThaiBatDau,
                                     IsEnd = ch.IsEnd,
                                 };
            var query = dataBKTChuaEnd.ToList().Where(p => (_context.KetQuas.Any(u => u.MaBaiKiemTra == p.MaBaiKT && u.MaSinhVien == maSV)));
            var result = query.Union(data).Distinct();
            return Ok(result.ToList());
        }
        [HttpPost,Route("capnhattime/{id?}")]
        public IActionResult CapNhatTime(int id,dynamic val)
        {
            string time = (string)val.time;
            string date = (string)val.date;
            BaiKiemTra baiKiemTra = this._context.BaiKiemTras.Find(id);
            baiKiemTra.ThoiGianBatDau = time;
            baiKiemTra.TrangThaiBatDau = true;
            baiKiemTra.Ngay = DateTime.Parse(date);
            this._context.BaiKiemTras.Update(baiKiemTra);
            this._context.SaveChanges();
            var lopHocPhan = this._context.LopHocPhans.Where(u => u.MaLopHP == baiKiemTra.MaLopHocPhan).FirstOrDefault();
            return Ok(new BaiKiemTraJoinLopHocPhan
            {
                BaiKiemTra = baiKiemTra,
                LopHocPhan = lopHocPhan
            });
        }
        [HttpPost,Route("congtime/{id?}")]
        public IActionResult CongTime(int id, dynamic val)
        {
            int thoiGian = (int)val.thoiGian;
            var baiKiemTra = this._context.BaiKiemTras.Find(id);

            baiKiemTra.ThoiGianLam =  baiKiemTra.ThoiGianLam + thoiGian;
            this._context.BaiKiemTras.Update(baiKiemTra);
            this._context.SaveChanges();
            var lopHocPhan = this._context.LopHocPhans.Where(u => u.MaLopHP == baiKiemTra.MaLopHocPhan).FirstOrDefault();
            return Ok(new BaiKiemTraJoinLopHocPhan
            {
                BaiKiemTra = baiKiemTra,
                LopHocPhan = lopHocPhan
            });
        }
        [HttpGet,Route("ketthuc/{id?}")]
        public IActionResult KetThuc(int id)
        {
            int _id = id;
            var baiKiemTra = this._context.BaiKiemTras.Find(_id);
            baiKiemTra.IsEnd = true;
            this._context.BaiKiemTras.Update(baiKiemTra);
            this._context.SaveChanges();
            var lopHocPhan = this._context.LopHocPhans.Where(u => u.MaLopHP == baiKiemTra.MaLopHocPhan).FirstOrDefault();
            return Ok(new BaiKiemTraJoinLopHocPhan
            {
                BaiKiemTra = baiKiemTra,
                LopHocPhan = lopHocPhan
            });
        }

        [HttpPost, Route("ThongBaoBKTHomNay")]
        public IActionResult ThongBaoBKTHomNay(dynamic val)
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
                }).Where(u => u.LopHocPhan.TrangThai == 1).Distinct();

            var lhpTheoCTLHP = _context.CTLopHPs.Where(u => u.MaSinhVien == maSV && u.Status)
                .Join(_context.LopHocPhans, ct => ct.MaLopHocPhan, lhp => lhp.MaLopHP,
                (ct, lhp) => new
                {
                    LopHocPhan = lhp
                }).Where(u => u.LopHocPhan.TrangThai == 1).Distinct();

            var query = lhpTheoLop.Union(lhpTheoCTLHP);


            List<BaiKiemTra> baiKiemTras = _context.BaiKiemTras.ToList();
            List<LopHocPhan> lopHocPhans = _context.LopHocPhans.ToList();

            var data = from ch in baiKiemTras
                       where query.Any(u => u.LopHocPhan.MaLopHP == ch.MaLopHocPhan) &&
                       ch.TrangThai == true &&
                       ch.ThoiGianBatDau != null &&
                       ch.TrangThaiBatDau == false &&
                       ch.IsEnd == false &&
                       (ch.Ngay == DateTime.Now.Date &&
                       TimeSpan.Parse(ch.ThoiGianBatDau) >= DateTime.Now.TimeOfDay)
                       join lhp in lopHocPhans on ch.MaLopHocPhan equals lhp.MaLopHP
                       select new BaiKiemTraJoinLopHocPhan
                       {
                           //MaBaiKT = ch.MaBaiKT,
                           //KeyBaiKT = ch.KeyBaiKT,
                           //MaLopHocPhan = ch.MaLopHocPhan,
                           //Ngay = ch.Ngay,
                           //ThoiGianBatDau = ch.ThoiGianBatDau,
                           //MaGiangVien = ch.MaGiangVien,
                           //TenBaiKT = ch.TenBaiKT,
                           //ThoiGianLam = ch.ThoiGianLam,
                           //TrangThai = ch.TrangThai,
                           //TrangThaiBatDau = ch.TrangThaiBatDau,
                           //IsEnd = ch.IsEnd,
                           BaiKiemTra = ch,
                           LopHocPhan = lhp,
                       };

            return Ok(data);
        }

        [HttpPost, Route("ThongBaoBKTGV")]
        public IActionResult ThongBaoBKTGV(dynamic val)
        {
            int maGV = (int)val.MaGV;

            var bktCuaGVHomNay = _context.LopHocPhans.Where(u => u.MaGiangVien == maGV && u.TrangThai == 1)
                .Join(_context.BaiKiemTras, lhp => lhp.MaLopHP, bkt => bkt.MaLopHocPhan,
                (lhp, bkt) => new
                {
                    BaiKiemTra = bkt,
                    LopHocPhan = lhp
                })
                .AsEnumerable()
                .Where(u => u.BaiKiemTra.TrangThai && u.BaiKiemTra.ThoiGianBatDau != null &&
                       u.BaiKiemTra.TrangThaiBatDau == false &&
                       u.BaiKiemTra.IsEnd == false &&
                       (u.BaiKiemTra.Ngay == DateTime.Now.Date &&
                       TimeSpan.Parse(u.BaiKiemTra.ThoiGianBatDau) >= DateTime.Now.TimeOfDay))
                .ToList();

            return Ok(bktCuaGVHomNay);
        }
    }
}
