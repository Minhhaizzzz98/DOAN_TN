using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class ChuDe
    {
        [Key]
        public int MaChuDe { get; set; }
        [Display(Name = "Tên chủ đề")]
        public string TenChuDe { get; set; }
        [Display(Name = "Môn học")]
        public int MonHoc { get; set; }
        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
        public int MaGV { get; set; }
    }
}
