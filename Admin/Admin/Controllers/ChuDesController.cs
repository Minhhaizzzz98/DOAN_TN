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
    public class ChuDesController : Controller
    {
        private readonly ProjectContext _context;

        public ChuDesController(ProjectContext context)
        {
            _context = context;
        }

        // GET: ChuDes
        public ActionResult Index()
        {
            List<ChuDe> chuDes = _context.ChuDes.ToList();
            List<MonHoc> monHocs = _context.MonHocs.ToList();
            var data = from c in chuDes
                       join m in monHocs on c.MonHoc equals m.MaMonHoc
                       select new ChuDeJoinMonHoc
                       {
                           ChuDe = c,
                           MonHoc = m
                       };
            return View(data);
        }

        // GET: ChuDes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _context.ChuDes
                .FirstOrDefaultAsync(m => m.MaChuDe == id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        // GET: ChuDes/Create
        public IActionResult Create()
        {
            ViewData["MonHocId"] = new SelectList(_context.MonHocs.ToList(), "MaMonHoc", "TenMonHoc");
            return View();
        }

        // POST: ChuDes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaChuDe,TenChuDe,MonHoc,TrangThai")] ChuDe chuDe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chuDe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chuDe);
        }

        // GET: ChuDes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["MonHocId"] = new SelectList(_context.MonHocs.ToList(), "MaMonHoc", "TenMonHoc");
            var chuDe = await _context.ChuDes.FindAsync(id);
            if (chuDe == null)
            {
                return NotFound();
            }
            return View(chuDe);
        }

        // POST: ChuDes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaChuDe,TenChuDe,MonHoc,TrangThai")] ChuDe chuDe)
        {
            if (id != chuDe.MaChuDe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chuDe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChuDeExists(chuDe.MaChuDe))
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
            return View(chuDe);
        }

        // GET: ChuDes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _context.ChuDes
                .FirstOrDefaultAsync(m => m.MaChuDe == id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        // POST: ChuDes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chuDe = await _context.ChuDes.FindAsync(id);
            chuDe.TrangThai = false;
            _context.Update(chuDe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChuDeExists(int id)
        {
            return _context.ChuDes.Any(e => e.MaChuDe == id);
        }
    }
}
