using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class CTKetQua
    {
        [Key]
        public int MaCTKetQua { get; set; }
        public int SinhVienMaSV {get; set; }
        public SinhVien SinhVien { get; set; }
      
        public BaiKiemTra BaiKiemTra { get; set; }
        public int BaiKiemTraMaBaiKT { get; set; }
        public int CauHoiMaCauHoi { get; set; }
        public CauHoi CauHoi { get; set; }
        [Display(Name ="Đáp án SV chọn")]
        public string DapAnSVChon { get; set; }

    }
}
