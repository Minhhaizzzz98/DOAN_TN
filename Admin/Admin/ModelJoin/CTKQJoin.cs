using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;

namespace Admin.ModelJoin
{
    public class CTKQJoin
    {
        public CTKetQua CTKQ { get; set; }
        public SinhVien SinhVien { get; set; }
        public CauHoi CauHoi { get; set; }
        public BaiKiemTra BaiKT { get; set; }
    }
}
