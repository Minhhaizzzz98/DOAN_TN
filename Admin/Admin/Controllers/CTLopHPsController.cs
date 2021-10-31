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
    public class CTLopHPsController : Controller
    {
        private readonly ProjectContext _context;

        public CTLopHPsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: CTLopHPs
        public IActionResult Index()
        {
            var data = GetCtLHP();

            return View(data);
        }

        // GET: CTLopHPs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTLopHP = await _context.CTLopHPs
                .FirstOrDefaultAsync(m => m.MaCTLopHP == id);
            if (cTLopHP == null)
            {
                return NotFound();
            }

            return View(cTLopHP);
        }

        // GET: CTLopHPs/Create
        public IActionResult Create()
        {
            SetSelectListLoai();
            return View();
        }
        
        // POST: CTLopHPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCTLopHP,LopHocPhanMaLopHP,SinhVienMaSV,Status")] CTLopHP cTLopHP)
        {
            if (ModelState.IsValid)
            {
                if (!GetLHPSVDaThamGia(cTLopHP.SinhVienMaSV))
                {
                    _context.Add(cTLopHP);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "SV đã tham gia LHP này!");
                SetSelectListLoai();
            }
            return View(cTLopHP);
        }

        // GET: CTLopHPs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SetSelectListLoai();
            
            if (id == null)
            {
                return NotFound();
            }

            var cTLopHP = await _context.CTLopHPs.FindAsync(id);
            var data = GetCtLHP();

            if (cTLopHP == null || data.FirstOrDefault() == null)
            {
                return NotFound();
            }
            var result = data.AsEnumerable().FirstOrDefault(u => u.CTLopHP.MaCTLopHP == id);
            ViewData["TenSV"] = result.SinhVien.TenSV;
            return View(cTLopHP);
        }

        // POST: CTLopHPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaCTLopHP,LopHocPhanMaLopHP,SinhVienMaSV, Status")] CTLopHP cTLopHP)
        {
            if (id != cTLopHP.MaCTLopHP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!GetLHPSVDaThamGia(cTLopHP.SinhVienMaSV))
                {
                    _context.Update(cTLopHP);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "SV đã tham gia LHP này!");
                SetSelectListLoai();
            }
            return View(cTLopHP);
        }

        // GET: CTLopHPs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTLopHP = await _context.CTLopHPs
                .FirstOrDefaultAsync(m => m.MaCTLopHP == id);
            if (cTLopHP == null)
            {
                return NotFound();
            }

            return View(cTLopHP);
        }

        // POST: CTLopHPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cTLopHP = await _context.CTLopHPs.FindAsync(id);
            cTLopHP.Status = false;
            _context.Update(cTLopHP); await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var sinhviens = (from sinhvien in _context.SinhViens
                             where sinhvien.TenSV.Contains(prefix) && sinhvien.TrangThai
                             select new
                             {
                                 label = sinhvien.TenSV,
                                 val = sinhvien.MaSV
                             }).ToList();

            return Json(sinhviens);
        }

        private bool CTLopHPExists(int id)
        {
            return _context.CTLopHPs.Any(e => e.MaCTLopHP == id);
        }

        public IEnumerable<CTLopHocPhanJoin> GetCtLHP()
        {
            List<SinhVien> sinhViens = _context.SinhViens.ToList();
            List<CTLopHP> cTLopHPs = _context.CTLopHPs.ToList();
            List<LopHocPhan> lopHocPhans = _context.LopHocPhans.ToList();

            var data = from ct in cTLopHPs
                       join sv in sinhViens on ct.SinhVienMaSV equals sv.MaSV
                       join lhp in lopHocPhans on ct.LopHocPhanMaLopHP equals lhp.MaLopHP
                       select new CTLopHocPhanJoin
                       {
                           SinhVien = sv,
                           CTLopHP = ct,
                           LopHocPhan = lhp
                       };

            return (data);
        }

        private void SetSelectListLoai()
        {
            ViewData["LopHPId"] = new SelectList(_context.LopHocPhans.Where(u => u.TrangThai == 1).ToList(), "MaLopHP", "TenLopHP");
        }

        private bool GetLHPSVDaThamGia(int maSV)
        {
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

            var lhpTheoCTLHP = _context.CTLopHPs.Where(u => u.SinhVienMaSV == maSV && u.Status)
                .Join(_context.LopHocPhans, ct => ct.LopHocPhanMaLopHP, lhp => lhp.MaLopHP,
                (ct, lhp) => new
                {
                    LopHocPhan = lhp
                }).Where(u => u.LopHocPhan.TrangThai == 1).Distinct();

            var countA = lhpTheoCTLHP.ToList().Count();
            var countB = lhpTheoLop.ToList().Count();
            if (countA == 0)
            {
                if (countB > 0)
                {
                    return true;
                }
            }
            if (countB == 0)
            {
                if (countA > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
