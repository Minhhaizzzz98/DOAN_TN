using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin.Data;
using Admin.Models;
using Admin.ModelJoin;

namespace Admin.Controllers
{
    public class CTKetQuasController : Controller
    {
        private readonly ProjectContext _context;

        public CTKetQuasController(ProjectContext context)
        {
            _context = context;
        }

        // GET: CTKetQuas
        public IActionResult Index()
        {
            var data = _context.CTKetQuas
                .Include(y => y.SinhVien)
                .Include(y => y.CauHoi)
                .Include(y => y.BaiKiemTra)
                .ToList();
 
            return View(data);
        }

        // GET: CTKetQuas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTKetQua = await _context.CTKetQuas
                .FirstOrDefaultAsync(m => m.MaCTKetQua == id);
            if (cTKetQua == null)
            {
                return NotFound();
            }

            return View(cTKetQua);
        }

        // GET: CTKetQuas/Create
        public IActionResult Create()
        {
            ViewData["CauHoiId"] = new SelectList(_context.CauHois.ToList(), "MaCauHoi", "TenCauHoi");
            ViewData["BaiKiemTraId"] = new SelectList(_context.BaiKiemTras.ToList(), "MaBaiKT", "TenBaiKT");
            ViewData["SinhVienId"] = new SelectList(_context.SinhViens.ToList(), "MaSV", "TenSV");

            return View();
        }

        // POST: CTKetQuas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCTKetQua,MaKetQua,SinhVienMaSV,BaiKiemTraMaBaiKT,CauHoiMaCauHoi,DapAnSVChon")] CTKetQua cTKetQua)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cTKetQua);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cTKetQua);
        }

        // GET: CTKetQuas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["CauHoiId"] = new SelectList(_context.CauHois.ToList(), "MaCauHoi", "TenCauHoi");
            ViewData["BaiKiemTraId"] = new SelectList(_context.BaiKiemTras.ToList(), "MaBaiKT", "TenBaiKT");
            ViewData["SinhVienId"] = new SelectList(_context.SinhViens.ToList(), "MaSV", "TenSV");

            if (id == null)
            {
                return NotFound();
            }

            var cTKetQua = await _context.CTKetQuas.FindAsync(id);
            if (cTKetQua == null)
            {
                return NotFound();
            }
            return View(cTKetQua);
        }

        // POST: CTKetQuas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaCTKetQua,MaKetQua,SinhVienMaSV,BaiKiemTraMaBaiKT,CauHoiMaCauHoi,DapAnSVChon")] CTKetQua cTKetQua)
        {
            if (id != cTKetQua.MaCTKetQua)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //cTKetQua.SinhVienMaSV = cTKetQua.SinhVien.MaSV;

                    _context.Update(cTKetQua);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CTKetQuaExists(cTKetQua.MaCTKetQua))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cTKetQua);
        }

        // GET: CTKetQuas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTKetQua = await _context.CTKetQuas
                .FirstOrDefaultAsync(m => m.MaCTKetQua == id);
            if (cTKetQua == null)
            {
                return NotFound();
            }

            return View(cTKetQua);
        }

        // POST: CTKetQuas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cTKetQua = await _context.CTKetQuas.FindAsync(id);
            _context.CTKetQuas.Remove(cTKetQua);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CTKetQuaExists(int id)
        {
            return _context.CTKetQuas.Any(e => e.MaCTKetQua == id);
        }

        //private IEnumerable<CTKQJoin> GetCTKQsJoin()
        //{
        //    List<CauHoi> cauHoi = _context.CauHois.ToList();
        //    List<CTKetQua> ctKQ = _context.CTKetQuas.ToList();
        //    List<BaiKiemTra> bKT = _context.BaiKiemTras.ToList();
        //    List<SinhVien> sinhVien = _context.SinhViens.ToList();

        //    //var data = from ch in cauHoi
        //    //           join cd in ctKQ on ch.MaCauHoi equals cd.CauHoi
        //    //           join bkt in bKT on cd.BaiKiemTra equals bkt.MaBaiKT
        //    //           join sv in sinhVien on cd.SinhVien equals sv.MaSV
        //    //           select new CTKQJoin
        //    //           {
        //    //               CauHoi = ch,
        //    //               CTKQ = cd,
        //    //               BaiKT = bkt,
        //    //               SinhVien = sv
        //    //           };

        //    return data;
        //}
    }
}
