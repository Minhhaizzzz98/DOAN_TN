using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class MonHoc
    {
        [Key]
        public int MaMonHoc { get; set; }
        [Required]
        [Display(Name = "Tên môn học")]
        public string TenMonHoc { get; set; }
        [Required]
        [Display(Name = "Số tín chỉ")]
        public int SoTinChi { get; set; }
        [Display(Name = "Số Tiết")]
        public int SoTiet { get; set; }
        public string Type { get; set; }
        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
  
    }
}
