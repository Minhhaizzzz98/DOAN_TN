using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class CTBaiKT
    {
        [Key]
        public int MaCTBaiKT { get; set; }
        public int MaBaiKT { get; set; }
        public int CauHoi { get; set; }
        public int STT { get; set; }
    }
}
