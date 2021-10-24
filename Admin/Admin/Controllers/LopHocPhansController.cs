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
    public class LopHocPhansController : Controller
    {
        private readonly ProjectContext _context;

        public LopHocPhansController(ProjectContext context)
        {
            _context = context;
        }

        // GET: LopHocPhans
        public ActionResult Index()
        {
            var data = GetListLopHPJoin();

            return View(data);
        }

        // GET: LopHocPhans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var data = GetListLopHPJoin();
            var lopHP = data.FirstOrDefault(x => x.LopHocPhan.MaLopHP == id);
            
            if (id == null)
            {
                return NotFound();
            }

            var lopHocPhan = await _context.LopHocPhans.FirstOrDefaultAsync(m => m.MaLopHP == id);

            if (lopHocPhan == null)
            {
                return NotFound();
            }

            return View(lopHP);
        }

        // GET: LopHocPhans/Create
        public IActionResult Create()
        {
            ViewData["LopId"] = new SelectList(_context.Lops.Where(u => u.TrangThai ==true ).ToList(), "MaLop", "TenLop");
            ViewData["MonHocId"] = new SelectList(_context.MonHocs.Where(u => u.TrangThai == true ).ToList(), "MaMonHoc", "TenMonHoc");
            ViewData["GiangVienId"] = new SelectList(_context.GiangViens.Where(u => u.TrangThai ==true).ToList(), "MaGiangVien", "TenGiangVien");

            return View();
        }

        // POST: LopHocPhans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLopHP,TenLopHP,MaGiangVien,MaMonHoc,MaLop")] LopHocPhan lopHocPhan)
        {
            if (ModelState.IsValid)
            {
                lopHocPhan.TrangThai = 1;
                _context.Add(lopHocPhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lopHocPhan);
        }

        // GET: LopHocPhans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["LopId"] = new SelectList(_context.Lops.ToList(), "MaLop", "TenLop");
            ViewData["MonHocId"] = new SelectList(_context.MonHocs.ToList(), "MaMonHoc", "TenMonHoc");
            ViewData["GiangVienId"] = new SelectList(_context.GiangViens.ToList(), "MaGiangVien", "TenGiangVien");

            if (id == null)
            {
                return NotFound();
            }

            var lopHocPhan = await _context.LopHocPhans.FindAsync(id);
            if (lopHocPhan == null)
            {
                return NotFound();
            }
            return View(lopHocPhan);
        }

        // POST: LopHocPhans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLopHP,TenLopHP,MaGiangVien,MaMonHoc,MaLop,TrangThai")] LopHocPhan lopHocPhan)
        {
            if (id != lopHocPhan.MaLopHP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lopHocPhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LopHocPhanExists(lopHocPhan.MaLopHP))
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
            return View(lopHocPhan);
        }

        // GET: LopHocPhans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lopHocPhan = await _context.LopHocPhans
                .FirstOrDefaultAsync(m => m.MaLopHP == id);
            if (lopHocPhan == null)
            {
                return NotFound();
            }

            return View(lopHocPhan);
        }

        // POST: LopHocPhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lopHocPhan = await _context.LopHocPhans.FindAsync(id);
            lopHocPhan.TrangThai = 0;
            _context.Update(lopHocPhan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string inputKeyword)
        {
            var data = GetListLopHPJoin();

            var lopHPs = data.Where(x => x.LopHocPhan.TenLopHP.Contains(inputKeyword) ||
                                         x.MonHoc.TenMonHoc.Contains(inputKeyword) ||
                                         x.GiangVien.TenGiangVien.Contains(inputKeyword) ||
                                         x.Lop.TenLop.Contains(inputKeyword));

            if (inputKeyword == null)
            {
                return View(data);
            }

            return View(lopHPs);
        }
        private bool LopHocPhanExists(int id)
        {
            return _context.LopHocPhans.Any(e => e.MaLopHP == id);
        }

        private IEnumerable<LopHocPhanJoinGiangVien> GetListLopHPJoin()
        {
            List<LopHocPhan> lopHocPhans = _context.LopHocPhans.ToList();
            List<GiangVien> giangViens = _context.GiangViens.ToList();
            List<MonHoc> monHocs = _context.MonHocs.ToList();
            List<Lop> lops = _context.Lops.ToList();

            var data = from lhp in lopHocPhans
                       join gv in giangViens on lhp.MaGiangVien equals gv.MaGiangVien
                       join l in lops on lhp.MaLop equals l.MaLop
                       join mh in monHocs on lhp.MaMonHoc equals mh.MaMonHoc
                       select new LopHocPhanJoinGiangVien
                       {
                           LopHocPhan = lhp,
                           GiangVien = gv,
                           Lop = l,
                           MonHoc = mh
                       };

            return data;
        }
    }
}
