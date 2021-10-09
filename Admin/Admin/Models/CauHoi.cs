
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class CauHoi
    {
        [Key]
        public int MaCauHoi { get; set; }
        [Display(Name ="Tên câu hỏi")]
        public string TenCauHoi { get; set; }
        [Display(Name = "Đáp án A")]
        public string A { get; set; }
        [Display(Name = "Đáp án B")]
        public string B { get; set; }
        [Display(Name = "Đáp án C")]
        public string C { get; set; }
        [Display(Name = "Đáp án D")]
        public string D { get; set; }
        [Display(Name = "Đáp án")]
        public string DapAn { get; set; }
        [Display(Name = "Chủ đề")]
        public int  MaChuDe { get; set; }
        [Display(Name = "Giảng Viên")]
        public int MaGV { get; set; }
        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
        public List<CTKetQua> CTKetQuas { get; set; }
    }
}
