using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class LopHocPhan
    {
        [Key]
        public int MaLopHP { get; set; }
        [Required]
        [Display(Name = "Tên lớp học phần")]
        public string TenLopHP { get; set; }
        public int MaGiangVien { get; set; }
        [Required]
        public int MaMonHoc { get; set; }
        public List<CTLopHP> CTLopHPs { get; set; }
        public int MaLop { get; set; }
        [Display(Name = "Trạng thái")]
        public int TrangThai { get; set; }

       

    }
}
