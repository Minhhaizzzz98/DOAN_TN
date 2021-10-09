using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class TaiKhoan
    {
        [Key]
        public int Id { set; get; }
        [Display(Name = "Tên đăng nhập")]
        public string UserName { set; get; }
        [Display(Name = "Mật khẩu")]

        public string Password { set; get; }
        [Display(Name = "Loại tài khoản")]

        public int LoaiTaiKhoan { set; get; }
        [Display(Name = "Trạng thái")]

        public bool TrangThai { set; get; }
    }
}
