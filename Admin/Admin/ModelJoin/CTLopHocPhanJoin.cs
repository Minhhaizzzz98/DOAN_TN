using Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ModelJoin
{
    public class CTLopHocPhanJoin
    {
        public SinhVien SinhVien { get; set; }
        public CTLopHP CTLopHP { get; set; }
        public LopHocPhan LopHocPhan { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
    }
}
