using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Admin.Controllers
{
    [Route("api/monhoc")]
    [ApiController]
    public class MonHocApiController : ControllerBase
    {
        private readonly ProjectContext _context;
        public MonHocApiController(ProjectContext context)
        {
            _context = context;
        }
        [HttpGet, Route("index")]
        public async Task<ActionResult<IEnumerable<MonHoc>>> Index()
        {
            return await _context.MonHocs.Where(u => u.TrangThai == true).ToArrayAsync();
        }

    }
}

