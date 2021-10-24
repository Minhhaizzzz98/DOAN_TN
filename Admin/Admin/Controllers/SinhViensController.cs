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
    public class SinhViensController : Controller
    {
        private readonly ProjectContext _context;

        public SinhViensController(ProjectContext context)
        {
            _context = context;
        }

        // GET: SinhViens
        public async Task<IActionResult> Index()
        {
            var data = GetSinhVienJoins();

            return View(data);
        }

        // GET: SinhViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            List<TaiKhoan> taiKhoans = _context.TaiKhoans.ToList();
            List<SinhVien> sinhViens = _context.SinhViens.ToList();
            List<Lop> lops = _context.Lops.ToList();

            var data = GetSinhVienJoins();

            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = data
                .FirstOrDefault(m => m.SinhVien.MaSV == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // GET: SinhViens/Create
        public IActionResult Create()
        {
            var tkSV = _context.TaiKhoans.Where(u => u.LoaiTaiKhoan == 2).ToList();
            var existedTKSV = _context.TaiKhoans
                .Join(
                _context.SinhViens,
                tk => tk.Id,
                sv => sv.MaTaiKhoan,
                (tk, sv) => new
                {
                    tkId = tk.Id,
                });

            ViewData["LopId"] = new SelectList(_context.Lops.Where(u => u.TrangThai).ToList(), "MaLop", "TenLop");
            ViewData["TaiKhoanId"] = new SelectList(tkSV.Where(u => !existedTKSV.Any(item => item.tkId == u.Id)), "Id", "UserName");

            return View();
        }

        // POST: SinhViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSV,TenSV,DiaChi,SoDienThoai,Email,TrangThai,Type,Lop,MaTaiKhoan")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sinhVien);
        }

        // GET: SinhViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["LopId"] = new SelectList(_context.Lops.Where(u => u.TrangThai).ToList(), "MaLop", "TenLop");

            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens.FindAsync(id);
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans.Where(u => u.Id == sinhVien.MaTaiKhoan), "Id", "UserName");

            if (sinhVien == null)
            {
                return NotFound();
            }
            return View(sinhVien);
        }

        // POST: SinhViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSV,TenSV,DiaChi,SoDienThoai,Email,TrangThai,Type,Lop,MaTaiKhoan")] SinhVien sinhVien)
        {
            if (id != sinhVien.MaSV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinhVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhVienExists(sinhVien.MaSV))
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
            return View(sinhVien);
        }

        // GET: SinhViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens
                .FirstOrDefaultAsync(m => m.MaSV == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // POST: SinhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);
            sinhVien.TrangThai = false;
            _context.Update(sinhVien); await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string inputKeyword)
        {
            var data = GetSinhVienJoins();

            var sinhviens = data.Where(x => x.SinhVien.MaSV.ToString().Contains(inputKeyword) ||
                                         x.SinhVien.TenSV.Contains(inputKeyword) ||
                                         x.SinhVien.Email.Contains(inputKeyword) ||
                                         x.SinhVien.DiaChi.Contains(inputKeyword) ||
                                         x.SinhVien.SoDienThoai.Contains(inputKeyword) ||
                                         x.Lop.TenLop.Contains(inputKeyword) ||
                                         x.TaiKhoan.UserName.Contains(inputKeyword));

            if (inputKeyword == null)
            {
                return View(data);
            }

            return View(sinhviens);
        }

        private bool SinhVienExists(int id)
        {
            return _context.SinhViens.Any(e => e.MaSV == id);
        }

        private IEnumerable<SinhVienJoin> GetSinhVienJoins()
        {
            List<TaiKhoan> taiKhoans = _context.TaiKhoans.ToList();
            List<SinhVien> sinhViens = _context.SinhViens.ToList();
            List<Lop> lops = _context.Lops.ToList();

            var data = from sv in sinhViens
                       join tk in taiKhoans on sv.MaTaiKhoan equals tk.Id
                       join l in lops on sv.Lop equals l.MaLop
                       select new SinhVienJoin
                       {
                           SinhVien = sv,
                           TaiKhoan = tk,
                           Lop = l
                       };

            return data;
        }
    }
}
