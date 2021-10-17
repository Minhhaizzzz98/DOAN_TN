using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin.Data;
using Admin.Models;

namespace Admin.Controllers
{
    public class BaiKiemTrasController : Controller
    {
        private readonly ProjectContext _context;

        public BaiKiemTrasController(ProjectContext context)
        {
            _context = context;
        }

        // GET: BaiKiemTras
        public async Task<IActionResult> Index()
        {
            return View(await _context.BaiKiemTras.ToListAsync());
        }

        // GET: BaiKiemTras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiKiemTra = await _context.BaiKiemTras
                .FirstOrDefaultAsync(m => m.MaBaiKT == id);
            if (baiKiemTra == null)
            {
                return NotFound();
            }

            return View(baiKiemTra);
        }

        // GET: BaiKiemTras/Create
        public IActionResult Create()
        {
            ViewData["GiangVienId"] = new SelectList(_context.GiangViens.ToList(), "MaGiangVien", "TenGiangVien");
            ViewData["LopHPId"] = new SelectList(_context.LopHocPhans.ToList(), "MaLopHP", "TenLopHP");

            return View();
        }

        // POST: BaiKiemTras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBaiKT,TenBaiKT,KeyBaiKT,Ngay,MaGiangVien,MaLopHocPhan,TrangThai,ThoiGianLam")] BaiKiemTra baiKiemTra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baiKiemTra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baiKiemTra);
        }

        // GET: BaiKiemTras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["GiangVienId"] = new SelectList(_context.GiangViens.ToList(), "MaGiangVien", "TenGiangVien");
            ViewData["LopHPId"] = new SelectList(_context.LopHocPhans.ToList(), "MaLopHP", "TenLopHP");

            if (id == null)
            {
                return NotFound();
            }

            var baiKiemTra = await _context.BaiKiemTras.FindAsync(id);
            if (baiKiemTra == null)
            {
                return NotFound();
            }
            return View(baiKiemTra);
        }

        // POST: BaiKiemTras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaBaiKT,TenBaiKT,KeyBaiKT,Ngay,MaGiangVien,MaLopHocPhan,TrangThai,ThoiGianLam")] BaiKiemTra baiKiemTra)
        {
            if (id != baiKiemTra.MaBaiKT)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baiKiemTra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaiKiemTraExists(baiKiemTra.MaBaiKT))
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
            return View(baiKiemTra);
        }

        // GET: BaiKiemTras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiKiemTra = await _context.BaiKiemTras
                .FirstOrDefaultAsync(m => m.MaBaiKT == id);
            if (baiKiemTra == null)
            {
                return NotFound();
            }

            return View(baiKiemTra);
        }

        // POST: BaiKiemTras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baiKiemTra = await _context.BaiKiemTras.FindAsync(id);
            baiKiemTra.TrangThai = false;
            _context.Update(baiKiemTra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiKiemTraExists(int id)
        {
            return _context.BaiKiemTras.Any(e => e.MaBaiKT == id);
        }
    }
}
