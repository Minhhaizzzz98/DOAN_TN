using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class CsvImporter
    {
        public int MaSV { get; set; }
        public string TenSV { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public bool TrangThai { get; set; }
        public int Lop { get; set; }
        public int MaTaiKhoan { get; set; }

        public IFormFile FileUpload { set; get; }

    }
}
