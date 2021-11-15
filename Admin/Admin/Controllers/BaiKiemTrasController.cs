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
            List<BaiKiemTra> bkts = _context.BaiKiemTras.ToList();
            List<GiangVien> gvs = _context.GiangViens.ToList();
            List<LopHocPhan> lhps = _context.LopHocPhans.ToList();
            var data = from bkt in bkts
                       join gv in gvs on bkt.MaGiangVien equals gv.MaGiangVien
                       join lhp in lhps on bkt.MaLopHocPhan equals lhp.MaLopHP
                       select new BaiKiemTraJoinLopHocPhan
                       {
                           BaiKiemTra = bkt,

                           GiangVien = gv,
                           LopHocPhan = lhp
                       };
            return View(data);
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
            SetSelectListLoai();

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
                string key = String.Empty;
                List<string> listKey = this._context.BaiKiemTras.Select(u => u.KeyBaiKT).ToList();
                bool f = true;
                do
                {

                    key = this.generateCode(6);

                    for (int i = 0; i < listKey.Count(); i++)
                    {
                        if ( key == listKey[i])
                        {
                            f = false;
                            break;
                        }
                    }
                } while (f == false);
                baiKiemTra.KeyBaiKT = key;
                DateTime dt = DateTime.Parse(baiKiemTra.Ngay.ToString());
                baiKiemTra.ThoiGianBatDau = dt.ToString("HH:mm");
                _context.Add(baiKiemTra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baiKiemTra);
        }

        // GET: BaiKiemTras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SetSelectListLoai();

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
                    DateTime dt = DateTime.Parse(baiKiemTra.Ngay.ToString());
                    baiKiemTra.ThoiGianBatDau = dt.ToString("HH:mm");
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

        private void SetSelectListLoai()
        {
            ViewData["GiangVienId"] = new SelectList(_context.GiangViens.Where(item => item.TrangThai).ToList(), "MaGiangVien", "TenGiangVien");
            ViewData["LopHPId"] = new SelectList(_context.LopHocPhans.Where(item => item.TrangThai == 1).ToList(), "MaLopHP", "TenLopHP");
        }
        private string generateCode(int len)
        {
            var random = new Random();
            string chars = "abcdefghijklmnopqrstubwsyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, len)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
