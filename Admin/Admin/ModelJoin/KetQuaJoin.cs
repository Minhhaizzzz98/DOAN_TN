using Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ModelJoin
{
    public class KetQuaJoin
    {
        public KetQua KetQua { get; set; }
        public SinhVien SinhVien { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
        public BaiKiemTra BaiKiemTra { get; set; }
    }
}
