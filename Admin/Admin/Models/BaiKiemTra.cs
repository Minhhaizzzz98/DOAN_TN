using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class BaiKiemTra
    {
        [Key]
        public int MaBaiKT { get; set; }
        [Display(Name = "Tên")]
        public string TenBaiKT { get; set; }
        [Display(Name = "Key kiểm tra")]
        public string KeyBaiKT { get; set; }
        [Display(Name = "Ngày")]
        public DateTime Ngay { get; set; }
        [Display(Name = "Tên giảng viên")]
        public int MaGiangVien { get; set; }
        [Display(Name = "Lớp học phần")]
        public int MaLopHocPhan { get; set; }
        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
        [Display(Name = "Trạng thái bắt đầu")]
        public bool TrangThaiBatDau { get; set; }
        public bool IsEnd { get; set; }
        [Display(Name = "Thời gian làm")]
        public int ThoiGianLam { get; set; }
        public string ThoiGianBatDau { get; set; }
    }
}
