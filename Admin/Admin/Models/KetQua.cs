using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class KetQua
    {
        [Key]
        public int MaKetQua { get; set; }
        [Display(Name = "Mã sinh viên")]

        public int MaSinhVien { get; set; }
        [Display(Name = "Mã bài kiểm tra")]

        public int MaBaiKiemTra { get; set; }
        [Display(Name = "Mã điểm")]

        public float  Diem { get; set; }
        [Display(Name = "Số câu đúng")]

        public int SoCauDung { get; set; }
        [Display(Name = "Trạng thái")]

        public bool TrangThai { get; set; }
    }
}
