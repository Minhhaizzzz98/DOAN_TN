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
using Admin.Services;

namespace Admin.Controllers
{
    public class CauHoisController : Controller
    {
        private readonly ProjectContext _context;
        public CauHoisController(ProjectContext context)
        {
            _context = context;
        }

        // GET: CauHois
        public  ActionResult Index()
        {
            var data = GetCauHoisJoin();
         
            return View(data);
        }

        // GET: CauHois/Details/5
        public IActionResult Details(int? id)
        {
            var data = GetCauHoisJoin();
            if (id == null)
            {
                return NotFound();
            }

            var cauHoi =  data
                .FirstOrDefault(m => m.CauHoi.MaCauHoi == id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            return View(cauHoi);
        }

        // GET: CauHois/Create
        public IActionResult Create()
        {
            ViewData["ChuDeId"] = new SelectList(_context.ChuDes.ToList(), "MaChuDe", "TenChuDe");
            return View();
        }

        // POST: CauHois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCauHoi,TenCauHoi,A,B,C,D,DapAn,MaChuDe,TrangThai")] CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cauHoi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cauHoi);
        }

        // GET: CauHois/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["ChuDeId"] = new SelectList(_context.ChuDes.ToList(), "MaChuDe", "TenChuDe");

            if (id == null)
            {
                return NotFound();
            }

            var cauHoi = await _context.CauHois.FindAsync(id);
            if (cauHoi == null)
            {
                return NotFound();
            }
            return View(cauHoi);
        }

        // POST: CauHois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaCauHoi,TenCauHoi,A,B,C,D,DapAn,MaChuDe,TrangThai")] CauHoi cauHoi)
        {
            if (id != cauHoi.MaCauHoi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cauHoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauHoiExists(cauHoi.MaCauHoi))
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
            return View(cauHoi);
        }

        // GET: CauHois/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cauHoi = await _context.CauHois
                .FirstOrDefaultAsync(m => m.MaCauHoi == id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            return View(cauHoi);
        }

        // POST: CauHois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cauHoi = await _context.CauHois.FindAsync(id);
            _context.CauHois.Remove(cauHoi);
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
            var data = GetCauHoisJoin();

            var cauhois = data.Where(x => x.CauHoi.TenCauHoi.Contains(inputKeyword) ||
                                         x.ChuDe.TenChuDe.Contains(inputKeyword) ||
                                         x.ChuDe.MonHoc.ToString().Contains(inputKeyword));

            if (inputKeyword == null)
            {
                return View(data);
            }

            return View(cauhois);
        }

        private bool CauHoiExists(int id)
        {
            return _context.CauHois.Any(e => e.MaCauHoi == id);
        }

        private IEnumerable<CauHoiJoinChuDe> GetCauHoisJoin()
        {
            List<CauHoi> cauHoi = _context.CauHois.ToList();
            List<ChuDe> chuDe = _context.ChuDes.ToList();
            var data = from ch in cauHoi
                       join cd in chuDe on ch.MaChuDe equals cd.MaChuDe
                       select new CauHoiJoinChuDe
                       {
                           CauHoi = ch,
                           ChuDe = cd
                       };

            return data;
        }
    }
}
