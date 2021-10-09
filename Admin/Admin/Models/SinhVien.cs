using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class SinhVien
    {
        [Key]
        public int MaSV { get; set; }
        [Display(Name = "Tên sinh viên")]
        [Required]
        public string TenSV { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required]
        public string DiaChi { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required]
        public string SoDienThoai { get; set; }
        [Required]
        public string Email { get; set; }
        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
        [Display(Name = "Loại")]
        public string Type { get; set; }
        public List<CTLopHP> CTLopHPs { get; set; }
        public int Lop { get; set; }
        public int MaTaiKhoan { get; set; }
        public List<CTKetQua> CTKetQuas { get; set; }
    }
}
