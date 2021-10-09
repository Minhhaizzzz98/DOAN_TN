using Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ModelJoin
{
    public class LopHocPhanJoinGiangVien
    {
        public LopHocPhan LopHocPhan { get; set; }
        public GiangVien GiangVien { get; set; }
        public Lop Lop { get; set; }
        public MonHoc MonHoc { get; set; }

    }
}
