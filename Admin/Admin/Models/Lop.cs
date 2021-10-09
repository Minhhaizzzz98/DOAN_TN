using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class Lop
    {
        [Key]
        public int MaLop { get; set; }
        [Display(Name = "Tên lớp")]

        public string TenLop { get; set; }
        [Display(Name = "Số lượng sinh viên")]
        public int SoLuongSV { get; set; }
        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
    }
}
