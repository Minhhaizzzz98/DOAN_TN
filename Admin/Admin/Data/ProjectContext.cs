using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;
namespace Admin.Data
{
    public class ProjectContext: DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
        {
        }

       


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Lop>()
                .HasIndex(u => u.TenLop)
                .IsUnique();
        }

        public DbSet<BaiKiemTra> BaiKiemTras { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<ChuDe> ChuDes { get; set; }
        public DbSet<CTBaiKT> CTBaiKTs { get; set; }
        public DbSet<CTLopHP> CTLopHPs { get; set; }
        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<LopHocPhan> LopHocPhans { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<KetQua> KetQuas { get; set; }
        public DbSet<CTKetQua> CTKetQuas { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
    }
}
