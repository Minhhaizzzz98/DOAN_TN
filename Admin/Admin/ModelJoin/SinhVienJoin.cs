using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;

namespace Admin.ModelJoin
{
    public class SinhVienJoin
    {
        public SinhVien SinhVien { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
        public Lop Lop { get; set; }
    }
}
