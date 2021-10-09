using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    public class LoaiTK
    {
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
    }
    public class TaiKhoansController : Controller
    {
        private readonly ProjectContext _context;

        public TaiKhoansController(ProjectContext context)
        {
            _context = context;
        }

        // GET: TaiKhoans
        public async Task<IActionResult> Index()
        {
            return View(_context.TaiKhoans.ToList());
        }

        // GET: TaiKhoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikhoan = _context.TaiKhoans
                .FirstOrDefault(m => m.Id == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // GET: Lops/Create
        public IActionResult Create()
        {
            List<LoaiTK> loaiTKs = new List<LoaiTK>();
            LoaiTK loai = new LoaiTK { MaLoai = 1, TenLoai = "Giảng Viên" };
            LoaiTK loai1 = new LoaiTK { MaLoai = 2, TenLoai = "Sinh Viên" };
            loaiTKs.Add(loai);
            loaiTKs.Add(loai1);

            ViewData["LoaiTK"] = new SelectList(loaiTKs, "MaLoai", "TenLoai");

            return View();
        }

        // POST: Lops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaiKhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                var tenTK = _context.TaiKhoans.FirstOrDefault(u => u.UserName == taikhoan.UserName);
                if (tenTK == null)
                {
                    _context.Add(taikhoan);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Username đã tồn tại");
            }
            return View(taikhoan);
        }

        // GET: Lops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<LoaiTK> loaiTKs = new List<LoaiTK>();
            LoaiTK loai = new LoaiTK { MaLoai = 1, TenLoai = "Giảng Viên" };
            LoaiTK loai1 = new LoaiTK { MaLoai = 2, TenLoai = "Sinh Viên" };
            loaiTKs.Add(loai);
            loaiTKs.Add(loai1);

            ViewData["LoaiTK"] = new SelectList(loaiTKs, "MaLoai", "TenLoai");

            if (id == null)
            {
                return NotFound();
            }

            var lop = await _context.TaiKhoans.FindAsync(id);
            if (lop == null)
            {
                return NotFound();
            }
            return View(lop);
        }

        // POST: Lops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!LopExists(taiKhoan.Id))
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
            return View(taiKhoan);
        }

        // GET: Lops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = _context.TaiKhoans
                .FirstOrDefault(m => m.Id == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: Lops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lop = await _context.TaiKhoans.FindAsync(id);
            _context.TaiKhoans.Remove(lop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LopExists(int id)
        {
            return _context.TaiKhoans.Any(e => e.Id == id);
        }

    }

}
