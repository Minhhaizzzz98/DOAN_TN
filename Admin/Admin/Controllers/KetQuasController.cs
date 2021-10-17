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
    public class KetQuasController : Controller
    {
        private readonly ProjectContext _context;

        public KetQuasController(ProjectContext context)
        {
            _context = context;
        }

        // GET: KetQuas
        public async Task<IActionResult> Index()
        {
            var data = GetKetQuaJoins();

            return View(data);
        }

        // GET: KetQuas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var data = GetKetQuaJoins();

            if (id == null)
            {
                return NotFound();
            }

            var ketQua = data
                .FirstOrDefault(m => m.KetQua.MaKetQua == id);
            if (ketQua == null)
            {
                return NotFound();
            }

            return View(ketQua);
        }

        // GET: KetQuas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KetQuas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKetQua,MaSinhVien,MaBaiKiemTra,Diem,TrangThai")] KetQua ketQua)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ketQua);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ketQua);
        }

        // GET: KetQuas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ketQua = await _context.KetQuas.FindAsync(id);
            if (ketQua == null)
            {
                return NotFound();
            }
            return View(ketQua);
        }

        // POST: KetQuas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKetQua,MaSinhVien,MaBaiKiemTra,Diem,TrangThai")] KetQua ketQua)
        {
            if (id != ketQua.MaKetQua)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ketQua);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KetQuaExists(ketQua.MaKetQua))
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
            return View(ketQua);
        }

        // GET: KetQuas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var data = GetKetQuaJoins();
            if (id == null)
            {
                return NotFound();
            }

            var ketQua = data
                .FirstOrDefault(m => m.KetQua.MaKetQua == id);
            if (ketQua == null)
            {
                return NotFound();
            }

            return View(ketQua);
        }

        // POST: KetQuas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ketQua = await _context.KetQuas.FindAsync(id);
            ketQua.TrangThai = false;
            _context.Update(ketQua); await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KetQuaExists(int id)
        {
            return _context.KetQuas.Any(e => e.MaKetQua == id);
        }

        private IEnumerable<KetQuaJoin> GetKetQuaJoins()
        {
            List<KetQua> ketQuas = _context.KetQuas.ToList();
            List<SinhVien> sinhViens = _context.SinhViens.ToList();
            List<BaiKiemTra> baiKiemTras = _context.BaiKiemTras.ToList();
            List<TaiKhoan> taiKhoans = _context.TaiKhoans.ToList();

            var data = from kq in ketQuas
                       join sv in sinhViens on kq.MaSinhVien equals sv.MaSV
                       join tk in taiKhoans on sv.MaTaiKhoan equals tk.Id
                       join bkt in baiKiemTras on kq.MaBaiKiemTra equals bkt.MaBaiKT
                       select new KetQuaJoin
                       {
                           KetQua = kq,
                           SinhVien = sv,
                           BaiKiemTra = bkt,
                           TaiKhoan = tk
                       };

            return data;
        }
    }
}
