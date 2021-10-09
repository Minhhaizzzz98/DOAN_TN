using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class GiangVien
    {
        [Key]
        public int MaGiangVien { get; set; }
        [Display(Name = "Tên giảng viên")]
        public string TenGiangVien { get; set; }
        [Display(Name = "Địa chỉ")]
        public string  DiaChi { get; set; }
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
        public int MaTaiKhoan { get; set; }
    }
}
