using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ApiControllers
{
    [Route("api/lop")]
    [ApiController]
    public class LopApiController : Controller
    {
        private readonly ProjectContext _context;
        public LopApiController(ProjectContext context)
        {
            this._context = context;
        }
        [HttpPost, Route("GetTenLopTheoIdLop")]
        public async Task<IActionResult> GetTenLopTheoIdLop(dynamic tk)
        {
            int maLop = (int)tk.MaLop;

            Lop lop = _context.Lops.SingleOrDefault(u => u.MaLop == maLop);

            return Ok(lop.TenLop);
        }
    }
}
