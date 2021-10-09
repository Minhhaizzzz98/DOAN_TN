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
    public class CTBaiKTsController : Controller
    {
        private readonly ProjectContext _context;

        public CTBaiKTsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: CTBaiKTs
        public IActionResult Index()
        {
            var data = GetCTBKTsJoin();
            return View(data);
        }

        // GET: CTBaiKTs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTBaiKT = await _context.CTBaiKTs
                .FirstOrDefaultAsync(m => m.MaCTBaiKT == id);
            if (cTBaiKT == null)
            {
                return NotFound();
            }

            return View(cTBaiKT);
        }

        // GET: CTBaiKTs/Create
        public IActionResult Create()
        {
            ViewData["CauHoiId"] = new SelectList(_context.CauHois.ToList(), "MaCauHoi", "TenCauHoi");
            ViewData["BaiKiemTraId"] = new SelectList(_context.BaiKiemTras.ToList(), "MaBaiKT", "TenBaiKT");

            return View();
        }

        // POST: CTBaiKTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCTBaiKT,MaBaiKT,CauHoi,STT")] CTBaiKT cTBaiKT)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cTBaiKT);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cTBaiKT);
        }

        // GET: CTBaiKTs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["CauHoiId"] = new SelectList(_context.CauHois.ToList(), "MaCauHoi", "TenCauHoi");
            ViewData["BaiKiemTraId"] = new SelectList(_context.BaiKiemTras.ToList(), "MaBaiKT", "TenBaiKT");

            if (id == null)
            {
                return NotFound();
            }

            var cTBaiKT = await _context.CTBaiKTs.FindAsync(id);
            if (cTBaiKT == null)
            {
                return NotFound();
            }
            return View(cTBaiKT);
        }

        // POST: CTBaiKTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaCTBaiKT,MaBaiKT,CauHoi,STT")] CTBaiKT cTBaiKT)
        {
            if (id != cTBaiKT.MaCTBaiKT)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cTBaiKT);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CTBaiKTExists(cTBaiKT.MaCTBaiKT))
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
            return View(cTBaiKT);
        }

        // GET: CTBaiKTs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTBaiKT = await _context.CTBaiKTs
                .FirstOrDefaultAsync(m => m.MaCTBaiKT == id);
            if (cTBaiKT == null)
            {
                return NotFound();
            }

            return View(cTBaiKT);
        }

        // POST: CTBaiKTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cTBaiKT = await _context.CTBaiKTs.FindAsync(id);
            _context.CTBaiKTs.Remove(cTBaiKT);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CTBaiKTExists(int id)
        {
            return _context.CTBaiKTs.Any(e => e.MaCTBaiKT == id);
        }

        private IEnumerable<CTBKTJoin> GetCTBKTsJoin()
        {
            List<CauHoi> cauHoi = _context.CauHois.ToList();
            List<CTBaiKT> ctBKT = _context.CTBaiKTs.ToList();
            List<BaiKiemTra> bKT = _context.BaiKiemTras.ToList();

            var data = from ch in cauHoi
                       join cd in ctBKT on ch.MaCauHoi equals cd.CauHoi
                       join bkt in bKT on cd.MaBaiKT equals bkt.MaBaiKT
                       select new CTBKTJoin
                       {
                           CauHoi = ch,
                           CTBKT = cd, 
                           BaiKT = bkt
                       };

            return data;
        }
    }
}
