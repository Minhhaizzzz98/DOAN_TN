using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;
using Admin.Data;

namespace Admin.ApiControllers
{
    [Route("api/sinhviens")]
    [ApiController]
    public class SinhViensApiController : ControllerBase
    {
        private readonly ProjectContext _context;
        public SinhViensApiController(ProjectContext context)
        {
            _context = context;
        }

        [HttpGet, Route("index")]
        public ActionResult<IEnumerable<SinhVien>> Get()
        {
            return Ok(_context.SinhViens.ToList());
        }

        [HttpGet, Route("index/{id}")]
        public ActionResult<SinhVien> Get(int id)
        {
            return Ok(_context.SinhViens.FirstOrDefault(x => x.MaSV == id));
        }

        [HttpPost, Route("edit/{id}")]
        public ActionResult<SinhVien> Edit(int id, SinhVien sv)
        {
            SinhVien sinhVien = _context.SinhViens.FirstOrDefault(x => x.MaSV == id);
            _context.SinhViens.Update(sv);
            return Ok(_context.SinhViens.Update(sv));
        }
    }
}
