using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class CTLopHP
    {
        [Key]
        public int MaCTLopHP { get; set; }
        public int MaLopHocPhan { get; set; }
        public int MaSinhVien { get; set; }
        public SinhVien SinhVien { get; set; }
        public int SinhVienMaSV { get; set; }
        public int LopHocPhanMaLopHP { get; set; }
        public LopHocPhan LopHocPhan { get; set; }
        public bool Status { get; set; }
    }
}
