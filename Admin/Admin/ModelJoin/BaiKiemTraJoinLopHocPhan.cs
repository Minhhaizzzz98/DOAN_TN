using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;
namespace Admin.ModelJoin
{
    public class BaiKiemTraJoinLopHocPhan
    {
        public BaiKiemTra BaiKiemTra { get; set; }
        public LopHocPhan LopHocPhan { get; set; }
        public GiangVien GiangVien { get; set; }
    }
}
