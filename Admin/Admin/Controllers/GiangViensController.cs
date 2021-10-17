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
    public class GiangViensController : Controller
    {
        private readonly ProjectContext _context;

        public GiangViensController(ProjectContext context)
        {
            _context = context;
        }

        // GET: GiangViens
        public async Task<IActionResult> Index()
        {
            List<TaiKhoan> taiKhoans = _context.TaiKhoans.ToList();
            List<GiangVien> giangViens = _context.GiangViens.ToList();

            var data = from gv in giangViens
                       join tk in taiKhoans on gv.MaTaiKhoan equals tk.Id
                       select new GiangVienJoin
                       {
                           GiangVien = gv,
                           TaiKhoan = tk,
                       };
            return View(data);
        }

        // GET: GiangViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            List<TaiKhoan> taiKhoans = _context.TaiKhoans.ToList();
            List<GiangVien> giangViens = _context.GiangViens.ToList();

            var data = from gv in giangViens
                       join tk in taiKhoans on gv.MaTaiKhoan equals tk.Id
                       select new GiangVienJoin
                       {
                           GiangVien = gv,
                           TaiKhoan = tk,
                       };

            if (id == null)
            {
                return NotFound();
            }

            var giangVien = data
                .FirstOrDefault(m => m.GiangVien.MaGiangVien == id);
            if (giangVien == null)
            {
                return NotFound();
            }

            return View(giangVien);
        }

        // GET: GiangViens/Create
        public IActionResult Create()
        {
            var tkGV = _context.TaiKhoans.Where(u => u.LoaiTaiKhoan == 1).ToList();
            var existedTKGV = _context.TaiKhoans
                .Join(
                _context.GiangViens,
                tk => tk.Id,
                sv => sv.MaTaiKhoan,
                (tk, sv) => new
                {
                    tkId = tk.Id,
                });

            ViewData["TaiKhoanId"] = new SelectList(tkGV.Where(u => !existedTKGV.Any(item => item.tkId == u.Id)), "Id", "UserName");

            return View();
        }

        // POST: GiangViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaGiangVien,TenGiangVien,DiaChi,SoDienThoai,Email,Password,IsAdmin,TrangThai,MaTaiKhoan")] GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giangVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(giangVien);
        }

        // GET: GiangViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangVien = await _context.GiangViens.FindAsync(id);

            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans.Where(u => u.Id == giangVien.MaTaiKhoan), "Id", "UserName");

            if (giangVien == null)
            {
                return NotFound();
            }
            return View(giangVien);
        }

        // POST: GiangViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaGiangVien,TenGiangVien,DiaChi,SoDienThoai,Email,Password,IsAdmin,TrangThai,MaTaiKhoan")] GiangVien giangVien)
        {
            if (id != giangVien.MaGiangVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giangVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiangVienExists(giangVien.MaGiangVien))
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
            return View(giangVien);
        }

        // GET: GiangViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangVien = await _context.GiangViens
                .FirstOrDefaultAsync(m => m.MaGiangVien == id);
            if (giangVien == null)
            {
                return NotFound();
            }

            return View(giangVien);
        }

        // POST: GiangViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giangVien = await _context.GiangViens.FindAsync(id);
            giangVien.TrangThai = false;
            _context.Update(giangVien); await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiangVienExists(int id)
        {
            return _context.GiangViens.Any(e => e.MaGiangVien == id);
        }
    }
}
