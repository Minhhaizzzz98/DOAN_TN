﻿// <auto-generated />
using System;
using Admin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Admin.Migrations
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20210607150549_add chitet ket qua 2")]
    partial class addchitetketqua2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Admin.Models.BaiKiemTra", b =>
                {
                    b.Property<int>("MaBaiKT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("KeyBaiKT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaGiangVien")
                        .HasColumnType("int");

                    b.Property<int>("MaLopHocPhan")
                        .HasColumnType("int");

                    b.Property<DateTime>("Ngay")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenBaiKT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThoiGianBatDau")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThoiGianLam")
                        .HasColumnType("int");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaBaiKT");

                    b.ToTable("BaiKiemTras");
                });

            modelBuilder.Entity("Admin.Models.CTBaiKT", b =>
                {
                    b.Property<int>("MaCTBaiKT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CauHoi")
                        .HasColumnType("int");

                    b.Property<int>("MaBaiKT")
                        .HasColumnType("int");

                    b.Property<int>("STT")
                        .HasColumnType("int");

                    b.HasKey("MaCTBaiKT");

                    b.ToTable("CTBaiKTs");
                });

            modelBuilder.Entity("Admin.Models.CTKetQua", b =>
                {
                    b.Property<int>("MaCTKetQua")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BaiKiemTraMaBaiKT")
                        .HasColumnType("int");

                    b.Property<int>("CauHoiMaCauHoi")
                        .HasColumnType("int");

                    b.Property<string>("DapAnSVChon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SinhVienMaSV")
                        .HasColumnType("int");

                    b.HasKey("MaCTKetQua");

                    b.HasIndex("BaiKiemTraMaBaiKT");

                    b.HasIndex("CauHoiMaCauHoi");

                    b.HasIndex("SinhVienMaSV");

                    b.ToTable("CTKetQuas");
                });

            modelBuilder.Entity("Admin.Models.CTLopHP", b =>
                {
                    b.Property<int>("MaCTLopHP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LopHocPhanMaLopHP")
                        .HasColumnType("int");

                    b.Property<int>("MaLopHocPhan")
                        .HasColumnType("int");

                    b.Property<int>("MaSinhVien")
                        .HasColumnType("int");

                    b.Property<int>("SinhVienMaSV")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("MaCTLopHP");

                    b.HasIndex("LopHocPhanMaLopHP");

                    b.HasIndex("SinhVienMaSV");

                    b.ToTable("CTLopHPs");
                });

            modelBuilder.Entity("Admin.Models.CauHoi", b =>
                {
                    b.Property<int>("MaCauHoi")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("A")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("B")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("D")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DapAn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaChuDe")
                        .HasColumnType("int");

                    b.Property<string>("TenCauHoi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaCauHoi");

                    b.ToTable("CauHois");
                });

            modelBuilder.Entity("Admin.Models.ChuDe", b =>
                {
                    b.Property<int>("MaChuDe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MonHoc")
                        .HasColumnType("int");

                    b.Property<string>("TenChuDe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaChuDe");

                    b.ToTable("ChuDes");
                });

            modelBuilder.Entity("Admin.Models.GiangVien", b =>
                {
                    b.Property<int>("MaGiangVien")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<int>("MaTaiKhoan")
                        .HasColumnType("int");

                    b.Property<string>("SoDienThoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenGiangVien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaGiangVien");

                    b.ToTable("GiangViens");
                });

            modelBuilder.Entity("Admin.Models.KetQua", b =>
                {
                    b.Property<int>("MaKetQua")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Diem")
                        .HasColumnType("real");

                    b.Property<int>("MaBaiKiemTra")
                        .HasColumnType("int");

                    b.Property<int>("MaSinhVien")
                        .HasColumnType("int");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaKetQua");

                    b.ToTable("KetQuas");
                });

            modelBuilder.Entity("Admin.Models.Lop", b =>
                {
                    b.Property<int>("MaLop")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SoLuongSV")
                        .HasColumnType("int");

                    b.Property<string>("TenLop")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaLop");

                    b.HasIndex("TenLop")
                        .IsUnique()
                        .HasFilter("[TenLop] IS NOT NULL");

                    b.ToTable("Lops");
                });

            modelBuilder.Entity("Admin.Models.LopHocPhan", b =>
                {
                    b.Property<int>("MaLopHP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaGiangVien")
                        .HasColumnType("int");

                    b.Property<int>("MaLop")
                        .HasColumnType("int");

                    b.Property<int>("MaMonHoc")
                        .HasColumnType("int");

                    b.Property<string>("TenLopHP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaLopHP");

                    b.ToTable("LopHocPhans");
                });

            modelBuilder.Entity("Admin.Models.MonHoc", b =>
                {
                    b.Property<int>("MaMonHoc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SoTiet")
                        .HasColumnType("int");

                    b.Property<int>("SoTinChi")
                        .HasColumnType("int");

                    b.Property<string>("TenMonHoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaMonHoc");

                    b.ToTable("MonHocs");
                });

            modelBuilder.Entity("Admin.Models.SinhVien", b =>
                {
                    b.Property<int>("MaSV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lop")
                        .HasColumnType("int");

                    b.Property<int>("MaTaiKhoan")
                        .HasColumnType("int");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenSV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaSV");

                    b.ToTable("SinhViens");
                });

            modelBuilder.Entity("Admin.Models.TaiKhoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LoaiTaiKhoan")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TaiKhoans");
                });

            modelBuilder.Entity("Admin.Models.CTKetQua", b =>
                {
                    b.HasOne("Admin.Models.BaiKiemTra", "BaiKiemTra")
                        .WithMany()
                        .HasForeignKey("BaiKiemTraMaBaiKT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admin.Models.CauHoi", "CauHoi")
                        .WithMany("CTKetQuas")
                        .HasForeignKey("CauHoiMaCauHoi")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admin.Models.SinhVien", "SinhVien")
                        .WithMany("CTKetQuas")
                        .HasForeignKey("SinhVienMaSV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Admin.Models.CTLopHP", b =>
                {
                    b.HasOne("Admin.Models.LopHocPhan", "LopHocPhan")
                        .WithMany("CTLopHPs")
                        .HasForeignKey("LopHocPhanMaLopHP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admin.Models.SinhVien", "SinhVien")
                        .WithMany("CTLopHPs")
                        .HasForeignKey("SinhVienMaSV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
