using Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ModelJoin
{
    public class CTBKTJoin
    {
        public CauHoi CauHoi { get; set; }
        public CTBaiKT CTBKT { get; set; }
        public BaiKiemTra BaiKT { get; set; }
    }
}
