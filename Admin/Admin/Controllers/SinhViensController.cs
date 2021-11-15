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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Admin.Controllers
{
    public class SinhViensController : Controller
    {
        private readonly ProjectContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public SinhViensController(ProjectContext context, IWebHostEnvironment environment)
        {
            _context = context;
            hostingEnvironment = environment;
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

        public ActionResult ImportFile()
        {
            ViewData["LopId"] = new SelectList(_context.Lops.Where(u => u.TrangThai).ToList(), "MaLop", "TenLop");
            return View(new SinhVien());
        }

        [HttpPost]
        public ActionResult ImportFile(IFormFile postedFile, [Bind("Lop")] SinhVien sinhVien)
        {
            if (postedFile != null)
            {
                string fileExtension = Path.GetExtension(postedFile.FileName);

                //Validate uploaded file and return error.
                if (fileExtension != ".csv")
                {
                    ViewBag.Message = "Vui lòng chọn file có định dạng .csv";
                    ViewData["LopId"] = new SelectList(_context.Lops.Where(u => u.TrangThai).ToList(), "MaLop", "TenLop");
                    return View();
                }

                var sinhViens = new List<SinhVien>();
                List<string> lstFailedUsername = new List<string>();
                using (var sreader = new StreamReader(postedFile.OpenReadStream()))
                {
                    //First line is header. If header is not passed in csv then we can neglect the below line.
                    string[] headers = sreader.ReadLine().Split(',');
                    //Loop through the records
                    while (!sreader.EndOfStream)
                    {
                        string[] rows = sreader.ReadLine().Split(',');
                        string username = rows[5].ToString();
                        if (IsExistsUsername(username))
                        {
                            lstFailedUsername.Add(username);
                            continue;
                        }

                        TaiKhoan tkCreated = CreateTKSinhVien(username);

                        if (tkCreated == null)
                        {
                            lstFailedUsername.Add(username);
                            continue;
                        }

                        sinhViens.Add(new SinhVien
                        {
                            TenSV = rows[0].ToString(),
                            DiaChi = rows[1].ToString(),
                            SoDienThoai = rows[2].ToString(),
                            Email = rows[3].ToString(),
                            Lop = sinhVien.Lop,
                            MaTaiKhoan = tkCreated.Id,
                            TrangThai = true,
                        });
                    }

                    _context.AddRange(sinhViens);
                    _context.SaveChanges();
                }

                var data = GetSinhVienJoins();

                return RedirectToAction("Index", data);
            }
            else
            {
                ViewBag.Message = "Vui lòng chọn file!";
            }
            ViewData["LopId"] = new SelectList(_context.Lops.Where(u => u.TrangThai).ToList(), "MaLop", "TenLop");
            return View();
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

            var dataWithoutTK = from sv in sinhViens
                                where sv.MaTaiKhoan == 0
                                select new SinhVienJoin
                                {
                                    SinhVien = sv,
                                };
            var result = data.Union(dataWithoutTK);
            return result;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        private bool IsExistsUsername(string username)
        {
            var tenTK = _context.TaiKhoans.FirstOrDefault(u => u.UserName == username);

            return tenTK != null;
        }

        private TaiKhoan CreateTKSinhVien(string username)
        {
            TaiKhoan tk = new TaiKhoan
            {
                LoaiTaiKhoan = 2,
                UserName = username,
                Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                TrangThai = true,
            };
            _context.Add(tk);
            _context.SaveChanges();

            return _context.TaiKhoans.FirstOrDefault(u => u.UserName == username);
        }
    }
}
